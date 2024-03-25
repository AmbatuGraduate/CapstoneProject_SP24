using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Update
{
    public record UpdateTreeCommand(
        string TreeCode,
        string TreeLocation,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        int IntervalCutTime,
        Guid TreeTypeId,
        string Note,
        string UserId
        ) : IRequest<ErrorOr<AddTreeResult>>;
}