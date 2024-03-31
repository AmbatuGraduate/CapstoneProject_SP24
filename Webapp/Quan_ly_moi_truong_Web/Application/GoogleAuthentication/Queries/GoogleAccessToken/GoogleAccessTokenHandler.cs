using Application.Common.Interfaces.Authentication;
using Application.GoogleAuthentication.Common;
using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleAccessToken
{
    public class GoogleAccessTokenHandler : IRequestHandler<GoogleAccessTokenQuery, ErrorOr<GoogleAccessTokenResult>>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public GoogleAccessTokenHandler(IJwtTokenGenerator jwtTokenGenerator)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<GoogleAccessTokenResult>> Handle(GoogleAccessTokenQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            System.Diagnostics.Debug.WriteLine("handler level: " + request.jwt);
            if (request.jwt != null)
            {
                var accessToken = jwtTokenGenerator.DecodeTokenToGetAccessToken(request.jwt);
                if (accessToken != null)
                {
                    return new GoogleAccessTokenResult(accessToken);
                }
            }
            return new[] { Domain.Common.Errors.Errors.AccessToken.InvalidAccessToken };
        }
    }
}