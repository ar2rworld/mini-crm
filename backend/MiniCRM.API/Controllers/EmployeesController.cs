using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCRM.API.Services;
using MiniCRM.Core.Entities;
using MiniCRM.Core.Interfaces;

namespace MiniCRM.API.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class EmployeesController : ControllerBase
  {
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesController(IEmployeeRepository employeeRepository)
    {
      _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var employees = await _employeeRepository.GetAllAsync();
      return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var employee = await _employeeRepository.GetByIdAsync(id);

      if (employee == null)
        return NotFound();

      return Ok(employee);
    }

    [HttpGet("{id}/tasks")]
    public async Task<IActionResult> GetWithTasks(int id)
    {
      var employee = await _employeeRepository.GetByIdWithTasksAsync(id);

      if (employee == null)
        return NotFound();

      return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
      var employee = new Employee
      {
        FullName = dto.FullName,
        Position = dto.Position,
        Password = PasswordHasher.HashPassword(dto.Password)
      };

      var created = await _employeeRepository.AddAsync(employee);
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
      var employee = await _employeeRepository.GetByIdAsync(id);

      if (employee == null)
        return NotFound();

      employee.FullName = dto.FullName;
      employee.Position = dto.Position;

      await _employeeRepository.UpdateAsync(employee);
      return Ok(employee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var exists = await _employeeRepository.ExistsAsync(id);

      if (!exists)
        return NotFound();

      await _employeeRepository.DeleteAsync(id);
      return NoContent();
    }
  }

  public class CreateEmployeeDto
  {
    public string FullName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }

  public class UpdateEmployeeDto
  {
    public string FullName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
  }
}
