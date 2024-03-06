using Application.Common.Interfaces.Persistence;
using Domain.Entities.Cultivar;

namespace Infrastructure.Persistence.Repositories
{
    public class CultivarRepository : ICultivarRepository
    {
        private readonly WebDbContext _treeDbContext;

        public CultivarRepository(WebDbContext treeDbContext)
        {
            _treeDbContext = treeDbContext;
        }

        public Cultivars CreateCultivar(Cultivars cultivars)
        {
            _treeDbContext.Cultivars.Add(cultivars);
            _treeDbContext.SaveChanges();
            return cultivars;
        }

        public List<Cultivars> GetAllCultivars()
        {
            return _treeDbContext.Cultivars.ToList();
        }

        public Cultivars GetCultivarById(Guid id)
        {
            return _treeDbContext.Cultivars.FirstOrDefault(cultivars => cultivars.CultivarId == id);
        }

        public Cultivars UpdateCultivar(Cultivars cultivars)
        {
            _treeDbContext.Cultivars.Update(cultivars);
            _treeDbContext.SaveChanges();
            return cultivars;
        }
    }
}