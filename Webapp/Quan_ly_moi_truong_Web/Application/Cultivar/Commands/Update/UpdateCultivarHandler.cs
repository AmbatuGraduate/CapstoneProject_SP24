using Application.Common.Interfaces.Persistence;
using Application.Cultivar.Common;
using Domain.Entities.Cultivar;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Commands.Update
{
    public class UpdateCultivarHandler :
        IRequestHandler<UpdateCultivarCommand, ErrorOr<CultivarResult>>
    {

        private readonly ICultivarRepository cultivarRepository;

        public UpdateCultivarHandler(ICultivarRepository cultivarRepository)
        {
            this.cultivarRepository = cultivarRepository;
        }

        public async Task<ErrorOr<CultivarResult>> Handle(UpdateCultivarCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (cultivarRepository.GetCultivarById(request.CultivarId) is null)
            {
                return Errors.GetCultivarById.getCultivarById;
            }

            if (cultivarRepository.GetAllCultivars().FirstOrDefault(x => x.CultivarName == request.CultivarName) is not null)
            {
                return Errors.DuplicateCultivar.duplicateCultivar;
            }

            var cultivar = new Cultivars
            {
                CultivarId = request.CultivarId,
                TreeTypeId = request.TreeTypeId,
                CultivarName = request.CultivarName,
                CreateDate = cultivarRepository.GetCultivarById(request.CultivarId).CreateDate,
                CreateBy = cultivarRepository.GetCultivarById(request.CultivarId).CreateBy,
                UpdateBy = request.UpdateBy,
                UpdateDate = DateTime.Now,
            };

            var result = new CultivarResult(cultivarRepository.UpdateCultivar(cultivar));
            return result;
        }
    }
}
