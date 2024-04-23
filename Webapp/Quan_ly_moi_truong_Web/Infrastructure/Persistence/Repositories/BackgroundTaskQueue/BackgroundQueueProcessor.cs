using Application.Calendar.TreeCalendar.Commands.AutoAdd;
using Application.Calendar.TreeCalendar.Commands.AutoUpdateJobStatus;
using Application.Calendar.TreeCalendar.Queries.GetCalendarByDepartmentEmail;
using Application.Calendar.TreeCalendar.Queries.GetCalendarIdByCalendarType;
using Domain.Enums;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using GoogleApi.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Persistence.Repositories.BackgroundTaskQueue
{
    public class BackgroundQueueProcessor : BackgroundService
    {
        private readonly string[] scopes = { "https://www.googleapis.com/auth/calendar" };
        private readonly string serviceAccount = "vesinhdanang@cayxanh-412707.iam.gserviceaccount.com";

        //private readonly IMediator _mediator;

        private readonly IServiceProvider _serviceProvider;

        private readonly HttpClient _httpClient;
        public BackgroundQueueProcessor(HttpClient httpClient, IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Making a run at the midnight
            //var now = DateTime.Now;
            //var hours = 23 - now.Hour;
            //var minutes = 59 - now.Minute;
            //var seconds = 59 - now.Second;
            //var secondTillMidnight = hours * 3600 + minutes * 60 + seconds;
            //await Task.Delay(TimeSpan.FromSeconds(secondTillMidnight), stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    var currDirectory = Environment.CurrentDirectory;
                    string filePath = Directory.GetParent(currDirectory).FullName;
                    var certificate = new X509Certificate2(filePath + "\\Certification\\cayxanh-412707-2feafeea429d.p12", "notasecret", X509KeyStorageFlags.Exportable); // Create a cert for key of Account service

                    // Create credential to get token
                    ServiceAccountCredential credential = new ServiceAccountCredential(
                           new ServiceAccountCredential.Initializer(serviceAccount)
                           {
                               //KeyId = "2feafeea429d58b66b7733282bd19af6899bdada",
                               User = "ambatuadmin@vesinhdanang.xyz",
                               Scopes = scopes
                           }.FromCertificate(certificate));


                    // Run auto update status cut of the trees
                    // Prepare the HTTP request
                    var request = new HttpRequestMessage(HttpMethod.Put, "https://vesinhdanang.xyz:7024/api/tree/AutoUpdate");

                    // Send the request and get the response
                    var response = await _httpClient.SendAsync(request);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful response
                        var content = response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine("API call successful: " + content);
                    }
                    else
                    {
                        var reason = response.Content.ReadAsStringAsync();
                        // Handle unsuccessful response
                        System.Diagnostics.Debug.WriteLine("API call unsuccessful: " + reason);
                    }

                    if (credential != null)
                    {

                        // Create the calendar service
                        var service = new CalendarService(new BaseClientService.Initializer()
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = "Calendar Account Service",
                        });

                        // Run auto create calendar(s) for every tree have cut status is true
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                            var calendarIdTree = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(CalendarTypeEnum.CayXanh));
                            //var calendarIdGarbage = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(CalendarTypeEnum.ThuGom));
                            var calendarIdClean = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(CalendarTypeEnum.QuetDon));

                            var updateEventTree = await mediator.Send(new AutoUpdateJobStatusCommand(service, calendarIdTree.Value));
                            //var updateEventGarbage = await mediator.Send(new AutoUpdateJobStatusCommand(service, calendarIdGarbage.Value));
                            var updateEventClean = await mediator.Send(new AutoUpdateJobStatusCommand(service, calendarIdClean.Value));

                            // Get calendar Id by calendar type
                            var addEvents = await mediator.Send(new AutoAddTreeCalendarCommand(service, calendarIdTree.Value));
                            var listEvents = addEvents.Value;


                            Console.WriteLine("CREATE CALENDAR SUCCESSFUL:" + listEvents.Count);
                        }


                    }

                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    System.Diagnostics.Debug.WriteLine("Error calling API: " + ex.Message);
                }


            }
        }
    }
}
