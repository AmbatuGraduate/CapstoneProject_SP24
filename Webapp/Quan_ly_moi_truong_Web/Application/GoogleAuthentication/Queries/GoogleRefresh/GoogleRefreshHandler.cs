using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.GoogleAuthentication.Common;
using Domain.Common.Errors;
using ErrorOr;
using Google.Apis.Auth;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleRefresh
{
    public class GoogleRefreshHandler :
                IRequestHandler<GoogleRefreshQuery, ErrorOr<GoogleRefreshResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserRefreshTokenRepository userRefreshTokenRepository;

        public GoogleRefreshHandler(IJwtTokenGenerator jwtTokenGenerator, IAuthenticationService authenticationService, IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            this.authenticationService = authenticationService;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<ErrorOr<GoogleRefreshResult>> Handle(GoogleRefreshQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //Check jwt is not null
            if (request.jwt != null)
            {
                var userId = jwtTokenGenerator.DecodeTokenToGetUserId(request.jwt);
                var jwt_expire = jwtTokenGenerator.DecodeToken(request.jwt).Claims.First(claim => claim.Type == "exp").Value;
                var refresh_tkn = userRefreshTokenRepository.GetRefreshRokenByUserId(userId);

                //Check refresh token is exist or not
                if (refresh_tkn != null)
                {
                    if (DateTime.Now.CompareTo(new DateTime(refresh_tkn.Expire)) <= 0)
                    {
                        if (DateTimeOffset.FromUnixTimeSeconds((long)Convert.ToDouble(jwt_expire)).CompareTo(DateTime.Now) <= 0)
                        {
                            System.Diagnostics.Debug.WriteLine("REFRESH : " + "NEW TOKEN");

                            var tokenData = await authenticationService.RefreshTokenWithGoogle(refresh_tkn.RefreshToken);

                            // get new payload
                            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenData.id_token);

                            //generate new jwt token authen
                            DateTime date = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).LocalDateTime;
                            var token = jwtTokenGenerator.GenerateToken(payload.Subject, tokenData.access_token, date);

                            return new GoogleRefreshResult(payload.Subject, payload.Name, payload.Picture, date, token);
                        }
                    }
                }
            }

            return new[] { Errors.Authentication.ExpireRefreshToken };
        }
    }
}