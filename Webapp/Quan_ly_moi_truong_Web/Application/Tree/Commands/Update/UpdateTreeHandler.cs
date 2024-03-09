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

        public UpdateTreeHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<AddTreeResult>> Handle(UpdateTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (treeRepository.GetTreeByTreeCode(request.TreeCode) == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }

            var tree = new Trees
            {
                TreeId = treeRepository.GetTreeByTreeCode(request.TreeCode).TreeId,
                TreeCode = request.TreeCode,
                StreetId = request.StreetId,
                BodyDiameter = request.BodyDiameter,
                LeafLength = request.LeafLength,
                PlantTime = request.PlantTime,
                CutTime = request.PlantTime.AddMonths(request.IntervalCutTime),
                CultivarId = request.CultivarId,
                IntervalCutTime = request.IntervalCutTime,
                CreateBy = treeRepository.GetTreeByTreeCode(request.TreeCode).CreateBy,
                CreateDate = treeRepository.GetTreeByTreeCode(request.TreeCode).CreateDate,
                UpdateDate = DateTime.Now,
                UpdateBy = request.UpdateBy,
                Note = request.Note,
                isExist = treeRepository.GetTreeByTreeCode(request.TreeCode).isExist
            };

            var result = new AddTreeResult(treeRepository.UpdateTree(tree).TreeCode);

            return result;
        }
    }
}