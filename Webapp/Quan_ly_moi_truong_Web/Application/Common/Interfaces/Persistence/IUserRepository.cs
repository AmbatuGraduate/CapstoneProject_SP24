using Application.User.Common.Add;
using Application.User.Common.Group;
using Application.User.Common.List;
using Application.User.Common.UpdateUser;
using Domain.Entities.User;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        List<Users> GetAll();

        Users GetById(Guid id);

        Users? GetUserByPhone(string phoneNumber);

        void Add(Users user);

        void Update(Users user);
        
        Task<List<GoogleUser>> GetGoogleUsers(string accessToken);

        Task<AddGoogleUser> AddGoogleUser(AddGoogleUser user);

        Task<UpdateGoogleUser> UpdateGoogleUser(UpdateGoogleUser user);

        Task<GroupResult> GetGoogleGroupByEmail(string accessToken, string groupEmail);

    }
}