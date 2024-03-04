namespace Application.GoogleAuthentication.Common
{
    public record AccessTokenData(
        string sub,
        string scope,
        long exp,
        string email,
        bool email_verified);
}