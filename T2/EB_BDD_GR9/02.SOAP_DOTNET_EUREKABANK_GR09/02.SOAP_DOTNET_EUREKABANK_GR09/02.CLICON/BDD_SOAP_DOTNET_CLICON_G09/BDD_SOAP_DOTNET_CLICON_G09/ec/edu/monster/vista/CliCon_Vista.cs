using BDD_SOAP_DOTNET_CLICON_G09.ec.edu.monster.controlador;
using BDD_SOAP_DOTNET_CLICON_G09.ec.edu.monster.vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDD_SOAP_DOTNET_CLICON_G09;

namespace BDD_SOAP_DOTNET_CLICON_G09
{
    public class CliCon_Vista
    {
        private const string USUARIO = "MONSTER";
        private const string PASS = "MONSTER9";
        public static void Main(string[] args)
        {
            string[] logo = new[]
                {
                    "/\\      /\\ /\\    /\\  /\\    /\\ /\\    /\\ /\\  /\\  /\\   /\\ /\\  /\\      /\\   /\\ /\\   /\\   /\\   /\\",
                    "          ███╗   ███╗ ██████╗ ███╗   ██╗███████╗████████╗███████╗██████╗ ",
                    "          ████╗ ████║██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗",
                    "          ██╔████╔██║██║   ██║██╔██╗ ██║███████╗   ██║   █████╗  ██████╔╝",
                    "          ██║╚██╔╝██║██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══╝  ██╔══██╗",
                    "          ██║ ╚═╝ ██║╚██████╔╝██║ ╚████║███████║   ██║   ███████╗██║  ██║",
                    "          ╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝"
                };
            MostrarLogoYBienvenido(logo);

            int anchoCuadro = logo.Max(line => line.Length);

            var scanner = new Scanner();
            var controlador = new CliCon_Controlador();

            bool acceso = false;

            while (!acceso)
            {
                Console.Clear();
                MostrarLogoYBienvenido(logo);

                var (usuario, password) = PedirCredenciales(anchoCuadro);

                if (USUARIO.Equals(usuario, StringComparison.OrdinalIgnoreCase) && PASS.Equals(password, StringComparison.OrdinalIgnoreCase))
                {
                    acceso = true;

                    string mensajeExito = "Acceso exitoso";
                    string mensajeContinuar = "Presione cualquier tecla para continuar...";

                    int anchoConsola = Console.WindowWidth;

                    // Centrar mensaje de acceso exitoso
                    int posX = (anchoConsola - mensajeExito.Length) / 2;
                    int posY = Console.CursorTop + 2;
                    Console.SetCursorPosition(posX, posY);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(mensajeExito);

                    // Centrar mensaje de continuar
                    posX = (anchoConsola - mensajeContinuar.Length) / 2;
                    Console.SetCursorPosition(posX, posY + 1);
                    Console.WriteLine(mensajeContinuar);

                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    string mensajeDenegado = "Acceso denegado";
                    string mensajeContinuar = "Presione cualquier tecla para intentar nuevamente...";

                    int anchoConsola = Console.WindowWidth;

                    // Centrar mensaje de acceso denegado
                    int posX = (anchoConsola - mensajeDenegado.Length) / 2;
                    int posY = Console.CursorTop + 2;
                    Console.SetCursorPosition(posX, posY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(mensajeDenegado);

                    // Centrar mensaje de continuar
                    posX = (anchoConsola - mensajeContinuar.Length) / 2;
                    Console.SetCursorPosition(posX, posY + 1);
                    Console.WriteLine(mensajeContinuar);

                    Console.ResetColor();
                    Console.ReadKey();
                }

            }

            Console.Clear();
            MostrarLogoYBienvenido(logo);

            EjecutarMenu(scanner, controlador, logo);
        }

        public static void MostrarLogoYBienvenido(string[] logo)
        {
            Console.Clear();

            int width = Console.WindowWidth;
            int logoAncho = logo.Max(line => line.Length);
            int startPos = (width - logoAncho) / 2;

            // Imprimir logo con degradado cyan-verde
            int mitad = logo.Length / 2;

            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < mitad; i++)
            {
                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(logo[i]);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = mitad; i < logo.Length; i++)
            {
                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(logo[i]);
            }

            // Imprimir cuadro BIENVENIDO
            string titulo = "BIENVENIDO";
            int cuadroAncho = logoAncho;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.WriteLine(new string('-', cuadroAncho));

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(startPos, Console.CursorTop);

            int paddingTotal = cuadroAncho - 2 - titulo.Length;
            int paddingIzq = paddingTotal / 2;
            int paddingDer = paddingTotal - paddingIzq;

            Console.WriteLine("|" + new string(' ', paddingIzq) + titulo + new string(' ', paddingDer) + "|");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.WriteLine(new string('-', cuadroAncho));

            Console.ResetColor();
        }

