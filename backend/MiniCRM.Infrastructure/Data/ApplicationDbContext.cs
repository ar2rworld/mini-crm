using Microsoft.EntityFrameworkCore;
using MiniCRM.Core.Entities;

namespace MiniCRM.Infrastructure.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<TaskFile> TaskFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Employee>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Position).IsRequired().HasMaxLength(100);
        entity.Property(e => e.Password).HasMaxLength(256);

        entity.HasMany(e => e.Tasks)
                  .WithOne(t => t.Employee)
                  .HasForeignKey(t => t.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<TaskEntity>(entity =>
      {
        entity.HasKey(t => t.Id);
        entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
        entity.Property(t => t.Description).IsRequired().HasMaxLength(2000);
        entity.Property(t => t.StartDate).IsRequired();
        entity.Property(t => t.Deadline).IsRequired();
        entity.Property(t => t.Difficulty).IsRequired();
        entity.Property(t => t.CompletionPercentage).IsRequired();

        entity.HasMany(t => t.Comments)
                  .WithOne(c => c.TaskEntity)
                  .HasForeignKey(c => c.TaskId)
                  .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(t => t.Files)
                  .WithOne(f => f.TaskEntity)
                  .HasForeignKey(f => f.TaskId)
                  .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Comment>(entity =>
      {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Text).IsRequired().HasMaxLength(1000);
        entity.Property(c => c.CreatedAt).IsRequired();
      });

      modelBuilder.Entity<TaskFile>(entity =>
      {
        entity.HasKey(f => f.Id);
        entity.Property(f => f.FileName).IsRequired().HasMaxLength(255);
        entity.Property(f => f.FilePath).IsRequired().HasMaxLength(500);
        entity.Property(f => f.ContentType).IsRequired().HasMaxLength(100);
        entity.Property(f => f.FileSize).IsRequired();
        entity.Property(f => f.UploadedAt).IsRequired();
      });
    }
  }
}
