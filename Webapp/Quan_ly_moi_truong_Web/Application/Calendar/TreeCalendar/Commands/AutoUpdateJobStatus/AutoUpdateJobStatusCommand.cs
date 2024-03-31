using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.AutoUpdateJobStatus
{
    public record AutoUpdateJobStatusCommand(string accessToken, string calendarId) : IRequest<ErrorOr<List<MyUpdatedJobStatusResult>>>;
}