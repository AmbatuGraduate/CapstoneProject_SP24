namespace Contract.Tree
{
    public record AddTreeRequest
    (
        string TreeCode,
        string TreeLocation,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        DateTime CutTime,
        int IntervalCutTime,
        Guid TreeTypeId,
        string Note,
        string UserId,
        bool isExist
    );
}