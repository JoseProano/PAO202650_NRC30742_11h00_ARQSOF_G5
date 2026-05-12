using System;
using System.Text;
using System.Threading;
using ec.edu.monster.modelo;
using ec.edu.monster.controlador;

namespace ec.edu.monster.vista
{
    /// <summary>
    /// Vista para el menú principal y conversiones - Patrón MVC
    /// Maneja la presentación del menú y resultados de conversiones
    /// Replicado desde Java
    /// </summary>
    public class VistaMenu
    {
        public VistaMenu()
        {
            // Configurar la consola para soportar colores en Windows
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                // Habilitar códigos de color ANSI en Windows 10+
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    var handle = GetStdHandle(STD_OUTPUT_HANDLE);
                    GetConsoleMode(handle, out uint mode);
                    SetConsoleMode(handle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
                }
            }
            catch (Exception)
            {
                // Si falla, usar colores nativos de Console
            }
        }
        
        // Constantes para Windows API
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        
        /// <summary>
        /// Aplica color usando Console.ForegroundColor (compatible con Windows)
        /// </summary>
        private void AplicarColor(ConsoleColor color, string texto)
        {
            ConsoleColor colorOriginal = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(texto);
            Console.ForegroundColor = colorOriginal;
        }
        
        /// <summary>
        /// Aplica color usando Console.ForegroundColor y agrega salto de línea
        /// </summary>
        private void AplicarColorLine(ConsoleColor color, string texto)
        {
            ConsoleColor colorOriginal = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(texto);
            Console.ForegroundColor = colorOriginal;
        }
        
        /// <summary>
        /// Limpia completamente la pantalla de forma robusta
        /// </summary>
        private void LimpiarPantallaCompleta()
        {
            try
            {
                // Usar Console.Clear() que funciona correctamente en Windows y otras plataformas
                Console.Clear();
            }
            catch (Exception)
            {
                // Si falla, simplemente imprimir muchas líneas en blanco
                for (int i = 0; i < 50; i++)
                {
                    Console.WriteLine();
                }
                Console.SetCursorPosition(0, 0);
            }
        }
        
        /// <summary>
        /// Muestra el banner principal de la aplicación
        /// </summary>
        public void MostrarBanner()
        {
            MostrarEncabezado();
        }

