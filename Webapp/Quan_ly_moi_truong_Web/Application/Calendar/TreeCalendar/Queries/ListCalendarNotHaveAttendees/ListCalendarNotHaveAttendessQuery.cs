using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.ListCalendarNotHaveAttendees
{
    public record ListCalendarNotHaveAttendessQuery(string accessToken, string calendarId) : IRequest<ErrorOr<List<MyEvent>>>;
}