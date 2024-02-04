using Application.Common.Interfaces.Persistence;
using Domain.Entities.TreeType;

namespace Infrastructure.Persistence.Repositories
{
    public class TreeTypeRepository : ITreeTypeRepository
    {
        private readonly WebDbContext _treeDbContext;

        public TreeTypeRepository(WebDbContext treeDbContext)
        {
            _treeDbContext = treeDbContext;
        }

        public TreeTypes CreateTreeType(TreeTypes treeTypes)
        {
            _treeDbContext.TreeTypes.Add(treeTypes);
            _treeDbContext.SaveChanges();
            return treeTypes;
        }

        public List<TreeTypes> GetAllTreeTypes()
        {
            return _treeDbContext.TreeTypes.ToList();
        }

        public TreeTypes GetTreeTypeById(Guid id)
        {
            return _treeDbContext.TreeTypes.FirstOrDefault(treeTypes => treeTypes.TreeTypeId == id);
        }

        public TreeTypes UpdateTreeType(TreeTypes treeTypes)
        {
            _treeDbContext.TreeTypes.Update(treeTypes);
            _treeDbContext.SaveChanges();
            return treeTypes;
        }
    }
}