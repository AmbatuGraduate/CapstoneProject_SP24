using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.AutoAdd
{
    public class AutoAddTreeCalendarValidator : AbstractValidator<AutoAddTreeCalendarCommand>
    {
        public AutoAddTreeCalendarValidator()
        {
            RuleFor(x => x.accessToken).NotEmpty().NotNull();
            RuleFor(x => x.calendarId).NotEmpty().NotNull();
        }
    }
}
