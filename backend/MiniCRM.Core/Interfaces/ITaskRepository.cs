using MiniCRM.Core.Entities;
using MiniCRM.Core.DTOs;

namespace MiniCRM.Core.Interfaces
{
  public interface ITaskRepository
  {
    Task<TaskEntity?> GetByIdAsync(int id);
    Task<TaskEntity?> GetByIdWithDetailsAsync(int id);
    Task<List<TaskListDto>> GetAllAsync();
    Task<List<TaskListDto>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<TaskEntity>> GetTasksByDeadlineAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<TaskEntity>> GetOverdueTasksAsync();
    Task<IEnumerable<TaskEntity>> GetTasksByCompletionPercentageAsync(int minPercentage, int maxPercentage);
    Task<TaskEntity> AddAsync(TaskEntity TaskEntity);
    Task UpdateAsync(TaskEntity TaskEntity);
    Task DeleteAsync(int id);
    Task UpdateCompletionPercentageAsync(int id, int percentage);
    Task<bool> ExistsAsync(int id);
  }
}
