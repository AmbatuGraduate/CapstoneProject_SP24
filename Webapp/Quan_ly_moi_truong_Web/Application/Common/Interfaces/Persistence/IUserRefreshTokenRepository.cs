using Domain.Entities.UserRefreshToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUserRefreshTokenRepository
    {
        UserRefreshTokens AddRefreshRoken(UserRefreshTokens userRefreshTokens);
        UserRefreshTokens GetRefreshRokenByUserId(string userId);

    }
}
