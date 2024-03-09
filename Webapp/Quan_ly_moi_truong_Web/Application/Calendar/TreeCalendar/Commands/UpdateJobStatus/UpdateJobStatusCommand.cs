
using Domain.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.UpdateJobStatus
{
    public record UpdateJobStatusCommand(string accessToken, string calendarId, JobWorkingStatus jobWorkingStatus, string eventId) : IRequest<ErrorOr<MyUpdatedJobStatusResult>>;
}
