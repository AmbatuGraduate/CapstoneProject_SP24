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
        DateTime Expire_in
    );
}