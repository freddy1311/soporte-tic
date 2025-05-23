using AutoMapper;
using Domain.Business.Implementation;
using Domain.Business.Interface;
using Domain.Utils;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using soporte_tic.Models.ViewModels;

namespace soporte_tic.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        #region variables
        private readonly IMapper _mapper;
        private readonly IMantenimientoService _mantenimientoService;
        private readonly IConfiguracionODTService _configuracionODTService;
        #endregion

        #region constructor
        public MantenimientoController(
            IMapper mapper,
            IMantenimientoService mantenimientoService,
            IConfiguracionODTService configuracionODTService
        )
        {
            _mapper = mapper;
            _mantenimientoService = mantenimientoService;
            _configuracionODTService = configuracionODTService;
        }
        #endregion

        #region métodos
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConfiguracionOdt()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetConfiguraciones()
        {
            var rmConfiguraciones = await _configuracionODTService.GetListConfiguracionODT();
            List<ConfiguracionOdt> configuraciones = rmConfiguraciones.Result;

            List<VMConfiguracionODT> vmConfiguraciones = _mapper.Map<List<VMConfiguracionODT>>(configuraciones);
            rmConfiguraciones.Result = vmConfiguraciones;

            return Json(rmConfiguraciones);
        }

        [HttpGet]
        public IActionResult CreateConfiguracionODT()
        {
            VMConfiguracionODT vmConfiguracionODT = new VMConfiguracionODT();
            return PartialView(vmConfiguracionODT);
        }

        [HttpPost]
        public async Task<JsonResult> CreateConfiguracionODT([FromBody] VMConfiguracionODT model)
        {
            ConfiguracionOdt configuracionCreate = _mapper.Map<ConfiguracionOdt>(model);
            configuracionCreate.CodtId = (configuracionCreate.CodtId != "") ? configuracionCreate.CodtId!.ToUpper() : "";
            
            var rm = await _configuracionODTService.Create(configuracionCreate);
            if (rm.Response)
            {
                VMConfiguracionODT configuracionAdd = _mapper.Map<VMConfiguracionODT>(rm.Result);
                rm.Result = configuracionAdd;
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateConfiguracionODT(long codConfiguracion)
        {
            var rmConfiguracion = await _configuracionODTService.GetConfiguracionODT(codConfiguracion);
            ConfiguracionOdt configuracion = rmConfiguracion.Result;

            VMConfiguracionODT vmConfiguracion = _mapper.Map<VMConfiguracionODT>(configuracion);

            return PartialView(vmConfiguracion);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateConfiguracionODT(VMConfiguracionODT model)
        {
            ConfiguracionOdt configuracionUpdate = _mapper.Map<ConfiguracionOdt>(model);
            configuracionUpdate.CodtId = (configuracionUpdate.CodtId != "") ? configuracionUpdate.CodtId!.ToUpper() : "";

            var rm = await _configuracionODTService.Update(configuracionUpdate);

            if (rm.Response)
            {
                VMConfiguracionODT configuracionUpdated = _mapper.Map<VMConfiguracionODT>(rm.Result);
                rm.Result = configuracionUpdated;
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<JsonResult> DeleteConfiguracionODT(long codConfiguracion)
        {
            var rmConfiguracion = await _configuracionODTService.GetConfiguracionODT(codConfiguracion);
            ConfiguracionOdt configuracionDelete = rmConfiguracion.Result;
            configuracionDelete.CodtEstado = 2;

            var rm = await _configuracionODTService.Update(configuracionDelete);

            return Json(rm);
        }

        [HttpGet]
        public async Task<JsonResult> GetListODTSOpen()
        {
            var rmOrdenesAbiertas = await _mantenimientoService.GetListODTOpen();
            List<OrdenTrabajo> ordenesAbiertas = rmOrdenesAbiertas.Result;

            List<VMOrdenTrabajo> vmOrdenes = _mapper.Map<List<VMOrdenTrabajo>>(ordenesAbiertas);
            rmOrdenesAbiertas.Result = vmOrdenes;

            return Json(rmOrdenesAbiertas);
        }

        [HttpGet]
        public IActionResult CreateOrdenTrabajo()
        {
            VMOrdenTrabajo vmOrdenTrabajo = new VMOrdenTrabajo();
            vmOrdenTrabajo.UsuaResponsable = null;
            vmOrdenTrabajo.UsuaRevisa = null;
            return PartialView(vmOrdenTrabajo);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrdenTrabajo([FromBody] VMOrdenTrabajo model)
        {
            #region get config ODTs
            var rmConfigOrdenes = await _configuracionODTService.GetConfiguracionODTActivo();
            var configOrdenes = (ConfiguracionOdt)rmConfigOrdenes.Result;
            if (configOrdenes == null)
            {
                var rm1 = new ResponseModel();
                rm1.SetResponse(false, "No se encontró configuración de ODT's.");

                return Json(rm1);
            }
            #endregion 

            OrdenTrabajo ordenCreate = _mapper.Map<OrdenTrabajo>(model);
            ordenCreate.CodtCodigo = configOrdenes.CodtCodigo;

            var rm = await _mantenimientoService.Create(ordenCreate);
            if (rm.Response)
            {
                VMOrdenTrabajo ordenMaquinariaAdd = _mapper.Map<VMOrdenTrabajo>(rm.Result);
                rm.Result = ordenMaquinariaAdd;
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrdenTrabajo(long codOrden)
        {
            var rmOrden = await _mantenimientoService.GetODT(codOrden);
            VMOrdenTrabajo orden = new VMOrdenTrabajo();

            if (rmOrden.Response)
            {
                orden = _mapper.Map<VMOrdenTrabajo>(rmOrden.Result);
            }

            return PartialView(orden);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateOrdenTrabajo([FromBody] VMOrdenTrabajo model)
        {
            OrdenTrabajo ordenUpdate = _mapper.Map<OrdenTrabajo>(model);
            var rm = await _mantenimientoService.Update(ordenUpdate);
        
            return Json(rm);
        }

        #endregion
    }
}