        /// <summary>
        /// Muestra el encabezado principal
        /// </summary>
        private void MostrarEncabezado()
        {
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Cyan, "████████╗███████╗███╗   ███╗██████╗ ███████╗██████╗  █████╗ ████████╗██╗   ██╗██████╗  █████╗ ");
            AplicarColorLine(ConsoleColor.Cyan, "╚══██╔══╝██╔════╝████╗ ████║██╔══██╗██╔════╝██╔══██╗██╔══██╗╚══██╔══╝██║   ████╔══██╗██╔══██╗");
            AplicarColorLine(ConsoleColor.Cyan, "   ██║   █████╗  ██╔████╔██║██████╔╝█████╗  ██████╔╝███████║   ██║   ██║   ██║██████╔╝███████║");
            AplicarColorLine(ConsoleColor.Cyan, "   ██║   ██╔══╝  ██║╚██╔╝██║██╔═══╝ ██╔══╝  ██╔══██╗██╔══██║   ██║   ██║   ██║██╔══██╗██╔══██║");
            AplicarColorLine(ConsoleColor.Cyan, "   ██║   ███████╗██║ ╚═╝ ██║██║     ███████╗██║  ██║██║  ██║   ██║   ╚██████╔╝██║  ██║██║  ██║");
            AplicarColorLine(ConsoleColor.Cyan, "   ╚═╝   ╚══════╝╚═╝     ╚═╝╚═╝     ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝");
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Green, "╔══════════════════════════════════════════════════════════════════════════════╗");
            AplicarColorLine(ConsoleColor.Green, "║                      SISTEMA DE CONVERSIÓN DE TEMPERATURA                     ║");
            AplicarColorLine(ConsoleColor.Green, "╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Muestra información sobre colores
        /// </summary>
        public void MostrarInfoColores()
        {
            AplicarColorLine(ConsoleColor.Yellow, "Información: Los colores se muestran solo si tu terminal los soporta");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Muestra el menú principal
        /// </summary>
        public void MostrarMenuPrincipal()
        {
            LimpiarPantallaCompleta();
            MostrarEncabezado();
            
            AplicarColorLine(ConsoleColor.Cyan, "╔══════════════════════════════════════════════════════════════════════════════╗");
            AplicarColorLine(ConsoleColor.Cyan, "║                              MENÚ PRINCIPAL                                  ║");
            AplicarColorLine(ConsoleColor.Cyan, "╠══════════════════════════════════════════════════════════════════════════════╣");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[1]");
            Console.Write(" Conversiones de ");
            AplicarColor(ConsoleColor.Yellow, "Temperatura");
            Console.WriteLine("                                        ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[2]");
            Console.Write(" Conversiones de ");
            AplicarColor(ConsoleColor.Yellow, "Longitud");
            Console.WriteLine("                                          ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[3]");
            Console.Write(" Conversiones de ");
            AplicarColor(ConsoleColor.Yellow, "Peso/Masa");
            Console.WriteLine("                                       ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[4]");
            Console.WriteLine(" Pruebas Automáticas                                                      ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[5]");
            Console.WriteLine(" Pruebas Individuales                                                   ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Red, "[0]");
            Console.WriteLine(" Salir                                                                   ║");
            AplicarColorLine(ConsoleColor.Cyan, "╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            AplicarColor(ConsoleColor.Green, "Seleccione una opción: ");
        }
        
        /// <summary>
        /// Lee una opción del menú
        /// </summary>
        public int LeerOpcion()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int opcion))
            {
                return opcion;
            }
            return -1;
        }
        
        /// <summary>
        /// Lee un valor numérico
        /// </summary>
        public double LeerValor()
        {
            AplicarColor(ConsoleColor.Cyan, "Ingrese el valor a convertir: ");
            string input = Console.ReadLine();
            if (double.TryParse(input, out double valor))
            {
                return valor;
            }
            return 0.0;
        }
        
        /// <summary>
        /// Muestra el resultado de una conversión
        /// </summary>
        public void MostrarResultado(Conversion conversion)
        {
            Console.WriteLine();
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Green, "╔══════════════════════════════════════════════════════════════════════════════╗");
            AplicarColorLine(ConsoleColor.Green, "║                             RESULTADO                                        ║");
            AplicarColorLine(ConsoleColor.Green, "╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            
            if (conversion.Exitosa)
            {
                AplicarColorLine(ConsoleColor.Cyan, "¡Conversión exitosa! Los monstruos han trabajado muy bien.");
                Console.WriteLine();
                AplicarColor(ConsoleColor.Yellow, "Resultado: ");
                Console.WriteLine($"  {conversion.ValorOriginal} {conversion.UnidadOriginal} equivale a {conversion.ValorConvertido:F2} {conversion.UnidadDestino}");
            }
            else
            {
                AplicarColorLine(ConsoleColor.Red, "¡Algo monstruoso pasó!");
                Console.WriteLine();
                AplicarColorLine(ConsoleColor.Red, conversion.MensajeError);
                Console.WriteLine();
                AplicarColorLine(ConsoleColor.Yellow, "Sugerencia: Verifica que el servidor esté funcionando y vuelve a intentar.");
            }
            Console.WriteLine();
        }
        
        /// <summary>
        /// Muestra mensaje de opción inválida
        /// </summary>
        public void MostrarOpcionInvalida()
        {
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Red, "ERROR: Opción no válida. Por favor, elija una opción del menú.");
            Thread.Sleep(1500);
        }
        
        /// <summary>
        /// Muestra mensaje de despedida
        /// </summary>
        public void MostrarDespedida()
        {
            Console.Clear();
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Green, "╔══════════════════════════════════════════════════════════════════════════════╗");
            AplicarColorLine(ConsoleColor.Green, "║                             HASTA PRONTO                                      ║");
            AplicarColorLine(ConsoleColor.Green, "╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Cyan, "Gracias por utilizar el sistema Monster de conversión de temperatura.");
            AplicarColorLine(ConsoleColor.Yellow, "¡Que tenga un excelente día!");
            Thread.Sleep(2000);
        }
        
        /// <summary>
        /// Muestra pausa antes de continuar
        /// </summary>
        public void MostrarPausa()
        {
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Yellow, "Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Muestra el menú de temperatura
        /// </summary>
        public void MostrarMenuTemperatura()
        {
            LimpiarPantallaCompleta();
            MostrarEncabezado();
            
            AplicarColorLine(ConsoleColor.Cyan, "╔══════════════════════════════════════════════════════════════════════════════╗");
            AplicarColorLine(ConsoleColor.Cyan, "║                      CONVERSIONES DE TEMPERATURA                      ║");
            AplicarColorLine(ConsoleColor.Cyan, "╠══════════════════════════════════════════════════════════════════════════════╣");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[1]");
            Console.WriteLine(" Celsius a Fahrenheit                                          ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[2]");
            Console.WriteLine(" Fahrenheit a Celsius                                         ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[3]");
            Console.WriteLine(" Celsius a Kelvin                                             ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[4]");
            Console.WriteLine(" Kelvin a Celsius                                             ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[5]");
            Console.WriteLine(" Fahrenheit a Kelvin                                          ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[6]");
            Console.WriteLine(" Kelvin a Fahrenheit                                          ║");
            AplicarColorLine(ConsoleColor.Cyan, "╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            AplicarColor(ConsoleColor.Green, "Seleccione una opción: ");
        }

        /// <summary>
        /// Muestra el menú de longitud
        /// </summary>
        public void MostrarMenuLongitud()
        {
            LimpiarPantallaCompleta();
            MostrarEncabezado();
            
            AplicarColorLine(ConsoleColor.Cyan, "╔══════════════════════════════════════════════════════════════════════════════╗");
            AplicarColorLine(ConsoleColor.Cyan, "║                        CONVERSIONES DE LONGITUD                        ║");
            AplicarColorLine(ConsoleColor.Cyan, "╠══════════════════════════════════════════════════════════════════════════════╣");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[1]");
            Console.WriteLine(" Metros a Pies                                                ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[2]");
            Console.WriteLine(" Pies a Metros                                                ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[3]");
            Console.WriteLine(" Metros a Pulgadas                                            ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[4]");
            Console.WriteLine(" Pulgadas a Metros                                            ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[5]");
            Console.WriteLine(" Kilómetros a Millas                                          ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[6]");
            Console.WriteLine(" Millas a Kilómetros                                          ║");
            AplicarColorLine(ConsoleColor.Cyan, "╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            AplicarColor(ConsoleColor.Green, "Seleccione una opción: ");
        }

        /// <summary>
        /// Muestra el menú de peso/masa
        /// </summary>
        public void MostrarMenuPeso()
        {
            LimpiarPantallaCompleta();
            MostrarEncabezado();
            
            AplicarColorLine(ConsoleColor.Cyan, "╔══════════════════════════════════════════════════════════════════════════════╗");
            AplicarColorLine(ConsoleColor.Cyan, "║                       CONVERSIONES DE PESO/MASA                        ║");
            AplicarColorLine(ConsoleColor.Cyan, "╠══════════════════════════════════════════════════════════════════════════════╣");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[1]");
            Console.WriteLine(" Kilogramos a Libras                                          ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[2]");
            Console.WriteLine(" Libras a Kilogramos                                           ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[3]");
            Console.WriteLine(" Gramos a Onzas                                               ║");
            Console.Write("║  ");
            AplicarColor(ConsoleColor.Green, "[4]");
            Console.WriteLine(" Onzas a Gramos                                               ║");
            AplicarColorLine(ConsoleColor.Cyan, "╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            AplicarColor(ConsoleColor.Green, "Seleccione una opción: ");
        }


        /// <summary>
        /// Muestra banner de pruebas
        /// </summary>
        public void MostrarBannerPruebas()
        {
            LimpiarPantallaCompleta();
            MostrarEncabezado();
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Green, "╔══════════════════════════════════════════════════════════════════════════════╗");
            AplicarColorLine(ConsoleColor.Green, "║                        PRUEBAS AUTOMÁTICAS                            ║");
            AplicarColorLine(ConsoleColor.Green, "╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        /// <summary>
        /// Muestra título de pruebas de temperatura
        /// </summary>
        public void MostrarTituloPruebasTemperatura()
        {
            AplicarColorLine(ConsoleColor.Yellow, "PRUEBAS DE TEMPERATURA");
            Console.WriteLine();
        }

        /// <summary>
        /// Muestra título de pruebas de longitud
        /// </summary>
        public void MostrarTituloPruebasLongitud()
        {
            AplicarColorLine(ConsoleColor.Yellow, "PRUEBAS DE LONGITUD");
            Console.WriteLine();
        }

        /// <summary>
        /// Muestra título de pruebas de peso
        /// </summary>
        public void MostrarTituloPruebasPeso()
        {
            AplicarColorLine(ConsoleColor.Yellow, "PRUEBAS DE PESO/MASA");
            Console.WriteLine();
        }

        /// <summary>
        /// Muestra mensaje de pruebas completadas
        /// </summary>
        public void MostrarPruebasCompletadas()
        {
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Green, "TODAS LAS PRUEBAS COMPLETADAS EXITOSAMENTE");
            Console.WriteLine();
        }

        /// <summary>
        /// Muestra animación de cálculo
        /// </summary>
        public void MostrarCalculando()
        {
            Console.WriteLine();
            AplicarColor(ConsoleColor.Cyan, "Calculando...");
            // Pequeña animación para mostrar que está procesando
            for (int i = 0; i < 5; i++)
            {
                AplicarColor(ConsoleColor.Cyan, ".");
                Thread.Sleep(200);
            }
            Console.WriteLine();
        }
        
        /// <summary>
        /// Cierra los recursos
        /// </summary>
        public void Cerrar()
        {
            // En .NET no necesitamos cerrar Scanner como en Java
        }
    }
}
