using Application.TreeType.Common;
using ErrorOr;
using MediatR;

namespace Application.TreeType.Commands.Update
{
    public record UpdateTreeTypeCommand(
        Guid TreeTypeId,
        string TreeTypeName,
        string UpdateBy
    ) : IRequest<ErrorOr<TreeTypeResult>>;
}