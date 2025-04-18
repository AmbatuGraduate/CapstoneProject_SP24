﻿using System.Runtime.CompilerServices;

namespace Contract.Authentication
{
    public record AuthenticationResponse
    (
        string Name,
        string Image,
        string Email,
        string Role,
        string Department,
        string departmentEmail,
        DateTime Expire_in
    );
}