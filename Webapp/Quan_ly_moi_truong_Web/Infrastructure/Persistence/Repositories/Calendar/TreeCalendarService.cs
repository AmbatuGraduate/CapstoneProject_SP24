using Application.Calendar;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.User.Common;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

namespace Infrastructure.Persistence.Repositories.Calendar
{
    public class TreeCalendarService : ITreeCalendarService
    {
        private readonly Func<GoogleCredential, CalendarService> _calendarServiceFactory;

        public TreeCalendarService(Func<GoogleCredential, CalendarService> calendarServiceFactory)
        {
            _calendarServiceFactory = calendarServiceFactory;
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
