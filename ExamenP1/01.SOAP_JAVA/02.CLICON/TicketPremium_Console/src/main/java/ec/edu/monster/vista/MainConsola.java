package ec.edu.monster.vista;

import ec.edu.monster.servicio.ClienteFederacion;
import ec.edu.monster.ws.generated.*;
import java.util.List;
import java.util.Scanner;

/**
 * Cliente de Consola de TicketPremium.
 * Permite: ver partidos, ver localidades, comprar boletos y ver reporte.
 */
public class MainConsola {

    private static final ClienteFederacion cliente = new ClienteFederacion();
    private static final Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.println("==============================================");
        System.out.println("   TICKET PREMIUM - Sistema de Venta de Boletos");
        System.out.println("==============================================");

        int opcion;
        do {
            System.out.println("\n--- MENÚ PRINCIPAL ---");
            System.out.println("1. Ver partidos disponibles");
            System.out.println("2. Ver localidades de un partido");
            System.out.println("3. Comprar boletos");
            System.out.println("4. Reporte: Resumen de Ventas");
            System.out.println("0. Salir");
            System.out.print("Opción: ");
            opcion = leerInt();

            switch (opcion) {
                case 1: mostrarPartidos(); break;
                case 2: mostrarLocalidades(); break;
                case 3: comprarBoletos(); break;
                case 4: mostrarReporte(); break;
                case 0: System.out.println("¡Hasta luego!"); break;
                default: System.out.println("Opción no válida.");
            }
        } while (opcion != 0);
    }

    private static void mostrarPartidos() {
        List<PartidoFutbol> partidos = cliente.obtenerPartidosDisponibles();
        if (partidos == null || partidos.isEmpty()) {
            System.out.println("No hay partidos disponibles.");
            return;
        }
        System.out.println("\n--- PARTIDOS DISPONIBLES ---");
        System.out.printf("%-6s %-25s %-25s %-20s %-40s%n",
                "COD", "EQUIPO LOCAL", "EQUIPO VISITA", "FECHA", "LUGAR");
        System.out.println("-".repeat(120));
        for (PartidoFutbol p : partidos) {
            System.out.printf("%-6d %-25s %-25s %-20s %-40s%n",
                    p.getCodigo(), p.getEquipoLocal(), p.getEquipoVisita(),
                    p.getFecha(), p.getLugar());
        }
    }

    private static void mostrarLocalidades() {
        System.out.print("Ingrese código del partido: ");
        int cod = leerInt();
        List<LocalidadPartido> locs = cliente.obtenerLocalidades(cod);
        if (locs == null || locs.isEmpty()) {
            System.out.println("No hay localidades disponibles para ese partido.");
            return;
        }
        System.out.println("\n--- LOCALIDADES DISPONIBLES ---");
        System.out.printf("%-6s %-20s %-15s %-10s%n", "ID", "LOCALIDAD", "DISPONIBLES", "PRECIO");
        System.out.println("-".repeat(55));
        for (LocalidadPartido l : locs) {
            System.out.printf("%-6d %-20s %-15d $%-9.2f%n",
                    l.getId(), l.getCodigoLocalidad(), l.getDisponibilidad(), l.getPrecio());
        }
    }

    private static void comprarBoletos() {
        mostrarPartidos();
        System.out.print("\nCódigo del partido: ");
        int codPartido = leerInt();

        List<LocalidadPartido> locs = cliente.obtenerLocalidades(codPartido);
        if (locs == null || locs.isEmpty()) {
            System.out.println("No hay localidades disponibles.");
            return;
        }
        System.out.println("\nLocalidades:");
        for (LocalidadPartido l : locs) {
            System.out.printf("  ID: %d | %s | Disponibles: %d | Precio: $%.2f%n",
                    l.getId(), l.getCodigoLocalidad(), l.getDisponibilidad(), l.getPrecio());
        }

        System.out.print("ID de localidad a comprar: ");
        int idLoc = leerInt();
        System.out.print("Cantidad de boletos: ");
        int cantidad = leerInt();
        System.out.print("Nombre del cliente: ");
        sc.nextLine(); // limpiar buffer
        String nombre = sc.nextLine();

        // Buscar la localidad seleccionada
        LocalidadPartido seleccionada = null;
        for (LocalidadPartido l : locs) {
            if (l.getId() == idLoc) {
                seleccionada = l;
                break;
            }
        }
        if (seleccionada == null) {
            System.out.println("Localidad no encontrada.");
            return;
        }

        // Realizar la compra
        Factura factura = cliente.comprarBoleto(codPartido, nombre,
                seleccionada.getCodigoLocalidad(), idLoc, cantidad, seleccionada.getPrecio());

        if (factura == null) {
            System.out.println("ERROR: No se pudo realizar la compra. Verifique disponibilidad.");
            return;
        }

        // Mostrar factura
        System.out.println("\n========================================");
        System.out.println("        FACTURA DE COMPRA");
        System.out.println("========================================");
        System.out.println("Factura N°:  " + factura.getId());
        System.out.println("Cliente:     " + factura.getNombreCliente());
        System.out.println("Partido:     " + codPartido);
        System.out.println("Localidad:   " + seleccionada.getCodigoLocalidad());
        System.out.println("Cantidad:    " + cantidad);
        System.out.printf("P. Unitario: $%.2f%n", seleccionada.getPrecio());
        System.out.println("----------------------------------------");
        System.out.printf("Subtotal:    $%.2f%n", factura.getSubtotal());
        System.out.printf("IVA (15%%):   $%.2f%n", factura.getIva());
        System.out.printf("TOTAL:       $%.2f%n", factura.getTotal());
        System.out.println("========================================");
    }

    private static void mostrarReporte() {
        System.out.print("Código del partido para el reporte: ");
        int cod = leerInt();
        PartidoFutbol partido = cliente.obtenerPartido(cod);
        List<ResumenVenta> resumen = cliente.obtenerResumenVentas(cod);

        if (partido == null) {
            System.out.println("Partido no encontrado.");
            return;
        }

        System.out.println("\n========================================");
        System.out.println("   RESUMEN DE VENTAS DE UN PARTIDO");
        System.out.println("========================================");
        System.out.println("Partido: " + partido.getEquipoLocal() + " vs " + partido.getEquipoVisita());
        System.out.println("Fecha:   " + partido.getFecha());
        System.out.println("Lugar:   " + partido.getLugar());
        System.out.println("----------------------------------------");
        System.out.printf("%-20s %-12s %-15s%n", "Localidad", "Vendidos", "Total Recaudado");
        System.out.println("-".repeat(50));

        if (resumen == null || resumen.isEmpty()) {
            System.out.println("No hay ventas registradas para este partido.");
        } else {
            for (ResumenVenta r : resumen) {
                System.out.printf("%-20s %-12d $%-14.2f%n",
                        r.getCodigoLocalidad(), r.getVendidos(), r.getTotalRecaudado());
            }
        }
        System.out.println("========================================");
    }

    private static int leerInt() {
        try {
            return sc.nextInt();
        } catch (Exception e) {
            sc.nextLine();
            return -1;
        }
    }
}
