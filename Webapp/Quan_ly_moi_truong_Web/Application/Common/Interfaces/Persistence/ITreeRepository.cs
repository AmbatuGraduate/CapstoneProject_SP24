using Domain.Entities.Tree;

namespace Application.Common.Interfaces.Persistence
{
    public interface ITreeRepository
    {
        List<Trees> GetAllTrees();

        Trees GetTreeById(Guid id);

        Trees GetTreeByTreeCode(string treeCode);

        Trees CreateTree(Trees tree);

        Trees UpdateTree(Trees tree);
        void DeleteTree(Trees tree);
    }
}