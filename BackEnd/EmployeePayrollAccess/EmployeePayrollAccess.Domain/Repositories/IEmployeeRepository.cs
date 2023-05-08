using EmployeePayrollAccess.Domain.Entities;
using EmployeePayrollAccess.Domain.Repositories;

namespace EmployeePayrollAccess.Application.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
