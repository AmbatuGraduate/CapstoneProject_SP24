using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IPermissionService
    {
        Task<string> GetPermissionAsync(string userId);
    }
}
