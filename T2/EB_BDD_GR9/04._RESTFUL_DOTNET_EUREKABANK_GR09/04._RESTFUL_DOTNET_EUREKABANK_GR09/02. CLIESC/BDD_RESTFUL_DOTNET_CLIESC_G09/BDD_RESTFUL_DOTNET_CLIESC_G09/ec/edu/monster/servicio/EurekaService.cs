using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BDD_RESTFUL_DOTNET_CLIESC_G09.ec.edu.monster.modelo;

namespace BDD_RESTFUL_DOTNET_CLIESC_G09.ec.edu.monster.servicio
{
    /// <summary>
    /// Cliente RESTful para el servicio EurekaBank
    /// </summary>
    public class EurekaService
    {
        // URL del servicio RESTful (ajustar según tu configuración)
        private static readonly string SERVICE_URL = ConfigurationManager.AppSettings["ServiceUrl"] ?? "http://10.183.38.246:44385/api/corebancario";
        
        private readonly HttpClient httpClient;

        public EurekaService()
        {
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Obtiene los movimientos de una cuenta
        /// </summary>
        public List<Movimiento> TraerMovimientos(string cuenta)
        {
            try
            {
                string url = $"{SERVICE_URL}/movimientos/{Uri.EscapeDataString(cuenta)}";
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var movimientos = JsonConvert.DeserializeObject<List<Movimiento>>(json);
                    return movimientos ?? new List<Movimiento>();
                }
                else
                {
                    throw new Exception($"Error al consultar movimientos: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al consultar movimientos: {ex.Message}");
                throw new Exception($"No se pudo conectar al servicio RESTful en {SERVICE_URL}. " +
                    $"Verifique que el servicio esté corriendo y que la URL sea correcta. " +
                    $"Error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra un depósito en una cuenta
        /// </summary>
        public int RegDeposito(string cuenta, double importe)
        {
            try
            {
                string url = $"{SERVICE_URL}/deposito?cuenta={Uri.EscapeDataString(cuenta)}&importe={importe}";
                var content = new StringContent("{}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    
                    if (resultado != null && resultado.ContainsKey("estado"))
                    {
                        var estadoObj = resultado["estado"];
                        if (estadoObj != null)
                        {
                            // Manejar JToken de Newtonsoft.Json
                            if (estadoObj is Newtonsoft.Json.Linq.JToken token)
                            {
                                int estado = token.ToObject<int>();
                                return estado;
                            }
                            else
                            {
                                int estado = Convert.ToInt32(estadoObj);
                                return estado;
                            }
                        }
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al registrar depósito: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Registra un retiro de una cuenta
        /// </summary>
        public int RegRetiro(string cuenta, double importe)
        {
            try
            {
                string url = $"{SERVICE_URL}/retiro?cuenta={Uri.EscapeDataString(cuenta)}&importe={importe}";
                var content = new StringContent("{}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    
                    if (resultado != null && resultado.ContainsKey("estado"))
                    {
                        var estadoObj = resultado["estado"];
                        if (estadoObj != null)
                        {
                            // Manejar JToken de Newtonsoft.Json
                            if (estadoObj is Newtonsoft.Json.Linq.JToken token)
                            {
                                int estado = token.ToObject<int>();
                                return estado;
                            }
                            else
                            {
                                int estado = Convert.ToInt32(estadoObj);
                                return estado;
                            }
                        }
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al registrar retiro: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Registra una transferencia entre cuentas
        /// </summary>
        public int RegTransferencia(string cuentaOrigen, string cuentaDestino, double importe)
        {
            try
            {
                string url = $"{SERVICE_URL}/transferencia?cuentaOrigen={Uri.EscapeDataString(cuentaOrigen)}&cuentaDestino={Uri.EscapeDataString(cuentaDestino)}&importe={importe}";
                var content = new StringContent("{}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    
                    if (resultado != null && resultado.ContainsKey("estado"))
                    {
                        var estadoObj = resultado["estado"];
                        if (estadoObj != null)
                        {
                            // Manejar JToken de Newtonsoft.Json
                            if (estadoObj is Newtonsoft.Json.Linq.JToken token)
                            {
                                int estado = token.ToObject<int>();
                                return estado;
                            }
                            else
                            {
                                int estado = Convert.ToInt32(estadoObj);
                                return estado;
                            }
                        }
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al registrar transferencia: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Valida el ingreso de un usuario (no implementado en RESTful, se valida localmente)
        /// </summary>
        public string ValidarIngreso(string usuario, string password)
        {
            // La validación se hace localmente en el cliente
            const string USUARIO = "MONSTER";
            const string PASS_HASH = "6C3F6757E773775FD059E2F025BD14BA"; // MD5 de "MONSTER9"
            
            string hashPassword = CalcularMD5(password);
            if (USUARIO.Equals(usuario, StringComparison.OrdinalIgnoreCase) && 
                PASS_HASH.Equals(hashPassword, StringComparison.OrdinalIgnoreCase))
            {
                return "OK";
            }
            return "Error";
        }

        private string CalcularMD5(string input)
        {
            try
            {
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
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

