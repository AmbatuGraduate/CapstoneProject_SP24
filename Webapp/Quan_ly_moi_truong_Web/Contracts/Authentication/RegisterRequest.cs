using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Authentication
{
    public record RegisterRequest
    (
        string Name, string Address,
        string Phone, string Password,
        string Role, string Image
    );
}
