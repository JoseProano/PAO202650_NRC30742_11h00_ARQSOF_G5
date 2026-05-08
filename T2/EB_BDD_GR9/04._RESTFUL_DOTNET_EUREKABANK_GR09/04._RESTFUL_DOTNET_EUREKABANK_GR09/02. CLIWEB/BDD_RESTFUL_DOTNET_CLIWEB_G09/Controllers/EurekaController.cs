using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using BDD_RESTFUL_DOTNET_CLIWEB_G09.ec.edu.monster.servicio;

namespace BDD_RESTFUL_DOTNET_CLIWEB_G09.Controllers
{
    public class EurekaController : Controller
    {
        private readonly EurekaService _service;
        private const string USUARIO = "MONSTER";
        private const string PASS = "6C3F6757E773775FD059E2F025BD14BA";

        public EurekaController(EurekaService service)
        {
            _service = service;
        }

        // GET: /login
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("usuario") != null)
            {
                return RedirectToAction("Menu");
            }
            return View();
        }

        // POST: /login
        [HttpPost]
        public IActionResult Login(string usuario, string password)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Por favor, complete todos los campos.";
                return View();
            }

            // Validar con hash MD5
            string hashPassword = CalcularMD5(password);
            if (USUARIO.Equals(usuario, StringComparison.OrdinalIgnoreCase) && 
                PASS.Equals(hashPassword, StringComparison.OrdinalIgnoreCase))
            {
                HttpContext.Session.SetString("usuario", usuario);
                return RedirectToAction("Menu");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }
        }

        // GET: /menu
        [HttpGet]
        public IActionResult Menu()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Usuario = HttpContext.Session.GetString("usuario");
            return View();
        }

        // GET: /movimientos
        [HttpGet]
        public IActionResult Movimientos(string cuenta)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Cuenta = cuenta;
            if (!string.IsNullOrEmpty(cuenta))
            {
                try
                {
                    var movimientos = _service.TraerMovimientos(cuenta);
                    ViewBag.Movimientos = movimientos;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al consultar movimientos: {ex.Message}";
                }
            }
            return View();
        }

        // GET: /deposito
        [HttpGet]
        public IActionResult Deposito()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // POST: /deposito
        [HttpPost]
        public IActionResult Deposito(string cuenta, string importe)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Login");
            }

            if (string.IsNullOrEmpty(cuenta) || string.IsNullOrEmpty(importe))
            {
                ViewBag.Mensaje = "Por favor, complete todos los campos.";
                ViewBag.TipoMensaje = "error";
                return View();
            }

            if (!double.TryParse(importe, out double importeValue) || importeValue <= 0)
            {
                ViewBag.Mensaje = "El importe debe ser un número positivo.";
                ViewBag.TipoMensaje = "error";
                return View();
            }

            try
            {
                string resultado = _service.RegistrarDeposito(cuenta, importeValue);
                if (resultado.Contains("OK") || resultado.Contains("exitoso"))
                {
                    ViewBag.Mensaje = $"Depósito realizado exitosamente. {resultado}";
                    ViewBag.TipoMensaje = "success";
                }
                else
                {
                    ViewBag.Mensaje = resultado;
                    ViewBag.TipoMensaje = "error";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Error al realizar el depósito: {ex.Message}";
                ViewBag.TipoMensaje = "error";
            }

            return View();
        }

        // GET: /retiro
        [HttpGet]
        public IActionResult Retiro()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // POST: /retiro
        [HttpPost]
        public IActionResult Retiro(string cuenta, string importe)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Login");
            }

            if (string.IsNullOrEmpty(cuenta) || string.IsNullOrEmpty(importe))
            {
                ViewBag.Mensaje = "Por favor, complete todos los campos.";
                ViewBag.TipoMensaje = "error";
                return View();
            }

            if (!double.TryParse(importe, out double importeValue) || importeValue <= 0)
            {
                ViewBag.Mensaje = "El importe debe ser un número positivo.";
                ViewBag.TipoMensaje = "error";
                return View();
            }

            try
            {
                string resultado = _service.RegistrarRetiro(cuenta, importeValue);
                if (resultado.Contains("OK") || resultado.Contains("exitoso"))
                {
                    ViewBag.Mensaje = $"Retiro realizado exitosamente. {resultado}";
                    ViewBag.TipoMensaje = "success";
                }
                else
                {
                    ViewBag.Mensaje = resultado;
                    ViewBag.TipoMensaje = "error";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Error al realizar el retiro: {ex.Message}";
                ViewBag.TipoMensaje = "error";
            }

            return View();
        }

        // GET: /transferencia
        [HttpGet]
        public IActionResult Transferencia()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // POST: /transferencia
        [HttpPost]
        public IActionResult Transferencia(string cuentaOrigen, string cuentaDestino, string importe)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Login");
            }

            if (string.IsNullOrEmpty(cuentaOrigen) || string.IsNullOrEmpty(cuentaDestino) || string.IsNullOrEmpty(importe))
            {
                ViewBag.Mensaje = "Por favor, complete todos los campos.";
                ViewBag.TipoMensaje = "error";
                return View();
            }

            if (!double.TryParse(importe, out double importeValue) || importeValue <= 0)
            {
                ViewBag.Mensaje = "El importe debe ser un número positivo.";
                ViewBag.TipoMensaje = "error";
                return View();
            }

            try
            {
                string resultado = _service.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importeValue);
                if (resultado.Contains("OK") || resultado.Contains("exitoso"))
                {
                    ViewBag.Mensaje = $"Transferencia realizada exitosamente. {resultado}";
                    ViewBag.TipoMensaje = "success";
                }
                else
                {
                    ViewBag.Mensaje = resultado;
                    ViewBag.TipoMensaje = "error";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Error al realizar la transferencia: {ex.Message}";
                ViewBag.TipoMensaje = "error";
            }

            return View();
        }

        // POST: /logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string CalcularMD5(string input)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] messageDigest = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder hexString = new StringBuilder();
                    for (int i = 0; i < messageDigest.Length; i++)
                    {
                        string hex = messageDigest[i].ToString("X2");
                        hexString.Append(hex);
                    }
                    return hexString.ToString();
                }
            }
            catch
            {
                return input;
            }
        }
    }
}



