using Application.User.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.Add
{
    public record AddUserCommand
    (
        string Name, string Address,
        string Phone, string Password,
        string Image
    ) : IRequest<ErrorOr<UserResult>>;
}
