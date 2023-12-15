using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TreeManage
{
    public interface ITreeRepository
    {
        List<Tree> GetAllTrees();
        Tree CreateTree(Tree tree);
        void DeleteTree(int id);
        Tree UpdateTree(Tree tree);
    }
}
