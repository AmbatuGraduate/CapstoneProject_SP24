using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Queries.GetById
{
    public record GetByIdQuery(Guid TreeId) : IRequest<ErrorOr<TreeResult>>;
}