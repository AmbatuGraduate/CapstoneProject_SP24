using Domain.Entities.ScheduleTreeTrim;

// Interface de implement CRUD cho ScheduleTreeTrims
namespace Application.Common.Interfaces.Persistence.Schedules
{
    public interface IScheduleTreeTrimRepository
    {
        List<ScheduleTreeTrims> GetAllScheduleTreeTrims();
        ScheduleTreeTrims GetScheduleTreeTrimById(Guid id);
        ScheduleTreeTrims CreateScheduleTreeTrim(ScheduleTreeTrims schedule);
        ScheduleTreeTrims UpdateScheduleTreeTrim(ScheduleTreeTrims schedule);
    }
}
