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
        private readonly IUserRepository userRepository;

        public UpdateTreeHandler(ITreeRepository treeRepository, IUserRepository userRepository)
        {
            this.treeRepository = treeRepository;
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<AddTreeResult>> Handle(UpdateTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var oldTree = treeRepository.GetTreeByTreeCode(request.TreeCode);

            if (oldTree == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }


            var userId = userRepository.GetAll().FirstOrDefault(x => x.Email == request.Email).Id;

            if (userId == null)
            {
                return Errors.User.NotExist;
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
                UserId = userId,
                Note = request.Note,
                isExist = oldTree.isExist
            };

            var result = new AddTreeResult(treeRepository.UpdateTree(tree).TreeCode);

            return result;
        }
    }
}