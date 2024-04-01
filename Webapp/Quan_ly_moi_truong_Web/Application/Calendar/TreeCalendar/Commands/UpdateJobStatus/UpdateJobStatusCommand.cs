using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Calendar.TreeCalendar.Commands.UpdateJobStatus
{
    public record UpdateJobStatusCommand(string accessToken, string calendarId, JobWorkingStatus jobWorkingStatus, string eventId) : IRequest<ErrorOr<MyUpdatedJobStatusResult>>;
}