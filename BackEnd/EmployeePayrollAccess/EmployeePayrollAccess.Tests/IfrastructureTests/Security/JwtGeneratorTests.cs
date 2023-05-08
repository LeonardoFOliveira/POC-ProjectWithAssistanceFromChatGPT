using EmployeePayrollAccess.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace EmployeePayrollAccess.Tests.IfrastructureTests.Security
{
    public class JwtGeneratorTests
    {
        private readonly IConfiguration _configuration;
        private readonly JwtGenerator _jwtGenerator;

        public JwtGeneratorTests()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                new KeyValuePair<string, string>("Jwt:SecretKey", "SuaChaveSecretaSuperSegura1234!@#$"),
                new KeyValuePair<string, string>("Jwt:Issuer", "SeuNomeOuEmpresa"),
                new KeyValuePair<string, string>("Jwt:Audience", "NomeDoSeuProjetoOuAplicativo"),
                new KeyValuePair<string, string>("Jwt:ExpirationInMinutes", "60")
                });

            _configuration = configurationBuilder.Build();
            _jwtGenerator = new JwtGenerator(_configuration);
        }

        [Fact]
        public void Generate_ReturnsValidJwtToken()
        {
            // Arrange
            int employeeId = 1;

            // Act
            var token = _jwtGenerator.Generate(employeeId);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);

            var jwtSecurityToken = new JwtSecurityToken(token);
            Assert.NotNull(jwtSecurityToken);
            Assert.Equal(_configuration["Jwt:Issuer"], jwtSecurityToken.Issuer);
            Assert.Equal(_configuration["Jwt:Audience"], jwtSecurityToken.Audiences.First());
            Assert.Equal(employeeId, Convert.ToInt32(jwtSecurityToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value));
        }
    }
}
