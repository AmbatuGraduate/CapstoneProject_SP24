

using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.NumberOfEventsToday
{
    public class NumberOfEventsTodayHandler : IRequestHandler<NumberOfEventsQuery, ErrorOr<int>>
    {
        private readonly ITreeCalendarService _calendarService;

        public NumberOfEventsTodayHandler(ITreeCalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        public async Task<ErrorOr<int>> Handle(NumberOfEventsQuery request, CancellationToken cancellationToken)
        {
            return await _calendarService.NumberOfTasksToday(request.accessToken, request.calendarId, request.attendeeEmail);
        }
    }
}
