using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.Update
{
    public record UpdateCalendarCommand(string accessToken, string calendarId, MyUpdatedEvent myEvent, string eventId) : IRequest<ErrorOr<MyUpdatedEventResult>>;
}