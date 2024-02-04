using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Queries.GetByTreeCode
{
    public record GetByTreeCodeQuery(string TreeCode) : IRequest<ErrorOr<TreeResult>>;
}