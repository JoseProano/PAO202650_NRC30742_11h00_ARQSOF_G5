using System;
using System.Configuration;
using System.Threading.Tasks;
using CONUNI_RESTFUL_DOTNET_CLICON_G09.servicio;
using CONUNI_RESTFUL_DOTNET_CLICON_G09.prueba;
using CONUNI_RESTFUL_DOTNET_CLICON_G09.vista;

namespace CONUNI_RESTFUL_DOTNET_CLICON_G09
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Crear vista de login
            var vistaLogin = new VistaLogin();
            
            // Crear servicio de login
            var loginService = new LoginService(vistaLogin);

            // Proceso de autenticación
            if (!await loginService.AutenticarAsync())
            {
                return; // Salir si el login falla
            }

            // Obtener URL del servicio desde App.config
            string apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                Console.WriteLine("Error: La URL del servicio API no está configurada en App.config");
                return;
            }
            
            // Crear cliente API
            var apiClient = new ConversionApiClient(apiBaseUrl);

            // Crear instancia de prueba
            var prueba = new PruebaConversion(apiClient);

            // Ejecutar menú principal
            await prueba.EjecutarMenuAsync();

            // Limpiar recursos
            apiClient.Dispose();
        }
    }
}
