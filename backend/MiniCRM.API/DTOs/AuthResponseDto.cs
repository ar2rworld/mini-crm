namespace MiniCRM.API.DTOs
{
  public class AuthResponseDto
  {
    public string Token { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
  }
}
