using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Tree.Queries.GetById
{
    public class GetByIdHandler :
        IRequestHandler<GetByIdQuery, ErrorOr<TreeDetailResult>>
    {
        private readonly ITreeRepository treeRepository;
        private readonly ICultivarRepository cultivarRepository;

        public GetByIdHandler(ITreeRepository treeRepository,  /*IStreetRepository streetRepository,*/ ICultivarRepository cultivarRepository)
        {
            this.treeRepository = treeRepository;
            //this.streetRepository = streetRepository;
            this.cultivarRepository = cultivarRepository;
        }

        public async Task<ErrorOr<TreeDetailResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tree = treeRepository.GetTreeById(request.TreeId);

            if (tree == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }

            //var streetName = streetRepository.GetStreetById(tree.StreetId).StreetName;
            var cultivar = cultivarRepository.GetCultivarById(tree.CultivarId).CultivarName;
            var result = new TreeDetailResult(tree.TreeCode, /*streetName*/ tree.TreeLocation, cultivar, tree.BodyDiameter, tree.LeafLength, tree.PlantTime, tree.CutTime, tree.Note);

            return result;
        }
    }
}