using Application.Common.Interfaces.Persistence;
using Application.User.Common.Add;
using Application.User.Common.Group;
using Application.User.Common.List;
using Application.User.Common.UpdateUser;
using Domain.Entities.Deparment;
using Domain.Entities.User;
using Domain.Enums;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

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

        public List<Departments> GetAllGroups()
        {
            return webDbContext.Departments.ToList();
        }

        // add user to db
        public void Add(Domain.Entities.User.Users user)
        {
            webDbContext.Users.Add(user);
            webDbContext.SaveChanges();
        }

        public void Delete(string userEmail)
        {
            webDbContext.Users.Remove(GetByEmail(userEmail));
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

        public Domain.Entities.User.Users GetByEmail(string email)
        {
            return webDbContext.Users.SingleOrDefault(u => u.Email == email);
        }

        public async Task<GoogleUser> GetGoogleUserByEmail(string accessToken, string userEmail)
        {
            GoogleUser userResult = null;
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);

                var request = service.Users.Get(userEmail);
                var user = await request.ExecuteAsync();
                userResult = new GoogleUser
                {
                    Id = user.Id,
                    Name = user.Name.FullName,
                    Email = user.PrimaryEmail,
                    Picture = user.ThumbnailPhotoUrl
                };
                return userResult;
            }
            catch (Exception ex)
            {
                throw;
            }
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

                var userDB = GetById(newUser.Id);

                if (newUser != null && userDB == null)
                {
                    Domain.Entities.User.Users dbUser = new Domain.Entities.User.Users
                    {
                        Id = newUser.Id,
                        UserCode = ConvertToUserCodeString(UserCode.EM_NHS_QD),
                        Email = newUser.PrimaryEmail,
                        RoleId = new Guid(ConvertToUserRoleId(UserRole.Employee)),
                        DepartmentId = "03bj1y382j5l78b"
                    };
                    Add(dbUser);
                }

                AddGoogleUser addGoogleUser = new AddGoogleUser
                {
                    Email = newUser.PrimaryEmail,
                    Name = newUser.Name.GivenName + newUser.Name.FamilyName,
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

        public string ConvertToUserCodeString(UserCode userCode)
        {
            return userCode switch
            {
                UserCode.EM_NHS_QD => "EM_NHS_QD"
            };
        }

        public string ConvertToUserRoleId(UserRole role)
        {
            return role switch
            {
                UserRole.Employee => "8977EF77-E554-4EF3-8353-3E01161F84D0",
                UserRole.Manager => "ABCCDE85-C7DC-4F78-9E4E-B1B3E7ABEE84",
                UserRole.Leader => "CACD4B3A-8AFE-43E9-B757-F57F5C61F8D8"
            };
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

                var userDb = GetById(user_id);

                if (updatedUser != null && userDb != null)
                {
                    userDb.Email = updatedUser.PrimaryEmail;
                    Update(userDb);
                }

            UpdateGoogleUser addGoogleUser = new UpdateGoogleUser
                {
                    Email = updatedUser.PrimaryEmail,
                    Name = updatedUser.Name.GivenName + updatedUser.Name.FamilyName,
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

        public async Task<bool> DeleteGoogleUser(string accessToken, string userEmail)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);

                var request = service.Users.Delete(userEmail);
                var user = await request.ExecuteAsync();

                Delete(userEmail);
                return true;
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

        public async Task<GroupResult> GetGoogleGroupByEmail(string accessToken, string groupEmail)
        {
            try
            {  
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);

                // Retrieve the group
                var request = service.Groups.Get(groupEmail);
                var group = await request.ExecuteAsync();

                var groupDto = new GroupResult
                {
                    Id = group.Id,
                    Email = group.Email,
                    Name = group.Name,
                    Description = group.Description,
                    AdminCreated = (bool)group.AdminCreated,
                    DirectMembersCount = (long)group.DirectMembersCount
                };

                return groupDto;
            }
            catch (Exception e)
            {
                // Handle exception
                throw;
            }
        }

        public async Task<List<GroupResult>> GetAllGoogleGroupByUserEmail(string accessToken, string userEmail)
        {
            var groupResult = new List<GroupResult>();  
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);
                var dbGroups = GetAllGroups();

                foreach(var group in dbGroups)
                {
                    try
                    {
                        var memberRequest = service.Members.Get(group.DepartmentEmail, userEmail);
                        var member = memberRequest.Execute();
                        if(member != null)
                        {
                            groupResult.Add(new GroupResult
                            {
                                Id = member.Id,
                                Email = member.Email,
                                Name = group.DepartmentName,
                                Description = group.Description,
                                AdminCreated = group.AdminCreated,
                                DirectMembersCount= (long)group.DirectMembersCount
                            });
                        }
                    }
                    catch(Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine($"An error occurred: {e.Message}");
                    }
                }
                return groupResult;
            }
            catch (Exception e)
            {
                // Handle exception
                throw;
            }
        }
    }
}