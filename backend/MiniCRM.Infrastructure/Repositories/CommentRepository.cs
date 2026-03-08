using Microsoft.EntityFrameworkCore;
using MiniCRM.Core.Entities;
using MiniCRM.Core.Interfaces;
using MiniCRM.Infrastructure.Data;

namespace MiniCRM.Infrastructure.Repositories
{
  public class CommentRepository : ICommentRepository
  {
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
      return await _context.Comments.FindAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
      return await _context.Comments.ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetByTaskIdAsync(int taskId)
    {
      return await _context.Comments
          .Where(c => c.TaskId == taskId)
          .OrderBy(c => c.CreatedAt)
          .ToListAsync();
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
      comment.CreatedAt = DateTime.UtcNow;
      _context.Comments.Add(comment);
      await _context.SaveChangesAsync();
      return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
      _context.Comments.Update(comment);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      var comment = await GetByIdAsync(id);
      if (comment != null)
      {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteByTaskIdAsync(int taskId)
    {
      var comments = await _context.Comments
          .Where(c => c.TaskId == taskId)
          .ToListAsync();

      _context.Comments.RemoveRange(comments);
      await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
      return await _context.Comments.AnyAsync(c => c.Id == id);
    }

    public async Task<int> GetCountByTaskIdAsync(int taskId)
    {
      return await _context.Comments.CountAsync(c => c.TaskId == taskId);
    }
  }
}
