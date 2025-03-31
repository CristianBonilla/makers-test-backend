using System.Text;
using System.Security.Cryptography;

namespace Makers.Dev.Domain.Helpers;

public class HashPasswordHelper
{
  public static (string Password, byte[] SaltBytes) Create(string password)
  {
    byte[] saltBytes = GetSalt();
    string hashedPassword = Generate(password, saltBytes);
    string saltBase64 = Convert.ToBase64String(saltBytes);
    byte[] retrievedSaltBytes = Convert.FromBase64String(saltBase64);

    return (hashedPassword, retrievedSaltBytes);
  }

  public static bool Verify(string password, string hashedPassword, byte[] saltBytes) => hashedPassword == Generate(password, saltBytes);

  private static string Generate(string hashedPassword, byte[] saltBytes)
  {
    byte[] passwordBytes = Encoding.UTF8.GetBytes(hashedPassword);
    byte[] saltedPasswordBytes = new byte[passwordBytes.Length + saltBytes.Length];
    Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
    Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, passwordBytes.Length, saltBytes.Length);
    using SHA256 sha256 = SHA256.Create();
    byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
    sha256.Clear();
    byte[] saltedhashPasswordBytes = new byte[hashBytes.Length + saltBytes.Length];
    Buffer.BlockCopy(saltBytes, 0, saltedhashPasswordBytes, 0, saltBytes.Length);
    Buffer.BlockCopy(hashBytes, 0, saltedhashPasswordBytes, saltBytes.Length, hashBytes.Length);

    return Convert.ToBase64String(saltedhashPasswordBytes);
  }

  private static byte[] GetSalt()
  {
    using RandomNumberGenerator random = RandomNumberGenerator.Create();
    byte[] saltBytes = new byte[32];
    random.GetBytes(saltBytes);

    return saltBytes;
  }
}
