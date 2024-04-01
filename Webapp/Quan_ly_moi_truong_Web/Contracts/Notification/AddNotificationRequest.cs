namespace Contract.Notification
{
    public record AddNotificationRequest
    (
        string Username,
        string Message,
        string MessageType,
        string NotificationDateTime
    );
}