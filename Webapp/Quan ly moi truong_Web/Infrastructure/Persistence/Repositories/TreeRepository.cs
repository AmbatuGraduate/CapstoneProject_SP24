
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

        // business rule
        public Trees CreateTree(Trees tree)
        {
            _treeDbContext.Trees.Add(tree);
            _treeDbContext.SaveChanges();

            return tree;
        }

        public void DeleteTree(Guid id)
        {
            var tree = _treeDbContext.Trees.FirstOrDefault(t => t.TreeId == id);
            _treeDbContext.Trees.Remove(tree);
            _treeDbContext.SaveChanges();
        }

        public List<Trees> GetAllTrees()
        {
            return _treeDbContext.Trees.ToList();
        }

        public Trees GetTreeById(Guid id)
        {
            return _treeDbContext.Trees.FirstOrDefault(t => t.TreeId == id);
        }

        public Trees UpdateTree(Trees tree)
        {
            tree.IntervalCutTime = (int)(DateTime.Now - tree.CutTime).TotalDays;
            _treeDbContext.Trees.Update(tree);
            _treeDbContext.SaveChanges();
            return tree;
        }
    }
}
