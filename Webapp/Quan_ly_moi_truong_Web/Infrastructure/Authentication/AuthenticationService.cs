using Application.Common.Interfaces.Authentication;
using Application.Session.Token;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;

namespace Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly GoogleApiSettings _settings;

        //Request parameters
        private string clientId;

        private string clientSecret;
        private readonly string[] scopes = { "https://www.googleapis.com/auth/calendar" ,
            "https://www.googleapis.com/auth/admin.directory.group",
                                          "https://www.googleapis.com/auth/userinfo.email",
                                          "https://www.googleapis.com/auth/admin.directory.user",
                                          "https://www.googleapis.com/auth/userinfo.profile",
                                          "https://www.googleapis.com/auth/admin.directory.user.readonly",
                                          "openid", "profile", "email"};
        private string[] mobileScopes = { "https://www.googleapis.com/auth/calendar" ,
                                          "https://www.googleapis.com/auth/userinfo.email",
                                          "https://www.googleapis.com/auth/admin.directory.user",
                                          "https://www.googleapis.com/auth/userinfo.profile",
                                          "openid", "profile", "email"};
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
            System.Diagnostics.Debug.WriteLine("scopes: " + tokenRespone.Scope);
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

        public async Task<TokenData> RefreshTokenWithMobileClient(string refreshToken)
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "1083724780407-unohjah2mlejmahsc71516hsh9or3ash.apps.googleusercontent.com",
                },
                Scopes = mobileScopes
            });

            var tokenRespone = await flow.RefreshTokenAsync("", refreshToken, CancellationToken.None);

            return new TokenData(tokenRespone.AccessToken,
                    (long)tokenRespone.ExpiresInSeconds,
                    tokenRespone.RefreshToken,
                    tokenRespone.Scope,
                    tokenRespone.TokenType,
                    tokenRespone.IdToken);
        }
    }
}