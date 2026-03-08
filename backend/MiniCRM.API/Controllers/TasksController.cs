using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCRM.Core.Entities;
using MiniCRM.Core.Interfaces;

namespace MiniCRM.API.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class TasksController : ControllerBase
  {
    private readonly ITaskRepository _taskRepository;

    public TasksController(ITaskRepository taskRepository)
    {
      _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var tasks = await _taskRepository.GetAllAsync();
      return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var task = await _taskRepository.GetByIdAsync(id);

      if (task == null)
        return NotFound();

      return Ok(task);
    }

    [HttpGet("{id}/details")]
    public async Task<IActionResult> GetWithDetails(int id)
    {
      var task = await _taskRepository.GetByIdWithDetailsAsync(id);

      if (task == null)
        return NotFound();

      return Ok(task);
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployeeId(int employeeId)
    {
      var tasks = await _taskRepository.GetByEmployeeIdAsync(employeeId);
      return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
      var task = new TaskEntity
      {
        EmployeeId = dto.EmployeeId,
        Title = dto.Title,
        Description = dto.Description,
        StartDate = DateTime.UtcNow,
        Deadline = dto.Deadline,
        CompletionPercentage = 0
      };

      var created = await _taskRepository.AddAsync(task);
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
    {
      var task = await _taskRepository.GetByIdAsync(id);

      if (task == null)
        return NotFound();

      task.Title = dto.Title;
      task.Description = dto.Description;
      task.Deadline = dto.Deadline;
      task.CompletionPercentage = dto.CompletionPercentage;

      await _taskRepository.UpdateAsync(task);
      return Ok(task);
    }

    [HttpPatch("{id}/completion")]
    public async Task<IActionResult> UpdateCompletion(int id, [FromBody] UpdateCompletionDto dto)
    {
      var exists = await _taskRepository.ExistsAsync(id);

      if (!exists)
        return NotFound();

      await _taskRepository.UpdateCompletionPercentageAsync(id, dto.CompletionPercentage);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var exists = await _taskRepository.ExistsAsync(id);

      if (!exists)
        return NotFound();

      await _taskRepository.DeleteAsync(id);
      return NoContent();
    }
  }

  public class CreateTaskDto
  {
    public int EmployeeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
  }

  public class UpdateTaskDto
  {
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public int CompletionPercentage { get; set; }
  }

  public class UpdateCompletionDto
  {
    public int CompletionPercentage { get; set; }
  }
}
