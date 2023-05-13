using System.Security.Cryptography;
using System.Text;

namespace EventUp.Infrastructure.Tools;

public static class HashPasword
{
    public static string Hash(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashByts = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashByts).Replace("-", "").ToLower();
            return hash;
        }
    }
}