using Application.GoogleAuthentication.Common;
using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Queries.GoogleRefresh
{
    public record GoogleRefreshQuery(string jwt) : IRequest<ErrorOr<GoogleRefreshResult>>;
}