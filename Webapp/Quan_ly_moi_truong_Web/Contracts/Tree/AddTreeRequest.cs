namespace Contract.Tree
{
    public record AddTreeRequest
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
        string CreateBy,
        string UpdateBy,
        DateTime UpdateDate,
        bool isExist
    );
}