using System;
using System.Security.Cryptography;
using System.Text;

namespace Sombra.Core
{
    public static class Hash
    {
        public static string SHA256(string textToHash)
        {
            var bytes = Encoding.UTF8.GetBytes(textToHash);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);
            var hashString = string.Empty;
            foreach (var x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}