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
        private readonly ITreeTypeRepository treeTypeRepository;
        private readonly IUserRepository userRepository;

        public GetByIdHandler(ITreeRepository treeRepository, ITreeTypeRepository treeTypeRepository, IUserRepository userRepository)
        {
            this.treeRepository = treeRepository;
            this.treeTypeRepository = treeTypeRepository;
            this.userRepository = userRepository;
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
            var treeType = treeTypeRepository.GetTreeTypeById(tree.TreeTypeId).TreeTypeName;
            var user = userRepository.GetById(tree.UserId).Email;
            var result = new TreeDetailResult(tree.TreeCode, tree.TreeLocation, treeType, tree.BodyDiameter, tree.LeafLength, tree.PlantTime, tree.IntervalCutTime, tree.CutTime, tree.isCut, user, tree.Note);

            return result;
        }
    }
}