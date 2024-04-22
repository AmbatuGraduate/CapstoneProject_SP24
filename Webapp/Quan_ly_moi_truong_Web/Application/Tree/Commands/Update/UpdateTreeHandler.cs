using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Common.Errors;
using Domain.Entities.Tree;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Update
{
    public class UpdateTreeHandler :
        IRequestHandler<UpdateTreeCommand, ErrorOr<AddTreeResult>>
    {
        private readonly ITreeRepository treeRepository;
        private readonly IGroupRepository groupRepository;

        public UpdateTreeHandler(ITreeRepository treeRepository, IGroupRepository groupRepository)
        {
            this.treeRepository = treeRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<AddTreeResult>> Handle(UpdateTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var oldTree = treeRepository.GetTreeByTreeCode(request.TreeCode);

            if (oldTree == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }

            var department = groupRepository.GetGroupByEmail(request.Email);

            if (department == null)
            {
                return Errors.Group.notFoundGroup;
            }

            var tree = new Trees
            {
                TreeId = oldTree.TreeId,
                TreeCode = request.TreeCode,
                TreeLocation = request.TreeLocation,
                BodyDiameter = request.BodyDiameter,
                LeafLength = request.LeafLength,
                PlantTime = request.PlantTime,
                CutTime = request.PlantTime.AddMonths(request.IntervalCutTime),
                TreeTypeId = request.TreeTypeId,
                IntervalCutTime = request.IntervalCutTime,
                DepartmentId = department.DepartmentId,
                Note = request.Note
            };

            var result = new AddTreeResult(treeRepository.UpdateTree(tree).TreeCode);

            return result;
        }
    }
}