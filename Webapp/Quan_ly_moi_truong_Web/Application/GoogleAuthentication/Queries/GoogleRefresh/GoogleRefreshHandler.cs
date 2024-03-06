using Application.Common.Interfaces.Authentication;
using Application.GoogleAuthentication.Common;
using Domain.Common.Errors;
using ErrorOr;
using Google.Apis.Auth;
using MediatR;
using Newtonsoft.Json;

namespace Application.GoogleAuthentication.Queries.GoogleRefresh
{
    public class GoogleRefreshHandler :
                IRequestHandler<GoogleRefreshQuery, ErrorOr<GoogleRefreshResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IAuthenticationService authenticationService;

        public GoogleRefreshHandler(IJwtTokenGenerator jwtTokenGenerator, IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        private async Task<ErrorOr<GoogleRefreshResult>> ReturnGoogleRefreshResult(string refresh_tkn)
        {
            //Check refresh token is expire or not
            if (refresh_tkn != null)
            {
                System.Diagnostics.Debug.WriteLine("REFRESH : " + "NEW TOKEN");

                var tokenData = await authenticationService.RefreshTokenWithGoogle(refresh_tkn);

                // get new payload
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenData.id_token);

                //generate new jwt token authen
                DateTime date = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).LocalDateTime;
                var token = jwtTokenGenerator.GenerateToken(payload.Subject, payload.Name, tokenData.access_token, payload.Picture, date);

                return new GoogleRefreshResult(payload.Subject, payload.Name, payload.Picture, date, token);
            }
            System.Diagnostics.Debug.WriteLine("REFRESH : " + "EXPIRE");

            return new[] { Errors.Authentication.ExpireRefreshToken };
        }

        public async Task<ErrorOr<GoogleRefreshResult>> Handle(GoogleRefreshQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //Check jwt is not null
            if (request.jwt != null)
            {
                //Check jwt expire
                if (DateTimeOffset.FromUnixTimeSeconds((long)Convert.ToDouble(jwtTokenGenerator.DecodeToken(request.jwt).Claims.First(c => c.Type == "exp").Value)).LocalDateTime.CompareTo(DateTime.UtcNow) >= 0)
                {
                    System.Diagnostics.Debug.WriteLine("JWT CHECK: " + "NOT EXPIRE");
                    // Check access token is expire or not
                    using (var httpClient = new HttpClient())
                    {
                        var requestUrl = $"https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={jwtTokenGenerator.DecodeToken(request.jwt).Claims.First(c => c.Type == "atkn").Value}";
                        var response = await httpClient.GetAsync(requestUrl);
                        var tokenRespone = await response.Content.ReadAsStringAsync();
                        var accessTokenData = JsonConvert.DeserializeObject<AccessTokenData>(tokenRespone);

                        if (accessTokenData != null)
                        {
                            System.Diagnostics.Debug.WriteLine("ACCESS TKN CHECK: " + "NOT NULL");

                            DateTime expire = DateTimeOffset.FromUnixTimeSeconds(accessTokenData.exp).LocalDateTime;

                            //Check if access token is expire or not
                            if (expire.CompareTo(DateTime.UtcNow) == -1)
                            {
                                System.Diagnostics.Debug.WriteLine("ACCESS TKN EXPIRE: " + "TRUE");

                                return await ReturnGoogleRefreshResult(request.refresh_tkn);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("ACCESS TKN NOT EXPIRE: " + "OLD TOKEN");

                                var id = jwtTokenGenerator.DecodeToken(request.jwt).Subject;
                                var name = jwtTokenGenerator.DecodeToken(request.jwt).Claims.First(c => c.Type == "name").Value;
                                var img = jwtTokenGenerator.DecodeToken(request.jwt).Claims.First(c => c.Type == "img").Value;
                                var date = DateTimeOffset.FromUnixTimeSeconds((long)Convert.ToDouble(jwtTokenGenerator.DecodeToken(request.jwt).Claims.First(c => c.Type == "exp").Value)).LocalDateTime;

                                //Return back the old token
                                return new GoogleRefreshResult(id, name, img, date, request.jwt);
                            }
                        }
                    }
                }
            }

            return await ReturnGoogleRefreshResult(request.refresh_tkn);
        }
    }
}