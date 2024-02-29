
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.List
{
    public record ListTreeCalendarQuery(string accessToken, string calendarId) : IRequest<ErrorOr<List<MyEventResult>>>;
}
