using Domain.Entities.UserRefreshToken;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUserRefreshTokenRepository
    {
        UserRefreshTokens AddRefreshRoken(UserRefreshTokens userRefreshTokens);

        UserRefreshTokens GetRefreshRokenByUserId(string userId);
    }
}