namespace Application.Tree.Common
{
    public record TreeResult(
    string TreeCode,
    string StreetName,
    string TreeType,
    float BodyDiameter,
    float LeafLength,
    DateTime CutTime,
    bool isCut);
}