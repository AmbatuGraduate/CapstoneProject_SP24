

using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.NumberOfEventsToday
{
    public record NumberOfEventsQuery(string accessToken, string calendarId, string attendeeEmail) : IRequest<ErrorOr<int>>;
}
