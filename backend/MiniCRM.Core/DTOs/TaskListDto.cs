namespace MiniCRM.Core.DTOs
{
  public class TaskListDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CompletionPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime Deadline { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int CommentsCount { get; set; }
  }
}
