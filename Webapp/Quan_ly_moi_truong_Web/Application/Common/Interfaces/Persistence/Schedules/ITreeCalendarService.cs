

using Application.Calendar;

namespace Application.Common.Interfaces.Persistence.Schedules
{
    public interface ITreeCalendarService
    {
        // get all events
        Task<List<MyEvent>> GetEvents(string token, string calendarId);

        Task<MyAddedEvent> AddEvent(string token, string calendarId, MyAddedEvent myEvent);

        Task<MyUpdatedEvent> UpdateEvent(string token, string calendarId, MyUpdatedEvent myEvent, string eventId);
    }
}
