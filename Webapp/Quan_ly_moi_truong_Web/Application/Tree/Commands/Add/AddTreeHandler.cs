using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Entities.Tree;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Add
{
    public class AddTreeHandler :
        IRequestHandler<AddTreeCommand, ErrorOr<AddTreeResult>>
    {
        private readonly ITreeRepository treeRepository;

        public AddTreeHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<AddTreeResult>> Handle(AddTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tree = new Trees
            {
                TreeId = Guid.NewGuid(),
                TreeCode = request.TreeCode,
                StreetId = request.StreetId,
                BodyDiameter = request.BodyDiameter,
                LeafLength = request.LeafLength,
                PlantTime = request.PlantTime,
                CutTime = request.CutTime,
                CultivarId = request.CultivarId,
                IntervalCutTime = request.IntervalCutTime,
                CreateBy = request.CreateBy,
                UpdateDate = DateTime.Now,
                UpdateBy = request.UpdateBy,
                Note = request.Note,
            };

            var result = new AddTreeResult(treeRepository.CreateTree(tree).TreeCode);

            return result;
        }
    }
}