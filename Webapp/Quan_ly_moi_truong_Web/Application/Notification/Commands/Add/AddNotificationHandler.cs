using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Notification.Common;
using Domain.Entities.Notification;
using ErrorOr;
using MediatR;

namespace Application.Notification.Commands.Add
{
    public class AddNotificationHandler :
        IRequestHandler<AddNotificationCommand, ErrorOr<NotificationResult>>
    {
        private readonly INotificationRepository _notificationRepository;

        public AddNotificationHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<NotificationResult>> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var notification = new Notifications
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Message = request.Message,
                MessageType = request.MessageType,
                NotificationDateTime = DateTime.UtcNow,
            };

            var createdNotification = _notificationRepository.CreateNotification(notification);

            return new NotificationResult(createdNotification);
        }
    }
}