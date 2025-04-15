namespace Contract.Notification
{
    public record ListNotificationResponse
    (
        Guid Id,
        string Sender,
        string Username,
        string Message,
        string MessageType,
        string NotificationDateTime
    );
}