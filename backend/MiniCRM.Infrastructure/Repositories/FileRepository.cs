using Microsoft.EntityFrameworkCore;
using MiniCRM.Core.Entities;
using MiniCRM.Core.Interfaces;
using MiniCRM.Infrastructure.Data;

namespace MiniCRM.Infrastructure.Repositories
{
  public class FileRepository : IFileRepository
  {
    private readonly ApplicationDbContext _context;

    public FileRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<TaskFile?> GetByIdAsync(int id)
    {
      return await _context.TaskFiles.FindAsync(id);
    }

    public async Task<IEnumerable<TaskFile>> GetAllAsync()
    {
      return await _context.TaskFiles.ToListAsync();
    }

    public async Task<IEnumerable<TaskFile>> GetByTaskIdAsync(int taskId)
    {
      return await _context.TaskFiles
          .Where(f => f.TaskId == taskId)
          .OrderBy(f => f.UploadedAt)
          .ToListAsync();
    }

    public async Task<TaskFile> AddAsync(TaskFile file)
    {
      file.UploadedAt = DateTime.UtcNow;
      _context.TaskFiles.Add(file);
      await _context.SaveChangesAsync();
      return file;
    }

    public async Task DeleteAsync(int id)
    {
      var file = await GetByIdAsync(id);
      if (file != null)
      {
        _context.TaskFiles.Remove(file);
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteByTaskIdAsync(int taskId)
    {
      var files = await _context.TaskFiles
          .Where(f => f.TaskId == taskId)
          .ToListAsync();

      _context.TaskFiles.RemoveRange(files);
      await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
      return await _context.TaskFiles.AnyAsync(f => f.Id == id);
    }

    public async Task<int> GetCountByTaskIdAsync(int taskId)
    {
      return await _context.TaskFiles.CountAsync(f => f.TaskId == taskId);
    }

    public async Task<long> GetTotalSizeByTaskIdAsync(int taskId)
    {
      return await _context.TaskFiles
          .Where(f => f.TaskId == taskId)
          .SumAsync(f => f.FileSize);
    }
  }
}
