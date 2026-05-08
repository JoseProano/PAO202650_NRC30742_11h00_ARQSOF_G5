using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using BDD_RESTFUL_DOTNET_CLIWEB_G09.ec.edu.monster.modelo;
using Microsoft.Extensions.Configuration;

namespace BDD_RESTFUL_DOTNET_CLIWEB_G09.ec.edu.monster.servicio
{
    /// <summary>
    /// Cliente RESTful para el servicio EurekaBank
    /// </summary>
    public class EurekaService
    {
        private readonly string _serviceUrl;
        private readonly HttpClient _httpClient;
        private static readonly string COD_EMP_DEFAULT = "0001";

        public EurekaService(IConfiguration configuration)
        {
            _serviceUrl = configuration["ServiceUrl"] ?? "http://10.183.38.246:44385/api/corebancario";
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Obtiene los movimientos de una cuenta
        /// </summary>
        public List<Movimiento> TraerMovimientos(string cuenta)
        {
            try
            {
                string url = $"{_serviceUrl}/movimientos/{cuenta}";
                var response = _httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var movimientos = JsonSerializer.Deserialize<List<Movimiento>>(json, options);
                    return movimientos ?? new List<Movimiento>();
                }
                else
                {
                    throw new Exception($"Error al obtener movimientos: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar movimientos: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra un depósito
        /// </summary>
        public string RegistrarDeposito(string cuenta, double importe)
        {
            try
            {
                string url = $"{_serviceUrl}/deposito?cuenta={cuenta}&importe={importe}";
                var response = _httpClient.PostAsync(url, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var resultado = JsonSerializer.Deserialize<JsonElement>(json);
                    
                    if (resultado.TryGetProperty("estado", out var estadoElement))
                    {
                        int estado = estadoElement.GetInt32();
                        if (estado == 1)
                        {
                            return "OK - Depósito realizado exitosamente";
                        }
                        else
                        {
                            return "ERROR - No se pudo realizar el depósito";
                        }
                    }
                    return "OK - Depósito realizado exitosamente";
                }
                else
                {
                    throw new Exception($"Error al registrar depósito: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar depósito: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra un retiro
        /// </summary>
        public string RegistrarRetiro(string cuenta, double importe)
        {
            try
            {
                string url = $"{_serviceUrl}/retiro?cuenta={cuenta}&importe={importe}";
                var response = _httpClient.PostAsync(url, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var resultado = JsonSerializer.Deserialize<JsonElement>(json);
                    
                    if (resultado.TryGetProperty("estado", out var estadoElement))
                    {
                        int estado = estadoElement.GetInt32();
                        if (estado == 1)
                        {
                            return "OK - Retiro realizado exitosamente";
                        }
                        else
                        {
                            return "ERROR - No se pudo realizar el retiro";
                        }
                    }
                    return "OK - Retiro realizado exitosamente";
                }
                else
                {
                    throw new Exception($"Error al registrar retiro: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar retiro: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra una transferencia
        /// </summary>
        public string RegistrarTransferencia(string cuentaOrigen, string cuentaDestino, double importe)
        {
            try
            {
                string url = $"{_serviceUrl}/transferencia?cuentaOrigen={cuentaOrigen}&cuentaDestino={cuentaDestino}&importe={importe}";
                var response = _httpClient.PostAsync(url, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var resultado = JsonSerializer.Deserialize<JsonElement>(json);
                    
                    if (resultado.TryGetProperty("estado", out var estadoElement))
                    {
                        int estado = estadoElement.GetInt32();
                        if (estado == 1)
                        {
                            return "OK - Transferencia realizada exitosamente";
                        }
                        else
                        {
                            return "ERROR - No se pudo realizar la transferencia";
                        }
                    }
                    return "OK - Transferencia realizada exitosamente";
                }
                else
                {
                    throw new Exception($"Error al registrar transferencia: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar transferencia: {ex.Message}", ex);
            }
        }
    }
}

