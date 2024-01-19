using Application.User.Common;
using ErrorOr;
using MediatR;

namespace Application.User.Commands.Udpate
{
    public record UpdateUserCommand(
        Guid Id,
        string Name, string Address,
        string Phone, string Password,
        string Role, string Image,
        bool Status) : IRequest<ErrorOr<UserResult>>;
}