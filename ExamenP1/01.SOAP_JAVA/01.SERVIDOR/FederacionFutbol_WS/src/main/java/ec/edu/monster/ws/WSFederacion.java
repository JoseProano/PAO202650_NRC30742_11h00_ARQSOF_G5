package ec.edu.monster.ws;

import ec.edu.monster.modelo.*;
import ec.edu.monster.servicio.FederacionService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import jakarta.jws.WebService;
import java.util.List;

/**
 * WebService SOAP de la Federación de Fútbol.
 * Expone las operaciones para consultar partidos, localidades,
 * registrar compras y generar reportes.
 */
@WebService(serviceName = "WSFederacion")
public class WSFederacion {

    private final FederacionService servicio = new FederacionService();

    /**
     * Retorna los partidos de fútbol disponibles.
     * Condición: FECHA >= fecha actual.
     */
    @WebMethod(operationName = "obtenerPartidosDisponibles")
    public List<PartidoFutbol> obtenerPartidosDisponibles() {
        return servicio.obtenerPartidosDisponibles();
    }

    /**
     * Retorna las localidades disponibles para un partido.
     * Solo devuelve aquellas con DISPONIBILIDAD > 0.
     */
    @WebMethod(operationName = "obtenerLocalidades")
    public List<LocalidadPartido> obtenerLocalidades(
            @WebParam(name = "codigoPartido") int codigoPartido) {
        return servicio.obtenerLocalidades(codigoPartido);
    }

    /**
     * Decrementa la disponibilidad de una localidad al comprar boletos.
     * Retorna true si se pudo decrementar, false si no hay suficientes.
     */
    @WebMethod(operationName = "decrementarDisponibilidad")
    public boolean decrementarDisponibilidad(
            @WebParam(name = "idLocalidad") int idLocalidad,
            @WebParam(name = "cantidad") int cantidad) {
        return servicio.decrementarDisponibilidad(idLocalidad, cantidad);
    }

    /**
     * Registra la compra de boletos: crea Factura + DetalleFactura,
     * y decrementa la disponibilidad automáticamente.
     * Retorna la factura generada con el cálculo de IVA.
     */
    @WebMethod(operationName = "comprarBoleto")
    public Factura comprarBoleto(
            @WebParam(name = "codigoPartido") int codigoPartido,
            @WebParam(name = "nombreCliente") String nombreCliente,
            @WebParam(name = "codigoLocalidad") String codigoLocalidad,
            @WebParam(name = "idLocalidad") int idLocalidad,
            @WebParam(name = "cantidad") int cantidad,
            @WebParam(name = "precioUnitario") double precioUnitario) {

        // 1. Decrementar disponibilidad
        boolean ok = servicio.decrementarDisponibilidad(idLocalidad, cantidad);
        if (!ok) return null;

        // 2. Registrar la compra (factura + detalle)
        return servicio.registrarCompra(codigoPartido, nombreCliente,
                codigoLocalidad, cantidad, precioUnitario);
    }

    /**
     * Obtiene un partido por su código.
     */
    @WebMethod(operationName = "obtenerPartido")
    public PartidoFutbol obtenerPartido(
            @WebParam(name = "codigoPartido") int codigoPartido) {
        return servicio.obtenerPartido(codigoPartido);
    }

    /**
     * Genera el resumen de ventas para un partido específico.
     * Agrupa por localidad: vendidos y total recaudado.
     */
    @WebMethod(operationName = "obtenerResumenVentas")
    public List<ResumenVenta> obtenerResumenVentas(
            @WebParam(name = "codigoPartido") int codigoPartido) {
        return servicio.obtenerResumenVentas(codigoPartido);
    }
}
