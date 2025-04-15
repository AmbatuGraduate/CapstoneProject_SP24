using Application.Common.Interfaces.Authentication;
using Application.GoogleAuthentication.Common;
using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleRefreshMobile
{
    public class GoogleRefreshHandlerMobile :
                IRequestHandler<GoogleRefreshQueryMobile, ErrorOr<GoogleRefreshResultMobile>>
    {
        private readonly IAuthenticationService authenticationService;

        public GoogleRefreshHandlerMobile(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public async Task<ErrorOr<GoogleRefreshResultMobile>> Handle(GoogleRefreshQueryMobile request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            System.Diagnostics.Debug.WriteLine("handler level: " + request.refresh_tk);
            var refresh_tkn = request.refresh_tk;

            var token = await authenticationService.RefreshTokenWithMobileClient(refresh_tkn);

            return new GoogleRefreshResultMobile(token.expires_in, token.access_token);
        }
    }
}