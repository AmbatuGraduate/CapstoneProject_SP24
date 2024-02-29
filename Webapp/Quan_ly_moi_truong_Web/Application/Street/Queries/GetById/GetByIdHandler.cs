using Application.Common.Interfaces.Persistence;
using Application.Street.Common;
using ErrorOr;
using Domain.Common.Errors;
using MediatR;

namespace Application.Street.Queries.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, ErrorOr<StreetResult>>
    {
        private readonly IStreetRepository streetRepository;

        public GetByIdHandler(IStreetRepository streetRepository)
        {
            this.streetRepository = streetRepository;
        }

        public async Task<ErrorOr<StreetResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var street = streetRepository.GetStreetById(request.StreetId);

            if (street == null)
            {
                return Errors.GetStreetById.getStreetFail;
            }

            return new StreetResult(street);
        }
    }
}
