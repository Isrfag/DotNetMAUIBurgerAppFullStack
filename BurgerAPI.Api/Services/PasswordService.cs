using System.Security.Cryptography;
using System.Text;

namespace BurgerAPI.Api.Services
{
    public class PasswordService
    {
        private const int saltSize = 10;
        public (string salt, string hashedPassword) GenerateSaltAndHash(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentNullException(nameof(plainPassword));

            var buffer = RandomNumberGenerator.GetBytes(saltSize);
            string? salt = Convert.ToBase64String(buffer);

            string hashedPassword = GenerateHashedPassword(plainPassword, salt);

            return (salt, hashedPassword);
            
        }

        public bool IsEqual(string plainPassword, string salt, string hashedPassword)
        {
            string newHashedPassword = GenerateHashedPassword(plainPassword, salt);

            return newHashedPassword == hashedPassword;

        }

        private static string GenerateHashedPassword(string plainPassword, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainPassword + salt);

            byte[] hash = SHA256.HashData(bytes);
            string hashedPassword = Convert.ToBase64String(hash);

            return hashedPassword;
        }
    }
}
