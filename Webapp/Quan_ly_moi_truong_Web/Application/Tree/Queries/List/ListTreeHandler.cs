using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Queries.List
{
    public class ListTreeHandler :
        IRequestHandler<ListTreeQuery, ErrorOr<List<TreeResult>>>
    {

        private readonly ITreeRepository treeRepository;

        public ListTreeHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<List<TreeResult>>> Handle(ListTreeQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<TreeResult> treeResults = new List<TreeResult>();
            var trees = treeRepository.GetAllTrees();

            foreach(var tree in trees)
            {
                treeResults.Add(new TreeResult(tree));
            }

            return treeResults;
        }
    }
}
