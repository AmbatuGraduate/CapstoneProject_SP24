using Application.Group.Common;
using ErrorOr;
using MediatR;

namespace Application.Group.Queries.GetGroup
{
    public record GetGroupByGroupEmailQuery(string accessToken, string groupEmail) : IRequest<ErrorOr<GroupResult>>;
}