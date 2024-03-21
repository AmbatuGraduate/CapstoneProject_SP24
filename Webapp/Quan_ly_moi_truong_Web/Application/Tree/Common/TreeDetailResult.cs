namespace Application.Tree.Common
{
    public record TreeDetailResult(
        string TreeCode,
        string StreetName,
        string Cultivar,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        DateTime CutTime,
        string Note
    );
}