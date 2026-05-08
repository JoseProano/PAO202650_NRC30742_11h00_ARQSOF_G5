<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="ec.edu.monster.ws.generated.PartidoFutbol" %>
<%@ page import="ec.edu.monster.ws.generated.ResumenVenta" %>
<%@ page import="java.util.List" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>TicketPremium - Reporte de Ventas</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background: #f5f5f5; }
        h1 { color: #2c3e50; }
        table { width: 60%; margin: 0 auto; border-collapse: collapse; background: white; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }
        th { background: #8e44ad; color: white; padding: 12px; }
        td { padding: 10px; text-align: center; border-bottom: 1px solid #ddd; }
        .info { text-align: center; margin-bottom: 20px; }
        a { color: #2980b9; text-decoration: none; }
    </style>
</head>
<body>
    <a href="ticket?accion=partidos">← Volver a Partidos</a>
    <%
        PartidoFutbol partido = (PartidoFutbol) request.getAttribute("partido");
        List<ResumenVenta> resumen = (List<ResumenVenta>) request.getAttribute("resumen");
    %>
    <div class="info">
        <h1>📊 Resumen de Ventas de un Partido</h1>
        <% if (partido != null) { %>
            <h2>Partido: <%= partido.getEquipoLocal() %> vs <%= partido.getEquipoVisita() %></h2>
            <p>Fecha: <%= partido.getFecha() %></p>
        <% } %>
    </div>

    <table>
        <tr>
            <th>Localidad</th>
            <th>Vendidos</th>
            <th>Total Recaudado ($)</th>
        </tr>
        <% if (resumen != null && !resumen.isEmpty()) {
            for (ResumenVenta r : resumen) { %>
        <tr>
            <td><%= r.getCodigoLocalidad() %></td>
            <td><%= r.getVendidos() %></td>
            <td>$<%= String.format("%.2f", r.getTotalRecaudado()) %></td>
        </tr>
        <% }} else { %>
        <tr><td colspan="3">No hay ventas registradas para este partido.</td></tr>
        <% } %>
    </table>
</body>
</html>
