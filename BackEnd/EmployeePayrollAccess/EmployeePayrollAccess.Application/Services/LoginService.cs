using EmployeePayrollAccess.Application.Common;
using EmployeePayrollAccess.Application.DTOs;
using EmployeePayrollAccess.Application.Interfaces;
using EmployeePayrollAccess.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeePayrollAccess.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public LoginService(IEmployeeRepository employeeRepository, IJwtGenerator jwtGenerator, IPasswordHasher<Employee> passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<ServiceResult<string>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            if (string.IsNullOrEmpty(loginRequestDto.Cpf) || string.IsNullOrEmpty(loginRequestDto.Password))
            {
                return new ServiceResult<string>(false, errorMessage: "Cpf and Password fields must not be empty.");
            }

            var employee = await _employeeRepository.GetAsync(e => e.Cpf == loginRequestDto.Cpf);

            if (employee == null || _passwordHasher.VerifyHashedPassword(employee, employee.PasswordHash, loginRequestDto.Password) == PasswordVerificationResult.Failed)
                return new ServiceResult<string>(false, errorMessage: "CPF or password is incorrect.");

            var jwt = _jwtGenerator.Generate(employee.Id);
            return new ServiceResult<string>(true, jwt);
        }
    }
}