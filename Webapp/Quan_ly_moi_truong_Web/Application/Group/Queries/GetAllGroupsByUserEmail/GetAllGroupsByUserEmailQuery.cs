using Application.Group.Common;
using ErrorOr;
using MediatR;

namespace Application.Group.Queries.GetAllGroupsByUserEmail
{
    public record GetAllGroupsByUserEmailQuery(string accessToken, string userEmail) : IRequest<ErrorOr<List<GroupResult>>>;
}