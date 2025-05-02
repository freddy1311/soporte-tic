using AutoMapper;
using Domain.Business.Interface;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using soporte_tic.Models.ViewModels;

namespace soporte_tic.Controllers
{
    [Authorize]
    public class MaquinariaController : Controller
    {
        #region variables
        private readonly IMapper _mapper;
        private readonly IMaquinariaService _maquinariaService;
        #endregion

        #region constructor
        public MaquinariaController(
            IMapper mapper,
            IMaquinariaService maquinariaService
        )
        {
            _mapper = mapper;
            _maquinariaService = maquinariaService;
        }
        #endregion

        #region métodos
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListMaquinarias()
        {
            long codSucursal = (User.FindFirst("CodigoSucursal")?.Value != "") ? Convert.ToInt64(User.FindFirst("CodigoSucursal")?.Value) : 0;
            var rmMaquinarias = await _maquinariaService.GetMaquinarias(codSucursal);
            List<Maquinaria> maquinarias = rmMaquinarias.Result;

            List<VMMaquinaria> vmMaquinarias = _mapper.Map<List<VMMaquinaria>>(maquinarias);
            rmMaquinarias.Result = vmMaquinarias;

            return Json(rmMaquinarias);
        }

        [HttpGet]
        public async Task<JsonResult> GetListMaquinariasComponente(long codMaquinaria)
        {
            var rmMaquinarias = await _maquinariaService.GetMaquinariasComponente(codMaquinaria);
            List<Maquinaria> maquinarias = rmMaquinarias.Result;

            List<VMMaquinaria> vmMaquinarias = _mapper.Map<List<VMMaquinaria>>(maquinarias);
            rmMaquinarias.Result = vmMaquinarias;

            return Json(rmMaquinarias);
        }

        [HttpGet]
        public IActionResult CreateMaquinaria(long codMaquinaria = 0)
        {
            long codSucursal = (User.FindFirst("CodigoSucursal")?.Value != "") ? Convert.ToInt64(User.FindFirst("CodigoSucursal")?.Value) : 0;
            VMMaquinaria vmMaquinaria = new VMMaquinaria();
            vmMaquinaria.MaquEstado = 1;
            vmMaquinaria.MaquTipo = (codMaquinaria > 0) ? 2 : 1;
            if (codMaquinaria > 0)
            {
                vmMaquinaria.MaquCodigoFK = codMaquinaria;
            }
            vmMaquinaria.SucuCodigo = codSucursal;

            return PartialView(vmMaquinaria);
        }

        [HttpPost]
        public async Task<JsonResult> CreateMaquinaria([FromBody] VMMaquinaria model)
        {
            Maquinaria maquinariaCreate = _mapper.Map<Maquinaria>(model);
            maquinariaCreate.MaquNombre = (maquinariaCreate.MaquNombre != "") ? maquinariaCreate.MaquNombre?.ToUpper() : "";
            maquinariaCreate.MaquDescripcion = (maquinariaCreate.MaquDescripcion != "") ? maquinariaCreate.MaquDescripcion?.ToUpper() : "";
          
            var rm = await _maquinariaService.Create(maquinariaCreate);
            if (rm.Response)
            {
                VMMaquinaria maquinariaAdd = _mapper.Map<VMMaquinaria>(rm.Result);
                rm.Result = maquinariaAdd;
            }

            return Json(rm);
        }
        #endregion
    }
}
