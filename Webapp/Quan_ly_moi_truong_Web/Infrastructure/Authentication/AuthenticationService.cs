using Application.Common.Interfaces.Authentication;
using Application.Session.Token;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly GoogleApiSettings _settings;

        //Request parameters
        private string clientId;

        private string clientSecret;
        private string[] scopes = { "https://www.googleapis.com/auth/calendar" };
        private string redirect_Uri = "postmessage";

        public AuthenticationService(IOptions<GoogleApiSettings> googelApiSettings)
        {
            _settings = googelApiSettings.Value;
            clientId = _settings.ClientId;
            clientSecret = _settings.ClientSecret;
        }

        public AuthorizationCodeFlow CreateFlow()
        {
            return new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                Scopes = scopes
            });
        }

        public async Task<TokenData> AuthenticateWithGoogle(string authCode)
        {
            var tokenRespone = await CreateFlow().ExchangeCodeForTokenAsync("", authCode, redirect_Uri, CancellationToken.None);

            return new TokenData(tokenRespone.AccessToken,
                                (long)tokenRespone.ExpiresInSeconds,
                                tokenRespone.RefreshToken,
                                tokenRespone.Scope,
                                tokenRespone.TokenType,
                                tokenRespone.IdToken);
        }

        public async Task<TokenData> RefreshTokenWithGoogle(string refreshToken)
        {
            var tokenRespone = await CreateFlow().RefreshTokenAsync("", refreshToken, CancellationToken.None);

            return new TokenData(tokenRespone.AccessToken,
                    (long)tokenRespone.ExpiresInSeconds,
                    tokenRespone.RefreshToken,
                    tokenRespone.Scope,
                    tokenRespone.TokenType,
                    tokenRespone.IdToken);
        }
    }
}