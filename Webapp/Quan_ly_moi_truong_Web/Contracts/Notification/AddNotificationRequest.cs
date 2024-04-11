namespace Contract.Notification
{
    public record AddNotificationRequest
    (
        string Sender,
        string Username,
        string Message,
        string MessageType,
        string NotificationDateTime
    );
}