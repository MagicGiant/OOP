using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Models;

public class PasswordHasher
{
    public static string GetHash(string password)
    {
        byte[] hashCode;
        using (SHA256 sha256 = SHA256.Create())
            hashCode = sha256.ComputeHash(Encoding.ASCII.GetBytes(password));
        return Encoding.ASCII.GetString(hashCode);
    }
}