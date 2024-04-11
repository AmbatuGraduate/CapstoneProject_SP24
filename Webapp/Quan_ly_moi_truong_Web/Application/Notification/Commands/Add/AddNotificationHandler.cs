using Application.Calendar;
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

            var notification = new Domain.Entities.Notification.Notifications
            {
                Id = Guid.NewGuid(),
                Sender = "Hệ Thống",
                Username = request.Username,
                Message = request.Message,
                MessageType = "Single",
                NotificationDateTime = DateTime.Now,
            };

            var createdNotification = await _notificationRepository.CreateNotification(notification);

            return new NotificationResult(createdNotification);
        }
    }
}