using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Commands.AutoUpdateJobStatus
{
    public class AutoUpdateJobStatusValidator : AbstractValidator<AutoUpdateJobStatusCommand>
    {
        public AutoUpdateJobStatusValidator()
        {
            RuleFor(x => x.accessToken).NotEmpty();
            RuleFor(x => x.calendarId).NotEmpty();
        }
    }
}
