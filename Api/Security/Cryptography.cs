using System.Security.Cryptography;
using System.Text;

namespace Api.Security
{
    public class Cryptography
    {
        public static string Encrypt(string password)
        {
            string key = "0ram@1234xxxxxxxxxxtttttuuuuuiiiiio";
            var clearBytes = Encoding.Unicode.GetBytes(password);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(key, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                password = Convert.ToBase64String(ms.ToArray());
            }
            return password;
        }

        public static string Decrypt(string cipher)
        {
            string key = "0ram@1234xxxxxxxxxxtttttuuuuuiiiiio";
            cipher = cipher.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipher);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(key, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipher = Encoding.Unicode.GetString(ms.ToArray());
            }
            return cipher;
        }
    }
}
