using ErrorOr;
using Google.Apis.Calendar.v3;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.AutoAdd
{
    public record AutoAddTreeCalendarCommand(CalendarService Service, string CalendarId) : IRequest<ErrorOr<List<MyAddedEventResult>>>;
}