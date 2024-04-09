using Application.Common.Interfaces.Persistence;
using Application.User.Common.Add;
using Application.User.Common.List;
using Application.User.Common.UpdateUser;
using Domain.Common.Constant;
using Domain.Entities.Deparment;
using Domain.Enums;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Auth.OAuth2;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

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

        public async Task<string> GetGoogleUserImage(string accessToken, string userEmail)
        {
            string? photoUrl = null;
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);

                var photoRequest = service.Users.Photos.Get(userEmail);
                var photoResult = await photoRequest.ExecuteAsync();
                if (photoResult?.PhotoData != null)
                {
                    // convert  URL-safe base64 string to standard base64 string
                    string base64PhotoData = photoResult.PhotoData.Replace('-', '+').Replace('_', '/');
                    // create a data url
                    photoUrl = $"data:{photoResult.MimeType};base64,{base64PhotoData}";
                }
                return photoUrl;
            }
            catch (Exception ex)
            {
                // photo not found => null
                return String.Empty;
            }
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
                var userDb = GetByEmail(user.PrimaryEmail);

                string? photoUrl = null;
                try
                {
                    var photoRequest = service.Users.Photos.Get(user.PrimaryEmail);
                    var photoResult = await photoRequest.ExecuteAsync();
                    if (photoResult?.PhotoData != null)
                    {
                        // convert  URL-safe base64 string to standard base64 string
                        string base64PhotoData = photoResult.PhotoData.Replace('-', '+').Replace('_', '/');
                        // create a data url
                        photoUrl = $"data:{photoResult.MimeType};base64,{base64PhotoData}";
                    }
                }
                catch (Exception ex)
                {
                    // photo not found => null
                }

                userResult = new GoogleUser
                {
                    Id = user.Id,
                    Name = user.Name.FullName,
                    Email = user.PrimaryEmail,
                    Picture = photoUrl, // use the photoUrl from the request
                    Department = GetDepartmentNameById(userDb.DepartmentId),
                    PhoneNumber = user.Phones != null ?  user.Phones[0].Value : string.Empty,
                    Role = GetRoleNameById(userDb.RoleId.ToString()),
                    BirthDate = user.Relations != null ? DateTime.ParseExact(user.Relations[0].Value, "dd/MM/yyyy", CultureInfo.InvariantCulture) : new DateTime(),
                    Address = user.Addresses != null ? user.Addresses[0].Locality : string.Empty
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
                        // get user in db by email
                        var userDb = GetByEmail(user.PrimaryEmail);

                        // get user's photo
                        string? photoUrl = null;
                        try
                        {
                            var photoRequest = service.Users.Photos.Get(user.PrimaryEmail);
                            var photoResult = await photoRequest.ExecuteAsync();
                            if (photoResult?.PhotoData != null)
                            {
                                // convert  URL-safe base64 string to standard base64 string
                                string base64PhotoData = photoResult.PhotoData.Replace('-', '+').Replace('_', '/');
                                // create a data url
                                photoUrl = $"data:{photoResult.MimeType};base64,{base64PhotoData}";
                            }
                        }
                        catch (Exception ex)
                        {
                            // photo not found => null
                        }

                        users.Add(new GoogleUser
                        {
                            Id = user.Id,
                            Email = user.PrimaryEmail,
                            Name = user.Name.FullName,
                            Picture = photoUrl, // use the photoUrl from the request
                            Department = GetDepartmentNameById(userDb.DepartmentId),
                            PhoneNumber = user.Phones != null ? user.Phones[0].Value : string.Empty,
                            Role = GetRoleNameById(userDb.RoleId.ToString()),
                            BirthDate = user.Relations != null ? DateTime.ParseExact(user.Relations[0].Value, "dd/MM/yyyy", CultureInfo.InvariantCulture) : new DateTime(),
                            Address = user.Addresses != null ? user.Addresses[0].Locality : string.Empty
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
                    ChangePasswordAtNextLogin = false,
                    Phones = new List<UserPhone>
                    {
                        new UserPhone { Value = user.PhoneNumber}
                    },
                    Addresses = new List<UserAddress>
                    {
                        new UserAddress { Locality = user.Address}
                    },
                    Relations = new List<UserRelation>
                    {
                        new UserRelation { Value = user.BirthDate.ToString()}
                    }
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
                        DepartmentId = !user.DepartmentEmail.IsNullOrEmpty() && GetGroupByEmail(user.DepartmentEmail) != null ? 
                            GetGroupByEmail(user.DepartmentEmail).DepartmentId : 
                            DepartmentConstant.DefaultDepartmentId 
                    };
                    Add(dbUser);
                }

                AddGoogleUser addGoogleUser = new AddGoogleUser
                {
                    Email = newUser.PrimaryEmail,
                    Name = newUser.Name.GivenName + newUser.Name.FamilyName,
                    FamilyName = newUser.Name.FamilyName,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    BirthDate = user.BirthDate,
                    DepartmentEmail = user.DepartmentEmail,
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
                UserRole.Admin => "CACD4B3A-8AFE-43E9-B757-F57F5C61F8D8"
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
                if (!string.IsNullOrEmpty(user.PhoneNumber))
                {
                    currentUser.Phones = new List<UserPhone>
                    {
                        new UserPhone { Value = user.PhoneNumber}
                    };
                }
                if (!string.IsNullOrEmpty(user.Address))
                {
                    currentUser.Addresses =  new List<UserAddress>
                    {
                        new UserAddress { Locality = user.Address}
                    };
                }
                if (!string.IsNullOrEmpty(user.BirthDate.ToString()))
                {
                    currentUser.Relations = new List<UserRelation>
                    {
                        new UserRelation { Value = user.BirthDate.ToString()}
                    };
                }

                var request = service.Users.Update(currentUser, user_id);
                var updatedUser = await request.ExecuteAsync();
                var cser = await service.Users.Get(user_id).ExecuteAsync();
                var userDb = GetById(user_id);

                if (updatedUser != null && userDb != null)
                {
                    userDb.Email = updatedUser.PrimaryEmail;
                    userDb.DepartmentId = !user.DepartmentEmail.IsNullOrEmpty() && GetGroupByEmail(user.DepartmentEmail) != null ?
                            GetGroupByEmail(user.DepartmentEmail).DepartmentId :
                            DepartmentConstant.DefaultDepartmentId;
                    Update(userDb);
                }

                UpdateGoogleUser updateGoogleUser = new UpdateGoogleUser
                {
                    Email = updatedUser.PrimaryEmail,
                    Name = updatedUser.Name.GivenName + updatedUser.Name.FamilyName,
                    FamilyName = updatedUser.Name.FamilyName,
                    Password = updatedUser.Password,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    BirthDate = user.BirthDate,
                    DepartmentEmail = user.DepartmentEmail
                };

                return updateGoogleUser;
            }
            catch (Exception e)
            {
                // Handle exception
                throw;
            }
        }

        public Departments GetGroupByEmail(string groupEmail)
        {
            return webDbContext.Departments.SingleOrDefault(group => group.DepartmentEmail.ToLower().Equals(groupEmail.ToLower()));
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

        // --------------------------------------------------- DB ---------------------------------------------------
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

        // get department name by id
        public string GetDepartmentNameById(string departmentId)
        {
            return webDbContext.Departments.SingleOrDefault(d => d.DepartmentId == departmentId).DepartmentName;
        }

        // get role name by id
        public string GetRoleNameById(string roleId)
        {
            return webDbContext.Roles.SingleOrDefault(r => r.RoleId == Guid.Parse(roleId)).RoleName;
        }
    }
}