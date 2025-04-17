using Domain.Utils;
using Domain.Utils.Utils;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface IConfiguracionService
    {
        #region métodos
        Task<ResponseModel> GetConfiguracion(long codConfig);

        Task<ResponseModel> GetListConfiguracion(long codSucursal);

        Task<ResponseModel> Create(ConfiguracionGeneral entity);

        Task<ResponseModel> Update(ConfiguracionGeneral entity);

        Task<ResponseModel> Delete(long codConfig);
        #endregion
    }
}
