using Application.Common.Interfaces.Persistence;
using Application.TreeType.Common;
using ErrorOr;
using MediatR;

namespace Application.TreeType.Queries.List
{
    public class ListTreeTypeHandler
        : IRequestHandler<ListTreeTypeQuery, ErrorOr<List<TreeTypeResult>>>
    {
        private readonly ITreeTypeRepository treeTypeRepository;

        public ListTreeTypeHandler(ITreeTypeRepository treeTypeRepository)
        {
            this.treeTypeRepository = treeTypeRepository;
        }

        public async Task<ErrorOr<List<TreeTypeResult>>> Handle(ListTreeTypeQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var treeTypeList = treeTypeRepository.GetAllTreeTypes();
            var results = new List<TreeTypeResult>();

            foreach (var result in treeTypeList)
            {
                results.Add(new TreeTypeResult(result));
            }

            return results;
        }
    }
}