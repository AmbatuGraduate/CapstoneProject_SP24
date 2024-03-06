using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.Update
{
    public record UpdateCalendarCommand(string accessToken, string calendarId, MyUpdatedEvent myEvent, string eventId) : IRequest<ErrorOr<MyUpdatedEventResult>>;
}
