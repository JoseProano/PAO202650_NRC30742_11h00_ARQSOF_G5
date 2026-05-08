    using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BDD_RESTFUL_DOTNET_CLICON_G09.ec.edu.monster.modelo;

namespace BDD_RESTFUL_DOTNET_CLICON_G09.ec.edu.monster.servicio
{
    public class EurekaService
    {
        private static readonly string SERVICE_URL = ConfigurationManager.AppSettings["ServiceUrl"] ?? "http://10.183.38.246:44385/api/corebancario";
        private readonly HttpClient httpClient;

        public EurekaService()
        {
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public List<CliCon_Movimiento> TraerMovimientos(string cuenta)
        {
            try
            {
                string url = $"{SERVICE_URL}/movimientos/{cuenta}";
                var response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var movimientos = JsonConvert.DeserializeObject<List<CliCon_Movimiento>>(json);
                    return movimientos ?? new List<CliCon_Movimiento>();
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

        public double TraerSaldoCuenta(string cuenta)
        {
            try
            {
                var movimientos = TraerMovimientos(cuenta);
                double saldo = 0.0;

                foreach (var mov in movimientos)
                {
                    if (mov.Accion == "INGRESO" || mov.Accion == "DEPOSITO")
                    {
                        saldo += mov.Importe;
                    }
                    else if (mov.Accion == "SALIDA" || mov.Accion == "RETIRO")
                    {
                        saldo -= mov.Importe;
                    }
                }

                return saldo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener saldo: {ex.Message}", ex);
            }
        }

        public int RegistrarDeposito(string cuenta, double importe, string codEmp)
        {
            try
            {
                string url = $"{SERVICE_URL}/deposito?cuenta={cuenta}&importe={importe}";
                var response = httpClient.PostAsync(url, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<dynamic>(json);
                    int estado = resultado?.estado ?? -1;
                    return estado;
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

        public int RegistrarRetiro(string cuenta, double importe, string codEmp)
        {
            try
            {
                string url = $"{SERVICE_URL}/retiro?cuenta={cuenta}&importe={importe}";
                var response = httpClient.PostAsync(url, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<dynamic>(json);
                    int estado = resultado?.estado ?? -1;
                    return estado;
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

        public int RegistrarTransferencia(string cuentaOrigen, string cuentaDestino, double importe, string codEmp)
        {
            try
            {
                string url = $"{SERVICE_URL}/transferencia?cuentaOrigen={cuentaOrigen}&cuentaDestino={cuentaDestino}&importe={importe}";
                var response = httpClient.PostAsync(url, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<dynamic>(json);
                    int estado = resultado?.estado ?? -1;
                    return estado;
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

