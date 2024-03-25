using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.AutoAdd
{
    public record AutoAddTreeCalendarCommand(string accessToken, string calendarId) : IRequest<ErrorOr<List<MyAddedEventResult>>>;
}
