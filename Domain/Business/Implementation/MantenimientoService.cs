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
    public class MantenimientoService : IMantenimientoService
    {
        #region variables
        private readonly IGenericRepository<OrdenTrabajo> _ctx;
        private readonly IUtilsService _utilsService;
        #endregion

        #region constructor
        public MantenimientoService(
            IGenericRepository<OrdenTrabajo> ctx,
            IUtilsService utilsService
        )
        {
            _ctx = ctx;
            _utilsService = utilsService;
        }
        #endregion

        #region métodos
        public async Task<Utils.ResponseModel> Create(OrdenTrabajo entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                entity.OrtrFechaCreacion = _utilsService.GetCurrentDate();
                entity.OrtrFechaEmision = _utilsService.GetCurrentDate();
                entity.OrtrNúmero = await GetLastNumeroODT();

                if (entity.OrtrNúmero == 0)
                {
                    entity.OrtrNúmero = 1;
                }

                var rmCreate = await _ctx.Insert(entity);
                if (rmCreate.Response)
                {
                    OrdenTrabajo ordenCreated = (OrdenTrabajo)rmCreate.Result;

                    rm.SetResponse(true, "La orden de trabajo fue creada exitosamente!.", "Creación ODT", ordenCreated);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo crear la orden del trabajo!.", "Creación ODT");
                }

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo crear la orden de trabajo: {ex.Message}.", "Creación ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Delete(long codOrden)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmUser = await _ctx.Get(u => u.OrtrCodigo == codOrden);

                if (rmUser.Response)
                {
                    OrdenTrabajo ordenToDelete = (OrdenTrabajo)rmUser.Result;


                    var rmOrdenDelete = await _ctx.Delete(ordenToDelete);

                    if (rmOrdenDelete.Response)
                    {
                        rm.SetResponse(true, "Orden de trabajo eliminada exitosamente!.", "Eliminar ODT");
                    }
                    else
                    {
                        rm.SetResponse(false, "No se pudo eliminar la orden de trabajo!.", "Eliminar ODT");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar la orden de trabajo a eliminar!.", "Eliminar ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar la orden de trabajo: {ex.Message}.", "Eliminar ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetListODT(DateTime? fecha = null)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.OrtrFechaPrevistaInicio!.Value.Date == fecha!.Value.Date);
                IQueryable<OrdenTrabajo> query = (IQueryable<OrdenTrabajo>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var odts = query.
                        Include(m => m.CodtCodigoNavigation).
                        Include(m => m.MaquCodigoNavigation).
                            ThenInclude(f => f.MaquCodigoFkNavigation).
                        Include(m => m.UsuaResponsableNavigation).
                        Include(m => m.UsuaRevisaNavigation).
                        OrderBy(m => m.OrtrNúmero).
                        ToList();

                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "ODT", odts);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo las órdenes de trabajo!.", "ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de órdenes de trabajo: {ex.Message}.", "ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetListODT(DateTime fechaInicio, DateTime fechaFin)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => 
                    u.OrtrFechaEjecucionInicio!.Value.Date >= fechaInicio.Date && 
                    u.OrtrFechaEjecucionFin.Value.Date <= fechaFin.Date);
                IQueryable<OrdenTrabajo> query = (IQueryable<OrdenTrabajo>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var odts = query.
                        Include(m => m.CodtCodigoNavigation).
                        Include(m => m.MaquCodigoNavigation).
                            ThenInclude(f => f.MaquCodigoFkNavigation).
                        Include(m => m.UsuaResponsableNavigation).
                        Include(m => m.UsuaRevisaNavigation).
                        OrderBy(m => m.OrtrNúmero).
                        ToList();

                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "ODT", odts);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo las órdenes de trabajo!.", "ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de órdenes de trabajo: {ex.Message}.", "ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetListODTOpen()
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u =>
                    u.OrtrFechaEjecucionFin!.Value.Date == null);
                IQueryable<OrdenTrabajo> query = (IQueryable<OrdenTrabajo>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var odts = query.
                        Include(m => m.CodtCodigoNavigation).
                        Include(m => m.MaquCodigoNavigation).
                            ThenInclude(f => f.MaquCodigoFkNavigation).
                        Include(m => m.UsuaResponsableNavigation).
                        Include(m => m.UsuaRevisaNavigation).
                        OrderBy(m => m.OrtrNúmero).
                        ToList();

                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "ODT", odts);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo las órdenes de trabajo!.", "ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de órdenes de trabajo: {ex.Message}.", "ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetListODTPendientes()
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u =>
                    u.DetalleOdts.Any(d => d.DodtResultado == 2 || d.DodtResultado == 3));
                IQueryable<OrdenTrabajo> query = (IQueryable<OrdenTrabajo>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var odts = query.
                        Include(m => m.CodtCodigoNavigation).
                        Include(m => m.MaquCodigoNavigation).
                            ThenInclude(f => f.MaquCodigoFkNavigation).
                        Include(m => m.UsuaResponsableNavigation).
                        Include(m => m.UsuaRevisaNavigation).
                        OrderBy(m => m.OrtrNúmero).
                        ToList();

                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "ODT", odts);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo las órdenes de trabajo!.", "ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de órdenes de trabajo: {ex.Message}.", "ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetODT(long codOrden)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.Get(u =>
                    u.OrtrCodigo == codOrden);
                IQueryable<OrdenTrabajo> query = (IQueryable<OrdenTrabajo>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var odts = query.
                        Include(m => m.CodtCodigoNavigation).
                        Include(m => m.MaquCodigoNavigation).
                            ThenInclude(f => f.MaquCodigoFkNavigation).
                        Include(m => m.UsuaResponsableNavigation).
                        Include(m => m.UsuaRevisaNavigation).
                        FirstOrDefault();

                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "ODT", odts);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la orden de trabajo!.", "ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la orden de trabajo: {ex.Message}.", "ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Update(OrdenTrabajo entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {

                #region reassign value user
                var rmQuery = await _ctx.Get(u => u.OrtrCodigo == entity.OrtrCodigo);
                IQueryable<OrdenTrabajo> queryUser = rmQuery.Result;

                if (queryUser != null)
                {
                    OrdenTrabajo ordenUpdate = queryUser.First();

                    ordenUpdate.OrtrFechaPrevistaInicio = entity.OrtrFechaPrevistaInicio;
                    ordenUpdate.OrtrFechaPrevistaFin = entity.OrtrFechaPrevistaFin;
                    ordenUpdate.OrtrFechaEjecucionInicio = entity.OrtrFechaEjecucionInicio;
                    ordenUpdate.OrtrFechaEjecucionFin = entity.OrtrFechaEjecucionFin;
                    ordenUpdate.OrtrSemana = entity.OrtrSemana;
                    ordenUpdate.OrtrTipo = entity.OrtrTipo;
                    ordenUpdate.MaquCodigo = entity.MaquCodigo;
                    ordenUpdate.OrtrObservacion = entity.OrtrObservacion;
                    ordenUpdate.UsuaResponsable = entity.UsuaResponsable;
                    ordenUpdate.UsuaRevisa = entity.UsuaRevisa;

                    var rmUpdate = await _ctx.Update(ordenUpdate);

                    if (rmUpdate.Response)
                    {
                        OrdenTrabajo ordenUpdated = ordenUpdate;
                        rm.SetResponse(true, "La orden de trabajo fue actualizada correctamente!.", "Actualización ODT", ordenUpdated);
                    }
                    else
                    {
                        rm.SetResponse(true, "No se pudo actualizar la orden de trabajo!.", "Actualización ODT");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la orden de trabajo a actualizar!.", "Actualización ODT");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo editar la orden de trabajo: {ex.Message}.", "Actualización ODT");
            }

            return rm;
        }

        public async Task<int> GetLastNumeroODT()
        {
            int lastNum = 0;

            try
            {
                var rmQuery = await _ctx.GetLast(u => u.OrtrNúmero!);
                OrdenTrabajo queryUser = rmQuery.Result;

                if (queryUser != null)
                {
                    OrdenTrabajo ordentrabajo = queryUser;
                    lastNum = (int)ordentrabajo.OrtrNúmero! + 1;
                }
            }
            catch (Exception)
            {
                lastNum = 0;
            }

            return lastNum;
        }
        #endregion
    }
}
