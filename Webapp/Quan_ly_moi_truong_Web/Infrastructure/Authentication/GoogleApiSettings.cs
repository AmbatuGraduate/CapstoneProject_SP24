namespace Infrastructure.Authentication
{
    public class GoogleApiSettings
    {
        public const string SectionName = "GoogleSettings";
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
    }
}