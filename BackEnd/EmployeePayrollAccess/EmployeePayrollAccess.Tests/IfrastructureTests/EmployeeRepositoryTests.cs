using EmployeePayrollAccess.Domain.Entities;
using EmployeePayrollAccess.Infrastructure.Data;
using EmployeePayrollAccess.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeePayrollAccess.Tests.IfrastructureTests
{
    public class EmployeeRepositoryTests
    {
        private readonly DbContextOptions<EmployeePayrollAccessDbContext> _dbContextOptions;
        private readonly EmployeePayrollAccessDbContext _dbContext;
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<EmployeePayrollAccessDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeePayrollAccess")
                .Options;

            _dbContext = new EmployeePayrollAccessDbContext(_dbContextOptions);
            _employeeRepository = new EmployeeRepository(_dbContext);
        }

        [Fact]
        public async Task GetAsync_ReturnsEmployeeByCpf()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                Name = "Test Employee",
                Cpf = "12345678901",
                Email = "test@example.com",
                PhoneNumber = "555555555",
                PasswordHash = "hashedPassword"
            };

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _employeeRepository.GetAsync(e => e.Cpf == employee.Cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Cpf, result.Cpf);
        }

        [Fact]
        public async Task GetAsync_ReturnsNullWhenEmployeeNotFound()
        {
            // Arrange
            var nonExistentCpf = "11111111111";

            // Act
            var result = await _employeeRepository.GetAsync(e => e.Cpf == nonExistentCpf);

            // Assert
            Assert.Null(result);
        }
    }
}
