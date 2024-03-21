namespace Application.Tree.Common
{
    public record TreeResult(
    string TreeCode,
    string StreetName,
    string Cultivar,
    float BodyDiameter,
    float LeafLength,
    DateTime CutTime,
    bool isCut,
    bool isExist);
}