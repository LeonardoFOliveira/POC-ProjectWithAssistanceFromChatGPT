using EmployeePayrollAccess.Application.Interfaces;
using EmployeePayrollAccess.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollAccess.Infrastructure.Security.IoC
{
    public static class SecurityInfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurityInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            return services;
        }
    }
}
