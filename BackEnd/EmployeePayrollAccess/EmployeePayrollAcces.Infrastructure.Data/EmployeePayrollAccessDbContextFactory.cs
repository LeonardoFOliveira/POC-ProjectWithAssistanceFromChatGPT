using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeePayrollAccess.Infrastructure.Data
{
    public class EmployeePayrollAccessDbContextFactory : IDesignTimeDbContextFactory<EmployeePayrollAccessDbContext>
    {
        public EmployeePayrollAccessDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\EmployeePayrollAccess"));

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EmployeePayrollAccessDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("EmployeePayrollAccessDb"));

            return new EmployeePayrollAccessDbContext(optionsBuilder.Options);
        }
    }
}
