using Application.Session.Token;
using Google.Apis.Auth.OAuth2.Flows;

namespace Application.Common.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        AuthorizationCodeFlow CreateFlow();

        Task<TokenData> AuthenticateWithGoogle(string authCode);

        Task<TokenData> RefreshTokenWithGoogle(string refreshToken);
    }
}