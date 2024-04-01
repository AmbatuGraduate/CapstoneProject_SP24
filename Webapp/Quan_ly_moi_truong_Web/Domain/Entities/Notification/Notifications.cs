namespace Domain.Entities.Notification
{
    public class Notifications
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string MessageType { get; set; } = null!;
        public DateTime NotificationDateTime { get; set; } //Ngay gui thong bao
    }
}