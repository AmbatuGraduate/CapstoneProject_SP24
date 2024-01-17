using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Common.Errors;
using Domain.Entities.Tree;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.Delete
{
    public class DeleteTreeHandler :
        IRequestHandler<DeleteTreeCommand, ErrorOr<TreeResult>>
    {

        private readonly ITreeRepository treeRepository;

        public DeleteTreeHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<TreeResult>> Handle(DeleteTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (treeRepository.GetTreeByTreeCode(request.TreeCode) == null)
                return Errors.GetTreeById.getTreeFail;

            var tree = treeRepository.GetTreeByTreeCode(request.TreeCode);
            tree.isExist = false;
            treeRepository.UpdateTree(tree);

            return new TreeResult(tree);
        }
    }
}
