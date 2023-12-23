using Application.Tree.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.Delete
{
    public record DeleteTreeCommand(int id) : IRequest<ErrorOr<TreeResult>>;
}
