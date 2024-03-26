using Application.Calendar.TreeCalendar.Queries.List;
using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.GetByAttendeeId
{
    public class GetByAttendeeEmailHandler : IRequestHandler<GetByAttendeeEmailQuery, ErrorOr<List<MyEventResult>>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public GetByAttendeeEmailHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<List<MyEventResult>>> Handle(GetByAttendeeEmailQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            List<MyEventResult> treeEventResults = new List<MyEventResult>();

            var events = await _treeCalendarService.GetEventsByAttendeeEmail(request.accessToken, request.calendarId, request.attendeeEmail);

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
