using Application.User.Common;
using ErrorOr;
using MediatR;
namespace Application.User.Queries.GetById
{
    public record GetByIdQuery(Guid UserId) : IRequest<ErrorOr<UserResult>>;
}
