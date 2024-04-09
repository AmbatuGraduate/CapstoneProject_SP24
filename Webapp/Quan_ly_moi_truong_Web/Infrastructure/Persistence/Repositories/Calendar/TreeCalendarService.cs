using Application.Calendar;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.User.Common;
using Domain.Entities.User;
using Domain.Enums;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using System.Globalization;

namespace Infrastructure.Persistence.Repositories.Calendar
{
    public class TreeCalendarService : ITreeCalendarService
    {
        // Fields
        private readonly Func<GoogleCredential, CalendarService> _calendarServiceFactory;

        private readonly ITreeRepository _treeRepository;
        private readonly IUserRepository _userRepository;

        // Constants
        private const string TimeZone = "(GMT+07:00) Indochina Time - Ho Chi Minh City";

        private const string DefaultLocation = "800 Howard St., San Francisco, CA 94103";
        private const string ErrorMessage = "An error occurred: {0}";

        // Constructor
        public TreeCalendarService(Func<GoogleCredential, CalendarService> calendarServiceFactory, ITreeRepository treeRepository, IUserRepository userRepository)
        {
            _calendarServiceFactory = calendarServiceFactory;
            _treeRepository = treeRepository;
            _userRepository = userRepository;
        }

        // -------------------- Implementing ITreeCalendarService Interface --------------------
        // add event
        public async Task<MyAddedEvent> AddEvent(string accessToken, string calendarId, MyAddedEvent myEvent)
        {
            await Task.CompletedTask;
            try
            {
                //var treeinfo = _treeRepository.GetTreeByTreeCode(myEvent.TreeId);
                System.Diagnostics.Debug.WriteLine("access token: " + accessToken);

                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _calendarServiceFactory(credential);
                var addedEvent = new Event()
                {
                    Summary = myEvent.Summary,
                    Description = myEvent.Description,
                    Location = myEvent.location,
                    Start = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTimeDateTimeOffset = DateTime.Parse(myEvent.Start.DateTime),
                        TimeZone = TimeZone
                    },
                    End = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTimeDateTimeOffset = DateTime.Parse(myEvent.End.DateTime),
                        TimeZone = TimeZone
                    },
                    Attendees = myEvent.Attendees
                        .Where(attendee => !string.IsNullOrEmpty(attendee.Email)) 
                        .Select(attendee => new EventAttendee { Email = attendee.Email })
                        .ToList(),
                    ExtendedProperties = new Event.ExtendedPropertiesData
                    {
                        Private__ = new Dictionary<string, string>
                        {
                            {EventExtendedProperties.JobWorkingStatus, ConvertToJobWorkingStatusString(JobWorkingStatus.NotStart)},
                            {EventExtendedProperties.Tree, myEvent.TreeId},
                            {EventExtendedProperties.DepartmentEmail, myEvent.DepartmentEmail }
                        }
                    }
                };
                EventsResource.InsertRequest insertRequest = service.Events.Insert(addedEvent, calendarId);
                Event createdEvent = insertRequest.Execute();
                return myEvent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // update job status
        public async Task<string> UpdateJobStatus(string accessToken, string calendarId, JobWorkingStatus jobWorkingStatus, string eventId)
        {
            await Task.CompletedTask;
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _calendarServiceFactory(credential);
                var listTreesNeedUpdated = string.Empty;
                Event retrievedEvent = service.Events.Get(calendarId, eventId)
                    .Execute();

                if (retrievedEvent != null)
                {
                    string newJobStatus = ConvertToJobWorkingStatusString(jobWorkingStatus);
                    if (retrievedEvent.ExtendedProperties != null && retrievedEvent.ExtendedProperties.Private__ != null
                        && retrievedEvent.ExtendedProperties.Private__.ContainsKey(EventExtendedProperties.JobWorkingStatus))
                    {
                        retrievedEvent.ExtendedProperties.Private__[EventExtendedProperties.JobWorkingStatus] = newJobStatus;
                        listTreesNeedUpdated = retrievedEvent.ExtendedProperties.Private__.ContainsKey(EventExtendedProperties.Tree) ?
                            retrievedEvent.ExtendedProperties.Private__[EventExtendedProperties.Tree] : "";
                    }
                    else
                    {
                        retrievedEvent.ExtendedProperties = new Event.ExtendedPropertiesData
                        {
                            Private__ = new Dictionary<string, string>
                            {
                                {EventExtendedProperties.JobWorkingStatus, ConvertToJobWorkingStatusString(JobWorkingStatus.NotStart)},
                                {EventExtendedProperties.Tree, ""},
                                {EventExtendedProperties.DepartmentEmail, "" }
                            }
                        };
                    }
                    service.Events.Update(retrievedEvent, calendarId, eventId)
                        .Execute();
                }
                return listTreesNeedUpdated;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return string.Empty;
            }
        }

