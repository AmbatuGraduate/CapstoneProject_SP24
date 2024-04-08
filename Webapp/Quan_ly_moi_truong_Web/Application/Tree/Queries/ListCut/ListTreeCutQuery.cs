using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Queries.ListCut
{
    public record ListTreeCutQuery(string Address) : IRequest<ErrorOr<List<TreeResult>>>;
}
