using System;
using System.Collections.Generic;
using System.Linq;
using BDD_SOAP_DOTNET_CLICON_G09.ec.edu.monster.modelo;

namespace BDD_SOAP_DOTNET_CLICON_G09.ec.edu.monster.vista
{
    public class MovimientoView
    {
        public static void MostrarMovimientos(List<CliCon_Movimiento> movimientos, int anchoCuadro)
        {
            if (movimientos == null || movimientos.Count == 0)
            {
                return;
            }

            int width = Console.WindowWidth;
            int startPos = (width - anchoCuadro) / 2;

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Blue;

            // Línea superior
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.WriteLine(new string('=', anchoCuadro));

            // Asignación de anchos para columnas
            int anchoCuenta = 15;
            int anchoNroMov = 8;
            int anchoFecha = 12;
            int anchoTipo = 20;
            int anchoAccion = 10;
            int anchoImporte = anchoCuadro - (anchoCuenta + anchoNroMov + anchoFecha + anchoTipo + anchoAccion + 6);

            // Encabezado
            Console.SetCursorPosition(startPos, Console.CursorTop);
            string encabezado = string.Format(
                "{0,-" + anchoCuenta + "} {1,-" + anchoNroMov + "} {2,-" + anchoFecha + "} {3,-" + anchoTipo + "} {4,-" + anchoAccion + "} {5," + anchoImporte + "}",
                "Cuenta", "Nro Mov.", "Fecha", "Tipo", "Acción", "Importe");
            Console.WriteLine(encabezado);

            // Línea separadora
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.WriteLine(new string('=', anchoCuadro));

            Console.ForegroundColor = ConsoleColor.White;

            // Mostrar cada movimiento
            foreach (var mov in movimientos)
            {
                string tipo = mov.Tipo != null && mov.Tipo.Length > anchoTipo ? mov.Tipo.Substring(0, anchoTipo) : (mov.Tipo ?? "");
                string accion = mov.Accion != null && mov.Accion.Length > anchoAccion ? mov.Accion.Substring(0, anchoAccion) : (mov.Accion ?? "");

                Console.SetCursorPosition(startPos, Console.CursorTop);
                string linea = string.Format(
                    "{0,-" + anchoCuenta + "} {1,-" + anchoNroMov + "} {2,-" + anchoFecha + "} {3,-" + anchoTipo + "} {4,-" + anchoAccion + "} {5," + anchoImporte + "}",
                    mov.Cuenta ?? "",
                    mov.NroMov,
                    mov.Fecha.ToString("dd/MM/yyyy"),
                    tipo,
                    accion,
                    mov.Importe.ToString("N2")
                );
                Console.WriteLine(linea);
            }

            // Línea inferior
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(startPos, Console.CursorTop);
            Console.WriteLine(new string('=', anchoCuadro));

            Console.ResetColor();
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
    }
}

