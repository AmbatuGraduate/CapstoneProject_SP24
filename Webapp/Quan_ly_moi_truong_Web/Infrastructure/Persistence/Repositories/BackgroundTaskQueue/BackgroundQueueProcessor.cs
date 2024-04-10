using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Persistence.Repositories.BackgroundTaskQueue
{
    public class BackgroundQueueProcessor : BackgroundService
    {
        private readonly string[] scopes = { "https://www.googleapis.com/auth/calendar" };
        private readonly string serviceAccount = "vesinhdanang@cayxanh-412707.iam.gserviceaccount.com";

        private readonly HttpClient _httpClient;
        public BackgroundQueueProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    var certificate = new X509Certificate2("E:\\Project\\Đồ Án\\CapstoneProject_SP24\\Webapp\\Quan_ly_moi_truong_Web\\API\\cayxanh-412707-2feafeea429d.p12", "notasecret", X509KeyStorageFlags.Exportable);

                    ServiceAccountCredential credential = new ServiceAccountCredential(
                       new ServiceAccountCredential.Initializer(serviceAccount)
                       {
                           Scopes = scopes
                       }.FromCertificate(certificate));

                    // Prepare the HTTP request
                    var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7024/api/tree/AutoUpdate");

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
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    System.Diagnostics.Debug.WriteLine("Error calling API: " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}