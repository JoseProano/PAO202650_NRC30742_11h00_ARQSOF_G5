using Microsoft.AspNetCore.Mvc;

namespace CONUNI_SOAP_DOTNET_CLIWEB_G09.Controllers
{
    public class LoginController : Controller
    {
        private const string USUARIO_CORRECTO = "MONSTER";
        private const string CONTRASENA_CORRECTA = "MONSTER9";
        private const string SESSION_KEY_USUARIO = "Usuario";

        [HttpGet]
        public IActionResult Index()
        {
            // Si ya está logueado, redirigir al panel principal
            if (HttpContext.Session.GetString(SESSION_KEY_USUARIO) != null)
            {
                return RedirectToAction("Index", "Conversion");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (USUARIO_CORRECTO.Equals(username?.ToUpper()) && 
                CONTRASENA_CORRECTA.Equals(password))
            {
                HttpContext.Session.SetString(SESSION_KEY_USUARIO, username.ToUpper());
                return RedirectToAction("Index", "Conversion");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}


