namespace MiniCRM.Core.Entities
{
  public class TaskFile
  {
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; }

    public TaskEntity TaskEntity { get; set; }
  }
}