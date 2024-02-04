using Application.Street.Common;
using ErrorOr;
using MediatR;

namespace Application.Street.Commands.Update
{
    public record UpdateStreetCommand(
               Guid StreetId,
               string StreetName,
               float StreetLength,
               int NumberOfHouses,
               Guid StreetTypeId,
               Guid WardId,
               string UpdateBy
               ) : IRequest<ErrorOr<StreetResult>>;
}
