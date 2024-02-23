using Application.Cultivar.Common;
using ErrorOr;
using MediatR;


namespace Application.Cultivar.Queries.List
{
    public record ListCultivarQuery() : IRequest<ErrorOr<List<CultivarResult>>>;
}
