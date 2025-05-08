using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace soporte_tic.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        #region variables

        #endregion

        #region constructor
        public MantenimientoController()
        {
            
        }
        #endregion

        #region métodos
        public IActionResult Index()
        {
            return View();
        }


        #endregion
    }
}
