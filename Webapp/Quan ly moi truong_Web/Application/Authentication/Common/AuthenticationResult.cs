using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.User;

namespace Application.Authentication.Common
{
    /// <summary>
    /// Response of the the request when login or register
    /// </summary>
    /// <param name="User">The data user when login or register return</param>
    /// <param name="Token">Token is from generate when login or register</param>
    public record AuthenticationResult(Users User, string Token);

}
