<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="ec.edu.monster.ws.generated.LocalidadPartido" %>
<%@ page import="ec.edu.monster.ws.generated.PartidoFutbol" %>
<%@ page import="java.util.List" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>TicketPremium - Localidades</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background: #f5f5f5; }
        h1, h2 { color: #2c3e50; }
        table { width: 100%; border-collapse: collapse; background: white; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }
        th { background: #27ae60; color: white; padding: 12px; text-align: left; }
        td { padding: 10px; border-bottom: 1px solid #ddd; }
        tr:hover { background: #f0f0f0; }
        .form-compra { display: inline; }
        input[type="number"], input[type="text"] { padding: 5px; width: 80px; }
        button { background: #27ae60; color: white; border: none; padding: 8px 16px; cursor: pointer; border-radius: 4px; }
        button:hover { background: #219a52; }
        a { color: #2980b9; text-decoration: none; }
        .info-partido { background: #2c3e50; color: white; padding: 15px; border-radius: 8px; margin-bottom: 20px; }
    </style>
</head>
<body>
    <a href="ticket?accion=partidos">← Volver a Partidos</a>
    <%
        PartidoFutbol partido = (PartidoFutbol) request.getAttribute("partido");
        int codPartido = (int) request.getAttribute("codPartido");
        List<LocalidadPartido> localidades = (List<LocalidadPartido>) request.getAttribute("localidades");
    %>
    <div class="info-partido">
        <h2 style="color:white;margin:0;">
            <%= partido != null ? partido.getEquipoLocal() + " vs " + partido.getEquipoVisita() : "Partido #" + codPartido %>
        </h2>
        <p style="margin:5px 0 0;"><%= partido != null ? "Fecha: " + partido.getFecha() + " | Lugar: " + partido.getLugar() : "" %></p>
    </div>

    <h2>Localidades Disponibles</h2>
    <table>
        <tr>
            <th>Localidad</th>
            <th>Disponibles</th>
            <th>Precio ($)</th>
            <th>Cantidad</th>
            <th>Cliente</th>
            <th>Comprar</th>
        </tr>
        <% if (localidades != null) { for (LocalidadPartido l : localidades) { %>
        <tr>
            <form method="post" action="ticket">
                <input type="hidden" name="accion" value="comprar"/>
                <input type="hidden" name="codPartido" value="<%= codPartido %>"/>
                <input type="hidden" name="idLocalidad" value="<%= l.getId() %>"/>
                <input type="hidden" name="codigoLocalidad" value="<%= l.getCodigoLocalidad() %>"/>
                <input type="hidden" name="precio" value="<%= l.getPrecio() %>"/>
                <td><strong><%= l.getCodigoLocalidad() %></strong></td>
                <td><%= l.getDisponibilidad() %></td>
                <td>$<%= String.format("%.2f", l.getPrecio()) %></td>
                <td><input type="number" name="cantidad" min="1" max="<%= l.getDisponibilidad() %>" value="1" required/></td>
                <td><input type="text" name="nombreCliente" placeholder="Nombre" required style="width:150px;"/></td>
                <td><button type="submit">🛒 Comprar</button></td>
            </form>
        </tr>
        <% }} %>
    </table>
</body>
</html>
