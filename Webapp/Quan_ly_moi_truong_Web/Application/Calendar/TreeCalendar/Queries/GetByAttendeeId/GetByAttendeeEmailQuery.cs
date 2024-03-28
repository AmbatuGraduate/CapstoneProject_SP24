using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.GetByAttendeeId
{
    public record GetByAttendeeEmailQuery(string accessToken, string calendarId, string attendeeEmail) : IRequest<ErrorOr<List<MyEventResult>>>;
}
