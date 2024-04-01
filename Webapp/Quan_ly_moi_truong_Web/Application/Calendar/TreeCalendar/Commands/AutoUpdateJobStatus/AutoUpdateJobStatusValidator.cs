using FluentValidation;

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