using Application.GoogleAuthentication.Common;
using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleRefreshMobile
{
    public record GoogleRefreshQueryMobile(string refresh_tk) : IRequest<ErrorOr<GoogleRefreshResultMobile>>;
}