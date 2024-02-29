using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Domain.Entities.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Authentication
{
    /// <summary>
    /// Generate the Jwt token for authentication and authorization
    /// </summary>
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings jwtSettings;
        private readonly IDateTimeProvider dateTimeProvider;

        public JwtTokenGenerator()
        {
        }

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            this.jwtSettings = jwtOptions.Value;
            this.dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Create a token for user when login or register
        /// </summary>
        /// <param name="user">The data of user login or register</param>
        /// <returns>String token</returns>
        public string GenerateToken(Users user)
        {
            var signingCredential = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                //new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredential
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}