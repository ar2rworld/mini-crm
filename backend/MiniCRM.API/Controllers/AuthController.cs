using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCRM.API.DTOs;
using MiniCRM.API.Services;
using MiniCRM.Core.Entities;
using MiniCRM.Core.Interfaces;

namespace MiniCRM.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IEmployeeRepository _employeeRepository;
    private readonly TokenService _tokenService;

    public AuthController(
        IEmployeeRepository employeeRepository,
        TokenService tokenService)
    {
      _employeeRepository = employeeRepository;
      _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
      var existingEmployee = await _employeeRepository.GetByFullNameAsync(dto.FullName);
      if (existingEmployee != null)
      {
        return BadRequest(new { message = "Employee with this name already exists" });
      }

      var hashedPassword = PasswordHasher.HashPassword(dto.Password);

      var employee = new Employee
      {
        FullName = dto.FullName,
        Position = dto.Position,
        Password = hashedPassword
      };

      await _employeeRepository.AddAsync(employee);

      var token = _tokenService.GenerateToken(employee);

      var response = new AuthResponseDto
      {
        Token = token,
        EmployeeId = employee.Id,
        FullName = employee.FullName,
        Position = employee.Position
      };

      return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
      var employee = await _employeeRepository.GetByFullNameAsync(dto.Username);

      if (employee == null || employee.Password == null)
      {
        return Unauthorized(new { message = "Invalid credentials" });
      }

      if (!PasswordHasher.VerifyPassword(dto.Password, employee.Password))
      {
        return Unauthorized(new { message = "Invalid credentials" });
      }

      var token = _tokenService.GenerateToken(employee);

      var response = new AuthResponseDto
      {
        Token = token,
        EmployeeId = employee.Id,
        FullName = employee.FullName,
        Position = employee.Position
      };

      return Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
      var employeeIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
      if (employeeIdClaim == null)
      {
        return Unauthorized();
      }

      var employeeId = int.Parse(employeeIdClaim.Value);
      var employee = await _employeeRepository.GetByIdAsync(employeeId);

      if (employee == null)
      {
        return NotFound();
      }

      return Ok(new
      {
        employeeId = employee.Id,
        fullName = employee.FullName,
        position = employee.Position
      });
    }
  }
}
