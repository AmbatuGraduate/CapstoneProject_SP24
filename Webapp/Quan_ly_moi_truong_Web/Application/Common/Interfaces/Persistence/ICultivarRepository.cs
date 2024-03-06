using Domain.Entities.Cultivar;

namespace Application.Common.Interfaces.Persistence
{
    public interface ICultivarRepository
    {
        List<Cultivars> GetAllCultivars();

        Cultivars GetCultivarById(Guid id);

        Cultivars CreateCultivar(Cultivars cultivars);

        Cultivars UpdateCultivar(Cultivars cultivars);
    }
}