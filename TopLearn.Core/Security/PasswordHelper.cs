using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Security
{
    public class PasswordHelper
    {
        public static string GenerateSalt() => BCrypt.Net.BCrypt.GenerateSalt();
        public static string HashPassword(string password, string salt) => BCrypt.Net.BCrypt.HashPassword(password, salt);
        public static bool VerifyPassword(string password, string hashedPass) => 
            BCrypt.Net.BCrypt.Verify(password, hashedPass);
    }
}
