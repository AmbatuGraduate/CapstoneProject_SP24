using Application.Common.Interfaces.Persistence;
using Application.Group.Common;
using Application.Group.Common.Add_Update;
using Application.User.Common.List;
using Domain.Entities.Deparment;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Auth.OAuth2;

namespace Infrastructure.Persistence.Repositories
{
    public class GroupRepositorys : IGroupRepository
    {
        private readonly WebDbContext webDbContext;
        private readonly Func<GoogleCredential, DirectoryService> _directoryServiceFactory;
        private readonly IUserRepository _userRepository;

        public GroupRepositorys(WebDbContext webDbContext, Func<GoogleCredential, DirectoryService> directoryServiceFactory, IUserRepository userRepository)
        {
            this.webDbContext = webDbContext;
            _directoryServiceFactory = directoryServiceFactory;
            _userRepository = userRepository;
        }

        public List<Departments> GetAllGroups()
        {
            return webDbContext.Departments.ToList();
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

        public async Task<List<GoogleUser>> GetAllMembersOfGroup(string accessToken, string groupId)
        {
            List<GoogleUser> users = new List<GoogleUser>();
            try
            {
                var allGoogleUsers = _userRepository.GetGoogleUsers(accessToken);
                var allDBUsers = _userRepository.GetAll();

                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);

                var request = service.Members.List(groupId);
                var members = request.Execute().MembersValue;
                foreach (var member in members)
                {
                    var memberDB = allDBUsers.FirstOrDefault(x => x.Email.Equals(member.Email, StringComparison.OrdinalIgnoreCase));
                    var memberGoogle = allGoogleUsers.Result.FirstOrDefault(x => x.Email.Equals(member.Email, StringComparison.OrdinalIgnoreCase));
                    if(memberDB != null && memberGoogle != null)
                    {
                        users.Add(
                            new GoogleUser()
                            {
                                Id = member.Id,
                                Email = member.Email,
                                Picture = _userRepository.GetGoogleUserImage(accessToken, member.Email).Result,
                                Name = memberGoogle.Name,
                                Department = _userRepository.GetDepartmentNameById(memberDB.DepartmentId),
                                DepartmentEmail = _userRepository.GetDepartmentEmailById(memberDB.DepartmentId),
                                PhoneNumber = memberGoogle.PhoneNumber,
                                Role = _userRepository.GetRoleNameById(memberDB.RoleId.ToString()),
                                Address = memberGoogle.Address,
                                
                            });
                    }
                }
                return users;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {e.Message}");
                return new List<GoogleUser>();
            }
        }

