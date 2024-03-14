using Application.Calendar;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.User.Common;
using Azure.Core;
using Domain.Entities.User;
using Domain.Enums;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using System.Linq;
using System.Security.Cryptography.Xml;

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
                        .ToList(),
                    ExtendedProperties = new Event.ExtendedPropertiesData
                    {
                        Private__ = new Dictionary<string, string>
                        {
                            {"JobWorkingStatus", ConvertToJobWorkingStatusString(JobWorkingStatus.NotStart) }
                        }
                    }
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

        public async Task<bool> UpdateJobStatus(string accessToken, string calendarId, JobWorkingStatus jobWorkingStatus, string eventId)
        {
            string[] Scopes = { CalendarService.Scope.Calendar };
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken).CreateScoped(Scopes);
                var service = _calendarServiceFactory(credential);
                Event retrievedEvent = service.Events.Get(calendarId, eventId)
                    .Execute();

                if (retrievedEvent != null)
                {
                    string newJobStatus = ConvertToJobWorkingStatusString(jobWorkingStatus);
                    if (retrievedEvent.ExtendedProperties.Private__ != null
                        && retrievedEvent.ExtendedProperties.Private__.ContainsKey("JobWorkingStatus"))
                    {
                        retrievedEvent.ExtendedProperties.Private__["JobWorkingStatus"] = newJobStatus;
                    }
                    service.Events.Update(retrievedEvent, calendarId, eventId)
                        .Execute();
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public string ConvertToJobWorkingStatusString(JobWorkingStatus jobWorkingStatus)
        {
            return jobWorkingStatus switch
            {
                JobWorkingStatus.None => "None",
                JobWorkingStatus.NotStart => "Not Start",
                JobWorkingStatus.InProgress => "In Progress",
                JobWorkingStatus.Done => "Done",
                JobWorkingStatus.DoneWithIssue => "Done With Issue",
            };

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

                if (retrievedEvent != null)
                {
                    retrievedEvent.Summary = myEvent.Summary;
                    retrievedEvent.Description = myEvent.Description;
                    retrievedEvent.Location = "800 Howard St., San Francisco, CA 94103";
                    retrievedEvent.Start = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTime = DateTime.Parse(myEvent.Start.DateTime),
                        TimeZone = "America/Los_Angeles"
                    };
                    retrievedEvent.End = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTime = DateTime.Parse(myEvent.End.DateTime),
                        TimeZone = "America/Los_Angeles"
                    };
                    retrievedEvent.Attendees = myEvent.Attendees
                        .Select(attendee => new EventAttendee { Email = attendee.Email })
                        .ToList();

                    service.Events.Update(retrievedEvent, calendarId, eventId)
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

        public async Task<bool> DeleteEvent(string accessToken, string calendarId, string eventId)
        {
            string[] Scopes = { CalendarService.Scope.Calendar };
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken).CreateScoped(Scopes);
                var service = _calendarServiceFactory(credential);
                Event retrievedEvent = service.Events.Get(calendarId, eventId)
                    .Execute();

                if (retrievedEvent != null)
                {
                    service.Events.Delete(calendarId, eventId)
                        .Execute();
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<MyEvent> GetEventById(string accessToken, string calendarId, string eventId)
        {
            MyEvent myEvent = new MyEvent();
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);

                // Use the factory to create a CalendarService with the correct credential
                var service = _calendarServiceFactory(credential);
                Event retrievenedEvent = service.Events.Get(calendarId, eventId)
                    .Execute();
                myEvent = new MyEvent
                {
                    Id = retrievenedEvent.Id,
                    Summary = retrievenedEvent.Summary,
                    Description = retrievenedEvent.Description,
                    Location = retrievenedEvent.Location,
                    Start = retrievenedEvent.Start.DateTime ?? DateTime.MinValue,
                    End = retrievenedEvent.End.DateTime ?? DateTime.MinValue,
                    Attendees = (retrievenedEvent.Attendees != null) ? retrievenedEvent.Attendees
                            .Select(attendee => new UserResult(new Users
                            {
                                Name = attendee.DisplayName,
                                Email = attendee.Email,
                            }
                            ))
                            .ToList() : new List<UserResult>()

                };
                return myEvent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<MyEvent>> GetEventsByAttendeeEmail(string accessToken, string calendarId, string attendeeEmail)
        {
            MyEvent myEvent = new MyEvent();
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
                List<MyEvent> myEvents = events.Items
                    .Where(myEvent => ((myEvent.Attendees != null) && (myEvent.Attendees.Any(user => user.Email.Equals(attendeeEmail)))))
                    .Select(returnEvent => new MyEvent
                    {
                        Id = returnEvent.Id,
                        Summary = returnEvent.Summary,
                        Description = returnEvent.Description,
                        Location = returnEvent.Location,
                        Start = returnEvent.Start.DateTime ?? DateTime.MinValue,
                        End = returnEvent.End.DateTime ?? DateTime.MinValue,
                        Attendees = (returnEvent.Attendees != null) ? returnEvent.Attendees
                            .Select(attendee => new UserResult(new Users
                            {
                                Name = attendee.DisplayName,
                                Email = attendee.Email,
                            }
                            ))
                            .ToList() : new List<UserResult>()
                    })
                    .ToList<MyEvent>();
                return myEvents;
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
                /*                listRequest.TimeMaxDateTimeOffset = DateTime.Now;
                */
                listRequest.ShowDeleted = false;
                listRequest.SingleEvents = true;
                listRequest.MaxResults = 250;
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
                        Attendees = (eventItem.Attendees != null) ? eventItem.Attendees
                            .Select(attendee => new UserResult(new Users
                            {
                                Name = attendee.DisplayName,
                                Email = attendee.Email,
                            }
                            ))
                            .ToList() : new List<UserResult>(),
                        ExtendedProperties = new EventExtendedProperties
                        {
                            PrivateProperties = (Dictionary<string, string>)(eventItem.ExtendedProperties?.Private__ ?? new Dictionary<string, string>())
                        }
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
