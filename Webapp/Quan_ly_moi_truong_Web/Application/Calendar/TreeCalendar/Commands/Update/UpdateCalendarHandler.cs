using Application.Calendar.TreeCalendar.Commands.Add;
using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.Update
{
    public class UpdateCalendarHandler : IRequestHandler<UpdateCalendarCommand, ErrorOr<MyUpdatedEventResult>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public UpdateCalendarHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<MyUpdatedEventResult>> Handle(UpdateCalendarCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var eventResult = await _treeCalendarService.UpdateEvent(request.accessToken, request.calendarId, request.myEvent, request.eventId);
            return new MyUpdatedEventResult(eventResult);
        }
    }
}
