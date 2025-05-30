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
    public class MaquinariaTareaController : Controller
    {
        #region variables
        private readonly IMapper _mapper;
        private readonly IMaquinariaTareaService _maquinariaTareaService;
        #endregion

        #region constructor
        public MaquinariaTareaController(
            IMapper mapper,
            IMaquinariaTareaService maquinariaTareaService
        )
        {
            _mapper = mapper;
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
        public async Task<JsonResult> GetListTareasMaquinaria()
        {
            var rmTareasMaquinarias = await _maquinariaTareaService.GetAllTareasMaquinaria();
            List<TareasMaquinaria> tareasMaquinarias = rmTareasMaquinarias.Result;

            List<VMMaquinariaTarea> vmMaquinarias = _mapper.Map<List<VMMaquinariaTarea>>(tareasMaquinarias);
            rmTareasMaquinarias.Result = vmMaquinarias;

            return Json(rmTareasMaquinarias);
        }

        [HttpGet]
        public IActionResult CreateTareaMaquinaria(long codMaquinaria = 0)
        {
            VMMaquinariaTarea vmTareaMaquinaria = new VMMaquinariaTarea();
            vmTareaMaquinaria.TamaEstado = 1;
            vmTareaMaquinaria.MaquCodigo = codMaquinaria;

            return PartialView(vmTareaMaquinaria);
        }

        [HttpPost]
        public async Task<JsonResult> CreateTareaMaquinaria([FromBody] VMMaquinariaTarea model)
        {
            TareasMaquinaria tareaMaquinariaquinariaCreate = _mapper.Map<TareasMaquinaria>(model);
            tareaMaquinariaquinariaCreate.TamaNombre = (tareaMaquinariaquinariaCreate.TamaNombre != "") ? tareaMaquinariaquinariaCreate.TamaNombre?.ToUpper() : "";
            tareaMaquinariaquinariaCreate.TamaDescripcion = (tareaMaquinariaquinariaCreate.TamaDescripcion != "") ? tareaMaquinariaquinariaCreate.TamaDescripcion?.ToUpper() : "";

            var rm = await _maquinariaTareaService.Create(tareaMaquinariaquinariaCreate);
            if (rm.Response)
            {
                VMMaquinariaTarea tareaMaquinariaAdd = _mapper.Map<VMMaquinariaTarea>(rm.Result);
                rm.Result = tareaMaquinariaAdd;
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTareaMaquinaria(long codTarea)
        {
            var rmTareaMaquinaria = await _maquinariaTareaService.GetTareaMaquinaria(codTarea);
            TareasMaquinaria tareaMaquinaria = rmTareaMaquinaria.Result;

            VMMaquinariaTarea vmTareaMaquinaria = _mapper.Map<VMMaquinariaTarea>(tareaMaquinaria);

            return PartialView(vmTareaMaquinaria);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateTareaMaquinaria([FromBody] VMMaquinariaTarea model)
        {
            TareasMaquinaria tareaMaquinariaUpdate = _mapper.Map<TareasMaquinaria>(model);
            tareaMaquinariaUpdate.TamaNombre = (tareaMaquinariaUpdate.TamaNombre != "") ? tareaMaquinariaUpdate.TamaNombre?.ToUpper() : "";
            tareaMaquinariaUpdate.TamaDescripcion = (tareaMaquinariaUpdate.TamaDescripcion != "") ? tareaMaquinariaUpdate.TamaDescripcion?.ToUpper() : "";

            var rm = await _maquinariaTareaService.Update(tareaMaquinariaUpdate);

            if (rm.Response)
            {
                VMMaquinariaTarea tareaMaquinariaUpdated = _mapper.Map<VMMaquinariaTarea>(rm.Result);
                rm.Result = tareaMaquinariaUpdated;
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<JsonResult> DeleteTareaMaquinaria(long codTarea)
        {
            var rmTareaMaquinaria = await _maquinariaTareaService.GetTareaMaquinaria(codTarea);
            TareasMaquinaria tareaMaquinariaDelete = rmTareaMaquinaria.Result;
            tareaMaquinariaDelete.TamaEstado = 3;

            var rm = await _maquinariaTareaService.Update(tareaMaquinariaDelete);

            return Json(rm);
        }

        [HttpGet]
        public async Task<JsonResult> GetListTareasMaquinariaNoAll(long codMaquinaria)
        {
            var rm = await _maquinariaTareaService.GetTareasMaquinaria(codMaquinaria);

            if (rm.Response)
            {
                List<VMMaquinariaTarea> tareasMaquinaria = _mapper.Map<List<VMMaquinariaTarea>>(rm.Result);
                rm.Result = tareasMaquinaria;
            }

            return Json(rm);
        }
        #endregion
    }
}
