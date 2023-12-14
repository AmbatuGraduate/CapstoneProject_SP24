using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class TreeService : ITreeService
    {
        private readonly ITreeRepository _treeRepository;

        // constructor dependency injection
        public TreeService(ITreeRepository treeRepository)
        {
            _treeRepository = treeRepository;
        }

        // CRUD
        public Tree CreateTree(Tree tree)
        {
            _treeRepository.CreateTree(tree);
            return tree;
        }
        public void DeleteTree(int id)
        {
            _treeRepository.DeleteTree(id);
        }

        public List<Tree> GetAllTrees()
        {
            var trees = _treeRepository.GetAllTrees();
            return trees;
        }

        public Tree UpdateTree(Tree tree)
        {
            _treeRepository.UpdateTree(tree);
            return tree;
        }
    }
}
