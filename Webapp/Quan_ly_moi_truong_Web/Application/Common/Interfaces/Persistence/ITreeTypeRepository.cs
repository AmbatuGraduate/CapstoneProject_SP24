using Domain.Entities.TreeType;

namespace Application.Common.Interfaces.Persistence
{
    public interface ITreeTypeRepository
    {
        List<TreeTypes> GetAllTreeTypes();

        TreeTypes GetTreeTypeById(Guid id);

        TreeTypes CreateTreeType(TreeTypes treeTypes);

        TreeTypes UpdateTreeType(TreeTypes treeTypes);
    }
}