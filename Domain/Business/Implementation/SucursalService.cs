using Domain.Business.Interface;
using Domain.Utils;
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
    public class SucursalService : ISucursalService
    {
        #region properties
        private readonly IGenericRepository<Sucursal> _ctx;
        #endregion

        #region constructor
        public SucursalService(IGenericRepository<Sucursal> ctx)
        {
            _ctx = ctx;
        }
        #endregion

        public async Task<Utils.ResponseModel> GetSucursal(long idSucursal)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region get sucursal
                var rmQuery = await _ctx.GetAll(s => s.SucuCodigo == idSucursal);
                IQueryable<Sucursal> query = (IQueryable<Sucursal>)rmQuery.Result;
                Sucursal sucursal = query.
                    Include(s => s.EmprCodigoNavigation).First();
                #endregion

                #region valid sucursal
                if (sucursal != null)
                {
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Obtener Sucursal", sucursal);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la sucursal seleccionada!.", "Obtener Sucursal");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}.", "Obtener Sucursal");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetSucursalesEmpresa(long idEmpresa)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region get sucursales
                var rmQuery = await _ctx.GetAll(s => s.EmprCodigo == idEmpresa);
                IQueryable<Sucursal> query = (IQueryable<Sucursal>)rmQuery.Result;
                var sucursal = query.Include(empresa => empresa.EmprCodigoNavigation).
                    ToList();
                #endregion

                #region valid sucursal
                if (sucursal != null)
                {
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Obtener Sucursales", sucursal);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la sucursal seleccionada!.", "Obtener Sucursales");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}.", "Obtener Sucursales");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Update(Sucursal entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();
            string titleResponse = "Actualización Sucursal";

            try
            {

                #region reassign value sucursal
                var rmQuery = await _ctx.GetAll(e => e.SucuCodigo == entity.SucuCodigo);
                IQueryable<Sucursal> querySucursal = (IQueryable<Sucursal>)rmQuery.Result;

                if (querySucursal != null)
                {
                    Sucursal sucursalUpdate = querySucursal.First();

                    sucursalUpdate.SucuNombre = entity.SucuNombre;
                    sucursalUpdate.SucuDireccion = entity.SucuDireccion;
                    sucursalUpdate.SucuTelefono = entity.SucuTelefono;
                    sucursalUpdate.SucuResponsable = entity.SucuResponsable;
                    sucursalUpdate.SucuTelefono = entity.SucuTelefono;
                    sucursalUpdate.SucuEmail = entity.SucuEmail;

                    var rmUpdate = await _ctx.Update(sucursalUpdate);

                    if (rmUpdate.Response)
                    {
                        Sucursal sucursalNewUpdated = querySucursal.First();
                        rm.SetResponse(true, "La sucursal fue actualizada correctamente!.", titleResponse, sucursalNewUpdated);
                    }
                    else
                    {
                        rm.SetResponse(true, "No se pudo actualizar la sucursal!.", titleResponse);
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la sucursal a actualizar!.", titleResponse);
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}.", titleResponse);
            }

            return rm;
        }
    }
}
