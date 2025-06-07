using AutoMapper;
using Domain.Business.Interface;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using soporte_tic.Models;
using soporte_tic.Models.ViewModels;
using System.Diagnostics;

namespace soporte_tic.Controllers;

[Authorize]
public class HomeController : Controller
{
    #region variables
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly IMaquinariaService _maquinariaService;
    private readonly IMantenimientoService _mantenimientoService;
    private readonly IUsuarioService _usuarioService;
    #endregion

    #region constructor
    public HomeController(
        ILogger<HomeController> logger,
        IMapper mapper,
        IMaquinariaService maquinariaService,
        IMantenimientoService mantenimientoService,
        IUsuarioService usuarioService
    )
    {
        _logger = logger;
        _mapper = mapper;
        _maquinariaService = maquinariaService;
        _mantenimientoService = mantenimientoService;
        _usuarioService = usuarioService;
    }
    #endregion

    #region métodos
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> GetDataDashboard()
    {
        var rm = new ResponseModel();
        var vmDashboard = new VMDashboard();

        #region usuarios
        var rmUsuarios = await _usuarioService.GetUsers();
        if (rmUsuarios.Response)
        {
            var usuarios = rmUsuarios.Result;
            vmDashboard.CountUsuarios = usuarios.Count;
        }
        else
        {
            vmDashboard.CountUsuarios = 0;
        }
        #endregion

        #region líneas y maquinarias
        var rmLineas = await _maquinariaService.GetMaquinarias();
        var rmMaquinarias = await _maquinariaService.GetMaquinariasComponente();

        if (rmLineas.Response)
        {
            var lineas = rmLineas.Result;
            vmDashboard.CountLineas = lineas.Count;
        }
        else
        {
            vmDashboard.CountLineas = 0;
        }

        if (rmMaquinarias.Response)
        {
            var maquinarias = rmMaquinarias.Result;
            vmDashboard.CountMaquinarias = maquinarias.Count;
        }
        else
        {
            vmDashboard.CountMaquinarias = 0;
        }
        #endregion

        #region ordenes de trabajo
        var rmOrdenes = await _mantenimientoService.GetListODTOpenAnual();
        
        if (rmOrdenes.Response)
        {
            var ordenes = rmOrdenes.Result;
            vmDashboard.CountOrdenesTrabajo = ordenes.Count;
            List<VMOrdenTrabajo> vmOrdenesTrabajo = _mapper.Map<List<VMOrdenTrabajo>>(ordenes);
            vmDashboard.Ordenes = vmOrdenesTrabajo;
        }
        else
        {
            vmDashboard.CountOrdenesTrabajo = 0;
            vmDashboard.Ordenes = new List<VMOrdenTrabajo>();
        }

        #endregion
        rm.Response = true;
        rm.Result = vmDashboard;
        return Json(rm);
    }
    #endregion


}
