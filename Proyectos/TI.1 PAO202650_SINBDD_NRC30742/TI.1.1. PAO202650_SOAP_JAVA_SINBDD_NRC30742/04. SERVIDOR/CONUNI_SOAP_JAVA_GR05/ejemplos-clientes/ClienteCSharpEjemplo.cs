using System;

// NOTA: Este código es un ejemplo. Después de agregar la referencia de servicio
// en Visual Studio, las clases se generarán automáticamente.
// El namespace puede variar según cómo lo configures en "Add Service Reference"

namespace ConversionService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("========================================");
                Console.WriteLine("Cliente C# - Servicio WSConversion");
                Console.WriteLine("========================================\n");

                // IMPORTANTE: Después de agregar la referencia de servicio en Visual Studio,
                // usa el cliente generado. Ejemplo:
                /*
                
                // Crear instancia del cliente (ajustar según el namespace generado)
                var client = new WSConversionPortTypeClient();
                
                // Configurar timeout si es necesario
                client.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(30);
                client.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
                
                Console.WriteLine("=== Pruebas de Conversión ===\n");
                
                // Conversión de temperatura
                Console.WriteLine("1. Conversión de Temperatura:");
                double celsius = 25.0;
                double fahrenheit = client.celsiusAFahrenheit(celsius);
                Console.WriteLine($"   {celsius}°C = {fahrenheit}°F");
                
                double kelvin = client.celsiusAKelvin(celsius);
                Console.WriteLine($"   {celsius}°C = {kelvin}K\n");
                
                // Conversión de longitud
                Console.WriteLine("2. Conversión de Longitud:");
                double metros = 100.0;
                double pies = client.metrosAPies(metros);
                Console.WriteLine($"   {metros} metros = {pies} pies");
                
                double kilometros = 10.0;
                double millas = client.kilometrosAMillas(kilometros);
                Console.WriteLine($"   {kilometros} km = {millas} millas\n");
                
                // Conversión de peso
                Console.WriteLine("3. Conversión de Peso:");
                double kg = 70.0;
                double libras = client.kilogramosALibras(kg);
                Console.WriteLine($"   {kg} kg = {libras} libras\n");
                
                // Conversión de volumen
                Console.WriteLine("4. Conversión de Volumen:");
                double litros = 50.0;
                double galones = client.litrosAGalones(litros);
                Console.WriteLine($"   {litros} litros = {galones} galones\n");
                
                // Cerrar el cliente
                client.Close();
                
                Console.WriteLine("========================================");
                Console.WriteLine("Todas las pruebas completadas!");
                Console.WriteLine("========================================");
                */
                
                Console.WriteLine("NOTA: Este es un ejemplo básico.");
                Console.WriteLine("Para usar este código:");
                Console.WriteLine("1. En Visual Studio, clic derecho en el proyecto");
                Console.WriteLine("2. Add → Service Reference");
                Console.WriteLine("3. URL: http://192.168.1.100:8080/conversion/WSConversion?wsdl");
                Console.WriteLine("4. Namespace: ConversionService");
                Console.WriteLine("5. Usa las clases generadas automáticamente");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con el servicio: {ex.Message}");
                Console.WriteLine($"Detalles: {ex}");
            }
            
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}


