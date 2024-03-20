using Application.GoogleAuthentication.Common;
using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Commands.GoogleLogin
{
    public record GoogleLoginCommand(string authCode) : IRequest<ErrorOr<GoogleAuthenticationResult>>;
}