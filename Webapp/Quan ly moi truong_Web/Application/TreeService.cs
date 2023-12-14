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

        public Tree CreateTree(Tree tree)
        {
            _treeRepository.CreateTree(tree);
            return tree;
        }

        public List<Tree> GetAllTrees()
        {
            var trees = _treeRepository.GetAllTrees();
            return trees;
        }
    }
}
