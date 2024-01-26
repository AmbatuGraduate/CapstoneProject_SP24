using Application.Common.Interfaces.Persistence;
using Domain.Entities.ScheduleTreeTrim;
using Domain.Entities.User;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebDbContext webDbContext;

        public UserRepository(WebDbContext webDbContext)
        {
            this.webDbContext = webDbContext;
        }

        /// <summary>
        /// Using to get the list of user from database
        /// </summary>
        /// <returns>list user</returns>
        public List<Users> GetAll()
        {
            return webDbContext.Users.ToList();
        }

        /// <summary>
        /// Add new user to databse
        /// </summary>
        /// <param name="user">The new information about user want to add</param>
        public void Add(Users user)
        {
            webDbContext.Add(user);
            webDbContext.SaveChanges();
        }

        /// <summary>
        /// Get User from database
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>Return user if found, if not return null</returns>
        public Users? GetUserByPhone(string phoneNumber)
        {
            return webDbContext.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        /// <summary>
        /// Update the information of user
        /// </summary>
        /// <param name="user">The infomation of user has change</param>
        public void Update(Users user)
        {
            webDbContext.Users.Attach(user);
            webDbContext.Entry<Users>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            webDbContext.SaveChanges();
        }

        public Users GetById(Guid id)
        {
            return webDbContext.Users.SingleOrDefault(u => u.Id == id);
        }

        // get all schedules for a user
        public List<ScheduleTreeTrims> GetSchedulesByUserId(Guid userId)
        {
            return webDbContext.User_scheduleTreeTrim_maps
                .Where(map => map.UserId == userId)
                .Select(map => map.ScheduleTreeTrims)
                .ToList();
        }
    }
}