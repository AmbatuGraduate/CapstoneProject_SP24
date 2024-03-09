using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Tree.Queries.GetByTreeCode
{
    public class GetByTreeCodeHandler :
        IRequestHandler<GetByTreeCodeQuery, ErrorOr<TreeDetailResult>>
    {
        private readonly ITreeRepository treeRepository;
        private readonly IStreetRepository streetRepository;
        private readonly ICultivarRepository cultivarRepository;


        public GetByTreeCodeHandler(ITreeRepository treeRepository, IStreetRepository streetRepository, ICultivarRepository cultivarRepository)
        {
            this.treeRepository = treeRepository;
            this.streetRepository = streetRepository;
            this.cultivarRepository = cultivarRepository;
        }

        public async Task<ErrorOr<TreeDetailResult>> Handle(GetByTreeCodeQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tree = treeRepository.GetTreeByTreeCode(request.TreeCode);

            if (tree == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }

            var streetName = streetRepository.GetStreetById(tree.StreetId).StreetName;
            var cultivar = cultivarRepository.GetCultivarById(tree.CultivarId).CultivarName;
            var result = new TreeDetailResult(tree.TreeCode, streetName, cultivar, tree.BodyDiameter, tree.LeafLength, tree.PlantTime, tree.CutTime, tree.Note);

            return result;
        }
    }
}