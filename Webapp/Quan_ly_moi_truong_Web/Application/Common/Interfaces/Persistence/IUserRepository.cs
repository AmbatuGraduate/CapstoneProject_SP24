using Application.User.Common.Add;
using Application.User.Common.List;
using Application.User.Common.UpdateUser;
using Domain.Entities.User;
using Domain.Enums;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        List<Users> GetAll();

        Users GetById(string id);

        //Users? GetUserByPhone(string phoneNumber);

        void Add(Users user);

        void Update(Users user);
        string GetDepartmentNameById(string departmentId);
        string GetDepartmentEmailById(string departmentId);
        string GetRoleNameById(string roleId);
        Task<string> GetGoogleUserImage(string accessToken, string userEmail);

        Task<List<GoogleUser>> GetGoogleUsers(string accessToken);

        Task<GoogleUser> GetGoogleUserByEmail(string accessToken, string email);

        Task<AddGoogleUser> AddGoogleUser(AddGoogleUser user);

        Task<UpdateGoogleUser> UpdateGoogleUser(UpdateGoogleUser user);

        Task<bool> DeleteGoogleUser(string accessToken, string userEmail);

        string ConvertToUserRoleId(UserRole role);
    }
}