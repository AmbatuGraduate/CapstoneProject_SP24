﻿using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Queries.List
{
    public class ListTreeHandler : IRequestHandler<ListTreeQuery, ErrorOr<List<TreeResult>>>
    {
        private readonly ITreeRepository treeRepository;
        private readonly ITreeTypeRepository treeTypeRepository;

        public ListTreeHandler(ITreeRepository treeRepository, ITreeTypeRepository treeTypeRepository)
        {
            this.treeRepository = treeRepository;
            this.treeTypeRepository = treeTypeRepository;
        }

        public async Task<ErrorOr<List<TreeResult>>> Handle(ListTreeQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<TreeResult> treeResults = new List<TreeResult>();
            var trees = treeRepository.GetAllTrees();

            foreach (var tree in trees)
            {
                var treeType = treeTypeRepository.GetTreeTypeById(tree.TreeTypeId).TreeTypeName;
                var result = new TreeResult(tree.TreeCode, tree.TreeLocation, treeType, tree.BodyDiameter, tree.LeafLength, tree.CutTime, tree.isCut);
                treeResults.Add(result);
            }

            return treeResults;
        }
    }
}