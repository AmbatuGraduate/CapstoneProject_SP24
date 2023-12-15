using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.User
{
    public record AddUserRequest
    (
        string Name, string Address,
        string Phone, string Password,
        string Image
    );
}
