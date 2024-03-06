using Application.Common.Interfaces.Persistence;
using Application.Street.Common;
using Domain.Entities.Street;
using ErrorOr;
using MediatR;


namespace Application.Street.Commands.Add
{
    public class AddStreetHandler : IRequestHandler<AddStreetCommand, ErrorOr<StreetResult>>
    {
        private readonly IStreetRepository streetRepository;

        public AddStreetHandler(IStreetRepository streetRepository)
        {
            this.streetRepository = streetRepository;
        }

        public async Task<ErrorOr<StreetResult>> Handle(AddStreetCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var street = new Streets
            {
                
                StreetId = Guid.NewGuid(),
                StreetName = request.StreetName,
                StreetLength = request.StreetLength,
                NumberOfHouses = request.NumberOfHouses,
                StreetTypeId = request.StreetTypeId,
                ResidentialGroupId = request.ResidentialGroupId,
                CreateBy = request.CreateBy,
                UpdateDate = DateTime.Now,
                UpdateBy =  request.UpdateBy,
            };

            var result = new StreetResult(streetRepository.CreateStreet(street));

            return result;
        }
    }
}
