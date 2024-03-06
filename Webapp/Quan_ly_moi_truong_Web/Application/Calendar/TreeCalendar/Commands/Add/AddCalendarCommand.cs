using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.Add
{
    public record AddCalendarCommand(string accessToken, string calendarId, MyAddedEvent myEvent) : IRequest<ErrorOr<MyAddedEventResult>>;
}
