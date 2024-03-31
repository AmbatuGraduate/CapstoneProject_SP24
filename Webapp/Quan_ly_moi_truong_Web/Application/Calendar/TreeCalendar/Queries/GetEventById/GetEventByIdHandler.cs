using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.GetEventById
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIDQuery, ErrorOr<MyEventResult>>
    {
        private readonly ITreeCalendarService _treeCalendarService;

        public GetEventByIdHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<MyEventResult>> Handle(GetEventByIDQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var eventInfo = await _treeCalendarService.GetEventById(request.accessToken, request.calendarId, request.eventId);
            return new MyEventResult(eventInfo);
        }
    }
}