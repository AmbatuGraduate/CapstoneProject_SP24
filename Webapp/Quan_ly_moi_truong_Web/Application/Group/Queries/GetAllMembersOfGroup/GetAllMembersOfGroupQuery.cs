using Application.User.Common.List;
using ErrorOr;
using MediatR;

namespace Application.Group.Queries.GetAllMembersOfGroup
{
    public record GetAllMembersOfGroupQuery(string accessToken, string groupEmail) : IRequest<ErrorOr<List<GoogleUser>>>;
}