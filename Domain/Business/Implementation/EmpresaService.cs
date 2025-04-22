using Domain.Business.Interface;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Implementation
{
    public class EmpresaService: IEmpresaService
    {
        #region properties
        private readonly IGenericRepository<Empresa> _ctx;
        //private readonly IFirebaseService _firebaseService;
        #endregion

        #region constructor
        public EmpresaService(
            IGenericRepository<Empresa> ctx
            //IFirebaseService firebaseService
        )
        {
            _ctx = ctx;
            //_firebaseService = firebaseService;
        }
        #endregion

        #region methods
        public async Task<Utils.ResponseModel> GetEmpresa()
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region get empresa
                var rmQueryEmpresa = await _ctx.Get(e => e.EmprNombre != "");
                IQueryable<Empresa> queryEmpresa = rmQueryEmpresa.Result;
                Empresa empresa = queryEmpresa.FirstOrDefault()!;
                #endregion

                #region check valid empresa
                if (empresa != null)
                {
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Obtener Empresa", empresa);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo obtener la empresa correctamente!.", "Obtener Empresa");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}", "Obtener Empresa");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> UpdateEmpresa(Empresa entity, Stream logo = null)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {

                #region reassign value user
                var rmQuery = await _ctx.Get(e => e.EmprCodigo == entity.EmprCodigo);
                var queryEmpresa = (IQueryable< Empresa>)rmQuery.Result;
                var empresaUpd = queryEmpresa.FirstOrDefault();

                if (empresaUpd != null)
                {
                    Empresa empresaUpdate = empresaUpd;

                    empresaUpdate.EmprNombre = entity.EmprNombre;
                    empresaUpdate.EmprRuc = entity.EmprRuc;
                    empresaUpdate.EmprLogo = entity.EmprLogo;

                    var rmUpdate = await _ctx.Update(empresaUpdate);

                    if (rmUpdate.Response)
                    {
                        Empresa empresaUpdated = queryEmpresa.First();
                        rm.SetResponse(true, "La empresa fue actualizada correctamente!.", "Actualización Empresa", empresaUpdated);
                    }
                    else
                    {
                        rm.SetResponse(true, "No se pudo actualizar el usuario!.", "Actualización Empresa");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo el usuario a actualizar!.", "Actualización Empresa");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}.", "Actualización Empresa");
            }

            return rm;
        }


        #endregion
    }
}
