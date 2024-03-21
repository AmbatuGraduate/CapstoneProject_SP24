namespace Contract.Cultivar
{
    public record UpdateCultivarRequest
    (
        Guid CultivarId,
        string CultivarName,
        Guid TreeTypeId,
        string UpdateBy
    );
}