﻿using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.Delete
{
    public class DeleteCalendarHandler : IRequestHandler<DeleteCalendarCommand, ErrorOr<MyDeletedEventResult>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public DeleteCalendarHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<MyDeletedEventResult>> Handle(DeleteCalendarCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var eventResult = await _treeCalendarService.DeleteEvent(request.accessToken, request.calendarId, request.eventId);
            return new MyDeletedEventResult(eventResult);
        }
    }
}