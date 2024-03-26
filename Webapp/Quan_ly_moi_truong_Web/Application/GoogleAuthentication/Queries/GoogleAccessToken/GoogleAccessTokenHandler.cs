using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.GoogleAuthentication.Common;
using ErrorOr;
using Google.Apis.Calendar.v3.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if(accessToken != null)
                {
                    return new GoogleAccessTokenResult(accessToken);
                }
            }
            return new[] { Domain.Common.Errors.Errors.AccessToken.InvalidAccessToken };
        }
    }
}
