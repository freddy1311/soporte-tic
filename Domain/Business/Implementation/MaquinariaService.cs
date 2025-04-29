using Domain.Business.Interface;
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
    public class MaquinariaService : IMaquinariaService
    {
        #region variables
        private readonly IGenericRepository<Maquinaria> _ctx;
        private readonly IUtilsService _utilsService;
        #endregion

        #region constructor
        public MaquinariaService(
            IGenericRepository<Maquinaria> ctx,
            IUtilsService utilsService
        )
        {
            _ctx = ctx;
            _utilsService = utilsService;
        }
        #endregion

        #region métodos
        public async Task<Utils.ResponseModel> Create(Maquinaria entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region check user
                var rmExists = await _ctx.Check(u => u.MaquCodigoFk == entity.MaquCodigoFk && u.MaquNombre == entity.MaquNombre);
                if (rmExists.Response)
                {
                    rm.SetResponse(false, "Maquinaria existente!.", "Creación Maquinaria");
                    return rm;
                }
                #endregion

                entity.MaquFechaCreacion = _utilsService.GetCurrentDate();

                var rmCreate = await _ctx.Insert(entity);
                if (rmCreate.Response)
                {
                    Maquinaria userCreated = (Maquinaria)rmCreate.Result;

                    rm.SetResponse(true, "La maquinaria fue creado exitosamente!.", "Creación Maquinaria", entity);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo crear la maquinaria!.", "Creación Maquinaria");
                }

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo crear la maquinaria: {ex.Message}.", "Creación Maquinaria");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Delete(long id)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmUser = await _ctx.Get(u => u.MaquCodigo == id);

                if (rmUser.Response)
                {
                    Maquinaria maquinariaToDelete = (Maquinaria)rmUser.Result;

                    maquinariaToDelete.MaquEstado = 2;

                    var rmUserDelete = await _ctx.Update(maquinariaToDelete);

                    if (rmUserDelete.Response)
                    {
                        rm.SetResponse(true, "Maquinaria eliminado exitosamente!.", "Eliminar Maquinaria");
                    }
                    else
                    {
                        rm.SetResponse(false, "No se pudo eliminar la maquinaria!.", "Eliminar Maquinaria");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar la maquinaria a eliminar!.", "Eliminar Maquinaria");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar la maquinaria: {ex.Message}.", "Eliminar Maquinaria");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetMaquinaria(long codMaquinaria)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.MaquCodigo == codMaquinaria);
                IQueryable<Usuario> query = (IQueryable<Usuario>)rmQuery.Result;

                if (query.ToList().Count > 0)
                {
                    var users = query.FirstOrDefault();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Maquinarias", users);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo lista de maquinarias!.", "Maquinarias");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de maquinarias: {ex.Message}.", "Maquinarias");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetMaquinarias(long sucuCodigo)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.MaquEstado == 1 && u.SucuCodigo == sucuCodigo);
                IQueryable<Maquinaria> query = (IQueryable<Maquinaria>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var users = query.
                    OrderBy(m => m.MaquTipo).
                    OrderBy(m => m.MaquNombre).
                    ToList();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Maquinarias", users);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la maquinaria!.", "Maquinarias");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de maquinarias: {ex.Message}.", "Maquinarias");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetMaquinariasComponente(long codMaquinariaPadre)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll(u => u.MaquEstado == 1 && u.MaquCodigoFk == codMaquinariaPadre);
                IQueryable<Maquinaria> query = (IQueryable<Maquinaria>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var users = query.
                    OrderBy(m => m.MaquTipo).
                    OrderBy(m => m.MaquNombre).
                    ToList();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Maquinarias", users);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo lista de componente!.", "Maquinarias");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de maquinarias componente: {ex.Message}.", "Maquinarias");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Update(Maquinaria entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region check user
                var rmExists = await _ctx.Get(u => u.MaquNombre == entity.MaquNombre &&
                    u.MaquCodigo != entity.MaquCodigo);

                if (rmExists.Response)
                {
                    rm.SetResponse(false, "Usuario existente!.", "Actualización Usuario");
                    return rm;
                }
                #endregion

                #region reassign value user
                var rmQuery = await _ctx.Get(u => u.MaquCodigo == entity.MaquCodigo);
                IQueryable<Maquinaria> queryUser = rmQuery.Result;

                if (queryUser != null)
                {
                    Maquinaria maquinariaUpdate = queryUser.First();

                    maquinariaUpdate.MaquFechaAct = _utilsService.GetCurrentDate();
                    maquinariaUpdate.MaquNombre = entity.MaquNombre;
                    maquinariaUpdate.MaquDescripcion = entity.MaquDescripcion;
                    maquinariaUpdate.MaquTipo = entity.MaquTipo;
                    maquinariaUpdate.MaquEstado = entity.MaquEstado;
                    maquinariaUpdate.SucuCodigo = entity.SucuCodigo;
                    maquinariaUpdate.MaquCodigoFk = entity.MaquCodigoFk;

                    var rmUpdate = await _ctx.Update(maquinariaUpdate);

                    if (rmUpdate.Response)
                    {
                        Maquinaria maquinariaUpdated = maquinariaUpdate;
                        rm.SetResponse(true, "La maquinaria fue actualizada correctamente!.", "Actualización Maquinaria", maquinariaUpdated);
                    }
                    else
                    {
                        rm.SetResponse(true, "No se pudo actualizar la maquinaria!.", "Actualización Maquinaria");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la maquinaria a actualizar!.", "Actualización Maquinaria");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo editar la maquinaria: {ex.Message}.", "Actualización Maquinaria");
            }

            return rm;
        }

        #endregion
    }
}
