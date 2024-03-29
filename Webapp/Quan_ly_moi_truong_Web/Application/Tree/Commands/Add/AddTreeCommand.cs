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
        string UserId, // id user current login  -> FE input readonly -> nguoi dn hien tai hien tai
        bool isExist
        ) : IRequest<ErrorOr<AddTreeResult>>;
}