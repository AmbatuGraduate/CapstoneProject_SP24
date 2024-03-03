using Application.Common.Interfaces.Persistence;
using Application.Street.Common;
using ErrorOr;
using MediatR;

namespace Application.Street.Queries.List
{
    public class ListStreetHandler : IRequestHandler<ListStreetQuery, ErrorOr<List<StreetResult>>>
    {
        private readonly IStreetRepository streetRepository;

        public ListStreetHandler(IStreetRepository streetRepository)
        {
            this.streetRepository = streetRepository;
        }

        public async Task<ErrorOr<List<StreetResult>>> Handle(ListStreetQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<StreetResult> streetResults = new List<StreetResult>();
            var streets = streetRepository.GetAllStreets();

            foreach (var street in streets)
            {
                streetResults.Add(new StreetResult(street));
            }

            return streetResults;
        }
    }
}
