namespace Contract.Notification
{
    public record ListNotificationResponse
    (
        Guid Id,
        string Username,
        string Message,
        string MessageType,
        string NotificationDateTime
    );
}