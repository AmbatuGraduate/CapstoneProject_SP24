
namespace Contract.Tree
{
    public record UpdateTreeRequest
    (
        Guid StreetId,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        int IntervalCutTime,
        Guid CultivarId,
        string Note,
        string UpdateBy,
        bool isExist
    );
}
