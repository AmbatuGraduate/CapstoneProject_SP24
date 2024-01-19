using Application.TreeType.Common;
using ErrorOr;
using MediatR;

namespace Application.TreeType.Queries.GetById
{
    public record GetByIdQuery(Guid TreeTypeId) : IRequest<ErrorOr<TreeTypeResult>>;
}