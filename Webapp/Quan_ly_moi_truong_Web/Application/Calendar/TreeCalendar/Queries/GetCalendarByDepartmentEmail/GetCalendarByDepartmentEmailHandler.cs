using Application.Calendar.TreeCalendar.Queries.GetByAttendeeId;
using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.GetCalendarByDepartmentEmail
{
    public class GetCalendarByDepartmentEmailHandler : IRequestHandler<GetCalendarByDepartmentEmailCommand, ErrorOr<List<MyEventResult>>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public GetCalendarByDepartmentEmailHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<List<MyEventResult>>> Handle(GetCalendarByDepartmentEmailCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            List<MyEventResult> treeEventResults = new List<MyEventResult>();

            var events = await _treeCalendarService.GetEventsByDepartmentEmail(request.accessToken, request.calendarId, request.departmentEmail);

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
