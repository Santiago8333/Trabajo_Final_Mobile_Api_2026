using System.Security.Cryptography;
using System.Text;

namespace Trabajo_Final_Mobile_Api_2026.Helpers;

public static class ClaveHelper
{
    public static string Hashear(string clave, string salt)
    {
        var bytes = Encoding.UTF8.GetBytes(clave + salt);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }
}
