

using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Queries.GetEventById
{
    public record GetEventByIDQuery(string accessToken, string calendarId, string eventId) : IRequest<ErrorOr<MyEventResult>>;
}
