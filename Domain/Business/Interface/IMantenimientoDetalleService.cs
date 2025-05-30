using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface IMantenimientoDetalleService
    {
        #region métodos
        Task<Utils.ResponseModel> GetListDetalleOdt(long codOrden);

        Task<Utils.ResponseModel> Create(DetalleOdt entity);

        Task<Utils.ResponseModel> Update(DetalleOdt entity);

        Task<Utils.ResponseModel> Delete(long codDetalle);

        Task<Utils.ResponseModel> DeleteAll(long codOrden);

        Task<int> CountTareasOdt(long codOrden);
        #endregion
    }
}
