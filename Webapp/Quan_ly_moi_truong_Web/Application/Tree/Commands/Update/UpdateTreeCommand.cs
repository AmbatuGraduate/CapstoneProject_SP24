using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Update
{
    public record UpdateTreeCommand(
        string TreeCode,
        Guid StreetId,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        int IntervalCutTime,
        Guid CultivarId,
        string Note,
        string UpdateBy,
        bool isExist
        ) : IRequest<ErrorOr<AddTreeResult>>;
}