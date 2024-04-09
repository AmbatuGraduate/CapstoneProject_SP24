namespace Contract.Tree
{
    public record ListTreeResponse
    (
        string TreeCode,
        string StreetName,
        string TreeType,
        float BodyDiameter,
        float LeafLength,
        DateTime CutTime,
        bool isCut
    );
}