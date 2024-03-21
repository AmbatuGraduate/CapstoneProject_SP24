using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Queries.List
{
    public class ListTreeHandler : IRequestHandler<ListTreeQuery, ErrorOr<List<TreeResult>>>
    {
        private readonly ITreeRepository treeRepository;

        //private readonly IStreetRepository streetRepository;
        private readonly ICultivarRepository cultivarRepository;

        public ListTreeHandler(ITreeRepository treeRepository, /*IStreetRepository streetRepository,*/ ICultivarRepository cultivarRepository)
        {
            this.treeRepository = treeRepository;
            //this.streetRepository = streetRepository;
            this.cultivarRepository = cultivarRepository;
        }

        public async Task<ErrorOr<List<TreeResult>>> Handle(ListTreeQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<TreeResult> treeResults = new List<TreeResult>();
            var trees = treeRepository.GetAllTrees();

            foreach (var tree in trees)
            {
                //var streetName = streetRepository.GetStreetById(tree.StreetId).StreetName;
                var cultivar = cultivarRepository.GetCultivarById(tree.CultivarId).CultivarName;

                if (tree.CutTime.CompareTo(DateTime.Now) >= 0)
                {
                    tree.isCut = false;
                    var treeUpdate = treeRepository.UpdateTree(tree);
                    var result = new TreeResult(treeUpdate.TreeCode, treeUpdate.TreeLocation, cultivar, treeUpdate.BodyDiameter, treeUpdate.LeafLength, treeUpdate.CutTime, treeUpdate.isCut, treeUpdate.isExist);
                    treeResults.Add(result);
                }
                else
                {
                    var result = new TreeResult(tree.TreeCode, tree.TreeLocation, cultivar, tree.BodyDiameter, tree.LeafLength, tree.CutTime, tree.isCut, tree.isExist);
                    treeResults.Add(result);
                }

            }

            return treeResults;
        }
    }
}