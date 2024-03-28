
using Domain.Enums;


using Application.Calendar;

namespace Application.Common.Interfaces.Persistence.Schedules
{
    public interface ITreeCalendarService
    {
        // get all events
        Task<List<EventsInfo>> GetEvents(string token, string calendarId);                                              // get all events
        Task<List<MyEvent>> GetEventsByAttendeeEmail(string token, string calendarId, string attendeeEmail);            // get all events by attendee email
        Task<List<MyEvent>> GetUserTodayEvents(string token, string calendarId, string attendeeEmail);                  // get all events by attendee email
        Task<MyAddedEvent> AddEvent(string token, string calendarId, MyAddedEvent myEvent);                             // add event
        Task<MyUpdatedEvent> UpdateEvent(string token, string calendarId, MyUpdatedEvent myEvent, string eventId);      // update event
        Task<bool> UpdateJobStatus(string token, string calendarId, JobWorkingStatus jobWorkingStatus, string eventId); // update job status
        Task<bool> DeleteEvent(string token, string calendarId, string eventId);                                        // delete event
    }
}
