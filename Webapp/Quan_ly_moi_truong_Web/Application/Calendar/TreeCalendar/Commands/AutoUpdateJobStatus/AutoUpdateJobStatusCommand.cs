using ErrorOr;
using Google.Apis.Calendar.v3;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.AutoUpdateJobStatus
{
    public record AutoUpdateJobStatusCommand(CalendarService Service, string CalendarId) : IRequest<ErrorOr<List<MyUpdatedJobStatusResult>>>;
}