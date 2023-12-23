using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Common.Errors;
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

            if (treeRepository.GetTreeById(request.id) == null)
                return Errors.GetTreeById.getTreeFail;

            treeRepository.DeleteTree(request.id);

            return new TreeResult(new Domain.Entities.Trees());
        }
    }
}
