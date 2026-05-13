using System;
using ec.edu.monster.controlador;

namespace ec.edu.monster.prueba
{
    /// <summary>
    /// Clase de prueba para el cliente SOAP completo
    /// Replicado desde Java
    /// </summary>
    public class PruebaClienteSOAP
    {
        public static void EjecutarPruebasCompletas()
        {
            Console.WriteLine("=== PRUEBAS DEL CLIENTE SOAP COMPLETO ===");
            Console.WriteLine();

            var controlador = new ControladorConsola();

            try
            {
                // Iniciar la aplicación completa
                controlador.Iniciar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la aplicación: {ex.Message}");
                Console.WriteLine($"Detalles: {ex.StackTrace}");
            }
        }
    }
}
