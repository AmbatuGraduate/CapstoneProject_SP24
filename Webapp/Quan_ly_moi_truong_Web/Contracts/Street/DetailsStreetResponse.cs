namespace Contract.Street
{
    public record DetailsStreetResponse
    (
        string StreetName,
        float StreetLength,
        int NumberOfHouses,
        Guid  StreetTypeId,
        Guid WardId
    );
}
