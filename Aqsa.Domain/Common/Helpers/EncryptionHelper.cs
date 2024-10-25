using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Aqsa.Domain.Common;
public abstract class EncryptionHelper
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456");

    public static string EncryptString(string plainText)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    var encrypted = msEncrypt.ToArray();
                    return Convert.ToBase64String(encrypted);
                }
            }
        }
    }

    public static string DecryptString(string cipherText)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public static string UrlSafeBase64Encode(string input)
    {
        return HttpUtility.UrlEncode(input).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }

    public static string UrlSafeBase64Decode(string input)
    {
        var base64 = input.Replace("-", "+").Replace("_", "/");
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return HttpUtility.UrlDecode(base64);
    }

    public static string EncryptAndEncode(string plainText)
    {
        var encrypted = EncryptString(plainText);
        return UrlSafeBase64Encode(encrypted);
    }

    public static string DecodeAndDecrypt(string encodedText)
    {
        var decoded = UrlSafeBase64Decode(encodedText);
        return DecryptString(decoded);
    }
}
