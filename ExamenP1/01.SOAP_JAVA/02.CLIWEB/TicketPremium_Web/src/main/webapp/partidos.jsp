<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="ec.edu.monster.ws.generated.PartidoFutbol" %>
<%@ page import="java.util.List" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>TicketPremium - Partidos Disponibles</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background: #f5f5f5; }
        h1 { color: #2c3e50; text-align: center; }
        table { width: 100%; border-collapse: collapse; background: white; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }
        th { background: #2c3e50; color: white; padding: 12px; text-align: left; }
        td { padding: 10px; border-bottom: 1px solid #ddd; }
        tr:hover { background: #f0f0f0; }
        a { color: #2980b9; text-decoration: none; font-weight: bold; }
        a:hover { text-decoration: underline; }
        .header { background: #2c3e50; color: white; padding: 20px; text-align: center; margin: -20px -20px 20px; }
        .header h1 { color: white; margin: 0; }
    </style>
</head>
<body>
    <div class="header">
        <h1>🎟️ TICKET PREMIUM - Venta de Boletos</h1>
    </div>
    <h2>Partidos de Fútbol Disponibles</h2>
    <%
        List<PartidoFutbol> partidos = (List<PartidoFutbol>) request.getAttribute("partidos");
    %>
    <table>
        <tr>
            <th>Código</th>
            <th>Equipo Local</th>
            <th>Equipo Visita</th>
            <th>Fecha</th>
            <th>Lugar</th>
            <th>Acciones</th>
        </tr>
        <% if (partidos != null) { for (PartidoFutbol p : partidos) { %>
        <tr>
            <td><%= p.getCodigo() %></td>
            <td><%= p.getEquipoLocal() %></td>
            <td><%= p.getEquipoVisita() %></td>
            <td><%= p.getFecha() %></td>
            <td><%= p.getLugar() %></td>
            <td>
                <a href="ticket?accion=localidades&codPartido=<%= p.getCodigo() %>">Ver Localidades</a> |
                <a href="ticket?accion=reporte&codPartido=<%= p.getCodigo() %>">Reporte</a>
            </td>
        </tr>
        <% }} %>
    </table>
</body>
</html>
