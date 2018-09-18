using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Misc
{
   static class AuthHelper
    {
        public static string HashText(string text, string salt)
        {
            var provider = new SHA256CryptoServiceProvider();
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(text, salt));
            byte[] hashedBytes = provider.ComputeHash(textWithSaltBytes);
            provider.Clear();
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