        // update event
        public async Task<MyUpdatedEvent> UpdateEvent(string accessToken, string calendarId, MyUpdatedEvent myEvent, string eventId)
        {
            await Task.CompletedTask;
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _calendarServiceFactory(credential);
                Event retrievedEvent = await service.Events.Get(calendarId, eventId).ExecuteAsync();

                if (retrievedEvent != null)
                {
                    retrievedEvent.Summary = myEvent.Summary;
                    retrievedEvent.Description = myEvent.Description;
                    retrievedEvent.Location = myEvent.Location;
                    retrievedEvent.Start = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTimeDateTimeOffset = DateTime.Parse(myEvent.Start.DateTime),
                        TimeZone = TimeZone
                    };
                    retrievedEvent.End = new Google.Apis.Calendar.v3.Data.EventDateTime()
                    {
                        DateTimeDateTimeOffset = DateTime.Parse(myEvent.End.DateTime),
                        TimeZone = TimeZone
                    };
                    retrievedEvent.Attendees
                        .Where(attendee => !string.IsNullOrEmpty(attendee.Email)) // Skip attendees with null or empty email
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
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
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
                           .Select(attendee => new UserEventResult(
                                new Users
                                {
                                    Email = attendee.Email,
                                }, FullName: _userRepository.GetGoogleUserByEmail(accessToken, attendee.Email).Result.Name
                            ))
                            .ToList() : new List<UserEventResult>(),
                    ExtendedProperties = new EventExtendedProperties
                    {
                        PrivateProperties = (Dictionary<string, string>)(retrievenedEvent.ExtendedProperties?.Private__ ?? new Dictionary<string, string>())
                    }
                };
                return myEvent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<List<MyEvent>> GetEventsByDepartmentEmail(string accessToken, string calendarId, string departmentEmail)
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
                    .Where(myEvent => (myEvent.ExtendedProperties != null &&
                    myEvent.ExtendedProperties.Private__.ContainsKey(EventExtendedProperties.DepartmentEmail) &&
                    myEvent.ExtendedProperties.Private__[EventExtendedProperties.DepartmentEmail].Equals(departmentEmail, StringComparison.OrdinalIgnoreCase)
                    ))
                    .Select(returnEvent => new MyEvent
                    {
                        Id = returnEvent.Id,
                        Summary = returnEvent.Summary,
                        Description = returnEvent.Description,
                        Location = returnEvent.Location,
                        Start = returnEvent.Start.DateTime ?? DateTime.MinValue,
                        End = returnEvent.End.DateTime ?? DateTime.MinValue,
                        Attendees = (returnEvent.Attendees != null) ? returnEvent.Attendees
                            .Select(attendee => new UserEventResult(
                                new Users
                                {
                                    Email = attendee.Email,
                                }, FullName: _userRepository.GetGoogleUserByEmail(accessToken, attendee.Email).Result.Name
                            ))
                            .ToList() : new List<UserEventResult>(),
                        ExtendedProperties = new EventExtendedProperties
                        {
                            PrivateProperties = (Dictionary<string, string>)(returnEvent.ExtendedProperties?.Private__ ?? new Dictionary<string, string>())
                        }
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
                listRequest.ShowDeleted = false;
                listRequest.SingleEvents = true;
                listRequest.MaxResults = 250;
                listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                var events = await listRequest.ExecuteAsync();
                List<MyEvent> myEvents = events.Items
                    .Where(myEvent => ((myEvent.Attendees != null) && (myEvent.Attendees.Any(user => user.Email.Equals(attendeeEmail)))))
                    .Select(returnEvent => new MyEvent
                    {
                        Id = returnEvent.Id,
                        Summary = returnEvent.Summary,
                        Description = returnEvent.Description,
                        Start = returnEvent.Start.DateTime ?? DateTime.MinValue,
                        End = returnEvent.End.DateTime ?? DateTime.MinValue,
                        Location = returnEvent.Location,
                        Attendees = (returnEvent.Attendees != null) ? returnEvent.Attendees
                            .Select(attendee => new UserEventResult(
                                new Users
                                {
                                    Email = attendee.Email,
                                }, FullName: _userRepository.GetGoogleUserByEmail(accessToken, attendee.Email).Result.Name
                            ))
                            .ToList() : new List<UserEventResult>(),
                        ExtendedProperties = new EventExtendedProperties
                        {
                            PrivateProperties = (Dictionary<string, string>)(returnEvent.ExtendedProperties?.Private__ ?? new Dictionary<string, string>())
                        }
                    })
                    .ToList();
                return myEvents;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // get all events
        public async Task<List<MyEvent>?> GetEvents(string accessToken, string calendarId)
        {
            List<MyEvent> myEvents = new();
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
                    System.Diagnostics.Debug.WriteLine("date: " + eventItem.Start.DateTime);
                    myEvents.Add(new MyEvent
                    {
                        Id = eventItem.Id,
                        Summary = eventItem.Summary,
                        Description = eventItem.Description,
                        Location = eventItem.Location,
                        Start = eventItem.Start.DateTime ?? DateTime.MinValue,
                        End = eventItem.End.DateTime ?? DateTime.MinValue,
                        Attendees = (eventItem.Attendees != null) ? eventItem.Attendees
                            .Select(attendee => new UserEventResult(
                                new Users
                                {
                                    Email = attendee.Email,
                                }, FullName: _userRepository.GetGoogleUserByEmail(accessToken, attendee.Email).Result.Name
                            ))
                            .ToList() : new List<UserEventResult>(),
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
            MyEvent myEvent = new();
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
                listRequest.MaxResults = 20;
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
                            .Select(attendee => new UserEventResult(
                                new Users
                                {
                                    Email = attendee.Email,
                                }, FullName: _userRepository.GetGoogleUserByEmail(accessToken, attendee.Email).Result.Name
                            ))
                            .ToList() : new List<UserEventResult>(),
                        ExtendedProperties = new EventExtendedProperties
                        {
                            PrivateProperties = (Dictionary<string, string>)(returnEvent.ExtendedProperties?.Private__ ?? new Dictionary<string, string>())
                        }
                    })
                    .ToList();
                return myEvents;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // get number of tasks today
        public async Task<int> NumberOfTasksToday(string token, string calendarId, string attendeeEmail)
        {
            return (await GetUserTodayEvents(token, calendarId, attendeeEmail)).Count;
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

        public string GetCalendarIdByCalendarType(CalendarTypeEnum calendarType)
        {
            return calendarType switch
            {
                CalendarTypeEnum.CayXanh => CalendarIdsEnum.CayXanhCalendarId,
                CalendarTypeEnum.ThuGom => CalendarIdsEnum.ThuGomCalendarId,
                CalendarTypeEnum.QuetDon => CalendarIdsEnum.QuetDonCalendarId,
                CalendarTypeEnum.None => throw new ArgumentException("Invalid calendar type", nameof(calendarType)),
                _ => throw new ArgumentOutOfRangeException(nameof(calendarType), calendarType, null)
            };
        }
    }
}