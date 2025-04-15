using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Delete
{
    public class DeleteTreeHandler :
        IRequestHandler<DeleteTreeCommand, ErrorOr<AddTreeResult>>
    {
        private readonly ITreeRepository treeRepository;

        public DeleteTreeHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<AddTreeResult>> Handle(DeleteTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (treeRepository.GetTreeByTreeCode(request.TreeCode) == null)
                return Errors.GetTreeById.getTreeFail;

            var tree = treeRepository.GetTreeByTreeCode(request.TreeCode);
            treeRepository.DeleteTree(tree);

            return new AddTreeResult(tree.TreeCode);
        }
    }
}