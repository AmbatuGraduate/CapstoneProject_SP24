using Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TreeRepository : ITreeRepository
    {

        // constructor dependency injection
        private readonly TreeDbContext _treeDbContext;

        public TreeRepository(TreeDbContext treeDbContext)
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

        public List<Tree> GetAllTrees()
        {
            return _treeDbContext.Trees.ToList();
        }
    }
}