        public static (string usuario, string contraseña) PedirCredenciales(int anchoCuadro)
        {
            string usuario, contraseña;

            int width = Console.WindowWidth;
            int startPos = (width - anchoCuadro) / 2;

            // Línea superior del cuadro - borde azul
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.WriteLine(new string('-', anchoCuadro));

            // Líneas internas del cuadro con borde azul
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.Write("|" + new string(' ', anchoCuadro - 2) + "|");

            Console.SetCursorPosition(startPos, Console.CursorTop + 1);
            Console.Write("|" + new string(' ', anchoCuadro - 2) + "|");

            Console.SetCursorPosition(startPos, Console.CursorTop + 1);
            Console.Write("|" + new string(' ', anchoCuadro - 2) + "|");

            // Línea inferior del cuadro - borde azul
            Console.SetCursorPosition(startPos, Console.CursorTop + 1);
            Console.WriteLine(new string('-', anchoCuadro));

            // Texto "Ingrese su usuario:" en blanco, dentro del cuadro
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(startPos + 2, Console.CursorTop - 4);
            string textoUsuario = "Ingrese su usuario: ";
            Console.Write(textoUsuario);

            // Entrada usuario en verde
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(startPos + 2 + textoUsuario.Length, Console.CursorTop);
            usuario = Console.ReadLine();

            // Texto "Ingrese su contraseña:" en blanco, dentro del cuadro
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(startPos + 2, Console.CursorTop + 1);
            string textoContrasena = "Ingrese su contraseña: ";
            Console.Write(textoContrasena);

            // Entrada contraseña en verde, oculta con asteriscos
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(startPos + 2 + textoContrasena.Length, Console.CursorTop);

            StringBuilder passBuilder = new StringBuilder();
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (passBuilder.Length > 0)
                    {
                        passBuilder.Remove(passBuilder.Length - 1, 1);
                        int curPos = Console.CursorLeft;
                        Console.SetCursorPosition(curPos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(curPos - 1, Console.CursorTop);
                    }
                }
                else
                {
                    passBuilder.Append(key.KeyChar);
                    Console.Write("*");
                }
            }

            contraseña = passBuilder.ToString();

            Console.ResetColor();

            return (usuario, contraseña);
        }

