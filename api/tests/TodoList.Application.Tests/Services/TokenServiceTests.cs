using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TodoList.Application.Services;
using TodoList.Application.Models;
using TodoList.Application.IRepository;
using Moq;

namespace TodoList.Application.Tests.Services
{
    [TestClass]
    public class TokenServiceTests
    {
        private TokenOptions _tokenOptions = null!;
        private TokenService tokenService = null!;
        private Mock<IRoleRepository> mockedRoleRepository;

        [TestInitialize]
        public void SetUp()
        {
            _tokenOptions = new TokenOptions { Key = "rdgdfgdsfgdf hgfbhfgh gf jff dgdfg hfd gfh fgh fgh fgdhfgh bvbvbfghfghfghgj hfg jhg jf hfhg jgfh jfg j" };
            
            mockedRoleRepository = new Mock<IRoleRepository>();
            mockedRoleRepository.Setup(mrr => mrr.Find(It.IsAny<RoleSearchArgs>(), It.IsAny<CancellationToken>())).ReturnsAsync(["User"]);

            tokenService = new TokenService(Options.Create(_tokenOptions), mockedRoleRepository.Object);
        }

        [TestMethod]
        public async Task WhenCreateATokenForAUserThenReturnAValidBearerToken()
        {
            // Act
            var user = new User
            {
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
                Email = "test@test.com",
                UserName = "test_user"
            };

            var key = Encoding.ASCII.GetBytes(_tokenOptions.Key);
            var token = await tokenService.GenerateToken(user);
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
