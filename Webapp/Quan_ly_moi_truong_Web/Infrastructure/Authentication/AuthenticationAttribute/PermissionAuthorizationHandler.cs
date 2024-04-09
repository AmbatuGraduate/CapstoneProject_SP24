using Application.Common.Interfaces.Authentication;
using GoogleApi.Entities.Places.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.AuthenticationAttribute
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {

            var permissionClaim = context.User
                .Claims
                .FirstOrDefault(x => x.Type == "Deparmant");

            if(permissionClaim != null) 
            {
                var permission = permissionClaim.Value;
                var listPermission = requirement.Permission.Split(',');
                if (listPermission.Contains(permission))
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
