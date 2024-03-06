using Application.Common.Interfaces.Persistence;
using Application.Street.Common;
using ErrorOr;
using MediatR;
using Domain.Common.Errors;
using Domain.Entities.Street;

namespace Application.Street.Commands.Update
{
    public class UpdateStreetHandler : IRequestHandler<UpdateStreetCommand, ErrorOr<StreetResult>>
    {
        private readonly IStreetRepository streetRepository;

        public UpdateStreetHandler(IStreetRepository streetRepository)
        {
            this.streetRepository = streetRepository;
        }

        public async Task<ErrorOr<StreetResult>> Handle(UpdateStreetCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (streetRepository.GetStreetById(request.StreetId) == null)
            {
                return Errors.GetStreetById.getStreetFail;
            }

            var street = new Streets
            {
                StreetId = streetRepository.GetStreetById(request.StreetId).StreetId,
                StreetName = request.StreetName,
                StreetLength = request.StreetLength,
                NumberOfHouses = request.NumberOfHouses,
                StreetTypeId = request.StreetTypeId,
                ResidentialGroupId = request.ResidentialGroupId,
                CreateBy = streetRepository.GetStreetById(request.StreetId).CreateBy,
                CreateDate = streetRepository.GetStreetById(request.StreetId).CreateDate,
                UpdateDate = DateTime.Now,
                UpdateBy = request.UpdateBy,
            };

            var result = new StreetResult(streetRepository.UpdateStreet(street));

            return result;
        }
    }
}
