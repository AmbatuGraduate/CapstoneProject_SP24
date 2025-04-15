using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.ListLateCalendar
{
    public record ListLateCalendarQuery(string accessToken, string calendarId) : IRequest<ErrorOr<List<MyEvent>>>;
}