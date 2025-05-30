using Domain.Business.Interface;
using Domain.Utils;
using Domain.Utils.Utils;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Implementation
{
    public class MantenimientoDetalleService : IMantenimientoDetalleService
    {
        #region variables
        private readonly IGenericRepository<DetalleOdt> _ctx;
        private readonly IUtilsService _utilsService;
        #endregion

        #region constructor
        public MantenimientoDetalleService(
            IGenericRepository<DetalleOdt> ctx,
            IUtilsService utilsService
        )
        {
            _ctx = ctx;
            _utilsService = utilsService;
        }
        #endregion

        #region métodos
        public async Task<Utils.ResponseModel> Create(DetalleOdt entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                entity.DodtFecha = _utilsService.GetCurrentDate();

                var rmCreate = await _ctx.Insert(entity);
                if (rmCreate.Response)
                {
                    DetalleOdt detalleCreated = (DetalleOdt)rmCreate.Result;

                    rm.SetResponse(true, "Detalle de ODT agregado exitosamente!.", "Detalle ODT", detalleCreated);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo crear el detalle de la ODT!.", "Detalle ODT");
                }

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo crear el detalle de la ODT: {ex.Message}.", "Detalle ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Delete(long codDetalle)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmUser = await _ctx.Get(u => u.DodtCodigo == codDetalle);

                if (rmUser.Response)
                {
                    DetalleOdt detalleToDelete = (DetalleOdt)rmUser.Result;


                    var rmDetalleDelete = await _ctx.Delete(detalleToDelete);

                    if (rmDetalleDelete.Response)
                    {
                        rm.SetResponse(true, "Detalle de ODT eliminado exitosamente!.", "Detalle ODT");
                    }
                    else
                    {
                        rm.SetResponse(false, "No se pudo eliminar el detalle de la ODT!.", "Detalle ODT");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar el detalle de la ODT a eliminar!.", "Detalle ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar el detalle de la ODT: {ex.Message}.", "Detalle ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> DeleteAll(long codOrden)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.OrtrCodigo == codOrden);
                IQueryable<DetalleOdt> query = (IQueryable<DetalleOdt>)rmQuery.Result;
                
                if (query.ToList().Count > 0)
                {
                    var detallesToDelete = query.ToList();

                    foreach (var item in detallesToDelete)
                    {
                        var rmDetalleDelete = await _ctx.Delete(item);
                    }

                    rm.SetResponse(true, "Tarea de ODT eliminado exitosamente!.", "Detalle ODT");

                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar el detalle de la ODT a eliminar!.", "Detalle ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar el detalle de la ODT: {ex.Message}.", "Detalle ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetListDetalleOdt(long codOrden)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.OrtrCodigo == codOrden);
                IQueryable<DetalleOdt> query = (IQueryable<DetalleOdt>)rmQuery.Result;

                if (query.ToList().Count > 0)
                {
                    var detallesOrden = query
                        .Include(d => d.TamaCodigoNavigation)
                        .Include(d => d.OrtrCodigoNavigation)
                        .ToList();

                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Detalles ODT", detallesOrden);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar los detalles de la ODT a eliminar!.", "Detalles ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar el detalle de la ODT: {ex.Message}.", "Detalles ODT");
            }

            return rm;
        }

        public async Task<int> CountTareasOdt(long codOrden)
        {
            int count = 0;

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.OrtrCodigo == codOrden);
                IQueryable<DetalleOdt> query = (IQueryable<DetalleOdt>)rmQuery.Result;

                if (query.ToList().Count > 0)
                {
                    count = (int)query.ToList().Count;
                }
                else
                {
                    count = 0;
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public async Task<Utils.ResponseModel> Update(DetalleOdt entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {

                #region reassign value user
                var rmQuery = await _ctx.Get(u => u.DodtCodigo == entity.DodtCodigo);
                IQueryable<DetalleOdt> queryUser = rmQuery.Result;

                if (queryUser != null)
                {
                    DetalleOdt detalleUpdate = queryUser.First();

                    detalleUpdate.DodtResultado = entity.DodtResultado;
                    detalleUpdate.DodtObservacion = entity.DodtObservacion;

                    var rmUpdate = await _ctx.Update(detalleUpdate);

                    if (rmUpdate.Response)
                    {
                        DetalleOdt ordenUpdated = detalleUpdate;
                        rm.SetResponse(true, "El detalle de ODT fue actualizado correctamente!.", "Detalle ODT", ordenUpdated);
                    }
                    else
                    {
                        rm.SetResponse(true, "No se pudo actualizar el detalle de ODT!.", "Detalle ODT");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo el detalle de ODT a actualizar!.", "Detalle ODT");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo editar el detalle de ODT: {ex.Message}.", "Detalle ODT");
            }

            return rm;
        }
        #endregion
    }
}
