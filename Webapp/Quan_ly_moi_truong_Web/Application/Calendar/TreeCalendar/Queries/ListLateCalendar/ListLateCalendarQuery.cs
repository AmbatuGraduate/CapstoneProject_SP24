using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.ListLateCalendar
{
    public record ListLateCalendarQuery(string accessToken, string calendarId) : IRequest<ErrorOr<List<MyEvent>>>;
}
