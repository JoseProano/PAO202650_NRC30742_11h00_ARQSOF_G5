package ec.edu.monster.servicio;

import ec.edu.monster.ws.generated.*;
import java.util.List;

/**
 * Cliente SOAP que consume el WebService de la Federación de Fútbol.
 * Envuelve las llamadas al stub generado por JAX-WS.
 *
 * INSTRUCCIONES PARA GENERAR EL STUB EN NETBEANS:
 * 1. Desplegar primero el proyecto 01.SERVIDOR en Payara.
 * 2. En este proyecto, clic derecho -> New -> Web Service Client.
 * 3. En WSDL URL: http://localhost:8080/FederacionFutbol_WS/WSFederacion?wsdl
 * 4. Package: ec.edu.monster.ws.generated
 * 5. NetBeans generará automáticamente las clases stub.
 */
public class ClienteFederacion {

    private WSFederacion port;

    public ClienteFederacion() {
        try {
            WSFederacion_Service service = new WSFederacion_Service();
            this.port = service.getWSFederacionPort();
        } catch (Exception e) {
            System.err.println("ERROR: No se pudo conectar al WS de la Federación");
            System.err.println("Asegúrese de que el servidor esté encendido.");
            System.err.println("Detalle: " + e.getMessage());
            this.port = null;
        }
    }

    public List<PartidoFutbol> obtenerPartidosDisponibles() {
        if (port == null) return null;
        return port.obtenerPartidosDisponibles();
    }

    public List<LocalidadPartido> obtenerLocalidades(int codigoPartido) {
        if (port == null) return null;
        return port.obtenerLocalidades(codigoPartido);
    }

    public boolean decrementarDisponibilidad(int idLocalidad, int cantidad) {
        if (port == null) return false;
        return port.decrementarDisponibilidad(idLocalidad, cantidad);
    }

    public Factura comprarBoleto(int codigoPartido, String nombreCliente,
                                  String codigoLocalidad, int idLocalidad,
                                  int cantidad, double precioUnitario) {
        if (port == null) return null;
        return port.comprarBoleto(codigoPartido, nombreCliente,
                codigoLocalidad, idLocalidad, cantidad, precioUnitario);
    }

    public PartidoFutbol obtenerPartido(int codigoPartido) {
        if (port == null) return null;
        return port.obtenerPartido(codigoPartido);
    }

    public List<ResumenVenta> obtenerResumenVentas(int codigoPartido) {
        if (port == null) return null;
        return port.obtenerResumenVentas(codigoPartido);
    }
}
