using Domain.Business.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using soporte_tic.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;

namespace soporte_tic.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _userRepository;
        public LoginController(IUsuarioService userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet]   
        public IActionResult Login()
        {
            // Si el usuario ya está autenticado, redirige a Home/Index
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]VMLogin model)
        {
            ResponseModel rm = new ResponseModel();

            var rmUserLogin = await _userRepository.CheckLoginUser(model.Email, model.Password);

            if (rmUserLogin.Response)
            {
                try
                {
                    var user = rmUserLogin.Result;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UsuaNombre),
                        new Claim("CodigoUsuario", Convert.ToString(user.UsuaCodigo)),
                        new Claim("Email", user.UsuaEmail),
                        new Claim("CodigoSucursal", Convert.ToString(user.SucuCodigo)),
                        new Claim(ClaimTypes.Role, Convert.ToString(user.UsuaPerfil))
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    rmUserLogin.Result = null;
                }
                catch (Exception ex)
                {
                    rmUserLogin.Response = false;
                    rmUserLogin.Result = null;
                    rmUserLogin.Title = "Login";
                    rmUserLogin.Message = ex.Message;
                    return Ok(rmUserLogin);
                }

                return Ok(rmUserLogin);
            }
            else
            {
                return Ok(rmUserLogin);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
