using Application.Tree.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.Add
{
    public record AddTreeCommand (
        string TreeCode, 
        Guid StreetId,
        float BodyDiameter, 
        float LeafLength,
        DateTime PlantTime, 
        DateTime CutTime,
        int IntervalCutTime, 
        Guid CultivarId, 
        string Note,
        string CreateBy, 
        string UpdateBy, 
        bool isExist
        ) : IRequest<ErrorOr<TreeResult>>;
}
