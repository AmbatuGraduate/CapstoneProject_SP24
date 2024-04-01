using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.Add
{
    public record AddCalendarCommand(string accessToken, string calendarId, MyAddedEvent myEvent) : IRequest<ErrorOr<MyAddedEventResult>>;
}