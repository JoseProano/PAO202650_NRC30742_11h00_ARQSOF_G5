<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="ec.edu.monster.ws.generated.Factura" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>TicketPremium - Factura</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background: #f5f5f5; }
        .factura { max-width: 500px; margin: 40px auto; background: white; padding: 30px;
                   border-radius: 8px; box-shadow: 0 4px 16px rgba(0,0,0,0.15); }
        .factura h1 { text-align: center; color: #27ae60; }
        .factura table { width: 100%; }
        .factura td { padding: 8px 0; }
        .factura .total { font-size: 1.4em; font-weight: bold; color: #2c3e50; border-top: 2px solid #2c3e50; }
        a { color: #2980b9; text-decoration: none; }
        hr { border: 1px dashed #ccc; }
    </style>
</head>
<body>
    <div class="factura">
        <%
            Factura factura = (Factura) request.getAttribute("factura");
            String localidad = (String) request.getAttribute("localidad");
            int cantidad = (int) request.getAttribute("cantidad");
            double precioUnitario = (double) request.getAttribute("precioUnitario");
        %>
        <h1>✅ Compra Exitosa</h1>
        <h2 style="text-align:center;">FACTURA N° <%= factura.getId() %></h2>
        <hr/>
        <table>
            <tr><td><strong>Cliente:</strong></td><td><%= factura.getNombreCliente() %></td></tr>
            <tr><td><strong>Localidad:</strong></td><td><%= localidad %></td></tr>
            <tr><td><strong>Cantidad:</strong></td><td><%= cantidad %> boleto(s)</td></tr>
            <tr><td><strong>Precio Unitario:</strong></td><td>$<%= String.format("%.2f", precioUnitario) %></td></tr>
        </table>
        <hr/>
        <table>
            <tr><td><strong>Subtotal:</strong></td><td>$<%= String.format("%.2f", factura.getSubtotal()) %></td></tr>
            <tr><td><strong>IVA (15%):</strong></td><td>$<%= String.format("%.2f", factura.getIva()) %></td></tr>
            <tr class="total"><td><strong>TOTAL:</strong></td><td>$<%= String.format("%.2f", factura.getTotal()) %></td></tr>
        </table>
        <br/>
        <p style="text-align:center;"><a href="ticket?accion=partidos">← Volver a Partidos</a></p>
    </div>
</body>
</html>
