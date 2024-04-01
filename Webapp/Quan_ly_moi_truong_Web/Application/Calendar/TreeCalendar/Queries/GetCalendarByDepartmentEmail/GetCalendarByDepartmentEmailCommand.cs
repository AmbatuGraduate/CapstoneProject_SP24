using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.GetCalendarByDepartmentEmail
{
    public record GetCalendarByDepartmentEmailCommand(string accessToken, string calendarId, string departmentEmail) : IRequest<ErrorOr<List<MyEventResult>>>;
}
