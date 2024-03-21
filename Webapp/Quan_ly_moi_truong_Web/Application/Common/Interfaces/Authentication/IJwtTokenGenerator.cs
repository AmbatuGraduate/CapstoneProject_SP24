using System.IdentityModel.Tokens.Jwt;

namespace Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken( string id, string access_token, DateTime expire);

        JwtSecurityToken DecodeToken(string jwt);
    }
}