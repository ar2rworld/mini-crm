using MiniCRM.Core.Entities;

namespace MiniCRM.Core.Interfaces
{
  public interface IEmployeeRepository
  {
    // Read operations
    Task<Employee?> GetByIdAsync(int id);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdWithTasksAsync(int id);

    Task<Employee?> GetByFullNameAsync(string fullName);

    Task<Employee> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);

    // Check operations
    Task<bool> ExistsAsync(int id);
  }
}
