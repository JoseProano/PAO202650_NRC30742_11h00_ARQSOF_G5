using System;
using ec.edu.monster.servicios;
using ec.edu.monster.modelo;

namespace ec.edu.monster.prueba
{
    /// <summary>
    /// Clase de prueba para el cliente de conversiones
    /// Replicado desde Java
    /// </summary>
    public class PruebaClienteConversion
    {
        public static void EjecutarPruebas()
        {
            Console.WriteLine("=== PRUEBAS DEL CLIENTE DE CONVERSIONES ===");
            Console.WriteLine();

            var cliente = new ClienteConversionSOAP();

            try
            {
                // Pruebas de temperatura
                Console.WriteLine("--- PRUEBAS DE TEMPERATURA ---");
                Conversion temp1 = cliente.ConvertirTemperatura("celsiusAFahrenheit", 25.0);
                Console.WriteLine(temp1.ToString());

                Conversion temp2 = cliente.ConvertirTemperatura("fahrenheitACelsius", 77.0);
                Console.WriteLine(temp2.ToString());

                // Pruebas de longitud
                Console.WriteLine("\n--- PRUEBAS DE LONGITUD ---");
                Conversion long1 = cliente.ConvertirLongitud("metrosAPies", 10.0);
                Console.WriteLine(long1.ToString());

                Conversion long2 = cliente.ConvertirLongitud("piesAMetros", 32.8);
                Console.WriteLine(long2.ToString());

                // Pruebas de peso
                Console.WriteLine("\n--- PRUEBAS DE PESO ---");
                Conversion peso1 = cliente.ConvertirPeso("kilogramosALibras", 5.0);
                Console.WriteLine(peso1.ToString());

                Conversion peso2 = cliente.ConvertirPeso("librasAKilogramos", 11.0);
                Console.WriteLine(peso2.ToString());

                Console.WriteLine("\n=== PRUEBAS COMPLETADAS ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en las pruebas: {ex.Message}");
            }
            finally
            {
                cliente.Cerrar();
            }
        }
    }
}
