using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Notification.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Notification.Queries.GetById
{
    public class GetNotificationByIdHandler :
        IRequestHandler<GetNotificationByIdQuery, ErrorOr<NotificationResult>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetNotificationByIdHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<NotificationResult>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var notification = _notificationRepository.GetlNotification(request.Id);

            if (notification == null)
            {
                return Errors.Notification.getNotificationFail;
            }

            return new NotificationResult(notification);
        }
    }
}