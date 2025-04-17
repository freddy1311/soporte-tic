using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils.Encryptation
{
    public interface IEncryptionService
    {
        #region methods
        string Hash256Text(string textRaw);
        bool VerifyHash(string hashedText, string providedText);
        #endregion
    }
}
