using Application.Common.Interfaces.Persistence;
using Application.User.Common.Add;
using Application.User.Common.List;
using Application.User.Common.UpdateUser;
using Domain.Entities.User;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
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
        
        // get users from db
        public List<Domain.Entities.User.Users> GetAll()
        {
            return webDbContext.Users.ToList();
        }

        // add user to db
        public void Add(Domain.Entities.User.Users user)
        {
            webDbContext.Add(user);
            webDbContext.SaveChanges();
        }

        // get user by phone from db
        //public Domain.Entities.User.Users? GetUserByPhone(string phoneNumber)
        //{
        //    return webDbContext.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        //}

        // update user in db
        public void Update(Domain.Entities.User.Users user)
        {
            webDbContext.Users.Attach(user);
            webDbContext.Entry<Domain.Entities.User.Users>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            webDbContext.SaveChanges();
        }

        // get user by id from db
        public Domain.Entities.User.Users GetById(string id)
        {
            return webDbContext.Users.SingleOrDefault(u => u.Id == id);
        }

        // get google users list
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

        // add google user 
        public async Task<AddGoogleUser> AddGoogleUser(AddGoogleUser user)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(user.AccessToken);
                var service = _directoryServiceFactory(credential);


                User googleUser = new User
                {
                    Name = new UserName
                    {
                        GivenName = user.Name,
                        FamilyName = user.FamilyName
                    },
                    Password = user.Password,
                    PrimaryEmail = user.Email,
                    ChangePasswordAtNextLogin = false
                };

                var request = service.Users.Insert(googleUser);
                var newUser = await request.ExecuteAsync();

                AddGoogleUser addGoogleUser = new AddGoogleUser
                {
                    Email = newUser.PrimaryEmail,
                    Name = newUser.Name.FullName,
                    FamilyName = newUser.Name.FamilyName,
                    Password = user.Password
                };

                return addGoogleUser;
            }
            catch (Exception e)
            {
                // Handle exception
                throw;
            }
        }

        // update google user
        public async Task<UpdateGoogleUser> UpdateGoogleUser(UpdateGoogleUser user)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("User token: " + user.AccessToken);

                var credential = GoogleCredential.FromAccessToken(user.AccessToken);
                var service = _directoryServiceFactory(credential);
                var user_id = await GetUserId(user.AccessToken, user.Email);

                // Retrieve the current user data
                var currentUser = await service.Users.Get(user_id).ExecuteAsync();

                if (!string.IsNullOrEmpty(user.Name))
                {
                    currentUser.Name.GivenName = user.Name;
                }
                if (!string.IsNullOrEmpty(user.FamilyName))
                {
                    currentUser.Name.FamilyName = user.FamilyName;
                }
                if (!string.IsNullOrEmpty(user.Password))
                {
                    currentUser.Password = user.Password;
                }
                if (!string.IsNullOrEmpty(user.Email))
                {
                    currentUser.PrimaryEmail = user.Email;
                }

                var request = service.Users.Update(currentUser, user_id);
                var updatedUser = await request.ExecuteAsync();

                UpdateGoogleUser addGoogleUser = new UpdateGoogleUser
                {
                    Email = updatedUser.PrimaryEmail,
                    Name = updatedUser.Name.FullName,
                    FamilyName = updatedUser.Name.FamilyName,
                    Password = updatedUser.Password
                };

                return addGoogleUser;
            }
            catch (Exception e)
            {
                // Handle exception
                throw;
            }
        }

        // get id by email
        public async Task<string> GetUserId(string accessToken, string email)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);

                var request = service.Users.Get(email);
                var user = await request.ExecuteAsync();

                return user.Id;
            }
            catch (Exception e)
            {
                // Handle exception
                return $"Failed to get user ID: {e.Message}";
            }
        }
    }
}