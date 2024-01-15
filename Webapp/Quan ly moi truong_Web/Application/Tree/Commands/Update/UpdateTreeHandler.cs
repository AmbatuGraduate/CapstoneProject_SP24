using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Entities.Tree;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Update
{
    public class UpdateTreeHandler :
        IRequestHandler<UpdateTreeCommand, ErrorOr<TreeResult>>
    {

        private readonly ITreeRepository treeRepository;

        public UpdateTreeHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<TreeResult>> Handle(UpdateTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //if(treeRepository.GetTreeById(request.id) == null)
            //{
            //    return Errors.GetTreeById.getTreeFail;
            //}

            var tree = new Trees
            {
                //Id = request.id,
                //District = request.district,
                //Street = request.street,
                //RootType = request.rootType,
                //Type = request.type,
                //BodyDiameter = request.bodyDiameter,
                //LeafLength = request.leafLength,
                //PlantTime = request.plantTime,
                //CutTime = request.cutTime,
                //IntervalCutTime = request.intervalCutTime,
                //Note = request.note,
            };

            var result = new TreeResult(treeRepository.UpdateTree(tree));

            return result;
        }
    }
}
