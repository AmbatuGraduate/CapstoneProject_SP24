using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.Login
{
    public record LoginQuery
    (
        string Phone, string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}