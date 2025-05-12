using Domain.Business.Interface;
using Domain.Utils;
using Domain.Utils.Utils;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Implementation
{
    public class ConfiguracionODTService : IConfiguracionODTService
    {
        #region variables
        private readonly IGenericRepository<ConfiguracionOdt> _ctx;
        private readonly IUtilsService _utilsService;
        #endregion

        #region constructor
        public ConfiguracionODTService(
            IGenericRepository<ConfiguracionOdt> ctx,
            IUtilsService utilsService
        )
        {
            _ctx = ctx;
            _utilsService = utilsService;
        }
        #endregion

        #region methods
        public async Task<Utils.ResponseModel> Create(ConfiguracionOdt entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                entity.CodtFecha = _utilsService.GetCurrentDate();

                var rmCreate = await _ctx.Insert(entity);
                if (rmCreate.Response)
                {
                    ConfiguracionOdt configCreated = (ConfiguracionOdt)rmCreate.Result;

                    rm.SetResponse(true, "La configuración de ODT's fue creado exitosamente!.", "Creación Configuración ODT", configCreated);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo crear la configuración de ODT's!.", "Creación Configuración ODT");
                }

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo crear la configuración de ODT's: {ex.Message}.", "Creación Configuración ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Delete(long id)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmUser = await _ctx.Get(u => u.CodtCodigo == id);

                if (rmUser.Response)
                {
                    ConfiguracionOdt configuracionToDelete = (ConfiguracionOdt)rmUser.Result;
                    configuracionToDelete.CodtEstado = 2;

                    var rmConfigDelete = await _ctx.Update(configuracionToDelete);

                    if (rmConfigDelete.Response)
                    {
                        rm.SetResponse(true, "Configuración de ODT's eliminada exitosamente!.", "Eliminar Configuración ODT");
                    }
                    else
                    {
                        rm.SetResponse(false, "No se pudo eliminar la configuración de ODT's!.", "Eliminar Configuración ODT");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar la configuración de ODT's a eliminar!.", "Eliminar Configuración ODT");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar la configuración de ODT's: {ex.Message}.", "Eliminar Configuración ODT");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetConfiguracionODT(long confCodigo)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.Get(u => u.CodtCodigo == confCodigo);
                IQueryable<ConfiguracionOdt> query = (IQueryable<ConfiguracionOdt>)rmQuery.Result;

                if (query.ToList().Count > 0)
                {
                    var configuracion = query.FirstOrDefault();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Configuración ODT's", configuracion);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la configuración de ODT's!.", "Configuración ODT's");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la configuración de ODT's: {ex.Message}.", "Configuración ODT's");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetConfiguracionODTActivo()
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.Get(u => u.CodtEstado == 1);
                IQueryable<ConfiguracionOdt> query = (IQueryable<ConfiguracionOdt>)rmQuery.Result;

                if (query.ToList().Count > 0)
                {
                    var configuracion = query.FirstOrDefault();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Configuración ODT's", configuracion);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la configuración de ODT's!.", "Configuración ODT's");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la configuración de ODT's: {ex.Message}.", "Configuración ODT's");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetListConfiguracionODT()
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _ctx.GetAll();
                IQueryable<ConfiguracionOdt> query = (IQueryable<ConfiguracionOdt>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var configuraciones = query.
                        OrderByDescending(m => m.CodtVersion).
                        ToList();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Configuración ODT's", configuraciones);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo el listado de configuraciones de ODT's!.", "Configuración ODT's");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de configuraciones de ODT's: {ex.Message}.", "Configuración ODT's");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Update(ConfiguracionOdt entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region reassign value user
                var rmQuery = await _ctx.Get(u => u.CodtCodigo == entity.CodtCodigo);
                IQueryable<ConfiguracionOdt> queryUser = rmQuery.Result;

                if (queryUser != null)
                {
                    ConfiguracionOdt configuracionUpdate = queryUser.First();

                    configuracionUpdate.CodtVersion = entity.CodtVersion;
                    configuracionUpdate.CodtId = entity.CodtId;
                    configuracionUpdate.CodtEstado = entity.CodtEstado;
                    configuracionUpdate.CodtObservacion = entity.CodtObservacion;

                    var rmUpdate = await _ctx.Update(configuracionUpdate);

                    if (rmUpdate.Response)
                    {
                        ConfiguracionOdt configuracionUpdated = configuracionUpdate;
                        rm.SetResponse(true, "La configuración de ODT's fue actualizada correctamente!.", "Configuración ODT's", configuracionUpdated);
                    }
                    else
                    {
                        rm.SetResponse(true, "No se pudo actualizar la configuración de ODT's!.", "Configuración ODT's");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo la maquinaria a actualizar!.", "Configuración ODT's");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo editar la configuración: {ex.Message}.", "Configuración ODT's");
            }

            return rm;
        }
        #endregion
    }
}
