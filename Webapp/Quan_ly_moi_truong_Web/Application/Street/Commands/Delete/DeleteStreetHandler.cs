using Application.Common.Interfaces.Persistence;
using Application.Street.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Street.Commands.Delete
{
    public class DeleteStreetHandler : IRequestHandler<DeleteStreetCommand, ErrorOr<StreetResult>>
    {
        private readonly IStreetRepository streetRepository;

        public DeleteStreetHandler(IStreetRepository streetRepository)
        {
            this.streetRepository = streetRepository;
        }

        public async Task<ErrorOr<StreetResult>> Handle(DeleteStreetCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // convert request.StreetId to Guid  
            Guid formatted_streetId = request.StreetId;

            if (streetRepository.GetStreetById(formatted_streetId) == null)
                return Errors.GetStreetById.getStreetFail;

            var street = streetRepository.GetStreetById(formatted_streetId);
            streetRepository.DeleteStreet(street);

            return new StreetResult(street);
        }
    }
}
