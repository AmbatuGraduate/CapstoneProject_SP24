using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.User.Google
{
    public record AddGoogleUserRequest
(
    string Name,
    string FamilyName,
    string Email,
    string Password
    );
}
