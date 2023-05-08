using EmployeePayrollAccess.Application.Common;
using EmployeePayrollAccess.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollAccess.Application.Interfaces
{
    public interface IRegistrationService
    {
        Task<ServiceResult<bool>> RegisterAsync(RegisterRequestDto registerRequestDto);
    }
}
