using Domain.Utils;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface IEmpresaService
    {
        #region métodos
        Task<ResponseModel> GetEmpresa();

        Task<ResponseModel> UpdateEmpresa(Empresa entity, Stream logoEmpresa = null);
        #endregion
    }
}
