namespace Makers.Dev.Domain.Entities.Auth;

public class UserEntity
{
  public Guid UserId { get; set; }
  public required Guid RoleId { get; set; }
  public required string DocumentNumber { get; set; }
  public required string Mobile { get; set; }
  public required string Username { get; set; }
  public required string Password { get; set; }
  public required string Email { get; set; }
  public required string Firstname { get; set; }
  public required string Lastname { get; set; }
  public bool IsActive { get; set; }
  public required byte[] Salt { get; set; }
  public DateTimeOffset Created { get; set; }
  public uint Version { get; set; }
  public RoleEntity Role { get; set; } = null!;
  public ICollection<BankLoanEntity> BankLoans { get; set; } = [];
}
