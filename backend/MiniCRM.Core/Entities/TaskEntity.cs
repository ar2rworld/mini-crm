namespace MiniCRM.Core.Entities
{
  public class TaskEntity
  {
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime Deadline { get; set; }
    public int Difficulty { get; set; }
    public int CompletionPercentage { get; set; }

    public Employee Employee { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<TaskFile> Files { get; set; }
  }
}
