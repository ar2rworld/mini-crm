namespace MiniCRM.Core.Entities
{
  public class Employee
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Password { get; set; }

    public ICollection<TaskEntity> Tasks { get; set; }
  }
}