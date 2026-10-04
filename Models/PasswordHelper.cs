using System.Security.Cryptography;
using System.Text;

namespace PortfolioWebsite.Models
{
    /// <summary>
    /// Simple SHA-256 password hashing used for the Admins table.
    /// Good enough for a portfolio/demo project. For production use,
    /// swap this for Microsoft.AspNetCore.Identity's PasswordHasher
    /// (PBKDF2 + per-user salt).
    /// </summary>
    public static class PasswordHelper
    {
        public static string Hash(string plainTextPassword)
        {
            var bytes = Encoding.UTF8.GetBytes(plainTextPassword);
            var hashBytes = SHA256.HashData(bytes);
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static bool Verify(string plainTextPassword, string storedHash)
        {
            return Hash(plainTextPassword).Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
