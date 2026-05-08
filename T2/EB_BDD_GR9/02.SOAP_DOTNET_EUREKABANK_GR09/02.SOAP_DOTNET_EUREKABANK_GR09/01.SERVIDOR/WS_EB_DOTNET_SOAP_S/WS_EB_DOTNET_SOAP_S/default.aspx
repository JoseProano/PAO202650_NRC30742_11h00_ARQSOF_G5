<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WS_EB_DOTNET_SOAP_S._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>EurekaBank - Servicio SOAP WCF</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            margin: 0;
            padding: 20px;
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .container {
            background: white;
            border-radius: 10px;
            padding: 40px;
            max-width: 800px;
            box-shadow: 0 10px 40px rgba(0,0,0,0.2);
        }
        h1 {
            color: #667eea;
            margin-top: 0;
            font-size: 2.5em;
            text-align: center;
        }
        h2 {
            color: #764ba2;
            border-bottom: 2px solid #667eea;
            padding-bottom: 10px;
        }
        .info-box {
            background: #f8f9fa;
            border-left: 4px solid #667eea;
            padding: 15px;
            margin: 20px 0;
            border-radius: 5px;
        }
        .service-url {
            background: #e9ecef;
            padding: 10px;
            border-radius: 5px;
            font-family: 'Courier New', monospace;
            word-break: break-all;
            margin: 10px 0;
        }
        a {
            color: #667eea;
            text-decoration: none;
            font-weight: bold;
        }
        a:hover {
            text-decoration: underline;
        }
        .operations {
            list-style: none;
            padding: 0;
        }
        .operations li {
            padding: 8px;
            margin: 5px 0;
            background: #f8f9fa;
            border-radius: 5px;
            border-left: 3px solid #764ba2;
        }
        .footer {
            text-align: center;
            margin-top: 30px;
            color: #6c757d;
            font-size: 0.9em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>🏦 EurekaBank</h1>
            <h2>Servicio SOAP WCF</h2>
            
            <div class="info-box">
                <strong>Servicio creado exitosamente</strong><br/>
                Este es un servicio web SOAP basado en WCF para operaciones bancarias.
            </div>

            <h2>📍 URL del Servicio</h2>
            <div class="service-url">
                <strong>Servicio:</strong><br/>
                <a href="WSEureka.svc" target="_blank">WSEureka.svc</a>
            </div>

            <div class="service-url">
                <strong>WSDL:</strong><br/>
                <a href="WSEureka.svc?wsdl" target="_blank">WSEureka.svc?wsdl</a>
            </div>

            <h2>🔧 Operaciones Disponibles</h2>
            <ul class="operations">
                <li><strong>ValidarIngreso</strong> - Autenticación de usuario</li>
                <li><strong>ProbarConexionBD</strong> - Prueba de conexión a base de datos</li>
                <li><strong>TraerMovimientos</strong> - Consulta de movimientos de una cuenta</li>
                <li><strong>RegistrarDeposito</strong> - Registro de depósito</li>
                <li><strong>RegistrarRetiro</strong> - Registro de retiro</li>
                <li><strong>RegistrarTransferencia</strong> - Transferencia entre cuentas</li>
                <li><strong>TraerSaldoCuenta</strong> - Consulta de saldo</li>
            </ul>

            <div class="info-box">
                <strong>Base de Datos:</strong> SQL Server (LocalDB)<br/>
                <strong>Base de Datos:</strong> eurekabank<br/>
                <strong>Desarrollado por:</strong> Grupo Monster G09
            </div>

            <div class="footer">
                <p>Para probar el servicio, use un cliente SOAP o acceda al WSDL para generar el proxy del cliente.</p>
            </div>
        </div>
    </form>
</body>
</html>

