using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface IConfiguracionODTService
    {
        #region methods
        Task<Utils.ResponseModel> GetConfiguracionODT(long confCodigo);

        Task<Utils.ResponseModel> GetConfiguracionODTActivo();

        Task<Utils.ResponseModel> GetListConfiguracionODT();

        Task<Utils.ResponseModel> Create(ConfiguracionOdt entity);

        Task<Utils.ResponseModel> Update(ConfiguracionOdt entity);

        Task<Utils.ResponseModel> Delete(long id);
        #endregion
    }
}
