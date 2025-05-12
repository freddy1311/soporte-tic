using AutoMapper;
using Domain.Business.Implementation;
using Domain.Business.Interface;
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
        public async Task<JsonResult> GetListODTSOpen()
        {
            var rmOrdenesAbiertas = await _mantenimientoService.GetListODTOpen();
            List<OrdenTrabajo> ordenesAbiertas = rmOrdenesAbiertas.Result;

            List<VMOrdenTrabajo> vmOrdenes = _mapper.Map<List<VMOrdenTrabajo>>(ordenesAbiertas);
            rmOrdenesAbiertas.Result = vmOrdenes;

            return Json(rmOrdenesAbiertas);
        }

        #endregion
    }
}
