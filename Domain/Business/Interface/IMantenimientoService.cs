using Infrastructure.Models;
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
        Task<Utils.ResponseModel> GetListODT(DateTime? fecha = null);

        Task<Utils.ResponseModel> GetListODT(DateTime fechaInicio, DateTime fechaFin);

        Task<Utils.ResponseModel> GetListODTOpen();

        Task<Utils.ResponseModel> GetListODTPendientes();

        Task<Utils.ResponseModel> GetODT(long codOrden);

        Task<Utils.ResponseModel> Create(OrdenTrabajo entity);

        Task<Utils.ResponseModel> Update(OrdenTrabajo entity);

        Task<Utils.ResponseModel> Delete(long codOrden);

        Task<int> GetLastNumeroODT();
        #endregion
    }
}
