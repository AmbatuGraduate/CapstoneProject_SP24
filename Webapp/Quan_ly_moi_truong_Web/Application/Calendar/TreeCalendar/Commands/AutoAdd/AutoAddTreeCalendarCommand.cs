using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.AutoAdd
{
    public record AutoAddTreeCalendarCommand(string accessToken, string calendarId) : IRequest<ErrorOr<List<MyAddedEventResult>>>;
}