using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CONUNI_RESTFUL_DOTNET_CLICON_G09.servicio
{
    /// <summary>
    /// Cliente para consumir el servicio RESTful de conversión.
    /// </summary>
    public class ConversionApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ConversionApiClient(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("El baseUrl no puede ser nulo o vacío", nameof(baseUrl));
            }

            _baseUrl = baseUrl.TrimEnd('/');
            _httpClient = new HttpClient();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public async Task<string> ObtenerInfoAsync()
        {
            try
            {
                return await _httpClient.GetStringAsync($"{_baseUrl}/api/conversion/info");
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public Task<ConversionResponse> CelsiusAFahrenheitAsync(double celsius) => GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/celsius-to-fahrenheit?celsius={celsius}");
        public Task<ConversionResponse> FahrenheitACelsiusAsync(double fahrenheit) => GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/fahrenheit-to-celsius?fahrenheit={fahrenheit}");
        public Task<ConversionResponse> CelsiusAKelvinAsync(double celsius) => GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/celsius-to-kelvin?celsius={celsius}");
        public Task<ConversionResponse> KelvinACelsiusAsync(double kelvin) => GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/kelvin-to-celsius?kelvin={kelvin}");
        public Task<ConversionResponse> FahrenheitAKelvinAsync(double fahrenheit) => GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/fahrenheit-to-kelvin?fahrenheit={fahrenheit}");
        public Task<ConversionResponse> KelvinAFahrenheitAsync(double kelvin) => GetConversionAsync($"{_baseUrl}/api/conversion/temperatura/kelvin-to-fahrenheit?kelvin={kelvin}");

        public Task<ConversionResponse> MetrosAPiesAsync(double metros) => GetConversionAsync($"{_baseUrl}/api/conversion/longitud/metros-to-pies?metros={metros}");
        public Task<ConversionResponse> PiesAMetrosAsync(double pies) => GetConversionAsync($"{_baseUrl}/api/conversion/longitud/pies-to-metros?pies={pies}");
        public Task<ConversionResponse> KilometrosAMillasAsync(double kilometros) => GetConversionAsync($"{_baseUrl}/api/conversion/longitud/kilometros-to-millas?kilometros={kilometros}");
        public Task<ConversionResponse> MillasAKilometrosAsync(double millas) => GetConversionAsync($"{_baseUrl}/api/conversion/longitud/millas-to-kilometros?millas={millas}");
        public Task<ConversionResponse> MetrosAPulgadasAsync(double metros) => GetConversionAsync($"{_baseUrl}/api/conversion/longitud/metros-to-pulgadas?metros={metros}");
        public Task<ConversionResponse> PulgadasAMetrosAsync(double pulgadas) => GetConversionAsync($"{_baseUrl}/api/conversion/longitud/pulgadas-to-metros?pulgadas={pulgadas}");

        public Task<ConversionResponse> KilogramosALibrasAsync(double kilogramos) => GetConversionAsync($"{_baseUrl}/api/conversion/peso/kilogramos-to-libras?kilogramos={kilogramos}");
        public Task<ConversionResponse> LibrasAKilogramosAsync(double libras) => GetConversionAsync($"{_baseUrl}/api/conversion/peso/libras-to-kilogramos?libras={libras}");
        public Task<ConversionResponse> GramosAOnzasAsync(double gramos) => GetConversionAsync($"{_baseUrl}/api/conversion/peso/gramos-to-onzas?gramos={gramos}");
        public Task<ConversionResponse> OnzasAGramosAsync(double onzas) => GetConversionAsync($"{_baseUrl}/api/conversion/peso/onzas-to-gramos?onzas={onzas}");

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

                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = $"Error HTTP {response.StatusCode}: {responseString}"
                };
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
