using Application.User.Common;
using ErrorOr;
using MediatR;

namespace Application.User.Commands.Add
{
    public record AddUserCommand
    (
        string Name, string Address,
        string Phone, string Password,
        string Image
    ) : IRequest<ErrorOr<UserResult>>;
}