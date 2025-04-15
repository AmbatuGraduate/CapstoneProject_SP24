using ErrorOr;
using MediatR;

namespace Application.Group.Commands.Delete
{
    public record DeleteGroupCommand(string accessToken, string groupEmail) : IRequest<ErrorOr<bool>>;
}