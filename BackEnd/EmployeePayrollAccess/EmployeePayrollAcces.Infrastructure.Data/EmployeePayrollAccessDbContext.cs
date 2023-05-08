using EmployeePayrollAccess.Domain.Entities;
using EmployeePayrollAccess.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EmployeePayrollAccess.Infrastructure.Data
{
    public class EmployeePayrollAccessDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeePayrollAccessDbContext(DbContextOptions<EmployeePayrollAccessDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }
}