using FluentValidation;

namespace Application.Notification.Queries.ListByUsername
{
    public class ListNotificationByUsernameValidator : AbstractValidator<ListNotificationByUsernameQuery>
    {
        public ListNotificationByUsernameValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}