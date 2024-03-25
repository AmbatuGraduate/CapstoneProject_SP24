namespace Contract.Tree
{
    public record DetailTreeResponse
    (
        string TreeCode,
        string StreetName,
        string TreeType,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        int IntervalCutTime,
        DateTime CutTime,
        bool isCut,
        string User,
        string Note
    );
}