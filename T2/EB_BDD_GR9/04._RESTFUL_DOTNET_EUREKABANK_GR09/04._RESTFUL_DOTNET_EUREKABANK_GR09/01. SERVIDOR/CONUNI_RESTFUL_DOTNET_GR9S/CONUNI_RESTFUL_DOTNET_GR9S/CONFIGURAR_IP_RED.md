# Configurar IIS Express para escuchar en IP de red (SIN modificar IIS real)

## Importante
Esta configuración SOLO afecta IIS Express (desarrollo). Cuando publiques en IIS real, esto NO se aplica.

## Pasos

### 1. Configurar permisos (PowerShell como Administrador)
```powershell
netsh http add urlacl url=http://192.168.100.6:44385/ user=Everyone
netsh advfirewall firewall add rule name="IIS Express HTTP" dir=in action=allow protocol=TCP localport=44385
```

### 2. Encontrar applicationhost.config
Busca el archivo en:
- `.vs\config\applicationhost.config` (dentro de la carpeta del proyecto)
- O en: `%USERPROFILE%\Documents\IISExpress\config\applicationhost.config`

**Nota:** Si no existe, ejecuta el proyecto una vez en Visual Studio para que se genere.

### 3. Modificar applicationhost.config
1. Abre el archivo con un editor de texto (Notepad++, Visual Studio Code, etc.)
2. Busca tu sitio: `<site name="CONUNI_RESTFUL_DOTNET_GR9S">`
3. Dentro de `<bindings>`, agrega esta línea:
   ```xml
   <binding protocol="http" bindingInformation="*:44385:192.168.100.6" />
   ```

   Debería quedar así:
   ```xml
   <bindings>
       <binding protocol="http" bindingInformation="*:44385:localhost" />
       <binding protocol="http" bindingInformation="*:44385:192.168.100.6" />
       <binding protocol="https" bindingInformation="*:44384:localhost" />
   </bindings>
   ```

4. Guarda el archivo

### 4. Verificar en Visual Studio
- El Project Url debe ser: `http://localhost:44385`
- NO cambies esto, IIS Express lo necesita así

### 5. Ejecutar el proyecto
Presiona F5 en Visual Studio

### 6. Verificar que funciona
En PowerShell:
```powershell
netstat -an | findstr :44385
```

Deberías ver:
```
TCP    0.0.0.0:44385          0.0.0.0:0              LISTENING
```

Si ves `0.0.0.0:44385`, está escuchando en todas las interfaces y funcionará desde el móvil.

### 7. Probar desde el móvil
El cliente Android ya está configurado para usar `http://192.168.100.6:44385`

---

## Esto NO afecta IIS real
- `applicationhost.config` es SOLO para IIS Express (desarrollo)
- Cuando publiques en IIS real, usa su propia configuración
- Tu servidor seguirá funcionando normalmente


