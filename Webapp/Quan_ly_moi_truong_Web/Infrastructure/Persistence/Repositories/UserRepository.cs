using Application.Common.Interfaces.Persistence;
using Application.User.Common;
using Domain.Entities.User;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Auth.OAuth2;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebDbContext webDbContext;
        private readonly Func<GoogleCredential, DirectoryService> _directoryServiceFactory;

        public UserRepository(WebDbContext webDbContext, Func<GoogleCredential, DirectoryService> directoryServiceFactory)
        {
            this.webDbContext = webDbContext;
            _directoryServiceFactory = directoryServiceFactory;
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

        public async Task<List<GoogleUser>> GetGoogleUsers(string accessToken)
        {
            List<GoogleUser> users = new List<GoogleUser>();
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);
                var request = service.Users.List();
                request.Domain = "vesinhdanang.xyz";
                var result = await request.ExecuteAsync();
                if (result.UsersValue != null)
                {
                    foreach (var user in result.UsersValue)
                    {
                        users.Add(new GoogleUser
                        {
                            Id = user.Id,
                            Email = user.PrimaryEmail,
                            Name = user.Name.FullName,
                            Picture = user.ThumbnailPhotoUrl
                        });
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {e.Message}");
                System.Diagnostics.Debug.WriteLine($"Exception: {e}");
            }

            return users;
        }

    }
}