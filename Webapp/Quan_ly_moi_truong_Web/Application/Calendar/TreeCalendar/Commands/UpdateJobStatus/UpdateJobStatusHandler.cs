using Application.Calendar.TreeCalendar.Commands.Update;

using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.UpdateJobStatus
{
    public class UpdateJobStatusHandler : IRequestHandler<UpdateJobStatusCommand, ErrorOr<MyUpdatedJobStatusResult>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public UpdateJobStatusHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<MyUpdatedJobStatusResult>> Handle(UpdateJobStatusCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var eventResult = await _treeCalendarService.UpdateJobStatus(request.accessToken, request.calendarId, request.jobWorkingStatus, request.eventId);
            return new MyUpdatedJobStatusResult(eventResult);
        }
    }
}
