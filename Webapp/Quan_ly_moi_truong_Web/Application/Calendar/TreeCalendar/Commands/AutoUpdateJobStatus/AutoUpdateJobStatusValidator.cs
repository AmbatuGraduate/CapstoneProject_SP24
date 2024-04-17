using FluentValidation;

namespace Application.Calendar.TreeCalendar.Commands.AutoUpdateJobStatus
{
    public class AutoUpdateJobStatusValidator : AbstractValidator<AutoUpdateJobStatusCommand>
    {
        public AutoUpdateJobStatusValidator()
        {
            RuleFor(x => x.Service).NotEmpty();
            RuleFor(x => x.CalendarId).NotEmpty();
        }
    }
}