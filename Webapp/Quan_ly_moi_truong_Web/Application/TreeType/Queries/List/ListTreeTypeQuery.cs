using Application.TreeType.Common;
using ErrorOr;
using MediatR;

namespace Application.TreeType.Queries.List
{
    public record ListTreeTypeQuery() : IRequest<ErrorOr<List<TreeTypeResult>>>;
}