namespace Contract.Street
{
    public record ListStreetResponse
    (
        Guid StreetId,
        string StreetName,
        string StreetLength,
        int NumberOfHouses,
        Guid StreetTypeId,
        Guid ResidentialGroupId,
        Guid DistrictId
    );
}
