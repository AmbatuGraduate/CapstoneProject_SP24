namespace Application.GoogleAuthentication.Common
{
    public record GoogleRefreshResult
    (
        string id,
        string name,
        string avatar,
        DateTime expire_in,
        string token
    );
}