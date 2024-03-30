using Application.Group.Common;
using Application.Group.Common.Add_Update;
using Application.User.Common.List;
using Domain.Entities.Deparment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        public List<Departments> GetAllGroups();
        Task<GroupResult> GetGoogleGroupByEmail(string accessToken, string groupEmail);
        Task<List<GoogleUser>> GetAllMembersOfGroup(string accessToken, string groupId);
        Task<List<GroupResult>> GetAllGoogleGroupByUserEmail(string accessToken, string userEmail);
        Task<GroupResult> AddGoogleGroup(string accessToken, AddGoogleGroup group);
        bool AddGroupDB(Departments group);
        bool UpdateGroupDB(Departments group);
        bool DeleteGroupDB(string groupEmail);
        Task<GroupResult> UpdateGoogleGroup(string accessToken, UpdateGoogleGroup group);
        Task<bool> DeleteGoogleGroup(string accessToken, string groupEmail);
    }
}
