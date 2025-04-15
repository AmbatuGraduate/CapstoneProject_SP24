namespace Contract.Tree

{
    public record UpdateTreeRequest
    (
        string TreeLocation,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        int IntervalCutTime,
        Guid TreeTypeId,
        string Note,
        string Email
    );
}