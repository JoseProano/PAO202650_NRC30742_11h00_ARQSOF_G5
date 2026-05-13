using System;
using CONUNI_RESTFUL_DOTNET_CLICON_G09.servicio;

namespace CONUNI_RESTFUL_DOTNET_CLICON_G09.vista
{
    /// <summary>
    /// Vista para la aplicación cliente de consola RESTful
    /// </summary>
    public class VistaConsola
    {
        // Colores para la consola
        public const string CELESTE = "\u001b[38;5;51m";
        public const string VERDE = "\u001b[92m";
        public const string ROJO = "\u001b[91m";
        public const string AMARILLO = "\u001b[93m";
        public const string RESET = "\u001b[0m";
        
        private bool soportaColores;

        /// <summary>
        /// Detecta si el terminal soporta colores
        /// </summary>
        private bool DetectarSoporteColores()
        {
            try
            {
                string term = Environment.GetEnvironmentVariable("TERM");
                
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    return true; // Windows 10+ soporta ANSI
                }
                
                return term != null && !term.Equals("dumb");
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Aplica color solo si el terminal lo soporta
        /// </summary>
        private string AplicarColor(string color, string texto)
        {
            return soportaColores ? color + texto + RESET : texto;
        }

        public VistaConsola()
        {
            this.soportaColores = DetectarSoporteColores();
            
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                try
                {
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                }
                catch { }
            }
        }

        public void MostrarBienvenida()
        {
            MostrarEncabezado();
        }
        
        /// <summary>
        /// Muestra el encabezado principal
        /// </summary>
        private void MostrarEncabezado()
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(CELESTE, "████████╗███████╗███╗   ███╗██████╗ ███████╗██████╗  █████╗ ████████╗██╗   ██╗██████╗  █████╗ "));
            Console.WriteLine(AplicarColor(CELESTE, "╚══██╔══╝██╔════╝████╗ ████║██╔══██╗██╔════╝██╔══██╗██╔══██╗╚══██╔══╝██║   ████╔══██╗██╔══██╗"));
            Console.WriteLine(AplicarColor(CELESTE, "   ██║   █████╗  ██╔████╔██║██████╔╝█████╗  ██████╔╝███████║   ██║   ██║   ████████╔╝███████║"));
            Console.WriteLine(AplicarColor(CELESTE, "   ██║   ██╔══╝  ██║╚██╔╝██║██╔═══╝ ██╔══╝  ██╔══██╗██╔══██║   ██║   ██║   ████╔══██╗██╔══██║"));
            Console.WriteLine(AplicarColor(CELESTE, "   ██║   ███████╗██║ ╚═╝ ██║██║     ███████╗██║  ██║██║  ██║   ██║   ╚██████╔╝██║  ██║██║  ██║"));
            Console.WriteLine(AplicarColor(CELESTE, "   ╚═╝   ╚══════╝╚═╝     ╚═╝╚═╝     ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝"));
            Console.WriteLine();
            Console.WriteLine(AplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(VERDE, "                SISTEMA DE CONVERSIÓN DE UNIDADES RESTFUL               "));
            Console.WriteLine(AplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
        }

        public int MostrarMenu()
        {
            Console.WriteLine(AplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(CELESTE, "                         MENÚ PRINCIPAL                            "));
            Console.WriteLine(AplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[1]") + " Conversiones de " + AplicarColor(AMARILLO, "Temperatura") + "                                         " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[2]") + " Conversiones de " + AplicarColor(AMARILLO, "Longitud") + "                                       " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[3]") + " Conversiones de " + AplicarColor(AMARILLO, "Peso/Masa") + "                                    " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[4]") + " Información del Servicio                                                 " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(ROJO, "[0]") + " Salir                                                                   " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.Write(AplicarColor(VERDE, "Seleccione una opción: "));
            
