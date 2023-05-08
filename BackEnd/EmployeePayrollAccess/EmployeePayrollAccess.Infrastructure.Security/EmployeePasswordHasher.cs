using EmployeePayrollAccess.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeePayrollAccess.Infrastructure.Security
{
    public class EmployeePasswordHasher : IPasswordHasher<Employee>
    {
        private readonly PasswordHasher<Employee> _passwordHasher;

        public EmployeePasswordHasher()
        {
            _passwordHasher = new PasswordHasher<Employee>();
        }

        public string HashPassword(Employee employee, string password)
        {
            return _passwordHasher.HashPassword(employee, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(Employee employee, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(employee, hashedPassword, providedPassword);
        }
    }
}
