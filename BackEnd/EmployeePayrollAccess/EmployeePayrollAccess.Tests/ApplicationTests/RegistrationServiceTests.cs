using EmployeePayrollAccess.Application.Common;
using EmployeePayrollAccess.Application.DTOs;
using EmployeePayrollAccess.Application.Interfaces;
using EmployeePayrollAccess.Application.Services;
using EmployeePayrollAccess.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace EmployeePayrollAccess.Application.Tests.Services
{
    public class RegistrationServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<IPasswordHasher<Employee>> _mockPasswordHasher;
        private readonly RegistrationService _registrationService;

        public RegistrationServiceTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher<Employee>>();
            _registrationService = new RegistrationService(_mockEmployeeRepository.Object, _mockPasswordHasher.Object);
        }

        [Fact]
        public async Task RegisterAsync_WithValidData_ReturnsSuccess()
        {
            var registerRequest = new RegisterRequestDto
            {
                Cpf = "12345678900",
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "555-1234",
                Password = "Password123"
            };

            _mockEmployeeRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>())).ReturnsAsync((Employee)null);
            _mockPasswordHasher.Setup(p => p.HashPassword(It.IsAny<Employee>(), registerRequest.Password)).Returns("hashed_password");

            var result = await _registrationService.RegisterAsync(registerRequest);

            Assert.True(result.Success);
            _mockEmployeeRepository.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_WithExistingCpf_ReturnsError()
        {
            var registerRequest = new RegisterRequestDto
            {
                Cpf = "12345678900",
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "555-1234",
                Password = "Password123"
            };

            var existingEmployee = new Employee { Cpf = registerRequest.Cpf };

            _mockEmployeeRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Employee, bool>>>())).ReturnsAsync(existingEmployee);

            var result = await _registrationService.RegisterAsync(registerRequest);

            Assert.False(result.Success);
            Assert.Equal("CPF already exists.", result.ErrorMessage);
            _mockEmployeeRepository.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Never);
        }
    }
}