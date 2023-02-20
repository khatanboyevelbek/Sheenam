using System.Security.Cryptography;
using System.Text;

namespace Sheenam.Api.Services.Foundations.Security.PasswordHash
{
    public class PasswordHashServise : IPasswordHashServise
    {
        public string GenerateHashPassword(string password)
        {
            byte[] passwordHash;

            using (var hmacsha = SHA256.Create())
            {
                passwordHash =
                    hmacsha.ComputeHash(Encoding.Default.GetBytes(password));
            };

            return Convert.ToBase64String(passwordHash);
        }
    }
}
