using Domain.Business.Interface;
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
    public class ConfiguracionService: IConfiguracionService
    {
        #region properties
        private readonly IGenericRepository<ConfiguracionGeneral> _ctx;
        private readonly IUtilsService _utilsService;
        #endregion

        #region constructor
        public ConfiguracionService(
            IGenericRepository<ConfiguracionGeneral> ctx,
            IUtilsService utilsService
        )
        {
            _ctx = ctx;
            _utilsService = utilsService;
        }
        #endregion

        #region methods
        public async Task<Utils.ResponseModel> Create(ConfiguracionGeneral entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();
            string titleResponse = "Creación Configuración";

            try
            {
                #region check configuracion
                var rmExists = await _ctx.Get(c => c.CogeService == entity.CogeService &&
                    c.CogeKey == entity.CogeKey);

                IQueryable<ConfiguracionGeneral> queryExists = (IQueryable<ConfiguracionGeneral>)rmExists.Result;
                var configExists = queryExists.FirstOrDefault();

                if (configExists != null)
                {
                    rm.SetResponse(false, "Configuración existente!.", titleResponse);
                    return rm;
                }
                #endregion

                #region create cliente
                entity.CogeFecha = _utilsService.GetCurrentDate();
                var rmCreate = await _ctx.Insert(entity);

                if (rmCreate.Response)
                {
                    ConfiguracionGeneral configCreated = (ConfiguracionGeneral)rmCreate.Result;

                    rm.SetResponse(true, "Configuración creada exitosamente!.", titleResponse, configCreated);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo crear la configuración!.", titleResponse);
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error en CreateConfiguracion: {ex.Message}");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Delete(long codConfig)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();
            string titleResponse = "Eliminar Configuración";

            try
            {
                #region search configuracion
                var rmQuery = await _ctx.Get(u => u.CogeCodigo == codConfig);
                ConfiguracionGeneral confDelete = (ConfiguracionGeneral)rmQuery.Result;

                if (!rmQuery.Response)
                {
                    rm.SetResponse(false, "No se obtuvo la configuración que desea eliminar!.", titleResponse);
                    return rm;
                }
                #endregion

                #region delete configuracion
                var rmDelete = await _ctx.Delete(confDelete);

                if (rmDelete.Response)
                {
                    rm.SetResponse(true, "La configuración fue eliminada correctamente!.", titleResponse);
                }
                else
                {
                    rm.SetResponse(false, "No se pudo eliminar la configuración seleccionada!.", titleResponse);
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error en DeleteConfiguracion: {ex.Message}");
            }

            return rm;
        }

        public string GenerateKey()
        {
            throw new NotImplementedException();
        }

        public string GenerateKeyProducto(string texto)
        {
            throw new NotImplementedException();
        }

        public async Task<Utils.ResponseModel> GetConfiguracion(long codConfig)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();
            string titleResponse = "Eliminar Configuración";

            try
            {
                #region search configuracion
                var rmQuery = await _ctx.Get(c => c.CogeCodigo == codConfig);
                if (!rmQuery.Response)
                {
                    rm.SetResponse(false, "No se pudo obtener la configuración deseada!.", titleResponse);
                    return rm;
                }

                IQueryable<ConfiguracionGeneral> queryConfig = rmQuery.Result;
                ConfiguracionGeneral configuracionGet = queryConfig.FirstOrDefault()!;

                rm.SetResponse(true, "Consulta exitosa!.", titleResponse, configuracionGet);
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error en GetConfiguracion: {ex.Message}");
            }

            return rm;
        }

        public DateTime GetCurrentDate()
        {
            throw new NotImplementedException();
        }

        public async Task<Utils.ResponseModel> GetListConfiguracion(long codSucursal)
        {
            Utils.  ResponseModel rm = new Utils.ResponseModel();
            string titleResponse = "Obtener Lista Configuración";

            try
            {
                #region search configuraciones
                var rmQuery = await _ctx.GetAll(c => c.EmprCodigo == codSucursal);
                IQueryable<ConfiguracionGeneral> configuracionesGet = (IQueryable<ConfiguracionGeneral>)rmQuery.Result;

                if (!rmQuery.Response)
                {
                    rm.SetResponse(false, "No se pudo obtener listado de configuraciones!.", titleResponse);
                    return rm;
                }

                rm.SetResponse(true, "Consulta exitosa!.", titleResponse, configuracionesGet);
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error en GetListConfiguracion: {ex.Message}.", titleResponse);
            }

            return rm;
        }

        public string IncrementarNumeroEnString(string input)
        {
            throw new NotImplementedException();
        }

        public async Task<Utils.ResponseModel> Update(ConfiguracionGeneral entity)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();
            string titleResponse = "Actualización Configuración";

            try
            {
                #region check configuracion
                var rmExists = await _ctx.Get(c => c.CogeService == entity.CogeService &&
                    c.CogeKey == entity.CogeKey &&
                    c.CogeCodigo != entity.CogeCodigo);

                if (rmExists.Response)
                {
                    rm.SetResponse(false, "Configuración existente!.", titleResponse);
                    return rm;
                }
                #endregion

                #region search condifuración
                var rmQuery = await _ctx.Get(c => c.CogeCodigo == entity.CogeCodigo);
                ConfiguracionGeneral configUpdate = (ConfiguracionGeneral)rmQuery.Result;

                if (!rmQuery.Response)
                {
                    rm.SetResponse(false, "No se obtuvo la configuración que desea actualizar!.", titleResponse);
                    return rm;
                }
                #endregion

                #region reassign data
                configUpdate.CogeService = entity.CogeService;
                configUpdate.CogeKey = entity.CogeKey;
                configUpdate.CogeValue = entity.CogeValue;
                configUpdate.EmprCodigo = entity.EmprCodigo;
                #endregion

                #region update configuracion
                var rmUpdate = await _ctx.Update(configUpdate);

                if (rmUpdate.Response)
                {
                    rm.SetResponse(true, "La configuración fue actualizada correctamente!.", titleResponse, configUpdate);
                }
                else
                {
                    rm.SetResponse(true, "No se pudo actualizar la configuración!.", titleResponse);
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error en UpdateConfiguracion: {ex.Message}.", titleResponse);
            }

            return rm;
        }
        #endregion
    }
}
