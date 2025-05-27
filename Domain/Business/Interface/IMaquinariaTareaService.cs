using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface IMaquinariaTareaService
    {
        #region métodos
        Task<Utils.ResponseModel> GetAllTareasMaquinaria();

        Task<Utils.ResponseModel> GetTareasMaquinaria(long maquCodigo);

        Task<Utils.ResponseModel> GetTareaMaquinaria(long tareCodigo);

        Task<Utils.ResponseModel> Create(TareasMaquinaria entity);

        Task<Utils.ResponseModel> Update(TareasMaquinaria entity);

        Task<Utils.ResponseModel> Delete(long tareCodigo);

        Task<Utils.ResponseModel> DeleteTareasMaquinaria(long maquCodigo);
        #endregion
    }
}
