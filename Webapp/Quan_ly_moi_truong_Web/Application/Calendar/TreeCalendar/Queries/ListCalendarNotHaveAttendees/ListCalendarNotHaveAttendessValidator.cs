using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.ListCalendarNotHaveAttendees
{
    public class ListCalendarNotHaveAttendessValidator : AbstractValidator<ListCalendarNotHaveAttendessQuery>
    {
        public ListCalendarNotHaveAttendessValidator()
        {
            RuleFor(x => x.accessToken).NotEmpty();
            RuleFor(x => x.calendarId).NotEmpty();
        }
    }
}
