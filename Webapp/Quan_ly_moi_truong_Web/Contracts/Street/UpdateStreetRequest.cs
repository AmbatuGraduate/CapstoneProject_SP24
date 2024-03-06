namespace Contract.Street

{
    public record UpdateStreetRequest(
               string StreetName,
               float StreetLength,
               int NumberOfHouses,
               Guid StreetTypeId,
               Guid ResidentialGroupId,
               string UpdateBy
        );
}
