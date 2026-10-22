using System.Security.Cryptography;
using System.Text;

namespace Ede.Uofx.ThirdPartyAd.Sample.Helper
{
    public static class AesHelper
    {
        #region EncodeData

        public static string EncodeData(string sourceContent, byte[] byteKEY, byte[] byteIV)
        {
            var _aes = Aes.Create();
            _aes.Padding = PaddingMode.PKCS7;
            _aes.Mode = CipherMode.CBC;
            _aes.FeedbackSize = 128;
            _aes.Key = byteKEY;
            _aes.IV = byteIV;

            using var encryptor = _aes.CreateEncryptor();
            byte[] final = EncodeData(sourceContent, encryptor);

            return Convert.ToBase64String(final);
        }

        private static byte[] EncodeData(string sourceContent, ICryptoTransform encryptor)
        {
            byte[] dataByteArray = Encoding.UTF8.GetBytes(sourceContent);
            var final = encryptor.TransformFinalBlock(dataByteArray, 0, dataByteArray.Length);
            return final;
        }
        #endregion

        #region DecodeData

        public static string DecodeData(string enSourceContent, byte[] byteKEY, byte[] byteIV)
        {
            var _aes = Aes.Create();
            _aes.Padding = PaddingMode.PKCS7;
            _aes.Mode = CipherMode.CBC;
            _aes.FeedbackSize = 128;
            _aes.Key = byteKEY;
            _aes.IV = byteIV;

            using var decryptor = _aes.CreateDecryptor();
            byte[] final = DecodeData(enSourceContent, decryptor);

            return Encoding.UTF8.GetString(final);
        }

        private static byte[] DecodeData(string enSourceContent, ICryptoTransform decryptor)
        {
            byte[] byteDecrypt = System.Convert.FromBase64String(enSourceContent);
            byte[] decrypted = decryptor.TransformFinalBlock(byteDecrypt, 0, byteDecrypt.Length);
            return decrypted;
        }

        #endregion
    }
}
