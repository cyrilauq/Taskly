using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TodoList.Application.Services;
using TodoList.Application.Models;

namespace TodoList.Application.Tests.Services
{
    [TestClass]
    public class TokenServiceTests
    {
        private TokenOptions _tokenOptions = null!;
        private TokenService tokenService = null!;

        [TestInitialize]
        public void SetUp()
        {
            _tokenOptions = new TokenOptions("rdgdfgdsfgdf hgfbhfgh gf jff dgdfg hfd gfh fgh fgh fgdhfgh bvbvbfghfghfghgj hfg jhg jf hfhg jgfh jfg j");
            tokenService = new TokenService(Options.Create(_tokenOptions));
        }

        [TestMethod]
        public void WhenCreateATokenForAUserThenReturnAValidBearerToken()
        {
            // Act
            var user = new User
            {
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
                Email = "test@test.com",
                UserName = "test_user"
            };

            var key = Encoding.ASCII.GetBytes(_tokenOptions.Key);
            var token = tokenService.GenerateToken(user);
            var tokenParts = token.Split('.');

            // Assert
            Assert.AreEqual(3, tokenParts.Length);
            new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters()
            { 
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateAudience = false,
                ValidateIssuer = false
            }, out SecurityToken tokenValidated);
            Assert.IsTrue(true);
        }
    }
}
