using Application.Notification.Common;
using ErrorOr;
using MediatR;

namespace Application.Notification.Commands.Add
{
    public record AddNotificationCommand(
        string Username,
        string Message,
        string MessageType,
        DateTime NotificationDateTime
    ) : IRequest<ErrorOr<NotificationResult>>;
}