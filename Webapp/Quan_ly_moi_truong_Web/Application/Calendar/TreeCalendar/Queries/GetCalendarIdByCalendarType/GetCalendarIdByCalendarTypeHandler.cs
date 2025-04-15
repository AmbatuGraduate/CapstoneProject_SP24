using Application.Common.Interfaces.Persistence.Schedules;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.GetCalendarIdByCalendarType
{
    public class GetCalendarIdByCalendarTypeHandler : IRequestHandler<GetCalendarIdByCalendarTypeQuery, ErrorOr<string>>
    {
        private ITreeCalendarService _treeCalendarService;

        public GetCalendarIdByCalendarTypeHandler(ITreeCalendarService treeCalendarService)
        {
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<string>> Handle(GetCalendarIdByCalendarTypeQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return _treeCalendarService.GetCalendarIdByCalendarType(request.calendarTypeEnum);
        }
    }
}