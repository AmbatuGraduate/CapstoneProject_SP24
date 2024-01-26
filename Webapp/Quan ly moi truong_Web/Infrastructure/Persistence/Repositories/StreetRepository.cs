using Application.Common.Interfaces.Persistence;
using Domain.Entities.Street;

namespace Infrastructure.Persistence.Repositories
{
    public class StreetRepository : IStreetRepository
    {
        private readonly WebDbContext _streetDbContext;

        // constructor dependency injection
        public StreetRepository(WebDbContext streetDbContext)
        {
            _streetDbContext = streetDbContext;
        }

        // create
        public Streets CreateStreet(Streets street)
        {
            _streetDbContext.Streets.Add(street);
            _streetDbContext.SaveChanges();

            return street;
        }

        // get all
        public List<Streets> GetAllStreets()
        {
            return _streetDbContext.Streets.ToList();
        }

        // get id
        public Streets GetStreetById(Guid id)
        {
            return _streetDbContext.Streets.FirstOrDefault(x => x.StreetId == id);
        }

        // update
        public Streets UpdateStreet(Streets street)
        {
            _streetDbContext.Streets.Update(street);
            _streetDbContext.SaveChanges();
            return street;
        }

        // delete 
        public void DeleteStreet(Streets street)
        {
            _streetDbContext.Streets.Remove(street);
            _streetDbContext.SaveChanges();
        }

    }
}
