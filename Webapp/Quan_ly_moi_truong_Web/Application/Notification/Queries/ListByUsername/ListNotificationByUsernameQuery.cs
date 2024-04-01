using Application.Notification.Common;
using ErrorOr;
using MediatR;

namespace Application.Notification.Queries.ListByUsername
{
    public record ListNotificationByUsernameQuery(string Username) :
        IRequest<ErrorOr<List<NotificationResult>>>;
}