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
        private readonly IMantenimientoDetalleService _mantenimientoDetalleService;
        private readonly IMaquinariaTareaService _maquinariaTareaService;
        #endregion

        #region constructor
        public MantenimientoController(
            IMapper mapper,
            IMantenimientoService mantenimientoService,
            IConfiguracionODTService configuracionODTService,
            IMantenimientoDetalleService mantenimientoDetalleService,
            IMaquinariaTareaService maquinariaTareaService
        )
        {
            _mapper = mapper;
            _mantenimientoService = mantenimientoService;
            _configuracionODTService = configuracionODTService;
            _mantenimientoDetalleService = mantenimientoDetalleService;
            _maquinariaTareaService = maquinariaTareaService;
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
        public async Task<JsonResult> UpdateConfiguracionODT([FromBody] VMConfiguracionODT model)
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
            if (vmOrdenes.Count > 0)
            {
                foreach (var item in vmOrdenes)
                {
                    var countTareas = await _mantenimientoDetalleService.CountTareasOdt(item.OrtrCodigo);
                    item.CountTareas = countTareas;
                }
            }
            
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

        [HttpDelete]
        public async Task<JsonResult> DeleteOrdenTrabajo(long codOrden)
        {
            var rm = await _mantenimientoService.Delete(codOrden);
            if (rm.Response)
            {
                var rmTareasOrdenDelete = await _mantenimientoDetalleService.DeleteAll(codOrden);
                rm.Message += "\nLas tareas asociadas fueron eliminas exitosamente!.";
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<IActionResult> AddTareasOrdenTrabajo(long codOrden)
        {
            var rmOrdentrabajo = await _mantenimientoService.GetODT(codOrden);
            var ordenTrabajo = (rmOrdentrabajo.Response) ? rmOrdentrabajo.Result : new OrdenTrabajo();
            VMOrdenTrabajo orden = _mapper.Map<VMOrdenTrabajo>(ordenTrabajo);
            return PartialView(orden);
        }

        [HttpPost]
        public async Task<JsonResult> AddTareasOrdenTrabajo([FromBody]List<VMDetalleODT> listaTareas)
        {
            var rm = new ResponseModel();
            List<DetalleOdt> tareasMaquinaria = _mapper.Map<List<DetalleOdt>>(listaTareas);

            foreach (var tarea in tareasMaquinaria)
            {
                var rm1 = await _mantenimientoDetalleService.Create(tarea);
                rm = rm1;
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<IActionResult> ShowOrdenTrabajo(long codOrden)
        {
            var rmOrdentrabajo = await _mantenimientoService.GetODT(codOrden);
            var ordenTrabajo = (rmOrdentrabajo.Response) ? rmOrdentrabajo.Result : new OrdenTrabajo();
            VMOrdenTrabajo orden = _mapper.Map<VMOrdenTrabajo>(ordenTrabajo);

            if (orden.OrtrCodigo > 0)
            {
                #region get tareas odt
                var rmTareas = await _mantenimientoDetalleService.GetListDetalleOdt(orden.OrtrCodigo);
                var tareasOrden = (rmTareas.Response) ? rmTareas.Result : null;
                List<VMDetalleODT> tareas = _mapper.Map<List<VMDetalleODT>>(tareasOrden);
                orden.Tareas = tareas;
                #endregion
            }
            return View(orden);
        }

        [HttpPut]
        public async Task<JsonResult> FinalizeOrdenTrabajo([FromBody] VMOrdenTrabajo model)
        {
            List<DetalleOdt> tareasOrdenes = _mapper.Map<List<DetalleOdt>>(model.Tareas);
            OrdenTrabajo ordenUpdate = _mapper.Map<OrdenTrabajo>(model);
            ordenUpdate.UsuaRevisa = Convert.ToInt64(User.FindFirst("CodigoUsuario")?.Value);
            
            var rm = await _mantenimientoService.Update(ordenUpdate);

            #region update tareas de odt
            if (rm.Response && tareasOrdenes.Count > 0)
            {
                foreach (var tarea in tareasOrdenes)
                {
                    var rmTarea = await _mantenimientoDetalleService.Update(tarea);
                }
            }
            #endregion

            return Json(rm);
        }

        [HttpGet]
        public IActionResult Pendientes()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListODTPendientes()
        {
            var rmOrdenesConPendientes = await _mantenimientoService.GetListODTPendientes();
            List<OrdenTrabajo> ordenesAbiertas = rmOrdenesConPendientes.Result;

            List<VMOrdenTrabajo> vmOrdenes = _mapper.Map<List<VMOrdenTrabajo>>(ordenesAbiertas);
            if (vmOrdenes.Count > 0)
            {
                foreach (var item in vmOrdenes)
                {
                    var countTareas = await _mantenimientoDetalleService.CountTareasOdt(item.OrtrCodigo);
                    item.CountTareas = countTareas;
                }
            }

            rmOrdenesConPendientes.Result = vmOrdenes;

            return Json(rmOrdenesConPendientes);
        }
        #endregion
    }
}
