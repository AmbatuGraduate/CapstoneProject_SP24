using Application.Group.Common;
using ErrorOr;
using MediatR;

namespace Application.Group.Queries.GetAllGroups
{
    public record GetAllGroupsQuery() : IRequest<ErrorOr<List<GroupResult>>>;
}
