using EmployeePayrollAccess.Application.Interfaces;
using EmployeePayrollAccess.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeePayrollAccess.Application.IoC
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();

            return services;
        }
    }
}
