using Application.Tree.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.Update
{
    public record UpdateTreeCommand (
        int id, 
        string district, string street,
        string rootType, string type, float bodyDiameter,
        float leafLength, DateTime plantTime, DateTime cutTime,
        int intervalCutTime, string note
        ) : IRequest<ErrorOr<TreeResult>>;
}
