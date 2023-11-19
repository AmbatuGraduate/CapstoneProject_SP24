
using Application.Authentication.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register
{
    /// <summary>
    /// Request of user when register
    /// </summary>
    /// <param name="Name">Fullname of user</param>
    /// <param name="Address">Address of user</param>
    /// <param name="Phone">Phone number of user</param>
    /// <param name="Password">Password of user</param>
    /// <param name="Role">Role of user</param>
    /// <param name="Image">Avatar of user</param>
    public record RegisterCommand
    (
        string Name, string Address,
        string Phone, string Password,
        string Role, string Image
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}
