using Application.Common.Interfaces.Persistence.Schedules;

namespace Infrastructure.Persistence.Repositories
{
    public class ScheduleTreeTrimRepository : IScheduleTreeTrimRepository
    {
        private readonly WebDbContext _dbContext;

        // constructor
        public ScheduleTreeTrimRepository(WebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //// Create
        //public ScheduleTreeTrims CreateScheduleTreeTrim(ScheduleTreeTrims schedule)
        //{
        //    _dbContext.Add(schedule);
        //    _dbContext.SaveChanges();
        //    return schedule;
        //}

        //// return list of all schedule tree trims
        //public List<ScheduleTreeTrims> GetAllScheduleTreeTrims()
        //{
        //    return _dbContext.ScheduleTreeTrims.ToList();
        //}

        //// get by id
        //public ScheduleTreeTrims GetScheduleTreeTrimById(Guid id)
        //{
        //    return _dbContext.ScheduleTreeTrims.FirstOrDefault(s => s.ScheduleTreeTrimId == id);
        //}

        //// update
        //public ScheduleTreeTrims UpdateScheduleTreeTrim(ScheduleTreeTrims schedule)
        //{
        //    _dbContext.ScheduleTreeTrims.Update(schedule);
        //    _dbContext.SaveChanges();
        //    return schedule;
        //}

        //// get street associated with schedule
        //public List<Streets> GetStreetsOfSchedule(Guid scheduleId)
        //{
        //    return _dbContext.ScheduleTreeTrim_street_maps
        //        .Where(map => map.ScheduleTreeTrimId == scheduleId)
        //        .Select(map => map.Street)
        //        .ToList();
        //}
    }
}