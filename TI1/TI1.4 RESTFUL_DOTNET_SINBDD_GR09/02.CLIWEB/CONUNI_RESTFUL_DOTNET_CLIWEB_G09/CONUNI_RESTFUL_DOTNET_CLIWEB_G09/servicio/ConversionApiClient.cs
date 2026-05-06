using System.Net.Http.Json;
using CONUNI_RESTFUL_DOTNET_CLIWEB_G09.modelo;

namespace CONUNI_RESTFUL_DOTNET_CLIWEB_G09.servicio
{
    /// <summary>
    /// Cliente HTTP para consumir el servicio RESTful de conversión
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
        }

        /// <summary>
        /// Método genérico para conversiones POST
        /// </summary>
        public async Task<ConversionResponse?> ConvertirAsync(ConversionRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_baseUrl}/api/conversion/convertir", 
                    request);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ConversionResponse>();
                }
                
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = $"Error del servidor: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = $"Error al conectar con el servidor: {ex.Message}"
                };
            }
        }
    }
}


