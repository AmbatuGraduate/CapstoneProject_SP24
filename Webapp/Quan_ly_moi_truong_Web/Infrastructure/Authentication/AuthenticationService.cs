using Application.Common.Interfaces.Authentication;
using Application.Session.Token;
using Domain.Enums;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly GoogleApiSettings _settings;
        private readonly WebDbContext _context;

        //Request parameters
        private string clientId;

        private string clientSecret;

        private readonly string[] scopes = { "https://www.googleapis.com/auth/calendar" ,
                                             "https://www.googleapis.com/auth/admin.directory.group",
                                             "https://www.googleapis.com/auth/admin.directory.group.member",
                                             "https://www.googleapis.com/auth/admin.directory.group.member.readonly",
                                             "https://www.googleapis.com/auth/userinfo.email",
                                             "https://www.googleapis.com/auth/admin.directory.user",
                                             "https://www.googleapis.com/auth/userinfo.profile",
                                             "https://www.googleapis.com/auth/admin.directory.user.readonly",
                                             "https://mail.google.com/",
                                             "https://www.googleapis.com/auth/gmail.readonly",
                                             "https://www.googleapis.com/auth/gmail.compose",
                                             "https://www.googleapis.com/auth/gmail.labels",
                                             "https://www.googleapis.com/auth/gmail.send",
                                             "openid", "profile", "email"};

        private string[] mobileScopes = { "https://www.googleapis.com/auth/calendar" ,
                                          "https://www.googleapis.com/auth/userinfo.email",
                                          "https://www.googleapis.com/auth/admin.directory.user",
                                          "https://www.googleapis.com/auth/userinfo.profile",
                                          "https://mail.google.com/",
                                          "https://www.googleapis.com/auth/admin.directory.group",
                                          "https://www.googleapis.com/auth/admin.directory.group.member.readonly",
                                          "https://mail.google.com/",
                                          "https://www.googleapis.com/auth/gmail.send",
                                          "openid", "profile", "email"};

        private string redirect_Uri = "postmessage";

        public AuthenticationService(IOptions<GoogleApiSettings> googelApiSettings, WebDbContext webDbContext)
        {
            _settings = googelApiSettings.Value;
            clientId = _settings.ClientId;
            clientSecret = _settings.ClientSecret;
            _context = webDbContext;
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
            System.Diagnostics.Debug.WriteLine("refreshed");
            return new TokenData(tokenRespone.AccessToken,
                    (long)tokenRespone.ExpiresInSeconds,
                    tokenRespone.RefreshToken,
                    tokenRespone.Scope,
                    tokenRespone.TokenType,
                    tokenRespone.IdToken);
        }

        public async Task<bool> EmployeeInOrganization(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            // Check if user exists
            if (user == null)
            {
                return false;
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == user.RoleId);

            // Check if they are an employee
            if (role != null && role.RoleName == "Employee")
            {
                return true;
            }

            return false;
        }
    }
}