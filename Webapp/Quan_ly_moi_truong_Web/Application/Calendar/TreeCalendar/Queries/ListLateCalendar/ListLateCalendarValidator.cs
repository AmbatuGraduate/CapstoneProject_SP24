using FluentValidation;

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