            try
            {
                string entrada = Console.ReadLine()?.Trim() ?? "";
                return int.Parse(entrada);
            }
            catch
            {
                return -1;
            }
        }

        public void MostrarSubmenuTemperatura()
        {
            Console.WriteLine(AplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(VERDE, "                     CONVERSIONES DE TEMPERATURA                             "));
            Console.WriteLine(AplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.WriteLine(AplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(CELESTE, "                              MENÚ TEMPERATURA                                "));
            Console.WriteLine(AplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[1]") + " " + AplicarColor(AMARILLO, "Celsius") + " → " + AplicarColor(AMARILLO, "Fahrenheit") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[2]") + " " + AplicarColor(AMARILLO, "Fahrenheit") + " → " + AplicarColor(AMARILLO, "Celsius") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[3]") + " " + AplicarColor(AMARILLO, "Celsius") + " → " + AplicarColor(AMARILLO, "Kelvin") + "                                          " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[4]") + " " + AplicarColor(AMARILLO, "Kelvin") + " → " + AplicarColor(AMARILLO, "Celsius") + "                                          " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[5]") + " " + AplicarColor(AMARILLO, "Fahrenheit") + " → " + AplicarColor(AMARILLO, "Kelvin") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[6]") + " " + AplicarColor(AMARILLO, "Kelvin") + " → " + AplicarColor(AMARILLO, "Fahrenheit") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(ROJO, "[0]") + " Volver al menú principal                                               " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.Write(AplicarColor(VERDE, "Seleccione la conversión: "));
        }

        public void MostrarSubmenuLongitud()
        {
            Console.WriteLine(AplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(VERDE, "                         CONVERSIONES DE LONGITUD                            "));
            Console.WriteLine(AplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.WriteLine(AplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(CELESTE, "                              MENÚ LONGITUD                                  "));
            Console.WriteLine(AplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[1]") + " " + AplicarColor(AMARILLO, "Metros") + " → " + AplicarColor(AMARILLO, "Pies") + "                                          " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[2]") + " " + AplicarColor(AMARILLO, "Pies") + " → " + AplicarColor(AMARILLO, "Metros") + "                                          " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[3]") + " " + AplicarColor(AMARILLO, "Metros") + " → " + AplicarColor(AMARILLO, "Pulgadas") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[4]") + " " + AplicarColor(AMARILLO, "Pulgadas") + " → " + AplicarColor(AMARILLO, "Metros") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[5]") + " " + AplicarColor(AMARILLO, "Kilómetros") + " → " + AplicarColor(AMARILLO, "Millas") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[6]") + " " + AplicarColor(AMARILLO, "Millas") + " → " + AplicarColor(AMARILLO, "Kilómetros") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(ROJO, "[0]") + " Volver al menú principal                                               " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.Write(AplicarColor(VERDE, "Seleccione la conversión: "));
        }

        public void MostrarSubmenuPeso()
        {
            Console.WriteLine(AplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(VERDE, "                         CONVERSIONES DE PESO/MASA                            "));
            Console.WriteLine(AplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.WriteLine(AplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(CELESTE, "                              MENÚ PESO/MASA                                 "));
            Console.WriteLine(AplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[1]") + " " + AplicarColor(AMARILLO, "Kilogramos") + " → " + AplicarColor(AMARILLO, "Libras") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[2]") + " " + AplicarColor(AMARILLO, "Libras") + " → " + AplicarColor(AMARILLO, "Kilogramos") + "                                        " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[3]") + " " + AplicarColor(AMARILLO, "Gramos") + " → " + AplicarColor(AMARILLO, "Onzas") + "                                          " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(VERDE, "[4]") + " " + AplicarColor(AMARILLO, "Onzas") + " → " + AplicarColor(AMARILLO, "Gramos") + "                                          " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "") + "  " + AplicarColor(ROJO, "[0]") + " Volver al menú principal                                               " + AplicarColor(CELESTE, ""));
            Console.WriteLine(AplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.Write(AplicarColor(VERDE, "Seleccione la conversión: "));
        }



        public double SolicitarValor(string unidad)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(AplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
                Console.WriteLine(AplicarColor(VERDE, "                         INGRESO DE VALOR                                  "));
                Console.WriteLine(AplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
                Console.WriteLine();
                Console.Write(AplicarColor(AMARILLO, "Ingrese el valor a convertir: "));
                try
                {
                    string entrada = Console.ReadLine()?.Replace(",", ".") ?? "";
                    double valor = double.Parse(entrada);
                    
                    if (valor < 0)
                    {
                        MostrarErrorEntrada("No se permiten valores negativos");
                        continue;
                    }
                    
                    return valor;
                }
                catch
                {
                    MostrarErrorEntrada("Debe ingresar un valor numérico válido");
                }
            }
        }
        
        /// <summary>
        /// Muestra error de entrada
        /// </summary>
        public void MostrarErrorEntrada(string mensaje)
        {
            Console.WriteLine(AplicarColor(ROJO, "ERROR: " + mensaje));
        }

        public void MostrarResultado(ConversionResponse response)
        {
            Console.WriteLine();
            
            if (response.Exito)
            {
                Console.WriteLine(AplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
                Console.WriteLine(AplicarColor(VERDE, "                             RESULTADO                                        "));
                Console.WriteLine(AplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
                Console.WriteLine();
                Console.WriteLine("  " + AplicarColor(AMARILLO, $"{response.ValorOriginal} {response.UnidadOrigen} = {response.ValorConvertido:F4} {response.UnidadDestino}"));
                Console.WriteLine(AplicarColor(CELESTE, "Operación: " + response.Categoria));
            }
            else
            {
                Console.WriteLine(AplicarColor(ROJO, "╔══════════════════════════════════════════════════════════════════════════════╗"));
                Console.WriteLine(AplicarColor(ROJO, "                        ¡ERROR MONSTRUOSO DETECTADO!                          "));
                Console.WriteLine(AplicarColor(ROJO, "╚══════════════════════════════════════════════════════════════════════════════╝"));
                Console.WriteLine();
                Console.WriteLine(AplicarColor(ROJO, response.Mensaje));
                Console.WriteLine();
                Console.WriteLine(AplicarColor(AMARILLO, "╔══════════════════════════════════════════════════════════════════════════════╗"));
                Console.WriteLine(AplicarColor(AMARILLO, "                    ¡ESPERA A QUE EL MONSTRUO SE CALME!                      "));
                Console.WriteLine(AplicarColor(AMARILLO, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            }
            Console.WriteLine();
        }

        public void MostrarInfoServicio(string info)
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(VERDE, "                         INFORMACIÓN DEL SERVICIO                               "));
            Console.WriteLine(AplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.WriteLine(info);
            Console.WriteLine();
        }

        public void MostrarError(string mensaje)
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(ROJO, "❌ ERROR: " + mensaje));
        }

        public void MostrarOpcionNoValida()
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(ROJO, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(ROJO, "                        ⚠️  OPCIÓN NO VÁLIDA ⚠️                                  "));
            Console.WriteLine(AplicarColor(ROJO, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.WriteLine(AplicarColor(AMARILLO, "Por favor, seleccione una opción válida del menú."));
            Console.WriteLine();
        }

        public void MostrarPausa()
        {
            Console.Write(AplicarColor(AMARILLO, "\nPresione cualquier tecla para continuar..."));
            Console.ReadKey();
        }

        public void MostrarDespedida()
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            Console.WriteLine(AplicarColor(VERDE, "                             HASTA PRONTO                                      "));
            Console.WriteLine(AplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            Console.WriteLine();
            Console.WriteLine(AplicarColor(CELESTE, "Gracias por utilizar el sistema Monster de conversión de unidades."));
            Console.WriteLine(AplicarColor(AMARILLO, "¡Que tenga un excelente día!"));
            Console.WriteLine();
        }
    }
}

