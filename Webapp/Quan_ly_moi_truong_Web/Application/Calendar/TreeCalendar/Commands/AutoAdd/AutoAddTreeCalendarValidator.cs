using FluentValidation;

namespace Application.Calendar.TreeCalendar.Commands.AutoAdd
{
    public class AutoAddTreeCalendarValidator : AbstractValidator<AutoAddTreeCalendarCommand>
    {
        public AutoAddTreeCalendarValidator()
        {
            RuleFor(x => x.Service).NotEmpty().NotNull();
            RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        }
    }
}