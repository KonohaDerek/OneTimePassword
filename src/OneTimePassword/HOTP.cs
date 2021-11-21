using System;
using System.Security.Cryptography;
using System.Text;

namespace OneTimePassword
{
    /// <summary>
    /// TOTP = HOTP(key, counter)
    /// 
    /// HOTP(key, counter) = HMAC-SHA1(key, counter)
    /// 
    /// </summary>
    public class HOTP
    {
     
        public static string Generate(string key, long counter, int digits = 6 )
        {
            var counterBytes = BitConverter.GetBytes(counter);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(counterBytes);
            }

            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            if (hmac == null)
            {
                throw new ArgumentException("Invalid hash algorithm");
            }
            var hash = hmac.ComputeHash(counterBytes);
            var offset = hash[hash.Length - 1] & 0x0F;
            var binaryCode = (hash[offset] & 0x7F) << 24 |
                             (hash[offset + 1] & 0xFF) << 16 |
                             (hash[offset + 2] & 0xFF) << 8 |
                             (hash[offset + 3] & 0xFF);

            var code = binaryCode % (long)Math.Pow(10, digits);
            return code.ToString().PadLeft(digits, '0');
        }

        public static bool ValidateCode(string code, string key, long counter, int digits = 6)
        {
            var codeToValidate = Generate(key, counter, digits);
            return codeToValidate == code;
        }
    }
}
