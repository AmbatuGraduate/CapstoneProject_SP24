using System.IdentityModel.Tokens.Jwt;

namespace Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string id, string role, string department, string access_token, DateTime expire);

        JwtSecurityToken DecodeToken(string jwt);

        string DecodeTokenToGetUserId(string jwt);

        string DecodeTokenToGetAccessToken(string jwt);
    }
}