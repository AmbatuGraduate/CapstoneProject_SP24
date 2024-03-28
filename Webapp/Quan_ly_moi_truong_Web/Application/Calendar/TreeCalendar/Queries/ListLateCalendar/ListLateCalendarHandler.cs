using Application.Calendar.TreeCalendar.Commands.UpdateJobStatus;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Schedules;
using Domain.Enums;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.ListLateCalendar
{
    public class ListLateCalendarHandler
        : IRequestHandler<ListLateCalendarQuery, ErrorOr<List<MyEvent>>>

    {
        private readonly ITreeCalendarService _treeCalendarService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public ListLateCalendarHandler(ITreeCalendarService treeCalendarService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<List<MyEvent>>> Handle(ListLateCalendarQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var accessToken = _jwtTokenGenerator.DecodeTokenToGetAccessToken(request.accessToken);
            var list = await _treeCalendarService.GetEvents(accessToken, request.calendarId);


            var listLateCalendar = list.Where(x => x.ExtendedProperties.PrivateProperties["JobWorkingStatus"] == _treeCalendarService.ConvertToJobWorkingStatusString(JobWorkingStatus.Late)).ToList();

            return listLateCalendar;
        }
    }
}
