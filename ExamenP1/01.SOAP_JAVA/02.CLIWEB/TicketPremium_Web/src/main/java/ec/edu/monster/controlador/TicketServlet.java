package ec.edu.monster.controlador;

import ec.edu.monster.servicio.ClienteFederacion;
import ec.edu.monster.ws.generated.*;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.*;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

/**
 * Servlet principal de TicketPremium Web.
 * Maneja todas las acciones: listar partidos, localidades, comprar, reporte.
 */
@WebServlet(name = "TicketServlet", urlPatterns = {"/ticket"})
public class TicketServlet extends HttpServlet {

    private ClienteFederacion cliente;

    @Override
    public void init() throws ServletException {
        cliente = new ClienteFederacion();
    }

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        String accion = request.getParameter("accion");
        if (accion == null) accion = "partidos";

        switch (accion) {
            case "localidades":
                int codPartido = Integer.parseInt(request.getParameter("codPartido"));
                request.setAttribute("localidades", cliente.obtenerLocalidades(codPartido));
                request.setAttribute("codPartido", codPartido);
                PartidoFutbol partido = cliente.obtenerPartido(codPartido);
                request.setAttribute("partido", partido);
                request.getRequestDispatcher("/localidades.jsp").forward(request, response);
                break;
            case "reporte":
                int codRep = Integer.parseInt(request.getParameter("codPartido"));
                PartidoFutbol pRep = cliente.obtenerPartido(codRep);
                request.setAttribute("partido", pRep);
                request.setAttribute("resumen", cliente.obtenerResumenVentas(codRep));
                request.getRequestDispatcher("/reporte.jsp").forward(request, response);
                break;
            default:
                request.setAttribute("partidos", cliente.obtenerPartidosDisponibles());
                request.getRequestDispatcher("/partidos.jsp").forward(request, response);
                break;
        }
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        String accion = request.getParameter("accion");

        if ("comprar".equals(accion)) {
            int codPartido = Integer.parseInt(request.getParameter("codPartido"));
            int idLocalidad = Integer.parseInt(request.getParameter("idLocalidad"));
            String codigoLocalidad = request.getParameter("codigoLocalidad");
            int cantidad = Integer.parseInt(request.getParameter("cantidad"));
            double precio = Double.parseDouble(request.getParameter("precio"));
            String nombre = request.getParameter("nombreCliente");

            Factura factura = cliente.comprarBoleto(codPartido, nombre,
                    codigoLocalidad, idLocalidad, cantidad, precio);

            if (factura != null) {
                request.setAttribute("factura", factura);
                request.setAttribute("localidad", codigoLocalidad);
                request.setAttribute("cantidad", cantidad);
                request.setAttribute("precioUnitario", precio);
                request.getRequestDispatcher("/factura.jsp").forward(request, response);
            } else {
                request.setAttribute("error", "No se pudo realizar la compra. Verifique disponibilidad.");
                doGet(request, response);
            }
        }
    }
}
