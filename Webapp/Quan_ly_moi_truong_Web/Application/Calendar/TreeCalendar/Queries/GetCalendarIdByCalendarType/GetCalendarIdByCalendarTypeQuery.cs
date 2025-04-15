using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.GetCalendarIdByCalendarType
{
    public record GetCalendarIdByCalendarTypeQuery(CalendarTypeEnum calendarTypeEnum) : IRequest<ErrorOr<string>>;
}