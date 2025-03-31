namespace Makers.Dev.Contracts.DTO.User;

public class UserLoginRequest
{
  public required string UsernameOrEmail { get; set; }
  public required string Password { get; set; }
}
