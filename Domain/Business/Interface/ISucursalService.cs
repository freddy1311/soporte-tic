using Domain.Utils;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface ISucursalService
    {
        #region methods
        Task<ResponseModel> GetSucursalesEmpresa(long idEmpresa);

        Task<ResponseModel> GetSucursal(long idSucursal);

        Task<ResponseModel> Update(Sucursal entity);
        #endregion
    }
}
