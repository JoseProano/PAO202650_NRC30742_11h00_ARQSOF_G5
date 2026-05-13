using System;

namespace CONUNI_RESTFUL_DOTNET_CLICON_G09.vista
{
    /// <summary>
    /// Vista para el proceso de autenticación - Patrón MVC
    /// Maneja la presentación del login y entrada de credenciales
    /// </summary>
    public class VistaLogin
    {
        // Colores para la consola (códigos ANSI)
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
                // En Windows 10+, PowerShell y CMD modernos soportan colores ANSI
                string term = Environment.GetEnvironmentVariable("TERM");
                string os = Environment.OSVersion.ToString();
                
                // Habilitar colores ANSI en Windows
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    // Intentar habilitar colores ANSI en Windows
                    try
                    {
                        var handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                        // Windows 10+ soporta ANSI por defecto
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
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

        public VistaLogin()
        {
            // Detectar soporte de colores
            this.soportaColores = DetectarSoporteColores();
            
            // Habilitar colores ANSI en Windows si es posible
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                try
                {
                    // Intentar habilitar modo ANSI
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                }
                catch { }
            }
        }

        /// <summary>
        /// Muestra el banner de login
        /// </summary>
        public void MostrarBannerLogin()
        {
            MostrarLogo();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(AplicarColor(CELESTE, "MONSTERS INC. CONVERTER RESTFUL - AUTENTICACIÓN"));
            Console.WriteLine(AplicarColor(CELESTE, "Sistema de Seguridad Empresarial"));
            Console.WriteLine(new string('=', 60));
            Console.WriteLine();
            Console.WriteLine(AplicarColor(AMARILLO, "Acceso restringido - Ingrese sus credenciales"));
            Console.WriteLine();
        }

        /// <summary>
        /// Muestra el logo ASCII de MONSTERS INC
        /// </summary>
        private void MostrarLogo()
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(CELESTE, "███╗   ███╗ ██████╗ ███╗   ██╗███████╗████████╗███████╗██████╗     ██████╗ ██████╗ ██████╗"));
            Console.WriteLine(AplicarColor(CELESTE, "████╗ ████║██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗   ██╔════╝ ██╔══██╗╚═══██╗"));
            Console.WriteLine(AplicarColor(CELESTE, "██╔████╔██║██║   ██║██╔██╗ ██║███████╗   ██║   █████╗  ██████╔╝   ██║  ███╗██████╔╝  ███╔╝"));
            Console.WriteLine(AplicarColor(CELESTE, "██║╚██╔╝██║██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══╝  ██╔══██╗   ██║   ██║██╔══██╗ ███╔╝"));
            Console.WriteLine(AplicarColor(CELESTE, "██║ ╚═╝ ██║╚██████╔╝██║ ╚████║███████║   ██║   ███████╗██║  ██║   ╚██████╔╝██║  ██║██████╗"));
            Console.WriteLine(AplicarColor(CELESTE, "╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═════╝"));
            Console.WriteLine();
        }

        /// <summary>
        /// Lee el nombre de usuario
        /// </summary>
        public string LeerUsuario()
        {
            Console.Write(AplicarColor(CELESTE, "Usuario: "));
            return Console.ReadLine()?.Trim() ?? "";
        }

        /// <summary>
        /// Lee la contraseña oculta con asteriscos
        /// </summary>
        public string LeerContrasenaOculta()
        {
            Console.Write(AplicarColor(CELESTE, "Contraseña: "));
            
            string contraseña = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    contraseña += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && contraseña.Length > 0)
                {
                    contraseña = contraseña.Substring(0, contraseña.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return contraseña;
        }

        /// <summary>
        /// Muestra mensaje de éxito
        /// </summary>
        public void MostrarMensajeExito()
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(VERDE, "¡Autenticación exitosa!"));
            Console.WriteLine(AplicarColor(VERDE, "Bienvenido al sistema Monsters Inc. Converter RESTful"));
            Console.WriteLine(new string('=', 60));
            Console.WriteLine();
        }

        /// <summary>
        /// Muestra mensaje de error
        /// </summary>
        public void MostrarMensajeError(int intentoActual, int maxIntentos)
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(ROJO, "Credenciales incorrectas"));
            
            if (intentoActual < maxIntentos)
            {
                Console.WriteLine(AplicarColor(AMARILLO, "Intente nuevamente..."));
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Muestra mensaje de bloqueo
        /// </summary>
        public void MostrarMensajeBloqueo()
        {
            Console.WriteLine();
            Console.WriteLine(AplicarColor(ROJO, "ACCESO DENEGADO"));
            Console.WriteLine(AplicarColor(ROJO, "Demasiados intentos fallidos"));
            Console.WriteLine(AplicarColor(ROJO, "Contacte al administrador del sistema"));
            Console.WriteLine(new string('=', 60));
            Console.WriteLine();
        }

        /// <summary>
        /// Muestra información sobre el intento actual
        /// </summary>
        public void MostrarIntento(int intento, int maxIntentos)
        {
            Console.WriteLine(AplicarColor(AMARILLO, "INTENTO " + intento + " de " + maxIntentos));
            Console.WriteLine();
        }
    }
}

