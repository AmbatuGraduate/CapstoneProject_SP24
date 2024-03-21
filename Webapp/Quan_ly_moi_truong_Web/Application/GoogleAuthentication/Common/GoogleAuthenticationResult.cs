namespace Application.GoogleAuthentication.Common
{
    public record GoogleAuthenticationResult
    (
        string id,
        string name,
        string avatar,
        DateTime expire_in,
        string token
    );
}