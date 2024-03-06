using Application.Common.Interfaces.Authentication;
using Application.GoogleAuthentication.Common;
using Domain.Common.Errors;
using ErrorOr;
using Google.Apis.Auth;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleLogin
{
    public class GoogleLoginHandler :
        IRequestHandler<GoogleLoginQuery, ErrorOr<GoogleAuthenticationResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IAuthenticationService authenticationService;

        public GoogleLoginHandler(IJwtTokenGenerator jwtTokenGenerator, IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<GoogleAuthenticationResult>> Handle(GoogleLoginQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tokenData = await authenticationService.AuthenticateWithGoogle(request.authCode);

            if (tokenData != null)
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(tokenData.id_token);

                // kiểm tra xem có lấy được payload không
                if (payload != null)
                {
                    //Kiểm tra hạn của id token
                    DateTime date = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).LocalDateTime;
                    if (date.CompareTo(DateTime.Now) == 1 && payload.Issuer.Contains("accounts.google.com"))
                    {
                        var token = jwtTokenGenerator.GenerateToken(payload.Subject, payload.Name, tokenData.access_token, payload.Picture, date);
                        return new GoogleAuthenticationResult(payload.Subject, payload.Name, payload.Picture, date, token, tokenData.refresh_token);
                    }
                }
            }

            return new[] { Errors.Authentication.InvalidCredentials };
        }
    }
}