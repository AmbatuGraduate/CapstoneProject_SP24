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
        private readonly IUserRepository userRepository;

        public AddTreeHandler(ITreeRepository treeRepository, IUserRepository userRepository)
        {
            this.treeRepository = treeRepository;
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<AddTreeResult>> Handle(AddTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (treeRepository.GetTreeByTreeCode(request.TreeCode) != null)
            {
                return Errors.AddTree.DuplicateTreeCode;
            }

            var userId = userRepository.GetAll().FirstOrDefault(x => x.Email == request.Email).Id;
            
            if(userId == null)
            {
                return Errors.User.NotExist;
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
                UserId = userId,
                Note = request.Note,
            };

            var result = new AddTreeResult(treeRepository.CreateTree(tree).TreeCode);

            return result;
        }
    }
}