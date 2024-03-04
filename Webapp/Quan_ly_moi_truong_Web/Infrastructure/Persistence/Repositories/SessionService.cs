using Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistence.Repositories
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string getAccessToken()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("access_token");
        }

        public int getExpiresIn()
        {
            return (int)_httpContextAccessor.HttpContext.Session.GetInt32("expires_in");
        }

        public string getIdToken()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("id_token");
        }

        public string getRefreshToken()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("refresh_token");
        }

        public string getScope()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("scope");
        }

        public void setAccessToken(string accesstoken)
        {
            _httpContextAccessor.HttpContext.Session.SetString("access_token", accesstoken);
        }

        public void setExpiresIn(int expiresIn)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("expires_in", expiresIn);
        }

        public void setIdToken(string idToken)
        {
            _httpContextAccessor.HttpContext.Session.SetString("id_token", idToken);
        }

        public void setRefreshToken(string refreshToken)
        {
            _httpContextAccessor.HttpContext.Session.SetString("refresh_token", refreshToken);
        }

        public void setScope(string scope)
        {
            _httpContextAccessor.HttpContext.Session.SetString("scope", scope);
        }
    }
}