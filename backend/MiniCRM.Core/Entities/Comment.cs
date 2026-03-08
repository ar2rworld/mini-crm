namespace MiniCRM.Core.Entities
{
  public class Comment
  {
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int TaskId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }

    public TaskEntity TaskEntity { get; set; }
  }
}