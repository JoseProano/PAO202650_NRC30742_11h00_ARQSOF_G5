# Configurar IIS Express para aceptar conexiones desde la red local

## Problema
Por defecto, IIS Express solo escucha en `localhost` (127.0.0.1), por lo que las conexiones desde otros dispositivos en la red local fallan.

## Solución: Configurar IIS Express para aceptar conexiones de red

### Opción 1: Usar netsh (Recomendado)

1. **Abrir PowerShell o CMD como Administrador**

2. **Ejecutar el siguiente comando** (reemplaza `44384` con tu puerto si es diferente):
```powershell
netsh http add urlacl url=https://192.168.100.6:44384/ user=Everyone
```

3. **Agregar regla de firewall** (si es necesario):
```powershell
netsh advfirewall firewall add rule name="IIS Express HTTPS" dir=in action=allow protocol=TCP localport=44384
```

### Opción 2: Modificar applicationhost.config

1. **Encontrar el archivo applicationhost.config**:
   - Ubicación típica: `%USERPROFILE%\Documents\IISExpress\config\applicationhost.config`
   - O en: `.vs\config\applicationhost.config` dentro de tu proyecto

2. **Buscar la sección de bindings** para tu sitio:
```xml
<site name="CONUNI_RESTFUL_DOTNET_GR9S" id="1">
    <application path="/" applicationPool="Clr4IntegratedAppPool">
        <virtualDirectory path="/" physicalPath="D:\...\CONUNI_RESTFUL_DOTNET_GR9S" />
    </application>
    <bindings>
        <binding protocol="https" bindingInformation="*:44384:localhost" />
    </bindings>
</site>
```

3. **Agregar un binding adicional** para tu IP:
```xml
<bindings>
    <binding protocol="https" bindingInformation="*:44384:localhost" />
    <binding protocol="https" bindingInformation="*:44384:192.168.100.6" />
</bindings>
```

### Opción 3: Modificar desde Visual Studio

1. **Abrir el proyecto** en Visual Studio
2. **Clic derecho en el proyecto** → **Properties**
3. **Pestaña "Web"**
4. **En "Servers"**, cambiar de "IIS Express" a **"IIS Local"** (si tienes IIS instalado)
5. O modificar el **Project Url** para incluir tu IP: `https://192.168.100.6:44384`

## Verificar que el servidor está escuchando

1. **Abrir PowerShell**
2. **Ejecutar**:
```powershell
netstat -an | findstr :44384
```

Deberías ver algo como:
```
TCP    0.0.0.0:44384          0.0.0.0:0              LISTENING
```

Si solo ves `127.0.0.1:44384`, entonces IIS Express solo está escuchando en localhost.

## Ver logs de IIS Express

### Opción 1: Ventana de Salida de Visual Studio
1. En Visual Studio: **View** → **Output**
2. En "Show output from:", selecciona **"Debug"** o **"Web Server"**
3. Verás los logs de las peticiones HTTP

### Opción 2: Logs del Controlador
El controlador `ConversionController.cs` ahora tiene logging integrado. Los mensajes aparecerán en:
- **Output Window** de Visual Studio (Debug)
- **Console** si ejecutas desde línea de comandos

### Opción 3: Ver logs del sistema
```powershell
# Ver eventos de IIS Express
Get-EventLog -LogName Application -Source "IIS Express" -Newest 20

# O usar Event Viewer
eventvwr.msc
```

## Probar la conexión

### Desde tu PC (localhost):
```powershell
Invoke-WebRequest -Uri "https://localhost:44384/api/conversion/info" -SkipCertificateCheck
```

### Desde otro dispositivo en la red:
```powershell
# Desde otra PC en la misma red
Invoke-WebRequest -Uri "https://192.168.100.6:44384/api/conversion/info" -SkipCertificateCheck
```

## Solución de problemas

### Error: "Access is denied"
- Ejecuta PowerShell/CMD **como Administrador**

### Error: "The specified port is already in use"
- Cierra todas las instancias de IIS Express
- O cambia el puerto en las propiedades del proyecto

### Error: "Cannot bind to the specified address"
- Verifica que la IP 192.168.100.6 sea la correcta:
```powershell
ipconfig
```
- Asegúrate de que el firewall permita conexiones entrantes en el puerto 44384

### El dispositivo móvil aún no puede conectarse
1. Verifica que el dispositivo esté en la misma red Wi-Fi
2. Verifica que el firewall de Windows permita conexiones en el puerto 44384
3. Prueba desde un navegador en otra PC de la red: `https://192.168.100.6:44384/api/conversion/info`

## Notas importantes

- **HTTPS con certificado autofirmado**: El dispositivo móvil puede rechazar el certificado. Ya configuramos el cliente Android para aceptar certificados no confiables (solo desarrollo).
- **Firewall**: Asegúrate de que el firewall de Windows permita conexiones entrantes.
- **Red**: El dispositivo móvil y tu PC deben estar en la misma red local.


