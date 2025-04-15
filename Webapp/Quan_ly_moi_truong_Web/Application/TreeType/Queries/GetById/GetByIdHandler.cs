using Application.Common.Interfaces.Persistence;
using Application.TreeType.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.TreeType.Queries.GetById
{
    public class GetByIdHandler :
        IRequestHandler<GetByIdQuery, ErrorOr<TreeTypeResult>>
    {
        private readonly ITreeTypeRepository treeTypeRepository;

        public GetByIdHandler(ITreeTypeRepository treeTypeRepository)
        {
            this.treeTypeRepository = treeTypeRepository;
        }

        public async Task<ErrorOr<TreeTypeResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var treeType = treeTypeRepository.GetTreeTypeById(request.TreeTypeId);
            if (treeType == null)
            {
                return Errors.GetTreeTypeById.getTreeTypeFail;
            }

            return new TreeTypeResult(treeType);
        }
    }
}