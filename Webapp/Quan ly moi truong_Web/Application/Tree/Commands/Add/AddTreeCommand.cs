using Application.Tree.Common;
using ErrorOr;
using MediatR;

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
