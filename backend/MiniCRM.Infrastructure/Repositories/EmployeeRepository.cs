using Microsoft.EntityFrameworkCore;
using MiniCRM.Core.Entities;
using MiniCRM.Core.Interfaces;
using MiniCRM.Infrastructure.Data;

namespace MiniCRM.Infrastructure.Repositories
{
  public class EmployeeRepository : IEmployeeRepository
  {
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
      return await _context.Employees.FindAsync(id);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
      return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdWithTasksAsync(int id)
    {
      return await _context.Employees
          .Include(e => e.Tasks)
          .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee?> GetByFullNameAsync(string fullName)
    {
      return await _context.Employees
          .FirstOrDefaultAsync(e => e.FullName == fullName);
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
      _context.Employees.Add(employee);
      await _context.SaveChangesAsync();
      return employee;
    }

    public async Task UpdateAsync(Employee employee)
    {
      _context.Employees.Update(employee);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      var employee = await GetByIdAsync(id);
      if (employee != null)
      {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<bool> ExistsAsync(int id)
    {
      return await _context.Employees.AnyAsync(e => e.Id == id);
    }
  }
}
