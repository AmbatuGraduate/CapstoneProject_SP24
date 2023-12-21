using Application.Common.Interfaces.Persistence;
using Application.Tree.Common;
using Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Commands.Add
{
    public class AddTreeHandler :
        IRequestHandler<AddTreeCommand, ErrorOr<TreeResult>>
    {

        private readonly ITreeRepository treeRepository;

        public AddTreeHandler(ITreeRepository treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public async Task<ErrorOr<TreeResult>> Handle(AddTreeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var tree = new Trees {
                Id = request.id,
                District = request.district,
                Street = request.street,
                RootType   = request.rootType,
                Type = request.type,
                BodyDiameter = request.bodyDiameter,
                LeafLength = request.leafLength,
                PlantTime = request.plantTime,
                CutTime = request.cutTime,
                IntervalCutTime = request.intervalCutTime,
                Note = request.note,
            };

            var result = new TreeResult(treeRepository.CreateTree(tree));

            return result;

        }
    }
}
