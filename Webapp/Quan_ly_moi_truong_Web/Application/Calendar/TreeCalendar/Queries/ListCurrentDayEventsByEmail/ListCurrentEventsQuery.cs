using ErrorOr;
using MediatR;


namespace Application.Calendar.TreeCalendar.Queries.ListCurrentDayEventsByEmail
{
    public record ListCurrentEventsQuery(string accessToken, string calendarId, string attendeeEmail) : IRequest<ErrorOr<List<MyEventResult>>>;
}
