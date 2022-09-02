using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EShop.Infrastructure.Security
{
    public class Encrypter : IEncrypter
    {
        private readonly string salt = "K_hFfDZqDxBn)yAW)12:0/__P@+H=A)!aX4eczUbb4G16gtVbW";
        public string GetHash(string value, string salt)
        {
            var deriveBytes = new Rfc2898DeriveBytes(value, GetBytes(salt), 1000);
            return Convert.ToBase64String(deriveBytes.GetBytes(50));
        }

        public string GetSalt()
        {
            return salt;
            //var saltBytes = new Byte[50];
            //var range = RandomNumberGenerator.Create();
            //range.GetBytes(saltBytes);

            //return Convert.ToBase64String(saltBytes);
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new Byte[value.Length];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
