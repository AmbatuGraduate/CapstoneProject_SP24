namespace Contract.Cultivar
{
    public record ListCultivarRepsone
    (
        Guid CultivarId,
        string CultivarName,
        Guid TreeTypeId
    );
}
