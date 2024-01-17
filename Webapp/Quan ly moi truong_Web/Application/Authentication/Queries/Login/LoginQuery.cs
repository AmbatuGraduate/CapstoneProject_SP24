using Application.Authentication.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.Login
{
    public record LoginQuery
    (
        string Phone, string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}
