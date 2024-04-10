using System.Security.Cryptography;
using System.Text;

namespace PizzaStore.Models.Security;

public class Encryptor
{
    private static byte[] key =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    };
    
    public static string EncryptString(string json)
    {
        byte[] iv = new byte[16];
        byte[] result;

        using (Aes aes = Aes.Create()) {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream()) {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream)) {
                        streamWriter.Write(json);
                    }

                    result = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(result);
    }
    
    public static string DecryptString(string cipherText)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using MemoryStream memoryStream = new MemoryStream(buffer);
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }
}