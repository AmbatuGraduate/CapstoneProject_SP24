using Application.Common.Interfaces.Authentication;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.AuthenticationAttribute
{
    public class PermissionService : IPermissionService
    {

        private readonly WebDbContext _context;

        public PermissionService(WebDbContext context)
        {
            _context = context;   
        }

        public async Task<string> GetPermissionAsync(string userId)
        {
            var departmentId = _context.Users.FirstOrDefault(x => x.Id == userId).DepartmentId;
            var departmentName = _context.Departments.FirstOrDefault(x => x.DepartmentId == departmentId).DepartmentName;

            return departmentName;
        }
    }
}
