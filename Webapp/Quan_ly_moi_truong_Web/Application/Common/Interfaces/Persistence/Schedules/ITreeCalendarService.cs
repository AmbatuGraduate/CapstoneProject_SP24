using Application.Calendar;
using Domain.Enums;
using Google.Apis.Calendar.v3;

namespace Application.Common.Interfaces.Persistence.Schedules
{
    public interface ITreeCalendarService
    {
        // get all events
        Task<List<MyEvent>> GetEvents(string token, string calendarId);                                                 // get all events

        Task<List<MyEvent>?> GetEventsWithServiceAccount(CalendarService service, string calendarId);                   // get all by using account servie     

        Task<List<MyEvent>> GetEventsByAttendeeEmail(string token, string calendarId, string attendeeEmail);            // get all events by attendee email

        Task<List<MyEvent>> GetEventsByDepartmentEmail(string accessToken, string calendarId, string departmentEmail);  // get all events by department email

        Task<List<MyEvent>> GetUserTodayEvents(string token, string calendarId, string attendeeEmail);                  // get all events by attendee email

        Task<MyEvent> GetEventById(string accessToken, string calendarId, string eventId);                  // get all events by attendee email

        Task<MyAddedEvent> AddEvent(string token, string calendarId, MyAddedEvent myEvent);                             // add event

        Task<MyAddedEvent> AutoAddEvent(CalendarService service, string calendarId, MyAddedEvent myEvent);              // auto add event

        Task<MyUpdatedEvent> UpdateEvent(string token, string calendarId, MyUpdatedEvent myEvent, string eventId);      // update event

        Task<string> AutoUpdateJobStatus(CalendarService service, string calendarId, JobWorkingStatus jobWorkingStatus, string eventId); // auto update event

        Task<string> UpdateJobStatus(string token, string calendarId, JobWorkingStatus jobWorkingStatus, string eventId); // update job status

        Task<bool> DeleteEvent(string token, string calendarId, string eventId);                                        // delete event

        string GetCalendarIdByCalendarType(CalendarTypeEnum calendarType);                                              // get calendar id by calendar type

        string ConvertToJobWorkingStatusString(JobWorkingStatus jobWorkingStatus);                                      // convert to job working status string

        Task<int> NumberOfTasksToday(string token, string calendarId, string attendeeEmail);                            // get number of tasks today
    }
}