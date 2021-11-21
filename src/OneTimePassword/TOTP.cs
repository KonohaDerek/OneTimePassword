using System;
using System.Security.Cryptography;

namespace OneTimePassword
{
  /// <summary>
  /// 
  /// RFC6238: Time-Based One-Time Password Algorithm (TOTP)
  /// TOTP is a one-time password algorithm based on a time-dependent one-way function.
  /// TOTP = HOTP(K, T) where T is an integer and K is a secret key.
  /// 
  /// </summary>
  public class TOTP
  {
    public static string Generate(string secret, int digits = 6)
    {
      var epoch = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
      var time = (long)epoch.TotalSeconds;
      return HOTP.Generate(secret, time, digits);
    }

    public static bool ValidateCode(string code, string secret, int digits = 6)
    {
      var epoch = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
      var time = (long)epoch.TotalSeconds;
      return HOTP.ValidateCode(code, secret, time,  digits);
    }
  }
}


