using Domain.Entities.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence
{
    public interface ITreeRepository
    {
        List<Trees> GetAllTrees();
        Trees GetTreeById(Guid id);
        Trees GetTreeByTreeCode(string treeCode);
        Trees CreateTree(Trees tree);
        Trees UpdateTree(Trees tree);
    }
}
