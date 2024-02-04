using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using ErrorOr;
using MediatR;
using Domain.Common.Errors;

namespace Application.Tree.Queries.GetByTreeCode
{
    public class GetByTreeCodeHandler :
        IRequestHandler<GetByTreeCodeQuery, ErrorOr<TreeResult>>
    {

        private readonly ITreeRepository treeRepository;

        public GetByTreeCodeHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<TreeResult>> Handle(GetByTreeCodeQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tree = treeRepository.GetTreeByTreeCode(request.TreeCode);

            if (tree == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }

            return new TreeResult(tree);
        }
    }
}
