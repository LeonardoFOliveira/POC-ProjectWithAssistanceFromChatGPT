using EmployeePayrollAccess.Application.DTOs;
using EmployeePayrollAccess.Application.Interfaces;
using EmployeePayrollAccess.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePayrollAccess.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;

        public EmployeeController(ILoginService loginService, IRegistrationService registrationService)
        {
            _loginService = loginService;
            _registrationService = registrationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _loginService.LoginAsync(loginRequestDto);

            if (result.Success)
                return Ok(new { token = result.Data });

            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await _registrationService.RegisterAsync(registerRequestDto);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}
