using EmployeePayrollAccess.API.Controllers;
using EmployeePayrollAccess.Application.Common;
using EmployeePayrollAccess.Application.DTOs;
using EmployeePayrollAccess.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeePayrollAccess.Tests.PresentationTests
{
    public class EmployeeControllerTests
    {
        private readonly EmployeeController _employeeController;
        private readonly Mock<ILoginService> _loginServiceMock;

        public EmployeeControllerTests()
        {
            _loginServiceMock = new Mock<ILoginService>();
            _employeeController = new EmployeeController(_loginServiceMock.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsJwtToken()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto
            {
                Cpf = "12345678900",
                Password = "password"
            };

            var jwtToken = "GeneratedJwtToken";
            var serviceResult = new ServiceResult<string>(true, jwtToken);
            _loginServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDto>())).ReturnsAsync(serviceResult);

            // Act
            var result = await _employeeController.Login(loginRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseData = okResult.Value;
            var tokenValue = responseData.GetType().GetProperty("token").GetValue(responseData, null);
            Assert.Equal(jwtToken, tokenValue);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto
            {
                Cpf = "12345678900",
                Password = "wrong_password"
            };

            var serviceResult = new ServiceResult<string>(false, errorMessage: "Invalid credentials.");
            _loginServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDto>())).ReturnsAsync(serviceResult);

            // Act
            var result = await _employeeController.Login(loginRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseServiceResult = Assert.IsType<ServiceResult<string>>(badRequestResult.Value);
            Assert.False(responseServiceResult.Success);
            Assert.Equal("Invalid credentials.", responseServiceResult.ErrorMessage);
        }

        [Fact]
        public async Task Login_EmptyCredentials_ReturnsBadRequest()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto
            {
                Cpf = "",
                Password = ""
            };

            var serviceResult = new ServiceResult<string>(false, errorMessage: "Cpf and Password fields must not be empty.");
            _loginServiceMock.Setup(x => x.LoginAsync(loginRequestDto)).ReturnsAsync(serviceResult);

            // Act
            var result = await _employeeController.Login(loginRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseServiceResult = Assert.IsType<ServiceResult<string>>(badRequestResult.Value);
            Assert.False(responseServiceResult.Success);
            Assert.Equal("Cpf and Password fields must not be empty.", responseServiceResult.ErrorMessage);
        }
    }
}