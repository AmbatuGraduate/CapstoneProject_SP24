using Application.Street.Common;
using ErrorOr;
using MediatR;

namespace Application.Street.Commands.Add
{
    public record AddStreetCommand(
               string StreetName,
               float StreetLength,
               int NumberOfHouses,
               Guid StreetTypeId,
               Guid ResidentialGroupId,
               string CreateBy,
               string UpdateBy
                ) : IRequest<ErrorOr<StreetResult>>;
}
