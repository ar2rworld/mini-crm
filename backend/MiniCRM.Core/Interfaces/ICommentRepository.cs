using MiniCRM.Core.Entities;

namespace MiniCRM.Core.Interfaces
{
  public interface ICommentRepository
  {
    Task<Comment?> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetAllAsync();
    Task<IEnumerable<Comment>> GetByTaskIdAsync(int taskId);
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
    Task DeleteByTaskIdAsync(int taskId);
    Task<bool> ExistsAsync(int id);
    Task<int> GetCountByTaskIdAsync(int taskId);
  }
}