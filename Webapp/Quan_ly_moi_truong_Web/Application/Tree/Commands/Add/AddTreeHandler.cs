using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Common.Errors;
using Domain.Entities.Tree;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Add
{
    public class AddTreeHandler :
        IRequestHandler<AddTreeCommand, ErrorOr<AddTreeResult>>
    {
        private readonly ITreeRepository treeRepository;
        private readonly IGroupRepository groupRepository;

        public AddTreeHandler(ITreeRepository treeRepository, IGroupRepository groupRepository)
        {
            this.treeRepository = treeRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<ErrorOr<AddTreeResult>> Handle(AddTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (treeRepository.GetTreeByTreeCode(request.TreeCode) != null)
            {
                return Errors.AddTree.DuplicateTreeCode;
            }

            var department = groupRepository.GetGroupByEmail(request.Email);
            
            if(department == null)
            {
                return Errors.Group.notFoundGroup;
            }

            var tree = new Trees
            {
                TreeId = Guid.NewGuid(),
                TreeCode = request.TreeCode,
                TreeLocation = request.TreeLocation,
                BodyDiameter = request.BodyDiameter,
                LeafLength = request.LeafLength,
                PlantTime = request.PlantTime,
                CutTime = request.CutTime,
                TreeTypeId = request.TreeTypeId,
                IntervalCutTime = request.IntervalCutTime,
                DepartmentId = department.DepartmentId,
                Note = request.Note,
            };

            var result = new AddTreeResult(treeRepository.CreateTree(tree).TreeCode);

            return result;
        }
    }
}