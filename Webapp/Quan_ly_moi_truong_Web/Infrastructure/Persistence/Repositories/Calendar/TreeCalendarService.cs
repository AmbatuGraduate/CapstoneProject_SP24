using Application.Calendar;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.User.Common;
using Domain.Entities.User;
using Domain.Enums;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace Infrastructure.Persistence.Repositories.Calendar
{
    public class TreeCalendarService : ITreeCalendarService
    {
        private readonly Func<GoogleCredential, CalendarService> _calendarServiceFactory;
        private readonly ITreeRepository _treeRepository;
        private readonly IUserRepository _userRepository;

        public TreeCalendarService(Func<GoogleCredential, CalendarService> calendarServiceFactory, ITreeRepository treeRepository, IUserRepository userRepository)
        {
            _calendarServiceFactory = calendarServiceFactory;
            _treeRepository = treeRepository;
            _userRepository = userRepository;
        }

        // add event
        public async Task<MyAddedEvent> AddEvent(string accessToken, string calendarId, MyAddedEvent myEvent)
        {
            await Task.CompletedTask;
            string[] Scopes = { CalendarService.Scope.Calendar };
            try
            {
                //var treeinfo = _treeRepository.GetTreeByTreeCode(myEvent.TreeId);
                var credential = GoogleCredential.FromAccessToken(accessToken).CreateScoped(Scopes);
                var service = _calendarServiceFactory(credential);
                var addedEvent = new Event()
                {
                    Summary = myEvent.Summary,
                    Description = myEvent.Description,
                    //Location = _streetRepository.GetStreetById(treeinfo.StreetId).StreetName,
                    Location = /*treeinfo.TreeLocation*/ myEvent.location,
                    Start = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTime = DateTime.Parse(myEvent.Start.DateTime),
                        TimeZone = "(GMT+07:00) Indochina Time - Ho Chi Minh City"
                    },
                    End = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTime = DateTime.Parse(myEvent.End.DateTime),
                        TimeZone = "(GMT+07:00) Indochina Time - Ho Chi Minh City"
                    },
                    Attendees = myEvent.Attendees
                        .Select(attendee => new EventAttendee { Email = attendee.Email })
                        .ToList(),
                    ExtendedProperties = new Event.ExtendedPropertiesData
                    {

                        Private__ = new Dictionary<string, string>
                        {
                            {"JobWorkingStatus", ConvertToJobWorkingStatusString(JobWorkingStatus.NotStart)},
                            {"Tree", myEvent.TreeId}
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
        

        // update job status
        public async Task<bool> UpdateJobStatus(string accessToken, string calendarId, JobWorkingStatus jobWorkingStatus, string eventId)
        {
            await Task.CompletedTask;
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

      

        // update event
        public async Task<MyUpdatedEvent> UpdateEvent(string accessToken, string calendarId, MyUpdatedEvent myEvent, string eventId)
        {
            await Task.CompletedTask;
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
                        TimeZone = "(GMT+07:00) Indochina Time - Ho Chi Minh City"
                    };
                    retrievedEvent.End = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTime = DateTime.Parse(myEvent.End.DateTime),
                        TimeZone = "(GMT+07:00) Indochina Time - Ho Chi Minh City"
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

        // delete event
        public async Task<bool> DeleteEvent(string accessToken, string calendarId, string eventId)
        {
            await Task.CompletedTask;
            await Task.CompletedTask;
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

        // get event by id
        public async Task<MyEvent> GetEventById(string accessToken, string calendarId, string eventId)
        {
            await Task.CompletedTask;
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
                                //Name = attendee.DisplayName,
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

        // get events by attendee email
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
                                //Name = attendee.DisplayName,
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

        // get all events
        public async Task<List<EventsInfo>> GetEvents(string accessToken, string calendarId)
        {
            List<EventsInfo> myEvents = new List<EventsInfo>();
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
                    myEvents.Add(new EventsInfo
                    {
                        Id = eventItem.Id,
                        Summary = eventItem.Summary,
                        Description = eventItem.Description,
                        Location = eventItem.Location,
                        Start = eventItem.Start.DateTime ?? DateTime.MinValue,
                        End = eventItem.End.DateTime ?? DateTime.MinValue,
                        Attendees = (eventItem.Attendees != null) ? eventItem.Attendees
                            .Select(attendee => new CalendarUser(new CalendarUserResult
                            {
                                //Name = attendee.DisplayName,
                                Email = attendee.Email,
                                FullName = _userRepository.GetGoogleUserByEmail(accessToken, attendee.Email).Result.Name,
                            }
                            ))
                            .ToList() : new List<CalendarUser>(),
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

        // get user today events
        public async Task<List<MyEvent>> GetUserTodayEvents(string accessToken, string calendarId, string attendeeEmail)
        {
            MyEvent myEvent = new MyEvent();
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);

                // Use the factory to create a CalendarService with the correct credential
                var service = _calendarServiceFactory(credential);

                var listRequest = service.Events.List(calendarId);
                listRequest.TimeMaxDateTimeOffset = DateTime.Now;
                listRequest.TimeMinDateTimeOffset = DateTime.Today; 
                listRequest.TimeMaxDateTimeOffset = DateTime.Today.AddDays(1).AddTicks(-1); 
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

        // -------------------- Helper Functions --------------------
        // convert job status to string (helper function)
        public string ConvertToJobWorkingStatusString(JobWorkingStatus jobWorkingStatus)
        {
            return jobWorkingStatus switch
            {
                JobWorkingStatus.Late => "Late",
                JobWorkingStatus.NotStart => "Not Start",
                JobWorkingStatus.InProgress => "In Progress",
                JobWorkingStatus.Done => "Done",
                JobWorkingStatus.DoneWithIssue => "Done With Issue",
            };
        }

    }
}