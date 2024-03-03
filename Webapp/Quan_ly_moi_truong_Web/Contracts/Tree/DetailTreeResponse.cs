namespace Contract.Tree
{
    public record DetailTreeResponse
    (
        string TreeCode,
        Guid StreetId,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        DateTime CutTime,
        int IntervalCutTime,
        Guid CultivarId,
        string Note,
        bool isExist
    );
}