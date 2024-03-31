using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.Add
{
    public class AddCalendarHandler : IRequestHandler<AddCalendarCommand, ErrorOr<MyAddedEventResult>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public AddCalendarHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<MyAddedEventResult>> Handle(AddCalendarCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var eventResult = await _treeCalendarService.AddEvent(request.accessToken, request.calendarId, request.myEvent);
            return new MyAddedEventResult(eventResult);
        }
    }
}