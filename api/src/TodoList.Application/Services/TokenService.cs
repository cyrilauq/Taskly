using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Application.IService;
using TodoList.Domain.Entities;
using TodoList.Domain.Entities.Interfaces;

namespace TodoList.Application.Services
{
    public class TokenService : ITokenService
    {
        private TokenOptions _tokenOptions = null!;

        public TokenService(IOptions<TokenOptions> tokenOptions) 
        {
            _tokenOptions = tokenOptions.Value;
        }

        public string GenerateToken(IUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenOptions.Key);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(IUser user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name,  user.Email));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Role, "User"));

            return claims;
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}

public record TokenOptions(string Key);
