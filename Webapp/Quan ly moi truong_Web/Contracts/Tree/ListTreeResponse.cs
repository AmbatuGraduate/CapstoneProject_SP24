
namespace Contract.Tree
{
    public record ListTreeResponse
    (
        string TreeCode,
        Guid StreetId,
        DateTime PlantTime,
        DateTime CutTime,
        Guid CultivarId,
        bool isExist
    );
}
