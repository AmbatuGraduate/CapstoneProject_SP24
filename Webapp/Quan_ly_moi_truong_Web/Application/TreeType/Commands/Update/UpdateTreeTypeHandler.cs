using Application.Common.Interfaces.Persistence;
using Application.TreeType.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.TreeType.Commands.Update
{
    public class UpdateTreeTypeHandler :
        IRequestHandler<UpdateTreeTypeCommand, ErrorOr<TreeTypeResult>>
    {
        private readonly ITreeTypeRepository treeTypeRepository;

        public UpdateTreeTypeHandler(ITreeTypeRepository treeTypeRepository)
        {
            this.treeTypeRepository = treeTypeRepository;
        }

        public async Task<ErrorOr<TreeTypeResult>> Handle(UpdateTreeTypeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var treeType = treeTypeRepository.GetTreeTypeById(request.TreeTypeId);

            if (treeType is null)
                return Errors.GetTreeTypeById.getTreeTypeFail;

            treeType.TreeTypeName = request.TreeTypeName;
            treeType.UpdateDate = DateTime.Now;
            treeType.UpdateBy = request.UpdateBy;

            if (treeTypeRepository.GetAllTreeTypes().FirstOrDefault(type => type.TreeTypeName == treeType.TreeTypeName) is not null)
                return Errors.DuplicateTreeType.duplicationTreeType;

            var result = new TreeTypeResult(treeTypeRepository.UpdateTreeType(treeType));

            return result;
        }
    }
}