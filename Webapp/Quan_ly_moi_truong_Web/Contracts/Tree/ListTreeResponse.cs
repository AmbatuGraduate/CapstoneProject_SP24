namespace Contract.Tree
{
    public record ListTreeResponse
    (
        string TreeCode,
        string StreetName,
        string Cultivar,
        float BodyDiameter,
        float LeafLength,
        DateTime CutTime,
        bool isCut,
        bool isExist
    );
}