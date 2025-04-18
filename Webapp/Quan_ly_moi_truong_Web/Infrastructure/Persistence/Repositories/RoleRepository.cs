﻿using Application.Common.Interfaces.Persistence;
using Domain.Entities.Role;

namespace Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WebDbContext _roleDbContext;

        public RoleRepository(WebDbContext roleDbContext)
        {
            _roleDbContext = roleDbContext;
        }

        public Roles GetRole(Guid id)
        {
            return _roleDbContext.Roles.FirstOrDefault(x => x.RoleId == id);
        }

        public List<Roles> GetRoles()
        {
            return _roleDbContext.Roles.ToList();
        }
    }
}