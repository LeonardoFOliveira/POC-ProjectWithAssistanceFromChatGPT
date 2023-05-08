using EmployeePayrollAccess.Application.Common;
using EmployeePayrollAccess.Application.DTOs;

namespace EmployeePayrollAccess.Application.Interfaces
{
    public interface ILoginService
    {
        Task<ServiceResult<string>> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
