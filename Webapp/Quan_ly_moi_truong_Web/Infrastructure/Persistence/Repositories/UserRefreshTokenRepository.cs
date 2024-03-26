using Application.Common.Interfaces.Persistence;
using Domain.Entities.UserRefreshToken;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        // constructor dependency injection
        private readonly WebDbContext _userRefreshTokenDBContext;

        public UserRefreshTokenRepository(WebDbContext userRefreshTokenDBContext)
        {
            _userRefreshTokenDBContext = userRefreshTokenDBContext;
        }

        public UserRefreshTokens AddRefreshRoken(UserRefreshTokens userRefreshTokens)
        {
            _userRefreshTokenDBContext.UserRefreshTokens.Add(userRefreshTokens);
            _userRefreshTokenDBContext.SaveChanges();
            return userRefreshTokens;
        }

        public UserRefreshTokens GetRefreshRokenByUserId(string userId)
        {
            return _userRefreshTokenDBContext.UserRefreshTokens.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
