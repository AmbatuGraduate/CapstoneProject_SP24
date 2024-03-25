using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces.Authentication
{
    public interface ISessionService
    {
        //string getAccessToken();

        //void setAccessToken(string accesstoken);

        //string getRefreshToken();

        //void setRefreshToken(string refreshToken);

        //int getExpiresIn();

        //void setExpiresIn(int expiresIn);

        //string getIdToken();

        //void setIdToken(string idToken);

        //string getScope();

        //void setScope(string scope);

        HttpContext HttpContext { get; }
    }
}