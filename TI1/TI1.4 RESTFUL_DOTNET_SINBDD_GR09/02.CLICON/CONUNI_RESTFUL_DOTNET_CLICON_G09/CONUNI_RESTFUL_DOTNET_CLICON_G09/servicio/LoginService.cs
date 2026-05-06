using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CONUNI_RESTFUL_DOTNET_CLICON_G09.vista;

namespace CONUNI_RESTFUL_DOTNET_CLICON_G09.servicio
{
    /// <summary>
    /// Servicio para manejar la autenticación.
    /// <para>
    /// Las credenciales NO se almacenan en el cliente. La validación
    /// se delega al servidor a través del endpoint
    /// <c>POST api/auth/login</c>.
    /// </para>
    /// </summary>
    public class LoginService
    {
        private const int MAX_INTENTOS = 3;

        private readonly VistaLogin _vista;
        private readonly string     _authUrl;

        public LoginService(VistaLogin vista, string baseUrl)
        {
            _vista   = vista;
            // El endpoint de login está en api/auth/login
            _authUrl = baseUrl.TrimEnd('/') + "/auth/login";
        }

        /// <summary>
        /// Proceso completo de autenticación.
        /// Envía las credenciales al servidor y espera la respuesta.
        /// </summary>
        public async Task<bool> AutenticarAsync()
        {
            int intentos = 0;

            while (intentos < MAX_INTENTOS)
            {
                _vista.MostrarBannerLogin();
                _vista.MostrarIntento(intentos + 1, MAX_INTENTOS);

                string usuario   = _vista.LeerUsuario();
                string contrasena = _vista.LeerContrasenaOculta();

                if (await ValidarCredencialesEnServidorAsync(usuario, contrasena))
                {
                    _vista.MostrarMensajeExito();
                    await Task.Delay(1000);
                    return true;
                }
                else
                {
                    intentos++;
                    _vista.MostrarMensajeError(intentos, MAX_INTENTOS);

                    if (intentos < MAX_INTENTOS)
                    {
                        Console.WriteLine("Presione cualquier tecla para intentar de nuevo...");
                        Console.ReadKey();
                    }
                }
            }

            _vista.MostrarMensajeBloqueo();
            await Task.Delay(2000);
            return false;
        }

        /// <summary>
        /// Llama al endpoint REST del servidor para validar las credenciales.
        /// El cliente NUNCA almacena ni conoce las credenciales correctas.
        /// </summary>
        private async Task<bool> ValidarCredencialesEnServidorAsync(string usuario, string contrasena)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    string body = $"{{\"Usuario\":\"{usuario}\",\"Contrasena\":\"{contrasena}\"}}";
                    var content = new StringContent(body, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(_authUrl, content);
                    return response.IsSuccessStatusCode; // 200 OK = autenticado
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al conectar con el servidor para autenticar: {ex.Message}");
                return false;
            }
        }
    }
}
