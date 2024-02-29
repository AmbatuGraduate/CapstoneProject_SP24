using Application.Street.Common;
using ErrorOr;
using MediatR;


namespace Application.Street.Queries.List
{
    public record ListStreetQuery() : IRequest<ErrorOr<List<StreetResult>>>;
}
