using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CONUNI_RESTFUL_DOTNET_CLIESC_G09.servicio
{
    /// <summary>
    /// Cliente para consumir el servicio RESTful de conversión
    /// </summary>
    public class ConversionApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ConversionApiClient(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentException("El baseUrl no puede ser nulo o vacío", nameof(baseUrl));
            }
            _baseUrl = baseUrl.TrimEnd('/');
            _httpClient = new HttpClient();
            
            // Ignorar errores de certificado SSL para desarrollo local
            System.Net.ServicePointManager.ServerCertificateValidationCallback = 
                (sender, certificate, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// Obtiene información del servicio
        /// </summary>
        public async Task<string> ObtenerInfoAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/api/conversion/info");
                return response;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ========== TEMPERATURA ==========
        
        public async Task<ConversionResponse> CelsiusAFahrenheitAsync(double celsius)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/celsius-to-fahrenheit?celsius={celsius}");
        }

        public async Task<ConversionResponse> FahrenheitACelsiusAsync(double fahrenheit)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/fahrenheit-to-celsius?fahrenheit={fahrenheit}");
        }

        public async Task<ConversionResponse> CelsiusAKelvinAsync(double celsius)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/celsius-to-kelvin?celsius={celsius}");
        }

        public async Task<ConversionResponse> KelvinACelsiusAsync(double kelvin)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/kelvin-to-celsius?kelvin={kelvin}");
        }

        public async Task<ConversionResponse> FahrenheitAKelvinAsync(double fahrenheit)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/fahrenheit-to-kelvin?fahrenheit={fahrenheit}");
        }

        public async Task<ConversionResponse> KelvinAFahrenheitAsync(double kelvin)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/kelvin-to-fahrenheit?kelvin={kelvin}");
        }

        // ========== LONGITUD ==========
        
        public async Task<ConversionResponse> MetrosAPiesAsync(double metros)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/longitud/metros-to-pies?metros={metros}");
        }

        public async Task<ConversionResponse> PiesAMetrosAsync(double pies)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/longitud/pies-to-metros?pies={pies}");
        }

        public async Task<ConversionResponse> MetrosAPulgadasAsync(double metros)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/longitud/metros-to-pulgadas?metros={metros}");
        }

        public async Task<ConversionResponse> PulgadasAMetrosAsync(double pulgadas)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/longitud/pulgadas-to-metros?pulgadas={pulgadas}");
        }

        public async Task<ConversionResponse> KilometrosAMillasAsync(double kilometros)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/longitud/kilometros-to-millas?kilometros={kilometros}");
        }

        public async Task<ConversionResponse> MillasAKilometrosAsync(double millas)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/longitud/millas-to-kilometros?millas={millas}");
        }

        // ========== PESO ==========
        
        public async Task<ConversionResponse> KilogramosALibrasAsync(double kilogramos)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/peso/kilogramos-to-libras?kilogramos={kilogramos}");
        }

        public async Task<ConversionResponse> LibrasAKilogramosAsync(double libras)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/peso/libras-to-kilogramos?libras={libras}");
        }

        public async Task<ConversionResponse> GramosAOnzasAsync(double gramos)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/peso/gramos-to-onzas?gramos={gramos}");
        }

        public async Task<ConversionResponse> OnzasAGramosAsync(double onzas)
        {
            return await GetConversionAsync($"{_baseUrl}/api/conversion/peso/onzas-to-gramos?onzas={onzas}");
        }

        
        {
        }

        {
        }

        {
        }

        {
        }

        // ========== ÁREA ==========
        
        {
        }

        {
        }

        {
        }

        {
        }

        // ========== CONVERSIÓN GENÉRICA ==========
        
        public async Task<ConversionResponse> ConvertirAsync(ConversionRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/conversion/convertir", content);
                var responseString = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ConversionResponse>(responseString);
                }
                else
                {
                    return new ConversionResponse
                    {
                        Exito = false,
                        Mensaje = $"Error HTTP {response.StatusCode}: {responseString}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Método auxiliar para peticiones GET
        /// </summary>
        private async Task<ConversionResponse> GetConversionAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<ConversionResponse>(response);
            }
            catch (Exception ex)
            {
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

