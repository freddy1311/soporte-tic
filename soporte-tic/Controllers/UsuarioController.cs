using AutoMapper;
using Domain.Business.Implementation;
using Domain.Business.Interface;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using soporte_tic.Models.ViewModels;
using soporte_tic.Services.LocalStorage;

namespace soporte_tic.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        #region variables
        private readonly IUsuarioService _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ILocalFileService _localFileService;
        #endregion

        #region constructor
        public UsuarioController(
            IUsuarioService usuarioService,
            IMapper mapper,
            ILocalFileService localFileService
        )
        {
            _localFileService = localFileService;
            _mapper = mapper;
            _usuarioRepository = usuarioService;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListUsuarios()
        {
            var rmUsuarios = await _usuarioRepository.GetUsers();
            List<Usuario> usuarios = rmUsuarios.Result;

            List<VMUsuario> vmUsuarios = _mapper.Map<List<VMUsuario>>(usuarios);
            rmUsuarios.Result = vmUsuarios;

            return Json(rmUsuarios);
        }

        [HttpGet]
        public IActionResult CreateUsuario()
        {
            VMUsuario vmUsuario = new VMUsuario();
            vmUsuario.UsuaEstado = 1;
            vmUsuario.UsuaTipo = 1;
            vmUsuario.SucuCodigo = Convert.ToInt64(User.FindFirst("CodigoSucursal")?.Value);

            return PartialView(vmUsuario);
        }

        [HttpPost]
        public async Task<JsonResult> CreateUsuario([FromBody] VMUsuario model)
        {
            Usuario usuarioCreate = _mapper.Map<Usuario>(model);
            usuarioCreate.UsuaNombre = (usuarioCreate.UsuaNombre != "") ? usuarioCreate.UsuaNombre.ToUpper() : "";

            #region foto usuario
            if (model.File != null && model.File.Length != 0)
            {
                string nameImg = $"{model.UsuaCedula}.jpg";
                var rmLogo = await _localFileService.SaveImageAsync(model.File, nameImg);

                if (rmLogo.Response)
                {
                    usuarioCreate.UsuaFoto = (string)rmLogo.Result;
                }
            }
            #endregion

            var rm = await _usuarioRepository.Create(usuarioCreate);
            if (rm.Response)
            {
                VMUsuario usuarioAdd = _mapper.Map<VMUsuario>(rm.Result);
                rm.Result = usuarioAdd;
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUsuario(long codUsuario)
        {
            var rmUsuario = await _usuarioRepository.Get(codUsuario);
            Usuario usuario = rmUsuario.Result;

            VMUsuario vmUsuario = _mapper.Map<VMUsuario>(usuario);
            vmUsuario.UsuaPassword = "";

            return PartialView(vmUsuario);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateUsuario(VMUsuario model)
        {
            Usuario usuarioUpdate = _mapper.Map<Usuario>(model);
            usuarioUpdate.UsuaNombre = (usuarioUpdate.UsuaNombre != "") ? usuarioUpdate.UsuaNombre.ToUpper() : "";

            #region foto usuario
            if (model.File != null && model.File.Length != 0)
            {
                string nameImg = $"{model.UsuaCedula}.jpg";
                var rmLogo = await _localFileService.SaveImageAsync(model.File, nameImg);

                if (rmLogo.Response)
                {
                    usuarioUpdate.UsuaFoto = (string)rmLogo.Result;
                }
            }
            #endregion

            var rm = await _usuarioRepository.Update(usuarioUpdate);

            if (rm.Response)
            {
                VMUsuario usuarioUpdated = _mapper.Map<VMUsuario>(rm.Result);
                rm.Result = usuarioUpdated;
            }

            return Json(rm);
        }

        [HttpGet]
        public async Task<JsonResult> DeleteUsuario(long codUsuario)
        {
            var rmUsuario = await _usuarioRepository.Get(codUsuario);
            Usuario usuarioDelete = rmUsuario.Result;
            usuarioDelete.UsuaEstado = 2;

            var rm = await _usuarioRepository.Update(usuarioDelete);

            return Json(rm);
        }
    }
}
