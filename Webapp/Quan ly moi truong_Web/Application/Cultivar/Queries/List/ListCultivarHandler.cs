using Application.Common.Interfaces.Persistence;
using Application.Cultivar.Common;
using Application.Tree.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Queries.List
{
    public class ListCultivarHandler :
        IRequestHandler<ListCultivarQuery, ErrorOr<List<CultivarResult>>>
    {

        private readonly ICultivarRepository cultivarRepository;

        public ListCultivarHandler(ICultivarRepository cultivarRepository)
        {
            this.cultivarRepository = cultivarRepository;
        }


        public async Task<ErrorOr<List<CultivarResult>>> Handle(ListCultivarQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<CultivarResult> cultivalResults = new List<CultivarResult>();
            var cultivars = cultivarRepository.GetAllCultivars();

            foreach (var cultivar in cultivars)
            {
                cultivalResults.Add(new CultivarResult(cultivar));
            }

            return cultivalResults;
        }
    }
}
