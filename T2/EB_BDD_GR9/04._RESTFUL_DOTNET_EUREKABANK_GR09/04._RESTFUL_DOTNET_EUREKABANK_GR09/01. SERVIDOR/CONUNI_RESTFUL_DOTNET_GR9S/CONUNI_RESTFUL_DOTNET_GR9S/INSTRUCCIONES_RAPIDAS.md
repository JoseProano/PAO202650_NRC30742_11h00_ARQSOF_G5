# Instrucciones Rápidas - Solución de Conexión

## ⚠️ PROBLEMA: El móvil no puede conectarse al servidor

## ✅ SOLUCIÓN PASO A PASO

### Paso 1: Ejecutar el script de configuración

**Opción A: PowerShell (Recomendado)**
1. Clic derecho en `configurar_http.ps1`
2. Selecciona **"Ejecutar con PowerShell"**
3. Si te pide permisos, ejecuta como Administrador

**Opción B: Si PowerShell no funciona**
1. Ejecuta `configurar_red.bat` como Administrador
2. Luego sigue con el Paso 2 manualmente

### Paso 2: Configurar HTTP en Visual Studio

1. **Abre Visual Studio**
2. **Clic derecho en el proyecto** → **Properties**
3. **Pestaña "Web"**
4. **En "Servers"**, selecciona **"IIS Express"**
5. **En "Project Url"**, agrega o verifica:
   ```
   http://localhost:44385
   ```
6. **Guarda** (Ctrl+S)

### Paso 3: Verificar applicationhost.config

El script debería haber modificado el archivo automáticamente, pero verifica:

1. Busca el archivo en una de estas ubicaciones:
   - `%USERPROFILE%\Documents\IISExpress\config\applicationhost.config`
   - `.vs\config\applicationhost.config` (dentro de la carpeta del proyecto)

2. Abre el archivo y busca tu sitio `CONUNI_RESTFUL_DOTNET_GR9S`

3. Debería tener bindings como estos:
   ```xml
   <bindings>
       <binding protocol="http" bindingInformation="*:44385:192.168.100.6" />
       <binding protocol="http" bindingInformation="*:44385:*" />
       <binding protocol="https" bindingInformation="*:44384:localhost" />
   </bindings>
   ```

### Paso 4: Reiniciar Visual Studio

1. **Cierra Visual Studio completamente**
2. **Abre el proyecto nuevamente**
3. **Ejecuta el proyecto** (F5)

### Paso 5: Verificar que el servidor escucha en la red

Abre PowerShell y ejecuta:
```powershell
netstat -an | findstr :44385
```

Deberías ver algo como:
```
TCP    0.0.0.0:44385          0.0.0.0:0              LISTENING
```

Si solo ves `127.0.0.1:44385`, entonces solo escucha en localhost y necesitas revisar el Paso 2.

### Paso 6: Probar desde el navegador

Desde otra PC en la misma red o desde el móvil:
```
http://192.168.100.6:44385/api/conversion/info
```

Deberías ver una respuesta JSON.

### Paso 7: Probar desde el móvil

El cliente Android ya está configurado para usar HTTP en el puerto 44385. Solo asegúrate de que:
- El dispositivo esté en la misma red Wi-Fi
- La IP en `ClienteConversionREST.kt` sea `192.168.100.6`
- El puerto sea `44385`

---

## 🔧 Si aún no funciona

### Verificar firewall

```powershell
# Verificar reglas de firewall
Get-NetFirewallRule -Name "*IIS Express*" | Format-Table Name, Enabled, Direction
```

### Verificar URL ACL

```powershell
netsh http show urlacl
```

Busca la entrada para `http://192.168.100.6:44385/`

### Verificar que el proyecto use HTTP

En Visual Studio:
1. Properties → Web
2. Verifica que el Project Url tenga `http://` (no `https://`)
3. Verifica que el puerto sea `44385`

### Alternativa: Usar ngrok

Si nada funciona, usa ngrok:

1. Descarga ngrok: https://ngrok.com/download
2. Ejecuta: `ngrok.exe http 44385`
3. Copia la URL (ej: `http://abc123.ngrok.io`)
4. En `ClienteConversionREST.kt`:
   ```kotlin
   private const val USE_HTTPS = false
   private const val SERVER_IP = "abc123.ngrok.io"
   private const val HTTP_PORT = "80"
   ```

---

## 📝 Notas importantes

- **HTTP vs HTTPS**: Para desarrollo, HTTP es más fácil (sin certificados)
- **Puertos**: HTTP usa 44385, HTTPS usa 44384
- **IP**: Verifica tu IP con `ipconfig` si es diferente de 192.168.100.6
- **Red**: El móvil y la PC deben estar en la misma red Wi-Fi

---

## 🆘 ¿Necesitas ayuda?

Si después de seguir estos pasos aún no funciona:
1. Verifica los logs en Visual Studio (Output → Debug)
2. Prueba desde el navegador en otra PC
3. Verifica que el firewall no esté bloqueando
4. Considera usar ngrok como alternativa rápida


