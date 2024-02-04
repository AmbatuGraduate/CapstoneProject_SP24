
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Authentication
{
    public record AuthenticationResponse
    (
        Guid id,
        string Name, string Address,
        string Phone,
        string Role, string Image,
        string Token
    );
}
