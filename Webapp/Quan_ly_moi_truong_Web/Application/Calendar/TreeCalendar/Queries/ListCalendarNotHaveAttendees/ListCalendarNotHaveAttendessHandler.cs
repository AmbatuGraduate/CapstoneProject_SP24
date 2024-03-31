using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Schedules;
using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.ListCalendarNotHaveAttendees
{
    public class ListCalendarNotHaveAttendessHandler : IRequestHandler<ListCalendarNotHaveAttendessQuery, ErrorOr<List<MyEvent>>>
    {
        private readonly ITreeCalendarService _treeCalendarService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public ListCalendarNotHaveAttendessHandler(ITreeCalendarService treeCalendarService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<List<MyEvent>>> Handle(ListCalendarNotHaveAttendessQuery request, CancellationToken cancellationToken)
        {
            var accessToken = _jwtTokenGenerator.DecodeTokenToGetAccessToken(request.accessToken);
            var list = await _treeCalendarService.GetEvents(accessToken, request.calendarId);

            // Get list tree trim calendar not have attendess and not start
            var listNoAttendees = list.Where(e => e.Attendees.Count == 0 
            && e.ExtendedProperties.PrivateProperties["JobWorkingStatus"] == _treeCalendarService.ConvertToJobWorkingStatusString(JobWorkingStatus.NotStart)).ToList();

            return listNoAttendees;
        }
    }
}
