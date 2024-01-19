namespace Contract.Street
{
    public record ListStreetResponse
    (
        Guid StreetId,
        string StreetName,
        string StreetLength,
        int NumberOfHouses,
        Guid StreetTypeId,
        Guid WardId,
        Guid DistrictId,
        bool isExist
    );
}
