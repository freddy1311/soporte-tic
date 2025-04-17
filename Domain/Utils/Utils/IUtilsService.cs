using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils.Utils
{
    public interface IUtilsService
    {
        #region methods
        string GenerateKey();

        DateTime GetCurrentDate();

        string GenerateKeyProducto(string texto);

        string IncrementarNumeroEnString(string input);

        string CapitalizeAfterDot(string input);
        #endregion
    }
}
