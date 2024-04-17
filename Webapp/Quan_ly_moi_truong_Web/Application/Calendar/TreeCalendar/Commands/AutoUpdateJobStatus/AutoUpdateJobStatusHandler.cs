using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Schedules;
using Domain.Common.Errors;
using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Application.Calendar.TreeCalendar.Commands.AutoUpdateJobStatus
{
    public class AutoUpdateJobStatusHandler : IRequestHandler<AutoUpdateJobStatusCommand, ErrorOr<List<MyUpdatedJobStatusResult>>>
    {
        private readonly ITreeCalendarService _treeCalendarService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AutoUpdateJobStatusHandler(ITreeCalendarService treeCalendarService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<List<MyUpdatedJobStatusResult>>> Handle(AutoUpdateJobStatusCommand request, CancellationToken cancellationToken)
        {
            var listCalendar = await _treeCalendarService.GetEventsWithServiceAccount(request.Service, request.CalendarId);

            for (int i = 0; i < listCalendar.Count; i++)
            {
                // Check is event reach the dealine and not have status late
                if (listCalendar[i].End.CompareTo(DateTime.Now) <= 0
                    && listCalendar[i].ExtendedProperties.PrivateProperties["JobWorkingStatus"] != _treeCalendarService.ConvertToJobWorkingStatusString(JobWorkingStatus.Late))
                {
                    var result = await _treeCalendarService.AutoUpdateJobStatus(request.Service, request.CalendarId, JobWorkingStatus.Late, listCalendar[i].Id);
                    if (result.IsNullOrEmpty())
                    {
                        return Errors.UpdateGoogle.UpdateGoogleCalendarFail;
                    }
                }
            }

            return new List<MyUpdatedJobStatusResult>();
        }
    }
}