using Application.GoogleAuthentication.Common;
using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleAccessToken
{
    public record GoogleAccessTokenQuery(string jwt) : IRequest<ErrorOr<GoogleAccessTokenResult>>;
}