        public static void EjecutarMenu(Scanner scanner, CliCon_Controlador controlador, string[] logo)
        {
            int opcion;
            do
            {
                Console.Clear();

                // Mostrar logo y bienvenida
                MostrarLogoYBienvenido(logo);

                // Calcular el ancho del cuadro para el menú usando el logo
                int anchoCuadro = logo.Max(line => line.Length) + 4;

                // Mostrar menú con ancho calculado
                opcion = MostrarMenu(anchoCuadro);

                switch (opcion)
                {
                    case 1: ConsultarMovimientos(controlador, logo, anchoCuadro); break;
                    case 2: RealizarDeposito(scanner, controlador, logo, anchoCuadro); break;
                    case 3: RealizarRetiro(scanner, controlador, logo, anchoCuadro); break;
                    case 4: RealizarTransferencia(scanner, controlador, logo, anchoCuadro); break;
                    case 5: Salir(logo); break;
                    default:
                        MostrarOpcionNoValida();
                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 5);
        }

        public static int MostrarMenu(int anchoCuadro)
        {
            int width = Console.WindowWidth;
            int startPos = (width - anchoCuadro) / 2;

            // Dibujar línea superior del cuadro azul
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.WriteLine(new string('-', anchoCuadro));

            // Guardar línea base para posicionar contenido dentro del cuadro
            int lineaBase = Console.CursorTop;

            // Dibujar líneas internas vacías con bordes
            int lineasInternas = 9;
            for (int i = 0; i < lineasInternas; i++)
            {
                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.Write("|" + new string(' ', anchoCuadro - 2) + "|");
                Console.WriteLine();
            }

            // Dibujar línea inferior del cuadro azul
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.WriteLine(new string('-', anchoCuadro));

            // Escribir título centrado en la primera línea interna
            string titulo = "MENÚ PRINCIPAL";
            int tituloPos = startPos + (anchoCuadro - titulo.Length) / 2;
            Console.SetCursorPosition(tituloPos, lineaBase);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(titulo);

            string[] opciones =
            {
                "Consultar movimientos",
                "Realizar depósito",
                "Realizar retiro",
                "Realizar transferencia",
                "Salir"
            };

            // Desplazamiento para mover números y texto juntos a la izquierda
            int desplazamientoIzquierda = 10;

            // Centro del cuadro para alinear números (ajustado por desplazamiento)
            int centroCuadro = (startPos + anchoCuadro / 2) - desplazamientoIzquierda;

            // Ancho fijo para columna de números
            int anchoNumeros = 3;
            int numeroPos = centroCuadro - (anchoNumeros / 2);

            // Posición para texto, manteniendo separación con números
            int textoPos = numeroPos + anchoNumeros + 2;

            for (int i = 0; i < opciones.Length; i++)
            {
                int lineaY = lineaBase + 2 + i;

                // Números centrados en posición ajustada
                Console.SetCursorPosition(numeroPos, lineaY);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{i + 1}.");

                // Texto alineado a la izquierda en posición fija ajustada
                Console.SetCursorPosition(textoPos, lineaY);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(opciones[i]);
            }

            // Texto "Seleccione una opción:" centrado debajo de las opciones
            string textoSeleccion = "Seleccione una opción: ";
            int seleccionPos = startPos + (anchoCuadro - textoSeleccion.Length) / 2;
            Console.SetCursorPosition(seleccionPos, lineaBase + 2 + opciones.Length + 1);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(textoSeleccion);
            Console.ResetColor();

            // Validación de la opción ingresada
            int opcionSeleccionada;
            while (true)
            {
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= opciones.Length)
                {
                    break;
                }

                string errorMsg = "Opción no válida. Intente de nuevo.";
                int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                Console.SetCursorPosition(errorPos, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMsg);
                Console.ResetColor();

                // Reescribir texto selección para nuevo intento
                Console.SetCursorPosition(seleccionPos, Console.CursorTop);
                Console.Write(new string(' ', textoSeleccion.Length));
                Console.SetCursorPosition(seleccionPos, Console.CursorTop);
                Console.Write(textoSeleccion);
            }

            return opcionSeleccionada;
        }

