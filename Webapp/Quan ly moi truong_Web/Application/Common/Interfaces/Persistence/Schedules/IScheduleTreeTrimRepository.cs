using Domain.Entities.ScheduleTreeTrim;
using Domain.Entities.Street;

// Interface de implement CRUD cho ScheduleTreeTrims
namespace Application.Common.Interfaces.Persistence.Schedules
{
    public interface IScheduleTreeTrimRepository
    {
        List<ScheduleTreeTrims> GetAllScheduleTreeTrims();
        ScheduleTreeTrims GetScheduleTreeTrimById(Guid id);
        ScheduleTreeTrims CreateScheduleTreeTrim(ScheduleTreeTrims schedule);
        ScheduleTreeTrims UpdateScheduleTreeTrim(ScheduleTreeTrims schedule);

        // get street associated with schedule
        List<Streets> GetStreetsOfSchedule(Guid scheduleId);
    }
}
