using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeePayrollAccess.Application.Interfaces;
using EmployeePayrollAccess.Infrastructure.Data.Repositories;

namespace EmployeePayrollAccess.Infrastructure.Data.IoC
{
    public static class DataInfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddDataInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeePayrollAccessDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EmployeePayrollAccessDb")));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