        public static void ConsultarMovimientos(CliCon_Controlador controlador, string[] logo, int anchoCuadro)
        {
            int width = Console.WindowWidth;
            int startPos = (width - anchoCuadro) / 2;

            while (true)
            {
                Console.Clear();
                MostrarLogoYBienvenido(logo);

                // Cuadro azul para el título y entrada
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                int lineaBase = Console.CursorTop;

                int lineasInternas = 7;
                for (int i = 0; i < lineasInternas; i++)
                {
                    Console.SetCursorPosition(startPos, Console.CursorTop);
                    Console.Write("|" + new string(' ', anchoCuadro - 2) + "|");
                    Console.WriteLine();
                }

                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                // Título MOVIMIENTOS centrado en magenta
                string titulo = "MOVIMIENTOS";
                int tituloPos = startPos + (anchoCuadro - titulo.Length) / 2;
                Console.SetCursorPosition(tituloPos, lineaBase);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(titulo);

                // Solicitar número de cuenta con ESC para salir
                string textoCuenta = "Ingrese el número de cuenta (8 dígitos): ";
                int textoPos = startPos + 2;
                Console.SetCursorPosition(textoPos, lineaBase + 2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', anchoCuadro - 4));
                Console.SetCursorPosition(textoPos, lineaBase + 2);
                Console.Write(textoCuenta);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(textoPos + textoCuenta.Length, lineaBase + 2);

                if (!TryLeerLineaConEscape(out string cuenta))
                {
                    break;
                }

                // Validar entrada
                if (!(cuenta.Length == 8 && cuenta.All(char.IsDigit)))
                {
                    string errorMsg = "Número inválido. Debe tener 8 dígitos numéricos.";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.SetCursorPosition(errorPos, lineaBase + 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                Console.ResetColor();

                // Obtener movimientos
                var movimientos = controlador.ObtenerMovimientos(cuenta);

                Console.Clear();
                MostrarLogoYBienvenido(logo);

                if (movimientos.Count == 0)
                {
                    string msgNoMovimientos = "No se encontraron movimientos para la cuenta ingresada.";
                    int msgPos = startPos + (anchoCuadro - msgNoMovimientos.Length) / 2;
                    Console.SetCursorPosition(msgPos, Console.CursorTop + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msgNoMovimientos);
                    Console.ResetColor();
                }
                else
                {
                    // Mostrar número de cuenta
                    string cuentaMsg = $"Cuenta consultada: {cuenta}";
                    int cuentaMsgPos = startPos + (anchoCuadro - cuentaMsg.Length) / 2;
                    Console.SetCursorPosition(cuentaMsgPos, Console.CursorTop);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(cuentaMsg);
                    Console.ResetColor();
                    Console.WriteLine();

                    // Mostrar saldo actual fuera de la tabla, centrado y en amarillo
                    double saldoActual = controlador.ObtenerSaldo(cuenta);
                    string saldoMsg = $"Saldo actual: ${saldoActual:N2}";
                    int saldoMsgPos = startPos + (anchoCuadro - saldoMsg.Length) / 2;
                    Console.SetCursorPosition(saldoMsgPos, Console.CursorTop);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(saldoMsg);
                    Console.ResetColor();
                    Console.WriteLine();

                    MovimientoView.MostrarMovimientos(movimientos, anchoCuadro);
                }

                // Línea azul para separar
                Console.ForegroundColor = ConsoleColor.Blue;

                int cursorTop = Console.CursorTop + 1;
                if (cursorTop >= Console.BufferHeight)
                {
                    cursorTop = Console.BufferHeight - 1;
                }
                Console.SetCursorPosition(startPos, cursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                string mensajeRegresar = "Presione ENTER para ingresar otra cuenta o ESC para salir...";
                int mensajePos = startPos + (anchoCuadro - mensajeRegresar.Length) / 2;
                Console.SetCursorPosition(mensajePos, Console.CursorTop);
                Console.WriteLine(mensajeRegresar);

                Console.ResetColor();

                // Esperar tecla sin limpiar pantalla
                var tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        // Método auxiliar para leer línea con posibilidad de detectar ESC
        public static bool TryLeerLineaConEscape(out string texto)
        {
            texto = "";
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    texto = null;
                    return false;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return true;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (texto.Length > 0)
                    {
                        texto = texto.Substring(0, texto.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    texto += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
            }
        }

        public static void RealizarDeposito(Scanner scanner, CliCon_Controlador controlador, string[] logo, int anchoCuadro)
        {
            int width = Console.WindowWidth;
            int startPos = (width - anchoCuadro) / 2;

            while (true)
            {
                Console.Clear();
                MostrarLogoYBienvenido(logo);

                // Cuadro azul para título y marco
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                int lineaBase = Console.CursorTop;

                int lineasInternas = 8;
                for (int i = 0; i < lineasInternas; i++)
                {
                    Console.SetCursorPosition(startPos, Console.CursorTop);
                    Console.Write("|" + new string(' ', anchoCuadro - 2) + "|");
                    Console.WriteLine();
                }

                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                // Título DEPÓSITO en magenta
                string titulo = "DEPÓSITO";
                int tituloPos = startPos + (anchoCuadro - titulo.Length) / 2;
                Console.SetCursorPosition(tituloPos, lineaBase);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(titulo);

                // Entrada: Número de cuenta
                string textoCuenta = "Ingrese el número de cuenta: ";
                int textoPos = startPos + 2;
                int textoLinea = lineaBase + 2;

                Console.SetCursorPosition(textoPos, textoLinea);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', anchoCuadro - 4));
                Console.SetCursorPosition(textoPos, textoLinea);
                Console.Write(textoCuenta);

                Console.SetCursorPosition(textoPos + textoCuenta.Length, textoLinea);
                Console.ForegroundColor = ConsoleColor.Green;

                if (!TryLeerLineaConEscape(out string cuentaDeposito))
                    break;

                // Validar cuenta
                if (cuentaDeposito.Length != 8 || !cuentaDeposito.All(char.IsDigit))
                {
                    string errorMsg = "Número inválido. Debe tener 8 dígitos numéricos.";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.SetCursorPosition(errorPos, textoLinea + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                // Entrada: Importe
                string textoImporte = "Ingrese el importe a depositar: ";
                int textoImporteLinea = textoLinea + 3;

                Console.SetCursorPosition(textoPos, textoImporteLinea);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', anchoCuadro - 4));
                Console.SetCursorPosition(textoPos, textoImporteLinea);
                Console.Write(textoImporte);

                Console.SetCursorPosition(textoPos + textoImporte.Length, textoImporteLinea);
                Console.ForegroundColor = ConsoleColor.Green;

                if (!TryLeerLineaConEscape(out string importeStr))
                    break;

                // Intentar parsear importe
                if (!double.TryParse(importeStr, out double importe))
                {
                    string errorMsg = "Importe inválido. Debe ser un número válido.";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.SetCursorPosition(errorPos, textoImporteLinea + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                Console.ResetColor();

                // Intentar registrar depósito y mostrar mensaje centrado
                try
                {
                    controlador.RegistrarDeposito(cuentaDeposito, importe);
                    string exitoMsg = "Depósito realizado exitosamente.";
                    int exitoPos = startPos + (anchoCuadro - exitoMsg.Length) / 2;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(exitoPos, textoImporteLinea + 3);
                    Console.WriteLine(exitoMsg);
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Error al realizar el depósito: {ex.Message}";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(errorPos, textoImporteLinea + 3);
                    Console.WriteLine(errorMsg);
                }

                // Mensaje para continuar o salir
                string mensajeSalir = "Presione ENTER para limpiar y continuar o ESC para regresar al MENÚ PRINCIPAL";
                int mensajePos = (width - mensajeSalir.Length) / 2;
                int mensajeLinea = textoImporteLinea + 5;

                Console.SetCursorPosition(mensajePos, mensajeLinea);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(mensajeSalir);
                Console.ResetColor();

                var tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        public static void RealizarRetiro(Scanner scanner, CliCon_Controlador controlador, string[] logo, int anchoCuadro)
        {
            int width = Console.WindowWidth;
            int startPos = (width - anchoCuadro) / 2;

            while (true)
            {
                Console.Clear();
                MostrarLogoYBienvenido(logo);

                // Cuadro azul para título y marco
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                int lineaBase = Console.CursorTop;

                int lineasInternas = 8;
                for (int i = 0; i < lineasInternas; i++)
                {
                    Console.SetCursorPosition(startPos, Console.CursorTop);
                    Console.Write("|" + new string(' ', anchoCuadro - 2) + "|");
                    Console.WriteLine();
                }

                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                // Título RETIRO en magenta centrado
                string titulo = "RETIRO";
                int tituloPos = startPos + (anchoCuadro - titulo.Length) / 2;
                Console.SetCursorPosition(tituloPos, lineaBase);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(titulo);

                // Entrada: Número de cuenta
                string textoCuenta = "Ingrese el número de cuenta: ";
                int textoPos = startPos + 2;
                int textoLinea = lineaBase + 2;

                Console.SetCursorPosition(textoPos, textoLinea);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', anchoCuadro - 4));
                Console.SetCursorPosition(textoPos, textoLinea);
                Console.Write(textoCuenta);

                Console.SetCursorPosition(textoPos + textoCuenta.Length, textoLinea);
                Console.ForegroundColor = ConsoleColor.Green;

                if (!TryLeerLineaConEscape(out string cuentaRetiro))
                    break;

                // Validar cuenta
                if (cuentaRetiro.Length != 8 || !cuentaRetiro.All(char.IsDigit))
                {
                    string errorMsg = "Número inválido. Debe tener 8 dígitos numéricos.";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.SetCursorPosition(errorPos, textoLinea + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                // Entrada: Importe
                string textoImporte = "Ingrese el importe a retirar: ";
                int textoImporteLinea = textoLinea + 3;

                Console.SetCursorPosition(textoPos, textoImporteLinea);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', anchoCuadro - 4));
                Console.SetCursorPosition(textoPos, textoImporteLinea);
                Console.Write(textoImporte);

                Console.SetCursorPosition(textoPos + textoImporte.Length, textoImporteLinea);
                Console.ForegroundColor = ConsoleColor.Green;

                if (!TryLeerLineaConEscape(out string importeStr))
                    break;

                // Intentar parsear importe
                if (!double.TryParse(importeStr, out double importeRetiro))
                {
                    string errorMsg = "Importe inválido. Debe ser un número válido.";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.SetCursorPosition(errorPos, textoImporteLinea + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                Console.ResetColor();

                // Intentar registrar retiro y mostrar mensaje centrado
                try
                {
                    controlador.RegistrarRetiro(cuentaRetiro, importeRetiro);
                    string exitoMsg = "Retiro realizado exitosamente.";
                    int exitoPos = startPos + (anchoCuadro - exitoMsg.Length) / 2;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(exitoPos, textoImporteLinea + 3);
                    Console.WriteLine(exitoMsg);
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Error al realizar el retiro: {ex.Message}";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(errorPos, textoImporteLinea + 3);
                    Console.WriteLine(errorMsg);
                }

                // Mensaje para continuar o salir
                string mensajeSalir = "Presione ENTER para limpiar y continuar o ESC para regresar al MENÚ PRINCIPAL";
                int mensajePos = (width - mensajeSalir.Length) / 2;
                int mensajeLinea = textoImporteLinea + 5;

                Console.SetCursorPosition(mensajePos, mensajeLinea);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(mensajeSalir);
                Console.ResetColor();

                var tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        public static void RealizarTransferencia(Scanner scanner, CliCon_Controlador controlador, string[] logo, int anchoCuadro)
        {
            int width = Console.WindowWidth;
            int startPos = (width - anchoCuadro) / 2;

            while (true)
            {
                Console.Clear();
                MostrarLogoYBienvenido(logo);

                // Cuadro azul para título y marco
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                int lineaBase = Console.CursorTop;

                int lineasInternas = 10;
                for (int i = 0; i < lineasInternas; i++)
                {
                    Console.SetCursorPosition(startPos, Console.CursorTop);
                    Console.Write("|" + new string(' ', anchoCuadro - 2) + "|");
                    Console.WriteLine();
                }

                Console.SetCursorPosition(startPos, Console.CursorTop);
                Console.WriteLine(new string('-', anchoCuadro));

                // Título TRANSFERENCIA en cyan centrado
                string titulo = "TRANSFERENCIA";
                int tituloPos = startPos + (anchoCuadro - titulo.Length) / 2;
                Console.SetCursorPosition(tituloPos, lineaBase);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(titulo);

                // Entrada: Cuenta Origen
                string textoCuentaOrigen = "Ingrese el número de cuenta origen: ";
                int textoPos = startPos + 2;
                int lineaCuentaOrigen = lineaBase + 2;
                Console.SetCursorPosition(textoPos, lineaCuentaOrigen);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', anchoCuadro - 4));
                Console.SetCursorPosition(textoPos, lineaCuentaOrigen);
                Console.Write(textoCuentaOrigen);

                Console.SetCursorPosition(textoPos + textoCuentaOrigen.Length, lineaCuentaOrigen);
                Console.ForegroundColor = ConsoleColor.Green;
                if (!TryLeerLineaConEscape(out string cuentaOrigen))
                    break;

                // Entrada: Cuenta Destino
                string textoCuentaDestino = "Ingrese el número de cuenta destino: ";
                int lineaCuentaDestino = lineaCuentaOrigen + 2;
                Console.SetCursorPosition(textoPos, lineaCuentaDestino);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', anchoCuadro - 4));
                Console.SetCursorPosition(textoPos, lineaCuentaDestino);
                Console.Write(textoCuentaDestino);

                Console.SetCursorPosition(textoPos + textoCuentaDestino.Length, lineaCuentaDestino);
                Console.ForegroundColor = ConsoleColor.Green;
                if (!TryLeerLineaConEscape(out string cuentaDestino))
                    break;

                // Entrada: Importe
                string textoImporte = "Ingrese el importe a transferir: ";
                int lineaImporte = lineaCuentaDestino + 2;
                Console.SetCursorPosition(textoPos, lineaImporte);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', anchoCuadro - 4));
                Console.SetCursorPosition(textoPos, lineaImporte);
                Console.Write(textoImporte);

                Console.SetCursorPosition(textoPos + textoImporte.Length, lineaImporte);
                Console.ForegroundColor = ConsoleColor.Green;
                if (!TryLeerLineaConEscape(out string importeStr))
                    break;

                if (!double.TryParse(importeStr, out double importeTransferencia))
                {
                    string errorMsg = "Importe inválido. Debe ser un número válido.";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.SetCursorPosition(errorPos, lineaImporte + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }

                Console.ResetColor();

                // Intentar registrar transferencia y mostrar mensaje centrado
                try
                {
                    controlador.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importeTransferencia);
                    string exitoMsg = "Transferencia realizada exitosamente.";
                    int exitoPos = startPos + (anchoCuadro - exitoMsg.Length) / 2;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(exitoPos, lineaImporte + 3);
                    Console.WriteLine(exitoMsg);
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Error al realizar la transferencia: {ex.Message}";
                    int errorPos = startPos + (anchoCuadro - errorMsg.Length) / 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(errorPos, lineaImporte + 3);
                    Console.WriteLine(errorMsg);
                }

                // Mensaje para continuar o salir
                string mensajeSalir = "Presione ENTER para limpiar y continuar o ESC para regresar al MENÚ PRINCIPAL";
                int mensajePos = (width - mensajeSalir.Length) / 2;
                int mensajeLinea = lineaImporte + 5;

                Console.SetCursorPosition(mensajePos, mensajeLinea);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(mensajeSalir);
                Console.ResetColor();

                var tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        public static void Salir(string[] logo)
        {
            Console.Clear();
            MostrarLogoYBienvenido(logo);

            string mensajeSalida = "Saliendo del sistema. ¡Gracias!";
            string presioneTecla = "Presione cualquier tecla para salir...";

            int mensajePos = (Console.WindowWidth - mensajeSalida.Length) / 2;
            int teclaPos = (Console.WindowWidth - presioneTecla.Length) / 2;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(mensajePos, Console.CursorTop + 2);
            Console.WriteLine(mensajeSalida);

            Console.SetCursorPosition(teclaPos, Console.CursorTop + 1);
            Console.WriteLine(presioneTecla);
            Console.ResetColor();

            Console.ReadKey();
        }

        public static void MostrarOpcionNoValida()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOpción no válida. Intente nuevamente.");
            Console.ResetColor();
        }
    }
}

