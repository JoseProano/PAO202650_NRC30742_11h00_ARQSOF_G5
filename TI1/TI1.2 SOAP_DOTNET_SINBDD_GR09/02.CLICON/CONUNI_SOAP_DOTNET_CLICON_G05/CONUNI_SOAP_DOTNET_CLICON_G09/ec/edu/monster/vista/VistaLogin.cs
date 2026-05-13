using System;
using System.Text;

namespace ec.edu.monster.vista
{
    /// <summary>
    /// Vista para el proceso de autenticación - Patrón MVC
    /// Maneja la presentación del login y entrada de credenciales
    /// Replicado desde Java
    /// </summary>
    public class VistaLogin
    {
        public VistaLogin()
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
        /// Muestra el banner de login
        /// </summary>
        public void MostrarBannerLogin()
        {
            LimpiarPantalla();
            MostrarLogo();
            Console.WriteLine(new string('=', 60));
            AplicarColorLine(ConsoleColor.Cyan, "MONSTERS INC. CONVERTER - AUTENTICACIÓN");
            AplicarColorLine(ConsoleColor.Cyan, "Sistema de Seguridad Empresarial");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Yellow, "Acceso restringido - Ingrese sus credenciales");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Muestra el logo ASCII de MONSTERS INC
        /// </summary>
        private void MostrarLogo()
        {
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Cyan, "███╗   ███╗ ██████╗ ███╗   ██╗███████╗████████╗███████╗██████╗     ██████╗ ██████╗ ███████╗");
            AplicarColorLine(ConsoleColor.Cyan, "████╗ ████║██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗   ██╔════╝ ██╔══██╗██╔════╝");
            AplicarColorLine(ConsoleColor.Cyan, "██╔████╔██║██║   ██║██╔██╗ ██║███████╗   ██║   █████╗  ██████╔╝   ██║  ███╗██████╔╝███████╗");
            AplicarColorLine(ConsoleColor.Cyan, "██║╚██╔╝██║██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══╝  ██╔══██╗   ██║   ██║██╔══██╗╚════██║");
            AplicarColorLine(ConsoleColor.Cyan, "██║ ╚═╝ ██║╚██████╔╝██║ ╚████║███████║   ██║   ███████╗██║  ██║   ╚██████╔╝██║  ██║███████║");
            AplicarColorLine(ConsoleColor.Cyan, "╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Limpia la pantalla
        /// </summary>
        private void LimpiarPantalla()
        {
            Console.Clear();
        }
        
        /// <summary>
        /// Lee el nombre de usuario
        /// </summary>
        public string LeerUsuario()
        {
            AplicarColor(ConsoleColor.Cyan, "Usuario: ");
            return Console.ReadLine()?.Trim() ?? "";
        }
        
        /// <summary>
        /// Lee la contraseña oculta con asteriscos
        /// </summary>
        public string LeerContrasenaOculta()
        {
            AplicarColor(ConsoleColor.Cyan, "Contraseña: ");
            
            var pass = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.Remove(pass.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pass.Append(key.KeyChar);
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Salto de línea al final
            return pass.ToString();
        }
        
        /// <summary>
        /// Muestra mensaje de éxito
        /// </summary>
        public void MostrarMensajeExito()
        {
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Green, "¡Autenticación exitosa!");
            AplicarColorLine(ConsoleColor.Green, "Bienvenido al sistema Monsters Inc. Converter");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine();
        }
        
        /// <summary>
        /// Muestra mensaje de error
        /// </summary>
        public void MostrarMensajeError(int intentoActual, int maxIntentos)
        {
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Red, "¡Uy! Esas no son las credenciales. Por favor, intente nuevamente.");
            
            if (intentoActual < maxIntentos)
            {
                AplicarColorLine(ConsoleColor.Yellow, "Intente nuevamente...");
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Muestra mensaje de bloqueo
        /// </summary>
        public void MostrarMensajeBloqueo()
        {
            Console.WriteLine();
            AplicarColorLine(ConsoleColor.Red, "ACCESO DENEGADO");
            AplicarColorLine(ConsoleColor.Red, "Demasiados intentos fallidos");
            AplicarColorLine(ConsoleColor.Red, "Contacte al administrador del sistema");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine();
        }
        
        /// <summary>
        /// Muestra información sobre el intento actual
        /// </summary>
        public void MostrarIntento(int intento, int maxIntentos)
        {
            AplicarColorLine(ConsoleColor.Yellow, "INTENTO " + intento + " de " + maxIntentos);
            Console.WriteLine();
        }
        
        /// <summary>
        /// Cierra los recursos
        /// </summary>
        public void Cerrar()
        {
            
        }
    }
}
