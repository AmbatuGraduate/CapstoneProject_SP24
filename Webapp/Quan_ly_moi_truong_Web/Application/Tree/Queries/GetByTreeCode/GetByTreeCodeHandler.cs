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
        private readonly ITreeTypeRepository treeTypeRepository;
        private readonly IGroupRepository groupRepository;

        public GetByTreeCodeHandler(ITreeRepository treeRepository, ITreeTypeRepository treeTypeRepository, IGroupRepository groupRepository)
        {
            this.treeRepository = treeRepository;
            this.treeTypeRepository = treeTypeRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<TreeDetailResult>> Handle(GetByTreeCodeQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tree = treeRepository.GetTreeByTreeCode(request.TreeCode);

            if (tree == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }

            var treeType = treeTypeRepository.GetTreeTypeById(tree.TreeTypeId).TreeTypeName;
            var department = groupRepository.GetGroupDbById(tree.DepartmentId).DepartmentName;
            var result = new TreeDetailResult(tree.TreeCode, tree.TreeLocation, treeType, tree.BodyDiameter, tree.LeafLength, tree.PlantTime, tree.IntervalCutTime, tree.CutTime, tree.isCut, department, tree.Note);

            return result;
        }
    }
}