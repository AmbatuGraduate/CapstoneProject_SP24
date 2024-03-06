using System.IdentityModel.Tokens.Jwt;

namespace Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string name, string id, string access_token, string image, DateTime expire);

        JwtSecurityToken DecodeToken(string jwt);
    }
}