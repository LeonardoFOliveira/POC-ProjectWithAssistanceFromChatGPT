using EmployeePayrollAccess.Application.Common;
using EmployeePayrollAccess.Application.DTOs;
using EmployeePayrollAccess.Application.Interfaces;
using EmployeePayrollAccess.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeePayrollAccess.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public RegistrationService(IEmployeeRepository employeeRepository, IPasswordHasher<Employee> passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ServiceResult<bool>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var existingEmployee = await _employeeRepository.GetAsync(e => e.Cpf == registerRequestDto.Cpf);
            if (existingEmployee != null)
            {
                return new ServiceResult<bool>(false, false, "CPF already exists.");
            }

            var employee = new Employee
            {
                Cpf = registerRequestDto.Cpf,
                Name = registerRequestDto.Name,
                Email = registerRequestDto.Email,
                PhoneNumber = registerRequestDto.PhoneNumber,
                PasswordHash = _passwordHasher.HashPassword(null, registerRequestDto.Password)
            };

            await _employeeRepository.AddAsync(employee);

            return new ServiceResult<bool>(true, true);
        }
    }
}
