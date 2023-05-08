using EmployeePayrollAccess.Application.Common;
using EmployeePayrollAccess.Application.DTOs;
using EmployeePayrollAccess.Application.Interfaces;
using EmployeePayrollAccess.Application.Services;
using EmployeePayrollAccess.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace EmployeePayrollAccess.Tests.ApplicationTests
{
    public class LoginServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IJwtGenerator> _jwtGeneratorMock;
        private readonly Mock<IPasswordHasher<Employee>> _passwordHasherMock = new Mock<IPasswordHasher<Employee>>();


        public LoginServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _jwtGeneratorMock = new Mock<IJwtGenerator>();
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsJwtToken()
        {
            // Arrange
            var validCpf = "12345678901";
            var validPassword = "validPassword";
            var generatedJwtToken = "GeneratedJwtToken";
            var employee = new Employee
            {
                Id = 1,
                Cpf = validCpf,
                PasswordHash = "hashedPassword"
            };

            _employeeRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                .ReturnsAsync(employee);

            _passwordHasherMock.Setup(x => x.VerifyHashedPassword(employee, employee.PasswordHash, validPassword))
                .Returns(PasswordVerificationResult.Success);

            _jwtGeneratorMock.Setup(x => x.Generate(employee.Id))
                .Returns(generatedJwtToken);

            var loginService = new LoginService(_employeeRepositoryMock.Object, _jwtGeneratorMock.Object, _passwordHasherMock.Object);

            // Act
            var result = await loginService.LoginAsync(new LoginRequestDto { Cpf = validCpf, Password = validPassword });

            // Assert
            Assert.True(result.Success);
            Assert.Equal(generatedJwtToken, result.Data);
        }

        [Fact]
        public async Task LoginAsync_InvalidCpf_ReturnsFailure()
        {
            // Arrange
            _employeeRepositoryMock
                .Setup(x => x.GetAsync(e => e.Cpf == "invalidcpf"))
                .ReturnsAsync((Employee)null);

            _passwordHasherMock.Setup(x => x.VerifyHashedPassword(It.IsAny<Employee>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Failed);

            var loginService = new LoginService(_employeeRepositoryMock.Object, _jwtGeneratorMock.Object, _passwordHasherMock.Object);

            // Act
            var result = await loginService.LoginAsync(new LoginRequestDto
            {
                Cpf = "invalidcpf",
                Password = "password"
            });

            // Assert
            Assert.False(result.Success);
            Assert.Equal("CPF or password is incorrect.", result.ErrorMessage);
        }

        [Fact]
        public async Task LoginAsync_InvalidPassword_ReturnsFailure()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Name = "John Doe",
                Cpf = "12345678901",
                Email = "john.doe@email.com",
                PhoneNumber = "1234567890",
                PasswordHash = "hashedpassword"
            };

            _employeeRepositoryMock
                .Setup(x => x.GetAsync(e => e.Cpf == employee.Cpf))
                .ReturnsAsync(employee);

            _jwtGeneratorMock
                .Setup(x => x.Generate(employee.Id))
                .Returns("valid.jwt.token");

            _passwordHasherMock
                .Setup(x => x.VerifyHashedPassword(employee, employee.PasswordHash, "invalidpassword"))
                .Returns(PasswordVerificationResult.Failed);

            var loginService = new LoginService(_employeeRepositoryMock.Object, _jwtGeneratorMock.Object, _passwordHasherMock.Object);

            // Act
            var result = await loginService.LoginAsync(new LoginRequestDto
            {
                Cpf = employee.Cpf,
                Password = "invalidpassword"
            });

            // Assert
            Assert.False(result.Success);
            Assert.Equal("CPF or password is incorrect.", result.ErrorMessage);
        }

        [Fact]
        public async Task LoginAsync_EmptyCredentials_ReturnsFailure()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto
            {
                Cpf = "",
                Password = ""
            };

            var loginService = new LoginService(_employeeRepositoryMock.Object, _jwtGeneratorMock.Object, _passwordHasherMock.Object);

            // Act
            var result = await loginService.LoginAsync(loginRequestDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Cpf and Password fields must not be empty.", result.ErrorMessage);
        }

    }
}

