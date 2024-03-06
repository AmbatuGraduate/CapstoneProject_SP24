using Domain.Entities.Street;

namespace Application.Common.Interfaces.Persistence
{
    public interface IStreetRepository
    {
        List<Streets> GetAllStreets();

        Streets GetStreetById(Guid id);

        Streets CreateStreet(Streets street);

        Streets UpdateStreet(Streets street);

        void DeleteStreet(Streets street);
    }
}