        public async Task<List<GoogleUser>> GetAllGroupManager(string accessToken)
        {
            List<GoogleUser> users = new List<GoogleUser>();
            try
            {
                var allGoogleUsers = _userRepository.GetGoogleUsers(accessToken);
                var allDBUsers = _userRepository.GetAll();
                var allDbGroups = GetAllGroups();
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);

                foreach(var dbGroup in allDbGroups)
                {
                    var request = service.Members.List(dbGroup.DepartmentId);
                    var members = request.Execute().MembersValue;
                    if(members != null && members.Count() > 0)
                    {
                        foreach (var member in members)
                        {
                            var memberDB = allDBUsers.FirstOrDefault(x => x.Email.Equals(member.Email, StringComparison.OrdinalIgnoreCase));
                            var memberGoogle = allGoogleUsers.Result.FirstOrDefault(x => x.Email.Equals(member.Email, StringComparison.OrdinalIgnoreCase));
                            if (memberDB != null && memberGoogle != null && _userRepository.GetRoleNameById(memberDB.RoleId.ToString()).Equals("Manager"))
                            {
                                users.Add(
                                    new GoogleUser()
                                    {
                                        Id = member.Id,
                                        Email = member.Email,
                                        Picture = _userRepository.GetGoogleUserImage(accessToken, member.Email).Result,
                                        Name = memberGoogle.Name,
                                        Department = _userRepository.GetDepartmentNameById(memberDB.DepartmentId),
                                        DepartmentEmail = _userRepository.GetDepartmentEmailById(memberDB.DepartmentId),
                                        PhoneNumber = memberGoogle.PhoneNumber,
                                        Role = _userRepository.GetRoleNameById(memberDB.RoleId.ToString()),
                                        Address = memberGoogle.Address,

                                    });
                            }
                        }

                    }

                }
                return users;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {e.Message}");
                return new List<GoogleUser>();
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

                foreach (var group in dbGroups)
                {
                    try
                    {
                        var memberRequest = service.Members.Get(group.DepartmentEmail, userEmail);
                        var member = memberRequest.Execute();
                        if (member != null)
                        {
                            groupResult.Add(new GroupResult
                            {
                                Id = member.Id,
                                Email = member.Email,
                                Name = group.DepartmentName,
                                Description = group.Description,
                                AdminCreated = group.AdminCreated,
                                DirectMembersCount = (long)group.DirectMembersCount
                            });
                        }
                    }
                    catch (Exception e)
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

        public async Task<GroupResult> AddGoogleGroup(string accessToken, AddGoogleGroup group)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);
                
                var newGroup = new Group()
                {
                    Email = group.Email,
                    Name = group.Name,
                    Description = group.Description,
                    AdminCreated = group.AdminCreated
                };

                var request = service.Groups.Insert(newGroup);
                var googleGroup = await request.ExecuteAsync();

                if (googleGroup != null && group != null && (group.Members.Count() > 0 || group.Owners.Count() > 0))
                {
                    foreach (var member in group.Members)
                    {
                        await _userRepository.AddUserToGoogleGroup(new Application.User.Common.Add.AddGoogleUser
                        {
                            AccessToken = accessToken,
                            DepartmentEmail = group.Email,
                            Email = member
                        });
                    }

                    foreach (var owner in group.Owners)
                    {
                        await _userRepository.AddUserToGoogleGroup(new Application.User.Common.Add.AddGoogleUser
                        {
                            AccessToken = accessToken,
                            DepartmentEmail = group.Email,
                            Email = owner
                        });
                    }
                }

                return new GroupResult
                {
                    Id = googleGroup.Id,
                    Email = googleGroup.Email,
                    Name = googleGroup.Name,
                    Description = googleGroup.Description,
                    AdminCreated = (bool)googleGroup.AdminCreated
                };
            }
            catch (Exception e)
            {
                // Handle exception
                System.Diagnostics.Debug.WriteLine($"Failed to get user ID: {e.Message}");
                return null;
            }
        }

        public async Task<GroupResult> UpdateGoogleGroup(string accessToken, UpdateGoogleGroup group)
        {
            try
            {
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);
                var existingGroup = service.Groups.Get(group.Email).Execute();

                if (existingGroup != null)
                {
                    existingGroup.Email = group.Email;
                    existingGroup.Name = group.Name;
                    existingGroup.Description = group.Description;
                    existingGroup.AdminCreated = (bool)group.AdminCreated;
                }

                var request = service.Groups.Update(existingGroup, group.Email);
                var googleGroup = await request.ExecuteAsync();

                return new GroupResult
                {
                    Id = googleGroup.Id,
                    Email = googleGroup.Email,
                    Name = googleGroup.Name,
                    Description = googleGroup.Description,
                    AdminCreated = (bool)googleGroup.AdminCreated
                };
            }
            catch (Exception e)
            {
                // Handle exception
                System.Diagnostics.Debug.WriteLine($"Failed to get user ID: {e.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteGoogleGroup(string accessToken, string groupEmail)
        {
            try
            {
                var result = true;
                var credential = GoogleCredential.FromAccessToken(accessToken);
                var service = _directoryServiceFactory(credential);
                var existingGroup = service.Groups.Get(groupEmail).Execute();

                if (existingGroup != null)
                {
                    var request = service.Groups.Delete(groupEmail);
                    await request.ExecuteAsync();
                }
                return result;
            }
            catch (Exception e)
            {
                // Handle exception
                System.Diagnostics.Debug.WriteLine($"Failed to get user ID: {e.Message}");
                return false;
            }
        }

        public bool AddGroupDB(Departments group)
        {
            try
            {
                webDbContext.Departments.Add(group);
                webDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateGroupDB(Departments group)
        {
            try
            {
                webDbContext.Departments.Attach(group);
                webDbContext.Entry<Departments>(group).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                webDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteGroupDB(string groupEmail)
        {
            try
            {
                webDbContext.Departments.Remove(GetGroupByEmail(groupEmail));
                webDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Departments GetGroupByEmail(string groupEmail)
        {
            return webDbContext.Departments.SingleOrDefault(group => group.DepartmentEmail.ToLower().Equals(groupEmail.ToLower()));
        }

        public Departments GetGroupDbById(string groupId)
        {
            try
            {
                return webDbContext.Departments.FirstOrDefault(x => x.DepartmentId == groupId);
            }
            catch (Exception e)
            {
                return null ;
            }
        }
    }
}