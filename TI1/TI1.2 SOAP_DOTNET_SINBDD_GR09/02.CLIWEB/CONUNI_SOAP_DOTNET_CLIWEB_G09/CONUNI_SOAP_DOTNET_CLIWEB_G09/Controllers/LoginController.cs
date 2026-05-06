using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CONUNI_SOAP_DOTNET_CLIWEB_G09.Controllers
{
    /// <summary>
    /// Controlador de login de la aplicación web cliente SOAP.
    /// <para>
    /// Las credenciales NO se almacenan en el cliente. La validación
    /// se delega al servidor SOAP a través de la operación Login.
    /// La URL del servidor se llama vía el cliente WCF configurado.
    /// </para>
    /// </summary>
    public class LoginController : Controller
    {
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
        /// Llama al servidor SOAP vía HTTP POST para validar las credenciales.
        /// El cliente NUNCA almacena ni conoce las credenciales correctas.
        /// </summary>
        private static async Task<bool> ValidarCredencialesEnServidorAsync(string usuario, string contrasena)
        {
            try
            {
                // Llamada SOAP raw al método login del servidor
                string soapEnvelope =
                    "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" " +
                                    "xmlns:tns=\"http://CONUNI_SOAP_DOTNET_GR09/\">" +
                    "  <soapenv:Body>" +
                    "    <tns:login>" +
                    $"      <usuario>{usuario}</usuario>" +
                    $"      <contrasena>{contrasena}</contrasena>" +
                    "    </tns:login>" +
                    "  </soapenv:Body>" +
                    "</soapenv:Envelope>";

                using var httpClient = new HttpClient();
                httpClient.Timeout = System.TimeSpan.FromSeconds(10);
                var content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml");
                content.Headers.Add("SOAPAction", "login");

                // URL del servicio SOAP .NET (ajustar según despliegue)
                var response = await httpClient.PostAsync(
                    "http://localhost/CONUNI_SOAP_DOTNET_GR09/WSConversion.svc", content);

                if (!response.IsSuccessStatusCode) return false;

                string body = await response.Content.ReadAsStringAsync();
                return body.Contains("<return>true</return>") || body.Contains(">true<");
            }
            catch
            {
                return false;
            }
        }
    }
}
