using Application.Group.Common.Add_Update;
using Application.Group.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Commands.Delete
{
    public record DeleteGroupCommand(string accessToken, string groupEmail) : IRequest<ErrorOr<bool>>;
}
