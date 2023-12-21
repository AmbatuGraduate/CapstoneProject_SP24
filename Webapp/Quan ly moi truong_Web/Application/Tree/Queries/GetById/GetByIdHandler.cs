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

namespace Application.Tree.Queries.GetById
{
    public class GetByIdHandler :
        IRequestHandler<GetByIdQuery, ErrorOr<TreeResult>>
    {

        private readonly ITreeRepository treeRepository;

        public GetByIdHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<TreeResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tree = treeRepository.GetTreeById(request.id);

            if(tree == null)
            {
                return Errors.GetTreeById.getTreeFail;
            }

            return new TreeResult(tree);
        }
    }
}
