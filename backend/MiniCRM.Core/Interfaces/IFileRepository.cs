using MiniCRM.Core.Entities;

namespace MiniCRM.Core.Interfaces
{
  public interface IFileRepository
  {
    Task<TaskFile?> GetByIdAsync(int id);
    Task<IEnumerable<TaskFile>> GetAllAsync();
    Task<IEnumerable<TaskFile>> GetByTaskIdAsync(int taskId);
    Task<TaskFile> AddAsync(TaskFile file);
    Task DeleteAsync(int id);
    Task DeleteByTaskIdAsync(int taskId);
    Task<bool> ExistsAsync(int id);
    Task<int> GetCountByTaskIdAsync(int taskId);
    Task<long> GetTotalSizeByTaskIdAsync(int taskId);
  }
}