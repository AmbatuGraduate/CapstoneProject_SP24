using FluentValidation;

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