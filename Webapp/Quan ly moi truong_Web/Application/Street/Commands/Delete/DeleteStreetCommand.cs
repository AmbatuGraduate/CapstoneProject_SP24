using Application.Street.Common;
using ErrorOr;
using MediatR;

namespace Application.Street.Commands.Delete
{
    public record DeleteStreetCommand(Guid StreetId) : IRequest<ErrorOr<StreetResult>>;
}
