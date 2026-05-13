# Solución de Conexión Móvil - Varias Opciones

## Opción 1: Usar HTTP en lugar de HTTPS (MÁS FÁCIL) ⭐ RECOMENDADO

### Paso 1: Habilitar HTTP en el proyecto

1. **En Visual Studio:**
   - Clic derecho en el proyecto → **Properties**
   - Pestaña **"Web"**
   - En **"Servers"**, asegúrate de que **"IIS Express"** esté seleccionado
   - Abre el archivo `.vs\config\applicationhost.config` (o busca en `%USERPROFILE%\Documents\IISExpress\config\`)

2. **Agregar binding HTTP:**
   - Busca tu sitio en `applicationhost.config`
   - Agrega un binding HTTP:
   ```xml
   <bindings>
       <binding protocol="http" bindingInformation="*:44385:192.168.100.6" />
       <binding protocol="https" bindingInformation="*:44384:localhost" />
   </bindings>
   ```

3. **Configurar URL ACL (PowerShell como Administrador):**
   ```powershell
   netsh http add urlacl url=http://192.168.100.6:44385/ user=Everyone
   netsh advfirewall firewall add rule name="IIS Express HTTP" dir=in action=allow protocol=TCP localport=44385
   ```

4. **Actualizar el cliente móvil:**
   - Ya está configurado para usar HTTP por defecto
   - En `ClienteConversionREST.kt`, línea 32: `USE_HTTPS = false`
   - Verifica que `HTTP_PORT = "44385"` coincida con tu puerto

### Paso 2: Probar la conexión

Desde el navegador en otra PC:
```
http://192.168.100.6:44385/api/conversion/info
```

---

## Opción 2: Usar ngrok (Túnel Público) 🌐

### Paso 1: Descargar ngrok
1. Ve a https://ngrok.com/download
2. Descarga y descomprime ngrok

### Paso 2: Crear túnel
```powershell
# Desde la carpeta donde está ngrok.exe
.\ngrok.exe http 44384
```

Esto te dará una URL pública como: `https://abc123.ngrok.io`

### Paso 3: Actualizar el cliente móvil
En `ClienteConversionREST.kt`:
```kotlin
private const val USE_HTTPS = true
private const val SERVER_IP = "abc123.ngrok.io"  // Tu URL de ngrok
private const val HTTPS_PORT = "443"  // ngrok usa 443
```

**Ventajas:**
- ✅ No necesitas configurar firewall
- ✅ Funciona desde cualquier red
- ✅ HTTPS incluido

**Desventajas:**
- ⚠️ La URL cambia cada vez que reinicias ngrok (gratis)
- ⚠️ Requiere conexión a internet

---

## Opción 3: Configurar IIS Express correctamente para HTTPS

### Paso 1: Verificar que IIS Express escucha en la red
```powershell
netstat -an | findstr :44384
```

Si solo ves `127.0.0.1:44384`, entonces solo escucha en localhost.

### Paso 2: Modificar applicationhost.config

Ubicación: `.vs\config\applicationhost.config` o `%USERPROFILE%\Documents\IISExpress\config\applicationhost.config`

```xml
<site name="CONUNI_RESTFUL_DOTNET_GR9S" id="1">
    <bindings>
        <!-- Mantener localhost -->
        <binding protocol="https" bindingInformation="*:44384:localhost" />
        <!-- Agregar binding para la IP de red -->
        <binding protocol="https" bindingInformation="*:44384:192.168.100.6" />
        <!-- O escuchar en todas las interfaces -->
        <binding protocol="https" bindingInformation="*:44384:*" />
    </bindings>
</site>
```

### Paso 3: Configurar URL ACL y Firewall
```powershell
# Como Administrador
netsh http add urlacl url=https://192.168.100.6:44384/ user=Everyone
netsh advfirewall firewall add rule name="IIS Express HTTPS" dir=in action=allow protocol=TCP localport=44384
```

### Paso 4: Reiniciar IIS Express
- Cierra Visual Studio
- Abre nuevamente el proyecto
- Ejecuta el proyecto

---

## Opción 4: Usar IIS Local (en lugar de IIS Express)

### Paso 1: Instalar IIS (si no lo tienes)
1. **Panel de Control** → **Programas** → **Activar o desactivar características de Windows**
2. Marca **"Internet Information Services"**
3. Instala

### Paso 2: Configurar el proyecto para IIS
1. En Visual Studio: **Properties** → **Web**
2. Selecciona **"IIS Local"** en lugar de **"IIS Express"**
3. Configura el **Project Url**: `http://localhost:44385` o `https://localhost:44384`

### Paso 3: Configurar IIS para aceptar conexiones de red
1. Abre **IIS Manager**
2. Selecciona tu sitio
3. En **Bindings**, agrega:
   - **Type**: http
   - **IP Address**: 192.168.100.6 (o All Unassigned)
   - **Port**: 44385

---

## Verificar la Conexión

### Desde tu PC (localhost):
```powershell
# HTTP
Invoke-WebRequest -Uri "http://localhost:44385/api/conversion/info"

# HTTPS
Invoke-WebRequest -Uri "https://localhost:44384/api/conversion/info" -SkipCertificateCheck
```

### Desde otra PC en la red:
```powershell
# HTTP
Invoke-WebRequest -Uri "http://192.168.100.6:44385/api/conversion/info"

# HTTPS
Invoke-WebRequest -Uri "https://192.168.100.6:44384/api/conversion/info" -SkipCertificateCheck
```

### Desde el dispositivo móvil:
- El cliente Android ya está configurado
- Solo asegúrate de que `USE_HTTPS` y los puertos coincidan
- Verifica que el dispositivo esté en la misma red Wi-Fi

---

## Solución de Problemas Comunes

### Error: "Connection refused" o "Connection timeout"
1. **Verifica que el servidor esté corriendo:**
   ```powershell
   netstat -an | findstr :44384
   netstat -an | findstr :44385
   ```

2. **Verifica el firewall:**
   ```powershell
   # Ver reglas de firewall
   netsh advfirewall firewall show rule name="IIS Express HTTP"
   netsh advfirewall firewall show rule name="IIS Express HTTPS"
   ```

3. **Verifica la IP:**
   ```powershell
   ipconfig
   ```
   Asegúrate de usar la IP correcta (192.168.100.6)

### Error: "Certificate error" o "SSL error"
- Usa **HTTP** en lugar de HTTPS (Opción 1)
- O verifica que el cliente Android tenga `USE_HTTPS = true` y el cliente OkHttp ignore certificados

### El servidor no escucha en la red
1. Verifica `applicationhost.config` tiene el binding correcto
2. Reinicia Visual Studio completamente
3. Verifica que no haya otro proceso usando el puerto

---

## Recomendación Final

**Para desarrollo local, usa HTTP (Opción 1):**
- ✅ Más simple (sin certificados)
- ✅ No requiere configuración SSL compleja
- ✅ Funciona inmediatamente

**Para producción o pruebas externas, usa ngrok (Opción 2):**
- ✅ No requiere configuración de red
- ✅ Funciona desde cualquier lugar

**Para desarrollo más avanzado, configura IIS Express correctamente (Opción 3):**
- ✅ Más control sobre la configuración
- ✅ Similar a producción


