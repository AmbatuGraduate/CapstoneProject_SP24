using Application.Common.Interfaces.Persistence.Schedules;
using Domain.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.GetCalendarIdByCalendarType
{
    public class GetCalendarIdByCalendarTypeHandler : IRequestHandler<GetCalendarIdByCalendarTypeQuery, ErrorOr<string>>
    {
        ITreeCalendarService _treeCalendarService;

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
