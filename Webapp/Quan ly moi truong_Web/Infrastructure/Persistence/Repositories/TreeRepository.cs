using Application.TreeManage;
using Domain.Entities;

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
        public Tree CreateTree(Tree tree)
        {
            _treeDbContext.Trees.Add(tree);
            _treeDbContext.SaveChanges();

            return tree;
        }

        public void DeleteTree(int id)
        {
            _treeDbContext.Remove(id);
            _treeDbContext.SaveChanges();
        }

        public List<Tree> GetAllTrees()
        {
            return _treeDbContext.Trees.ToList();
        }

        public Tree UpdateTree(Tree tree)
        {
            tree.IntervalCutTime = (int)(DateTime.Now - tree.CutTime).TotalDays;
            _treeDbContext.Trees.Update(tree);
            _treeDbContext.SaveChanges();
            return tree;
        }
    }
}
