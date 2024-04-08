using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Tree.Queries.ListCut
{
    public class ListTreeCutHandler :
        IRequestHandler<ListTreeCutQuery, ErrorOr<List<TreeResult>>>
    {

        private readonly ITreeRepository treeRepository;
        private readonly ITreeTypeRepository treeTypeRepository;

        public ListTreeCutHandler(ITreeRepository treeRepository, ITreeTypeRepository treeTypeRepository)
        {
            this.treeRepository = treeRepository;
            this.treeTypeRepository = treeTypeRepository;
        }

        public async Task<ErrorOr<List<TreeResult>>> Handle(ListTreeCutQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var results = new List<TreeResult>();

            // Group all tree that have same address
            var treeByAddresses = treeRepository.GetAllTrees()
                .Where(x => x.isCut == false)
                .GroupBy(tree => Regex.Replace(request.Address, @"^\d+\s+", string.Empty).Split(",")[0])
                .ToDictionary(
                    group => group.Key,
                    group => group.Where(tree => Regex.Replace(tree.TreeLocation, @"^\d+\s+", string.Empty).Split(",")[0].ToLower() == group.Key.Split(",")[0].ToLower()));

            foreach ( var trees in treeByAddresses)
            {
                foreach(var tree in trees.Value)
                {
                    var treeType = treeTypeRepository.GetTreeTypeById(tree.TreeTypeId).TreeTypeName;
                    var result = new TreeResult(tree.TreeCode, tree.TreeLocation, treeType, tree.BodyDiameter, tree.LeafLength, tree.CutTime, tree.isCut);
                    results.Add(result);
                }
            }

            return results;
        }
    }
}
