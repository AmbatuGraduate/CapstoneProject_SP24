using Application.Common.Interfaces.Persistence;
using Application.Cultivar.Common;
using Domain.Common.Errors;
using Domain.Entities.Cultivar;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Commands.Add
{
    public class AddCultivarHandler :
        IRequestHandler<AddCultivarCommand, ErrorOr<CultivarResult>>
    {

        private readonly ICultivarRepository cultivarRepository;

        public AddCultivarHandler(ICultivarRepository cultivarRepository)
        {
            this.cultivarRepository = cultivarRepository;
        }

        public async Task<ErrorOr<CultivarResult>> Handle(AddCultivarCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if(cultivarRepository.GetAllCultivars().FirstOrDefault(x => x.CultivarName == request.CultivarName) is not null)
            {
                return Errors.DuplicateCultivar.duplicateCultivar; 
            }

            var cultivar = new Cultivars
            {
                CultivarId = Guid.NewGuid(),
                CultivarName = request.CultivarName,
                TreeTypeId = request.TreeTypeId,
                CreateBy = request.CreateBy,
                UpdateBy = request.UpdateBy,
                UpdateDate = DateTime.Now,
            };

            var result = new CultivarResult(cultivarRepository.CreateCultivar(cultivar));
            return result;
        }
    }
}
