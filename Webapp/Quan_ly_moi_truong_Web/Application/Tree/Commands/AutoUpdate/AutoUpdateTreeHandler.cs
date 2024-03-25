using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Entities.Tree;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.AutoUpdate
{
    public class AutoUpdateTreeHandler
            : IRequestHandler<AutoUpdateTreeCommand, ErrorOr<List<AddTreeResult>>>
    {
        private readonly ITreeRepository _treeRepository;

        public AutoUpdateTreeHandler(ITreeRepository treeRepository)
        {
            _treeRepository = treeRepository;
        }

        public async Task<ErrorOr<List<AddTreeResult>>> Handle(AutoUpdateTreeCommand request, CancellationToken cancellationToken)
        {

            var list = _treeRepository.GetAllTrees();
            var result = new List<AddTreeResult>();
            // check and auto update status isCut of tree
            foreach (var tree in list)
            {
                if (tree.CutTime.CompareTo(DateTime.Now) <=0 && tree.isCut)
                {
                    tree.isCut = false;
                    result.Add(new AddTreeResult(_treeRepository.UpdateTree(tree).TreeCode));
                }
            }

            return result;
        }
    }
}
