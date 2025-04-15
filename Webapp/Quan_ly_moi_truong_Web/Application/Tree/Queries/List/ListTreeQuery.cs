using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Queries.List
{
    public record ListTreeQuery() : IRequest<ErrorOr<List<TreeResult>>>;
}