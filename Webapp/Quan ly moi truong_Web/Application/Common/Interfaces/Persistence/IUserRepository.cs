<<<<<<< HEAD
﻿using Domain.Entities.ScheduleTreeTrim;
using Domain.Entities.User;
=======
﻿using Domain.Entities.User;
>>>>>>> vu/feature/get-information-of-cultivar

namespace Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        List<Users> GetAll();

        Users GetById(Guid id);

        Users? GetUserByPhone(string phoneNumber);

        void Add(Users user);
        void Update(Users user);
        
        // Get all schedules for a user
        List<ScheduleTreeTrims> GetSchedulesByUserId(Guid userId);
    }
}