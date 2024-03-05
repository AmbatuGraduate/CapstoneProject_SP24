using Application.Calendar;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.User.Common;
using Azure.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace Infrastructure.Persistence.Repositories.Calendar
{
    public class TreeCalendarService : ITreeCalendarService
    {
        private readonly Func<GoogleCredential, CalendarService> _calendarServiceFactory;

        public TreeCalendarService(Func<GoogleCredential, CalendarService> calendarServiceFactory)
        {
            _calendarServiceFactory = calendarServiceFactory;
        }

        public async Task<MyAddedEvent> AddEvent(string accessToken, string calendarId, MyAddedEvent myEvent)
        {
            string[] Scopes = { CalendarService.Scope.Calendar };
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken).CreateScoped(Scopes);
                var service = _calendarServiceFactory(credential);
                var addedEvent = new Event()
                {
                    Id = myEvent.Id,
                    Summary = myEvent.Summary,
                    Description = myEvent.Description,
                    Location = "800 Howard St., San Francisco, CA 94103",
                    Start = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTime = DateTime.Parse(myEvent.Start.DateTime),
                        TimeZone = "America/Los_Angeles"
                    },
                    End = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTime = DateTime.Parse(myEvent.End.DateTime),
                        TimeZone = "America/Los_Angeles"
                    },
                    Attendees = myEvent.Attendees
                        .Select(attendee => new EventAttendee { Email = attendee.Email })
                        .ToList()
                };
                EventsResource.InsertRequest insertRequest = service.Events.Insert(addedEvent, calendarId);
                Event createdEvent = insertRequest.Execute();
                Console.WriteLine("Event created: " + createdEvent.HtmlLink);
                return myEvent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<MyUpdatedEvent> UpdateEvent(string accessToken, string calendarId, MyUpdatedEvent myEvent, string eventId)
        {
            string[] Scopes = { CalendarService.Scope.Calendar };
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken).CreateScoped(Scopes);
                var service = _calendarServiceFactory(credential);
                Event retrievedEvent = service.Events.Get(calendarId, eventId)
                    .Execute();

                if(retrievedEvent != null)
                {
                    var updatedEvent = new Event()
                    {
                        Summary = myEvent.Summary,
                        Description = myEvent.Description,
                        Location = "800 Howard St., San Francisco, CA 94103",
                        Start = new Google.Apis.Calendar.v3.Data.EventDateTime()
                        {
                            DateTime = DateTime.Parse(myEvent.Start.DateTime),
                            TimeZone = "America/Los_Angeles"
                        },
                        End = new Google.Apis.Calendar.v3.Data.EventDateTime()
                        {
                            DateTime = DateTime.Parse(myEvent.End.DateTime),
                            TimeZone = "America/Los_Angeles"
                        },
                        Attendees = myEvent.Attendees
                            .Select(attendee => new EventAttendee { Email = attendee.Email })
                            .ToList()
                    };
                    service.Events.Update(updatedEvent, calendarId, eventId)
                        .Execute();
                }
                return myEvent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<MyEvent>> GetEvents(string accessToken, string calendarId)
        {
            List<MyEvent> myEvents = new List<MyEvent>();
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);

                // Use the factory to create a CalendarService with the correct credential
                var service = _calendarServiceFactory(credential);

                var listRequest = service.Events.List(calendarId);
                listRequest.TimeMaxDateTimeOffset = DateTime.Now;
                listRequest.ShowDeleted = false;
                listRequest.SingleEvents = true;
                listRequest.MaxResults = 10;
                listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                var events = await listRequest.ExecuteAsync();

                foreach (var eventItem in events.Items)
                {
                    myEvents.Add(new MyEvent
                    {
                        Id = eventItem.Id,
                        Summary = eventItem.Summary,
                        Description = eventItem.Description,
                        Location = eventItem.Location,
                        Start = eventItem.Start.DateTime ?? DateTime.MinValue,
                        End = eventItem.End.DateTime ?? DateTime.MinValue,
                        Attendees = new List<UserResult>()
                    });
                }

                return myEvents;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
