using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Diagnostics;
using CONUNI_RESTFUL_DOTNET_GR9S.Models;

namespace CONUNI_RESTFUL_DOTNET_GR9S.Controllers
{
    /// <summary>
    /// Controlador RESTful para operaciones bancarias del core bancario
    /// </summary>
    [RoutePrefix("api/corebancario")]
    public class CoreBancarioController : ApiController
    {
        private readonly EurekaService _service = new EurekaService();

        /// <summary>
        /// Método auxiliar para logging
        /// </summary>
        private void Log(string mensaje)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensaje}";
            Debug.WriteLine(logMessage);
            Trace.WriteLine(logMessage);
            System.Console.WriteLine(logMessage);
        }

        /// <summary>
        /// Obtiene la IP del cliente desde la petición HTTP
        /// </summary>
        private string GetClientIpAddress()
        {
            try
            {
                if (Request.Properties.ContainsKey("MS_HttpContext"))
                {
                    var httpContext = Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper;
                    if (httpContext != null)
                    {
                        return httpContext.Request.UserHostAddress;
                    }
                }

                // Fallback: obtener del header X-Forwarded-For
                if (Request.Headers.Contains("X-Forwarded-For"))
                {
                    var forwardedFor = Request.Headers.GetValues("X-Forwarded-For").FirstOrDefault();
                    if (!string.IsNullOrEmpty(forwardedFor))
                    {
                        return forwardedFor.Split(',')[0].Trim();
                    }
                }

                // Obtener del header X-Real-IP
                if (Request.Headers.Contains("X-Real-IP"))
                {
                    var realIp = Request.Headers.GetValues("X-Real-IP").FirstOrDefault();
                    if (!string.IsNullOrEmpty(realIp))
                    {
                        return realIp;
                    }
                }

                return "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Registra un depósito en una cuenta
        /// POST /api/corebancario/deposito?cuenta=00100001&importe=100.50
        /// </summary>
        [HttpPost]
        [Route("deposito")]
        public IHttpActionResult RegistrarDeposito([FromUri] string cuenta, [FromUri] double importe)
        {
            try
            {
                string ipCliente = GetClientIpAddress();
                Log($"POST /api/corebancario/deposito - IP Cliente: {ipCliente} - Cuenta: {cuenta}, Importe: {importe}");

                int estado;
                string codEmp = "0001"; // Código fijo del empleado

                try
                {
                    _service.RegistrarDeposito(cuenta, importe, codEmp);
                    estado = 1; // Éxito
                    Log($"Depósito exitoso - Cuenta: {cuenta}, Importe: {importe}");
                }
                catch (Exception ex)
                {
                    Log($"Error al registrar depósito en cuenta {cuenta}: {ex.Message}");
                    estado = -1; // Error
                }

                return Ok(new { estado });
            }
            catch (Exception ex)
            {
                Log($"Error interno en RegistrarDeposito: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Registra un retiro de una cuenta
        /// POST /api/corebancario/retiro?cuenta=00100001&importe=50.25
        /// </summary>
        [HttpPost]
        [Route("retiro")]
        public IHttpActionResult RegistrarRetiro([FromUri] string cuenta, [FromUri] double importe)
        {
            try
            {
                string ipCliente = GetClientIpAddress();
                Log($"POST /api/corebancario/retiro - IP Cliente: {ipCliente} - Cuenta: {cuenta}, Importe: {importe}");

                int estado;
                string codEmp = "0001"; // Código fijo del empleado

                try
                {
                    _service.RegistrarRetiro(cuenta, importe, codEmp);
                    estado = 1; // Éxito
                    Log($"Retiro exitoso - Cuenta: {cuenta}, Importe: {importe}");
                }
                catch (Exception ex)
                {
                    Log($"Error al registrar retiro en cuenta {cuenta}: {ex.Message}");
                    estado = -1; // Error
                }

                return Ok(new { estado });
            }
            catch (Exception ex)
            {
                Log($"Error interno en RegistrarRetiro: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Registra una transferencia entre cuentas
        /// POST /api/corebancario/transferencia?cuentaOrigen=00100001&cuentaDestino=00100002&importe=200.00
        /// </summary>
        [HttpPost]
        [Route("transferencia")]
        public IHttpActionResult RegistrarTransferencia(
            [FromUri] string cuentaOrigen,
            [FromUri] string cuentaDestino,
            [FromUri] double importe)
        {
            try
            {
                string ipCliente = GetClientIpAddress();
                Log($"POST /api/corebancario/transferencia - IP Cliente: {ipCliente} - Origen: {cuentaOrigen}, Destino: {cuentaDestino}, Importe: {importe}");

                int estado;
                string codEmp = "0001"; // Código fijo del empleado

                try
                {
                    _service.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, codEmp);
                    estado = 1; // Éxito
                    Log($"Transferencia exitosa - Origen: {cuentaOrigen}, Destino: {cuentaDestino}, Importe: {importe}");
                }
                catch (Exception ex)
                {
                    Log($"Error al registrar transferencia de {cuentaOrigen} a {cuentaDestino}: {ex.Message}");
                    estado = -1; // Error
                }

                return Ok(new { estado });
            }
            catch (Exception ex)
            {
                Log($"Error interno en RegistrarTransferencia: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Obtiene los movimientos de una cuenta
        /// GET /api/corebancario/movimientos/00100001
        /// </summary>
        [HttpGet]
        [Route("movimientos/{cuenta}")]
        public IHttpActionResult ObtenerMovimientos(string cuenta)
        {
            try
            {
                string ipCliente = GetClientIpAddress();
                Log($"GET /api/corebancario/movimientos/{cuenta} - IP Cliente: {ipCliente}");

                List<Movimiento> movimientos = _service.LeerMovimientos(cuenta);
                Log($"Movimientos obtenidos: {movimientos.Count} para cuenta {cuenta}");
                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                Log($"Error al obtener movimientos de cuenta {cuenta}: {ex.Message}");
                return InternalServerError(new Exception($"Error al obtener movimientos: {ex.Message}"));
            }
        }

        /// <summary>
        /// Maneja las peticiones OPTIONS para CORS
        /// </summary>
        [HttpOptions]
        [Route("{*path}")]
        public IHttpActionResult Options()
        {
            return Ok();
        }
    }
}

