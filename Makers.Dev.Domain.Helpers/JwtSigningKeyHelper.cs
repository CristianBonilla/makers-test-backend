using System.Collections;
using System.Text;

namespace Makers.Dev.Domain.Helpers;

public record struct JwtSigningKeyHelper
{
  public static byte[] GetSecretKey(string secretValue, int length = 256)
  {
    string secret = Convert.ToHexString(Encoding.UTF8.GetBytes(secretValue));
    byte[] secretKey = Encoding.UTF8.GetBytes(secret);

    return GetSecretKeyLength(secretKey) == length ? secretKey : throw new InvalidOperationException("Secret key length is not as indicated");
  }

  public static int GetSecretKeyLength(byte[] secretKey)
  {
    BitArray bit = new(secretKey);

    return bit.Length;
  }
}
