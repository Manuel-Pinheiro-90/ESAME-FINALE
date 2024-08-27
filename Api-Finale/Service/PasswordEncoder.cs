using System.Text;
using System.Security.Cryptography;

namespace Api_Finale.Service
{
    public class PasswordEncoder : IpasswordEncoder
    {
        public string Encode(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool Verify(string password, string encodedPassword)
        {
            return Encode(password) == encodedPassword;
        }
    }
}
