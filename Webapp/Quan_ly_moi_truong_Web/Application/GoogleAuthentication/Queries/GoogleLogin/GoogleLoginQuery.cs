using Application.GoogleAuthentication.Common;
using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleLogin
{
    public record GoogleLoginQuery(string authCode) : IRequest<ErrorOr<GoogleAuthenticationResult>>;
}