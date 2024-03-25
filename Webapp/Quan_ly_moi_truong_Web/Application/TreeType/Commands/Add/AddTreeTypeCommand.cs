using Application.TreeType.Common;
using ErrorOr;
using MediatR;

namespace Application.TreeType.Commands.Add
{
    public record AddTreeTypeCommand
    (
        string TreeTypeName
    ) : IRequest<ErrorOr<TreeTypeResult>>;
}