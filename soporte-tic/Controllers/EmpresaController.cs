using AutoMapper;
using Domain.Business.Interface;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using soporte_tic.Models.ViewModels;
using soporte_tic.Services.LocalStorage;

namespace soporte_tic.Controllers
{
    [Authorize]
    public class EmpresaController : Controller
    {
        #region properties
        private readonly IEmpresaService _empresaRepository;
        private readonly ISucursalService _sucursalRepository;
        private readonly IMapper _mapper;
        private readonly ILocalFileService _localFileService;
        #endregion

        #region constructor
        public EmpresaController(
            IEmpresaService empresaRepository,
            ISucursalService sucursalRepository,
            IMapper mapper,
            ILocalFileService localFileService
        )
        {
            _empresaRepository = empresaRepository;
            _sucursalRepository = sucursalRepository;
            _mapper = mapper;
            _localFileService = localFileService;
        }
        #endregion

        #region methods
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetEmpresa()
        {
            var rm = await _empresaRepository.GetEmpresa();
            Empresa empresa = rm.Result;

            VMEmpresa vmEmpresa = _mapper.Map<VMEmpresa>(empresa);
            rm.Result = vmEmpresa;

            return Json(rm);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateEmpresa(VMEmpresa empresa)
        {
            Empresa objEmpresa = _mapper.Map<Empresa>(empresa);
            Stream streamLogo;

            #region logo emp
            if (empresa.File == null || empresa.File.Length == 0)
            {
                streamLogo = null;
            }
            else
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    empresa.File.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    Stream stream = memoryStream;

                    streamLogo = stream;
                }
            }
            #endregion

            var rm = await _empresaRepository.UpdateEmpresa(objEmpresa, streamLogo!);
            if (rm.Response == true)
            {
                string nameImg = $"{empresa.EmprRuc}.jpg";
                var rmLogo = await _localFileService.SaveImageAsync(empresa.File, nameImg);
                
                if (rmLogo.Response)
                {
                    objEmpresa.EmprLogo = (string)rmLogo.Result;
                    var rmUpdateLogo = await _empresaRepository.UpdateEmpresa(objEmpresa, streamLogo!);
                }
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<JsonResult> GetSucursalesEmpresa(long codEmpresa)
        {
            var rm = await _sucursalRepository.GetSucursalesEmpresa(codEmpresa);
            List<Sucursal> sucursales = rm.Result;
            List<VMSucursal> vmSucursal = _mapper.Map<List<VMSucursal>>(sucursales);
            rm.Result = vmSucursal;

            return Json(rm);
        }

        [HttpGet]
        public async Task<IActionResult> EditSucursal(long codSucursal)
        {
            var rmSucursal = await _sucursalRepository.GetSucursal(codSucursal);

            Sucursal sucursal = rmSucursal.Result;
            VMSucursal vmSucursal = _mapper.Map<VMSucursal>(sucursal);

            return PartialView(vmSucursal);
        }

        [HttpPut]
        public async Task<JsonResult> EditSucursal([FromBody] VMSucursal sucursal)
        {
            Sucursal sucuUpdate = _mapper.Map<Sucursal>(sucursal);
            var rm = await _sucursalRepository.Update(sucuUpdate);

            return Json(rm);
        }
        #endregion

    }
}
