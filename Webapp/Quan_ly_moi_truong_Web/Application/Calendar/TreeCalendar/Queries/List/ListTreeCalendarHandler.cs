
using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.List
{
    public class ListTreeCalendarHandler : IRequestHandler<ListTreeCalendarQuery, ErrorOr<List<MyEvent>>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public ListTreeCalendarHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<List<MyEvent>>> Handle(ListTreeCalendarQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            List<MyEventResult> treeEventResults = new List<MyEventResult>();

            var events = await _treeCalendarService.GetEvents(request.accessToken, request.calendarId);

            //if (events != null)
            //{
            //    foreach (var treeEvent in events)
            //    {
            //        treeEventResults.Add(new MyEventResult(treeEvent));
            //    }
            //}

            return events;
        }
    }
}
