
using Application.Common.Interfaces.Persistence;
using Domain.Entities.Tree;

namespace Infrastructure.Persistence.Repositories
{
    public class TreeRepository : ITreeRepository
    {
        // constructor dependency injection
        private readonly WebDbContext _treeDbContext;

        public TreeRepository(WebDbContext treeDbContext)
        {
            _treeDbContext = treeDbContext;
        }

        public Trees CreateTree(Trees tree)
        {
            _treeDbContext.Trees.Add(tree);
            _treeDbContext.SaveChanges();

            return tree;
        }

        public List<Trees> GetAllTrees()
        {
            return _treeDbContext.Trees.ToList();
        }

        public Trees GetTreeById(Guid id)
        {
            return _treeDbContext.Trees.FirstOrDefault(t => t.TreeId == id);
        }

        public Trees GetTreeByTreeCode(string treeCode)
        {
            return _treeDbContext.Trees.FirstOrDefault(t => t.TreeCode == treeCode);
        }

        public Trees UpdateTree(Trees tree)
        {
            _treeDbContext.Trees.Update(tree);
            _treeDbContext.SaveChanges();
            return tree;
        }
    }
}
