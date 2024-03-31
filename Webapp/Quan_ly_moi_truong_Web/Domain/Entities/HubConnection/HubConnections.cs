namespace Domain.Entities.HubConnection
{
    public class HubConnections
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ConnectionId { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}