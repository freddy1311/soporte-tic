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
    public class MaquinariaTareaService : IMaquinariaTareaService
    {
        #region variables
        private readonly IGenericRepository<TareasMaquinaria> _ctx;
        private readonly IUtilsService _utilsService;
        #endregion

        #region constructor
        public MaquinariaTareaService(
            IGenericRepository<TareasMaquinaria> ctx,
            IUtilsService utilsService
        )
        {
            _ctx = ctx;
            _utilsService = utilsService;
        }
        #endregion

        #region métodos
        public async Task<Utils.ResponseModel> Create(TareasMaquinaria entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region check user
                var rmExists = await _ctx.Check(u => u.MaquCodigo == entity.MaquCodigo && u.TamaNombre == entity.TamaNombre);
                if (rmExists.Response)
                {
                    rm.SetResponse(false, "Tarea de Maquinaria existente!.", "Creación Tarea");
                    return rm;
                }
                #endregion

                entity.TamaFechaCreacion = _utilsService.GetCurrentDate();

                var rmCreate = await _ctx.Insert(entity);
                if (rmCreate.Response)
                {
                    TareasMaquinaria tareaCreated = (TareasMaquinaria)rmCreate.Result;

                    rm.SetResponse(true, "La tarea de la maquinaria fue creada exitosamente!.", "Creación Tarea", tareaCreated);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo crear la tarea de la maquinaria!.", "Creación Tarea");
                }

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo crear la tarea de la maquinaria: {ex.Message}.", "Creación Tarea");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Delete(long tareCodigo)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmUser = await _ctx.Get(u => u.TamaCodigo == tareCodigo);

                if (rmUser.Response)
                {
                    TareasMaquinaria tareaToDelete = (TareasMaquinaria)rmUser.Result;

                    tareaToDelete.TamaEstado = 2;

                    var rmTareaDelete = await _ctx.Update(tareaToDelete);

                    if (rmTareaDelete.Response)
                    {
                        rm.SetResponse(true, "Tarea de maquinaria eliminado exitosamente!.", "Eliminar Tarea");
                    }
                    else
                    {
                        rm.SetResponse(false, "No se pudo eliminar la tarea de la maquinaria!.", "Eliminar Tarea");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar la tarea de la maquinaria a eliminar!.", "Eliminar Tarea");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar la tarea de la maquinaria: {ex.Message}.", "Eliminar Tarea");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> DeleteTareasMaquinaria(long maquCodigo)
        {
            var rm = new Utils.ResponseModel();

            try
            {
                var rmTareasMaquinaria = await _ctx.Get(u => u.MaquCodigo == maquCodigo);
                if (rmTareasMaquinaria.Response)
                {
                    List<TareasMaquinaria> tareasToDelete = (List<TareasMaquinaria>)rmTareasMaquinaria.Result;

                    foreach (var item in tareasToDelete)
                    {
                        var rmTareaDelete = await _ctx.Update(item);
                    }

                    rm.SetResponse(true, "Tareas de maquinaria eliminadas exitosamente!.", "Eliminar Tareas");   
                }
                else
                {
                    rm.SetResponse(false, "No se encontraron tareas de la maquinaria!.", "Eliminar Tareas");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar las tareas de la maquinaria: {ex.Message}.", "Eliminar Tareas");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetAllTareasMaquinaria()
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.TamaEstado == 1);
                IQueryable<TareasMaquinaria> query = (IQueryable<TareasMaquinaria>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var users = query.
                        Include(m => m.MaquCodigoNavigation).
                        OrderBy(m => m.MaquCodigoNavigation.MaquNombre).
                        OrderBy(m => m.TamaNombre).
                        ToList();

                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Tareas Maquinaria", users);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo las tareas de maquinaria!.", "Tareas Maquinaria");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de tareas de maquinarias: {ex.Message}.", "Tareas Maquinaria");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetTareaMaquinaria(long tareCodigo)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.TamaCodigo == tareCodigo);
                IQueryable<TareasMaquinaria> query = (IQueryable<TareasMaquinaria>)rmQuery.Result;

                if (query.ToList().Count > 0)
                {
                    var users = query.
                        Include(t => t.MaquCodigoNavigation).
                        FirstOrDefault();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Tareas Maquinarias", users);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo lista de tareas de maquinarias!.", "Tareas Maquinarias");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de tareas de maquinarias: {ex.Message}.", "Tareas Maquinarias");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetTareasMaquinaria(long maquCodigo)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.TamaEstado == 1 && u.MaquCodigo == maquCodigo);
                IQueryable<TareasMaquinaria> query = (IQueryable<TareasMaquinaria>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var users = query.
                        Include(m => m.MaquCodigoNavigation).
                        OrderBy(m => m.TamaNombre).
                        ToList();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Tareas Maquinaria", users);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo las tareas de maquinaria!.", "Tareas Maquinaria");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de tareas de maquinarias: {ex.Message}.", "Tareas Maquinaria");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Update(TareasMaquinaria entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region check user
                var rmExists = await _ctx.Get(u => u.TamaNombre == entity.TamaNombre &&
                    u.MaquCodigo != entity.MaquCodigo);

                if (rmExists.Response)
                {
                    rm.SetResponse(false, "Tarea existente!.", "Actualización Tarea");
                    return rm;
                }
                #endregion

                #region reassign value user
                var rmQuery = await _ctx.Get(u => u.TamaCodigo == entity.TamaCodigo);
                IQueryable<TareasMaquinaria> queryUser = rmQuery.Result;

                if (queryUser != null)
                {
                    TareasMaquinaria maquinariaUpdate = queryUser.First();

                    maquinariaUpdate.TamaFechaAct = _utilsService.GetCurrentDate();
                    maquinariaUpdate.TamaNombre = entity.TamaNombre;
                    maquinariaUpdate.TamaDescripcion = entity.TamaDescripcion;
                    maquinariaUpdate.TamaEstado = entity.TamaEstado;

                    var rmUpdate = await _ctx.Update(maquinariaUpdate);

                    if (rmUpdate.Response)
                    {
                        TareasMaquinaria maquinariaUpdated = maquinariaUpdate;
                        rm.SetResponse(true, "La tarea de maquinaria fue actualizada correctamente!.", "Actualización Tarea", maquinariaUpdated);
                    }
                    else
                    {
                        rm.SetResponse(true, "No se pudo actualizar la maquinaria!.", "Actualización Tarea");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la tarea de maquinaria a actualizar!.", "Actualización Tarea");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo editar la tarea de maquinaria: {ex.Message}.", "Actualización Tarea");
            }

            return rm;
        }
        #endregion
    }
}
