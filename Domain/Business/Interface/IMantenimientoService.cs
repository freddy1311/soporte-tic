using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface IMantenimientoService
    {
        #region métodos
        Task<Utils.ResponseModel> GetListODT(DateTime fecha = null);
        
        
        #endregion
    }
}
