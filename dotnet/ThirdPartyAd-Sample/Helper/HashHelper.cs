using System.Security.Cryptography;
using System.Text;

namespace Ede.Uofx.ThirdPartyAd.Sample.Helper
{
    public static class HashHelper
    {
        public static string HMACSHA256(string input, string key)
        {
            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public static byte[] SHA256ToBytes(string input)
        {
            using var sha256Hash = SHA256.Create();
            return sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
    }
}
