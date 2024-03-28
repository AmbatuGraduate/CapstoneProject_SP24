

using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.ListCurrentDayEventsByEmail
{
    public class ListCurrentEventsHandler : IRequestHandler<ListCurrentEventsQuery, ErrorOr<List<MyEventResult>>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public ListCurrentEventsHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<List<MyEventResult>>> Handle(ListCurrentEventsQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            List<MyEventResult> treeEventResults = new List<MyEventResult>();

            var events = await _treeCalendarService.GetUserTodayEvents(request.accessToken, request.calendarId, request.attendeeEmail);

            if (events != null)
            {
                foreach (var treeEvent in events)
                {
                    treeEventResults.Add(new MyEventResult(treeEvent));
                }
            }

            return treeEventResults;
        }
    }
}
