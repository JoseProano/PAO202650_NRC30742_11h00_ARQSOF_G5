using System;
using System.Net;
using System.Web.Http;

namespace CONUNI_RESTFUL_DOTNET_GR9S.Controllers
{
    /// <summary>
    /// Controlador REST para la autenticación de usuarios.
    /// Las credenciales residen ÚNICAMENTE en el servidor.
    /// Los clientes envían usuario y contraseña vía POST y el servidor
    /// responde si son válidas.
    /// <para>Endpoint: POST api/auth/login</para>
    /// </summary>
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        // Credenciales almacenadas SOLO en el servidor
        private const string UsuarioValido    = "MONSTER";
        private const string ContrasenaValida = "MONSTER9";

        /// <summary>
        /// Valida las credenciales del usuario.
        /// </summary>
        /// <param name="credenciales">Objeto con usuario y contraseña</param>
        /// <returns>200 OK con autenticado=true, o 401 Unauthorized con autenticado=false</returns>
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginRequest credenciales)
        {
            if (credenciales == null
                || string.IsNullOrEmpty(credenciales.Usuario)
                || string.IsNullOrEmpty(credenciales.Contrasena))
            {
                return BadRequest("Credenciales no proporcionadas");
            }

            bool esValido = UsuarioValido.Equals(credenciales.Usuario.Trim().ToUpper())
                         && ContrasenaValida.Equals(credenciales.Contrasena.Trim());

            System.Diagnostics.Debug.WriteLine(
                $"DEBUG: Login – usuario='{credenciales.Usuario}' autenticado={esValido}");

            if (esValido)
            {
                return Ok(new LoginResponse { Autenticado = true, Mensaje = "Autenticación exitosa" });
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized,
                    new LoginResponse { Autenticado = false, Mensaje = "Credenciales incorrectas" });
            }
        }
    }

    /// <summary>DTO para recibir las credenciales del cliente.</summary>
    public class LoginRequest
    {
        public string Usuario    { get; set; }
        public string Contrasena { get; set; }
    }

    /// <summary>DTO para devolver el resultado de la autenticación.</summary>
    public class LoginResponse
    {
        public bool   Autenticado { get; set; }
        public string Mensaje     { get; set; }
    }
}
