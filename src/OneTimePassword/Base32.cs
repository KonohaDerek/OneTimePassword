using System.Text;

namespace OneTimePassword
{
  public class Base32
  {

    // Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"
    private static readonly char[] Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

    public static string Encode(byte[] data)
    {
      var result = new StringBuilder(data.Length * 8 / 5);
      var buffer = new byte[8];
      var index = 0;
      foreach (var b in data)
      {
        buffer[index] = b;
        index++;
        if (index == 8)
        {
          var bits = BitConverter.ToUInt32(buffer, 0);
          result.Append(Base32Alphabet[(bits >> 26) & 31]);
          result.Append(Base32Alphabet[(bits >> 21) & 31]);
          result.Append(Base32Alphabet[(bits >> 16) & 31]);
          result.Append(Base32Alphabet[(bits >> 11) & 31]);
          result.Append(Base32Alphabet[(bits >> 6) & 31]);
          result.Append(Base32Alphabet[bits & 31]);
          index = 0;
        }
      }
      if (index > 0)
      {
        var bits = BitConverter.ToUInt32(buffer, 0);
        for (var i = index; i < 8; i++)
        {
          bits <<= 8;
        }
        result.Append(Base32Alphabet[(bits >> 26) & 31]);
        result.Append(Base32Alphabet[(bits >> 21) & 31]);
        result.Append(Base32Alphabet[(bits >> 16) & 31]);
        result.Append(Base32Alphabet[(bits >> 11) & 31]);
        result.Append(Base32Alphabet[(bits >> 6) & 31]);
        result.Append(Base32Alphabet[bits & 31]);
      }
      return result.ToString();
    }

    public static byte[] Decode(string data)
    {
      var result = new byte[data.Length * 5 / 8];
      var buffer = new byte[8];
      var index = 0;
      foreach (var c in data)
      {
        var value = Array.IndexOf(Base32Alphabet, c);
        if (value < 0)
        {
          throw new ArgumentException("Invalid character");
        }
        buffer[index] = (byte)value;
        index++;
        if (index == 8)
        {
          var bits = BitConverter.ToUInt32(buffer, 0);
          result[0] = (byte)((bits >> 3) & 0xFF);
          result[1] = (byte)((bits >> 11) & 0xFF);
          result[2] = (byte)((bits >> 19) & 0xFF);
          result[3] = (byte)((bits >> 27) & 0xFF);
          index = 0;
        }
      }
      if (index > 0)
      {
        var bits = BitConverter.ToUInt32(buffer, 0);
        for (var i = index; i < 8; i++)
        {
          bits <<= 8;
        }
        result[0] = (byte)((bits >> 3) & 0xFF);
        result[1] = (byte)((bits >> 11) & 0xFF);
        result[2] = (byte)((bits >> 19) & 0xFF);
        result[3] = (byte)((bits >> 27) & 0xFF);
      }
      return result;
    }
  }
}
