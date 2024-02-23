

using Application.Calendar;

namespace Application.Common.Interfaces.Persistence.Schedules
{
    public interface ITreeCalendarService
    {
        // get all events
        Task<List<MyEvent>> GetEvents(string token, string calendarId);
    }
}
