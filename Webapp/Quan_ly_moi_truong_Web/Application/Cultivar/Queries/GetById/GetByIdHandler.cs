using Application.Common.Interfaces.Persistence;
using Application.Cultivar.Common;
using Application.Cultivar.Queries.List;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Queries.GetById
{
    public class GetByIdHandler :
        IRequestHandler<GetByIdQuery, ErrorOr<CultivarResult>>
    {
        private readonly ICultivarRepository cultivarRepository;

        public GetByIdHandler(ICultivarRepository cultivarRepository)
        {
            this.cultivarRepository = cultivarRepository;
        }


        public async Task<ErrorOr<CultivarResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var cultival = cultivarRepository.GetCultivarById(request.CultivarId);
            if (cultival is null) return Errors.GetCultivarById.getCultivarById;


            return new CultivarResult(cultival);
        }
    }
}
