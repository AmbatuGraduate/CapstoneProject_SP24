using Application.Notification.Common;
using ErrorOr;
using MediatR;

namespace Application.Notification.Queries.ListByUsername
{
    public record ListNotificationByUsernameQuery(string Username, int Page) : IRequest<ErrorOr<List<NotificationResult>>>;

}