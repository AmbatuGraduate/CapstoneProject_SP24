using FluentValidation;

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