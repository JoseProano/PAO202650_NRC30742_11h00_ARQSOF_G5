using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CONUNI_RESTFUL_DOTNET_CLIWEB_G09.controlador
{
    /// <summary>
    /// Controlador de login de la aplicación web cliente.
    /// <para>
    /// Las credenciales NO se almacenan en el cliente. La validación
    /// se delega al servidor a través del endpoint
    /// <c>POST api/auth/login</c>.
    /// </para>
    /// </summary>
    public class LoginController : Controller
    {
        private const string SESSION_KEY_USUARIO = "Usuario";
        private readonly string _authUrl;

        public LoginController(IConfiguration configuration)
        {
            string apiBaseUrl = configuration["ApiBaseUrl"] ?? "http://192.168.100.2:44385";
            _authUrl = apiBaseUrl.TrimEnd('/') + "/api/auth/login";
        }

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
        public async Task<IActionResult> Index(string username, string password)
        {
            bool autenticado = await ValidarCredencialesEnServidorAsync(username, password);

            if (autenticado)
            {
                HttpContext.Session.SetString(SESSION_KEY_USUARIO, username?.ToUpper() ?? "");
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

        /// <summary>
        /// Llama al endpoint REST del servidor para validar las credenciales.
        /// El cliente NUNCA almacena ni conoce las credenciales correctas.
        /// </summary>
        private async Task<bool> ValidarCredencialesEnServidorAsync(string usuario, string contrasena)
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.Timeout = System.TimeSpan.FromSeconds(10);

                string body = $"{{\"Usuario\":\"{usuario}\",\"Contrasena\":\"{contrasena}\"}}";
                var content = new StringContent(body, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(_authUrl, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
