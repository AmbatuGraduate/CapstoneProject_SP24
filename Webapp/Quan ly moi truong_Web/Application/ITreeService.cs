using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    // use case
    public interface ITreeService
    {
        List<Tree> GetAllTrees();
        Tree CreateTree(Tree tree);
        void DeleteTree(int id);
        Tree UpdateTree(Tree tree);
    }
}
