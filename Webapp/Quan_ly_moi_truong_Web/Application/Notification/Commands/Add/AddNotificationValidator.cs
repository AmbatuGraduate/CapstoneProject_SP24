using FluentValidation;

namespace Application.Notification.Commands.Add
{
    public class AddNotificationValidator : AbstractValidator<AddNotificationCommand>
    {
        public AddNotificationValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.MessageType).NotEmpty();
            RuleFor(x => x.NotificationDateTime).NotEmpty();
        }
    }
}