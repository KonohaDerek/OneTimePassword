using System;
using Xunit;
using OneTimePassword;

namespace OneTimePasswordTests{
  public class OneTimePasswordTests
  {
    [Fact]
    public void OneTimePasswordIsValidTest()
    {
      var otp = new OTPUtility();
      var secret = "12345678901234567890";
      var code = otp.GenerateCode(secret);
      Assert.True(otp.ValidateCode(code , secret) , "Code is not valid");
    }

    [Fact]
    public void OneTimePasswordGenerateTest()
    {
      var otp = new OTPUtility();
      var secret = "12345678901234567890";
      var code = otp.GenerateCode(secret);
      Assert.True(code.Length == 6 , "Code is not valid");
    }

    [Fact]
    public void OneTimePasswordGenerateSecretTest()
    {
      var otp = new OTPUtility();
      var seed = Guid.NewGuid().ToString().Replace("-" , "").ToUpper();
      var secret = otp.GenerateSecret(seed);
      Assert.True(secret.Length == 24, "Code is not valid");
    }
  }
}


