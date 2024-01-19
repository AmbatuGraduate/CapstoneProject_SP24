using Domain.Entities.Cultivar;
using Domain.Entities.TreeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
