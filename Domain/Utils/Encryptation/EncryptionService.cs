using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils.Encryptation
{
    public class EncryptionService : IEncryptionService
    {
        public string Hash256Text(string textRaw)
        {
            string hashedText = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(textRaw);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                hashedText = builder.ToString();
            }

            return hashedText;
        }

        public bool VerifyHash(string hashedText, string providedText)
        {
            throw new NotImplementedException();
        }
    }
}
