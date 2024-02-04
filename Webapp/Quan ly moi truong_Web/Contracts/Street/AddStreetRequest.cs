
namespace Contract.Street
{
    public record AddStreetRequest
    (
               string StreetName,
               float StreetLength,
               int NumberOfHouses,
               Guid StreetTypeId,
               Guid WardId,
               string CreateBy,
               string UpdateBy
           );
}
