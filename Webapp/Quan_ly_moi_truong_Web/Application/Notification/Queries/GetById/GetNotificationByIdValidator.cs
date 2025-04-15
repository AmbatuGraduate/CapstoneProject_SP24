using FluentValidation;

namespace Application.Notification.Queries.GetById
{
    public class GetNotificationByIdValidator : AbstractValidator<GetNotificationByIdQuery>
    {
        public GetNotificationByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}