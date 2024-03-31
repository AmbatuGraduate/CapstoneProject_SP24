using Application.Notification.Common;
using ErrorOr;
using MediatR;

namespace Application.Notification.Queries.GetById
{
    public record GetNotificationByIdQuery(Guid Id) : IRequest<ErrorOr<NotificationResult>>;
}