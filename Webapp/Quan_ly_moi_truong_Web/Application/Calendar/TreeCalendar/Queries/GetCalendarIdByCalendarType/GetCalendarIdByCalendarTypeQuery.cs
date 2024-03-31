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
    public record GetCalendarIdByCalendarTypeQuery(CalendarTypeEnum calendarTypeEnum) : IRequest<ErrorOr<string>>;
}
