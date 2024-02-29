

using Application.Common.Interfaces.Authentication;
using Application.Session.Token;
using Newtonsoft.Json;

namespace Application.Session.Common
{
    public class AuthenticationService
    {
        private readonly ISessionService _sessionService;

        public AuthenticationService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<string> AuthenticateWithGoogle(string authCode)
        {
            // Request parameters
            var tokenEndpoint = "https://oauth2.googleapis.com/token";
            var clientId = "1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com";
            var clientSecret = "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";


            // Exchange authorization code for tokens
            using (var httpClient = new HttpClient())
            {
                var tokenResponse = await httpClient.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "code", authCode },
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "redirect_uri", "postmessage" },
                    { "grant_type", "authorization_code" }
                }
             ));
                var tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();


                if (!tokenResponse.IsSuccessStatusCode)
                {
                    return "Failed to exchange authorization code for tokens.";
                }

                // Extract ID token from token response
                var tokenData = JsonConvert.DeserializeObject<TokenData>(tokenResponseContent);
                System.Diagnostics.Debug.WriteLine("tokenData: " + tokenData.scope);

                // Store the token in session
                _sessionService.setAccessToken(tokenData.access_token);
                _sessionService.setRefreshToken(tokenData.refresh_token);
                _sessionService.setExpiresIn(tokenData.expires_in);
                _sessionService.setScope(tokenData.scope);
                _sessionService.setIdToken(tokenData.id_token);


                return _sessionService.getIdToken();
            }
        }
    }
}