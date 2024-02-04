using Application.User.Common;
using ErrorOr;
using MediatR;

namespace Application.User.Queries.List
{
    public record ListQuery() : IRequest<ErrorOr<List<UserResult>>>;
}