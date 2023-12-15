

using Domain.Entities;

namespace Application.TreeManage
{
    public interface ITreeService
    {
        List<Tree> GetAllTrees();
        Tree CreateTree(Tree tree);
        void DeleteTree(int id);
        Tree UpdateTree(Tree tree);
    }
}
