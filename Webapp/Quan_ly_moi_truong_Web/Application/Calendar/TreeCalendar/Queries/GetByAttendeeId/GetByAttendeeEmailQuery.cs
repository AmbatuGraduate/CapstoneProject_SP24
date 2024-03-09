﻿using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.GetByAttendeeId
{
    public record GetByAttendeeEmailQuery(string accessToken, string calendarId, string attendeeEmail) : IRequest<ErrorOr<List<MyEventResult>>>;
}
