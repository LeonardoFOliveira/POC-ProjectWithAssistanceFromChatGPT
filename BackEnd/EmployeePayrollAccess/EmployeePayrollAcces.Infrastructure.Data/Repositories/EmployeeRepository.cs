using EmployeePayrollAccess.Infrastructure.Data;
using EmployeePayrollAccess.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using EmployeePayrollAccess.Domain.Entities;
using EmployeePayrollAccess.Domain.Repositories;

namespace EmployeePayrollAccess.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeePayrollAccessDbContext context)
            : base(context)
        {
        }

        // Implementação dos métodos específicos do Employee, se necessário
    }
}