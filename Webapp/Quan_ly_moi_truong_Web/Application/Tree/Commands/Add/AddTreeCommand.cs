using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Add
{
    public record AddTreeCommand(
        string TreeCode,
        string TreeLocation,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        DateTime CutTime,
        int IntervalCutTime,
        Guid TreeTypeId,
        string Note,
        string UserId,
        bool isExist
        ) : IRequest<ErrorOr<AddTreeResult>>;
}