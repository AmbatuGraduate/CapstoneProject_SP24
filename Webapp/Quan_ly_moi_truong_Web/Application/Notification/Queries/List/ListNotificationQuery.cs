using Application.Notification.Common;
using ErrorOr;
using MediatR;

namespace Application.Notification.Queries.List
{
    public record ListNotificationQuery() : IRequest<ErrorOr<List<NotificationResult>>>;
}