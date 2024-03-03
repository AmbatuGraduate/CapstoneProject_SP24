using Application.Street.Common;
using ErrorOr;
using MediatR;

namespace Application.Street.Queries.GetById
{
    public record GetByIdQuery(Guid StreetId) : IRequest<ErrorOr<StreetResult>>;
}
