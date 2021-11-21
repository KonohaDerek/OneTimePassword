
using System.Security.Cryptography;
using System.Text;

namespace OneTimePassword
{

  /// <summary>
  /// One-time password generator.
  /// </summary>
  public class OTPUtility
  {

    /// <summary>
    /// Vaild the code against the secret key.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="secret"></param>
    /// <param name="timeStep"></param>
    /// <param name="digits"></param>
    /// <param name="hashAlgorithm"></param>
    /// <returns></returns>
    public  bool ValidateCode(string code, string secret, int digits = 6)
    {
      var expectedCode = TOTP.Generate(secret, digits);
      return code == expectedCode;
    }

    public  string GenerateCode(string secret, int digits = 6)
    {
      return TOTP.Generate(secret, digits);
    }

    public  string GenerateSecret(object seed)
    {
      var bytes = Encoding.UTF8.GetBytes(seed.ToString());
      SHA256 sha256 = SHA256.Create();
      var hash = sha256.ComputeHash(bytes);
      return Base32.Encode(hash);
    }
  }
}

