using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Common.Errors;

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
            var accessToken = _jwtTokenGenerator.DecodeTokenToGetAccessToken(request.accessToken);

            var listCalendar = await _treeCalendarService.GetEvents(accessToken, request.calendarId);

            for(int i = 0; i < listCalendar.Count; i++)
            {
                // Check is event reach the dealine and not have status late
                if (listCalendar[i].End.CompareTo(DateTime.Now) <= 0 
                    && listCalendar[i].ExtendedProperties.PrivateProperties["JobWorkingStatus"] != _treeCalendarService.ConvertToJobWorkingStatusString(JobWorkingStatus.Late))
                {
                   var result = await _treeCalendarService.UpdateJobStatus(accessToken, request.calendarId, JobWorkingStatus.Late, listCalendar[i].Id);
                   if(!result)
                    {
                        return Errors.UpdateGoogle.UpdateGoogleCalendarFail;
                    }
                    
                }
            }

            return new List<MyUpdatedJobStatusResult>();
        }
    }
}
