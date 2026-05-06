using Microsoft.AspNetCore.Mvc;
using CONUNI_RESTFUL_DOTNET_CLIWEB_G09.servicio;
using CONUNI_RESTFUL_DOTNET_CLIWEB_G09.modelo;

namespace CONUNI_RESTFUL_DOTNET_CLIWEB_G09.controlador
{
    public class ConversionController : Controller
    {
        private const string SESSION_KEY_USUARIO = "Usuario";
        private readonly ConversionApiClient _apiClient;

        public ConversionController(IConfiguration configuration)
        {
            string apiBaseUrl = configuration["ApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                throw new Exception("La URL del servicio API no está configurada en appsettings.json");
            }
            _apiClient = new ConversionApiClient(apiBaseUrl);
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Verificar sesión
            if (HttpContext.Session.GetString(SESSION_KEY_USUARIO) == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Usuario = HttpContext.Session.GetString(SESSION_KEY_USUARIO);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Convertir([FromBody] ConversionRequest request)
        {
            // Verificar sesión
            if (HttpContext.Session.GetString(SESSION_KEY_USUARIO) == null)
            {
                return Json(new ConversionResponse
                {
                    Exito = false,
                    Mensaje = "Sesión expirada. Por favor, inicie sesión nuevamente."
                });
            }

            var resultado = await _apiClient.ConvertirAsync(request);
            return Json(resultado ?? new ConversionResponse
            {
                Exito = false,
                Mensaje = "Error al procesar la solicitud"
            });
        }
    }
}


