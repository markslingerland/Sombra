using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Sombra.Core
{
    public static class SHA256Hasher
    {
        public static string ComputeHash(string data)
        {
            //According to https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing

            byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: data,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
