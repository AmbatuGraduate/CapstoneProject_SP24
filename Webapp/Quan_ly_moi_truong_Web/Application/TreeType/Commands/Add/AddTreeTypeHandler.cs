using Application.Common.Interfaces.Persistence;
using Application.TreeType.Common;
using Domain.Common.Errors;
using Domain.Entities.TreeType;
using ErrorOr;
using MediatR;

namespace Application.TreeType.Commands.Add
{
    public class AddTreeTypeHandler :
        IRequestHandler<AddTreeTypeCommand, ErrorOr<TreeTypeResult>>
    {
        private readonly ITreeTypeRepository treeTypeRepository;

        public AddTreeTypeHandler(ITreeTypeRepository treeTypeRepository)
        {
            this.treeTypeRepository = treeTypeRepository;
        }

        public async Task<ErrorOr<TreeTypeResult>> Handle(AddTreeTypeCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if(treeTypeRepository.GetAllTreeTypes().FirstOrDefault(type => type.TreeTypeName == request.TreeTypeName) is not null)
            {
                return Errors.DuplicateTreeType.duplicationTreeType;
            }


            var treeType = new TreeTypes
            {
                TreeTypeId = Guid.NewGuid(),
                TreeTypeName = request.TreeTypeName,
                CreateBy = request.CreateBy,
                UpdateDate = DateTime.Now,
                UpdateBy = request.UpdateBy,
            };

            var result = new TreeTypeResult(treeTypeRepository.CreateTreeType(treeType));

            return result;
        }
    }
}