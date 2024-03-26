using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Queries.ListLateCalendar
{
    public class ListLateCalendarValidator : AbstractValidator<ListLateCalendarQuery>
    {
        public ListLateCalendarValidator()
        {
            RuleFor(x => x.accessToken).NotEmpty();
            RuleFor(x => x.calendarId).NotEmpty();
        }
    }
}
