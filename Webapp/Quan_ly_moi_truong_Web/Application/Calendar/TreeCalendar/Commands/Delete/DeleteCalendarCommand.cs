using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.Delete
{
    public record DeleteCalendarCommand(string accessToken, string calendarId, string eventId) : IRequest<ErrorOr<MyDeletedEventResult>>;
}
