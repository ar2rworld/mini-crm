using Microsoft.EntityFrameworkCore;
using MiniCRM.Core.Entities;
using MiniCRM.Core.Interfaces;
using MiniCRM.Infrastructure.Data;
using MiniCRM.Core.DTOs;
using System.ComponentModel;

namespace MiniCRM.Infrastructure.Repositories
{
  public class TaskRepository : ITaskRepository
  {
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<TaskEntity?> GetByIdAsync(int id)
    {
      return await _context.Tasks.FindAsync(id);
    }

    public async Task<TaskEntity?> GetByIdWithDetailsAsync(int id)
    {
      return await _context.Tasks
          .Include(t => t.Employee)
          .Include(t => t.Comments)
          .Include(t => t.Files)
          .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TaskListDto>> GetAllAsync()
    {
      return await _context.Tasks
          .Select(t => new TaskListDto
          {
            Id = t.Id,
            Title = t.Title,
            CompletionPercentage = t.CompletionPercentage,
            Deadline = t.Deadline,
            EmployeeName = t.Employee.FullName,
            CommentsCount = t.Comments.Count
          })
          .ToListAsync();
    }

    public async Task<List<TaskListDto>> GetByEmployeeIdAsync(int employeeId)
    {
      return await _context.Tasks
          .Where(t => t.EmployeeId == employeeId)
          .Include(t => t.Employee)
          .Select(t => new TaskListDto
          {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CompletionPercentage = t.CompletionPercentage,
            StartDate = t.StartDate,
            Deadline = t.Deadline,
            EmployeeName = t.Employee.FullName,
            CommentsCount = t.Comments.Count
          }).ToListAsync();
    }

    public async Task<IEnumerable<TaskEntity>> GetTasksByDeadlineAsync(DateTime startDate, DateTime endDate)
    {
      return await _context.Tasks
          .Where(t => t.Deadline >= startDate && t.Deadline <= endDate)
          .Include(t => t.Employee)
          .ToListAsync();
    }

    public async Task<IEnumerable<TaskEntity>> GetOverdueTasksAsync()
    {
      var now = DateTime.UtcNow;
      return await _context.Tasks
          .Where(t => t.Deadline < now && t.CompletionPercentage < 100)
          .Include(t => t.Employee)
          .ToListAsync();
    }

    public async Task<IEnumerable<TaskEntity>> GetTasksByCompletionPercentageAsync(int minPercentage, int maxPercentage)
    {
      return await _context.Tasks
          .Where(t => t.CompletionPercentage >= minPercentage && t.CompletionPercentage <= maxPercentage)
          .Include(t => t.Employee)
          .ToListAsync();
    }

    public async Task<TaskEntity> AddAsync(TaskEntity task)
    {
      _context.Tasks.Add(task);
      await _context.SaveChangesAsync();
      return task;
    }

    public async Task UpdateAsync(TaskEntity task)
    {
      _context.Tasks.Update(task);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      var task = await GetByIdAsync(id);
      if (task != null)
      {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
      }
    }

    public async Task UpdateCompletionPercentageAsync(int id, int percentage)
    {
      var task = await GetByIdAsync(id);
      if (task != null)
      {
        task.CompletionPercentage = percentage;
        await _context.SaveChangesAsync();
      }
    }

    public async Task<bool> ExistsAsync(int id)
    {
      return await _context.Tasks.AnyAsync(t => t.Id == id);
    }
  }
}
