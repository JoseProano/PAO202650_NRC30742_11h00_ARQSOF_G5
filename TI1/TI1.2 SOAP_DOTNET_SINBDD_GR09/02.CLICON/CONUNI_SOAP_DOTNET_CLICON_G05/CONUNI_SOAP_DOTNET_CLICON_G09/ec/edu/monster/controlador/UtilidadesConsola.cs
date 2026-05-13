using System;

namespace ec.edu.monster.controlador
{
    /// <summary>
    /// Utilidades para la consola
    /// Replicado desde Java
    /// </summary>
    public static class UtilidadesConsola
    {
        /// <summary>
        /// Limpia la pantalla de la consola
        /// </summary>
        public static void LimpiarPantalla()
        {
            try
            {
                // Console.Clear() funciona correctamente en Windows, Linux y Mac
                Console.Clear();
            }
            catch (Exception)
            {
                // Si falla, simplemente imprime muchas líneas en blanco
                for (int i = 0; i < 50; i++)
                {
                    Console.WriteLine();
                }
            }
        }
        
        /// <summary>
        /// Pausa la ejecución por un tiempo determinado
        /// </summary>
        public static void Pausar(int segundos)
        {
            try
            {
                System.Threading.Thread.Sleep(segundos * 1000);
            }
            catch (System.Threading.ThreadInterruptedException)
            {
                System.Threading.Thread.CurrentThread.Interrupt();
            }
        }
        
        /// <summary>
        /// Muestra una línea separadora
        /// </summary>
        public static void MostrarSeparador()
        {
            Console.WriteLine(new string('=', 60));
        }
        
        /// <summary>
        /// Muestra una línea separadora más pequeña
        /// </summary>
        public static void MostrarSeparadorPequeño()
        {
            Console.WriteLine(new string('-', 40));
        }
        
        /// <summary>
        /// Centra un texto en la consola
        /// </summary>
        public static void CentrarTexto(string texto)
        {
            int ancho = 60;
            int espacios = (ancho - texto.Length) / 2;
            Console.WriteLine(new string(' ', Math.Max(0, espacios)) + texto);
        }
    }
}


