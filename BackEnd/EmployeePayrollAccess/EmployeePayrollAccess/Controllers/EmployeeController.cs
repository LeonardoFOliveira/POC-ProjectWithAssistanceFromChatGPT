using EmployeePayrollAccess.Application.DTOs;
using EmployeePayrollAccess.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePayrollAccess.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public EmployeeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _loginService.LoginAsync(loginRequestDto);

            if (result.Success)
                return Ok(new { token = result.Data });

            return BadRequest(result);
        }
    }
}
