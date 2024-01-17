using Application.User.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.Udpate
{
    public record UpdateUserCommand(
        Guid Id,
        string Name, string Address,
        string Phone, string Password,
        string Role, string Image,
        bool Status) : IRequest<ErrorOr<UserResult>>;
}
