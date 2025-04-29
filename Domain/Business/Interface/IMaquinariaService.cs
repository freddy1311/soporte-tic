using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface IMaquinariaService
    {
        #region métodos
        Task<Utils.ResponseModel> GetMaquinarias(long sucuCodigo);

        Task<Utils.ResponseModel> GetMaquinariasComponente(long codMaquinariaPadre);

        Task<Utils.ResponseModel> GetMaquinaria(long codMaquinaria);

        Task<Utils.ResponseModel> Create(Maquinaria entity);

        Task<Utils.ResponseModel> Update(Maquinaria entity);

        Task<Utils.ResponseModel> Delete(long id);
        #endregion
    }
}
