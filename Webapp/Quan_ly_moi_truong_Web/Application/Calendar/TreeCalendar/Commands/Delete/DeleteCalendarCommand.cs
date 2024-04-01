using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.Delete
{
    public record DeleteCalendarCommand(string accessToken, string calendarId, string eventId) : IRequest<ErrorOr<MyDeletedEventResult>>;
}