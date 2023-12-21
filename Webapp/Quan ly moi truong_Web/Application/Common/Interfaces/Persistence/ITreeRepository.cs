using Domain.Entities;
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
        Trees GetTreeById(int id);
        Trees CreateTree(Trees tree);
        void DeleteTree(int id);
        Trees UpdateTree(Trees tree);
    }
}
