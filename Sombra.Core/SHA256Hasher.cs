using System;
using System.Security.Cryptography;
using System.Text;

namespace Sombra.Core
{
  public static class SHA256Hasher
  {
    public static string ComputeHash(string data)
    {
      return BitConverter.ToString(
        SHA256.Create().ComputeHash(
          Encoding.UTF8.GetBytes(data)
        )
      );
    }
  }
}
