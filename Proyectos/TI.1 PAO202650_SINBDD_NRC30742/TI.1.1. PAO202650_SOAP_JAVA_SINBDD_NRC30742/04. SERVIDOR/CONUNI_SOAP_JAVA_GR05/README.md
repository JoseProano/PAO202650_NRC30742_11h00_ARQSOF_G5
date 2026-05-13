# 📘 GUÍA COMPLETA: Desplegar Servicios SOAP y RESTful en Payara Server

## 🎯 ¿Qué vas a aprender aquí?

Esta guía te explica **paso a paso, desde cero**, cómo desplegar servicios SOAP y RESTful en Payara Server para que puedan ser consumidos desde otras computadoras.

---

## 📋 ÍNDICE

1. [¿Qué es Payara y cómo funciona?](#qué-es-payara)
2. [Ciclo completo de desarrollo](#ciclo-completo)
3. [Múltiples servicios en el mismo Payara](#múltiples-servicios)
4. [Paso a paso: Desplegar tu primer servicio](#paso-a-paso)
5. [Script automatizado](#script-automatizado)
6. [Probar el servicio](#probar-servicio)
7. [Hacerlo accesible desde otra PC](#acceso-remoto)
8. [Solución de problemas](#solución-problemas)

---

## 🔍 ¿QUÉ ES PAYARA Y CÓMO FUNCIONA?

### ¿Qué es Payara Server?

Payara Server es un servidor de aplicaciones Java (como IIS para .NET, pero para Java). Puedes desplegar múltiples aplicaciones web (.war) en un solo Payara.

### ¿Cómo funciona con múltiples servicios?

**IMPORTANTE:** ✅ **NO hay conflicto** - Múltiples servicios pueden usar el mismo puerto 8080.

**Ejemplo real con tus proyectos:**
- **SOAP Java** (`CONUNI_SOAP_JAVA_GR09`):
  - Context Root: `/conversion`
  - URL: `http://localhost:8080/conversion/WSConversion?wsdl`
  
- **RESTful Java** (`CONUNI_RESTFUL_JAVA_GR09`):
  - Context Root: `/api-rest` (o el que configures)
  - URL: `http://localhost:8080/api-rest/Conversion/celsiusAFahrenheit/25`

**¿Por qué no chocan?**
- Cada servicio tiene un **Context Root** diferente (como carpetas virtuales en IIS)
- Payara identifica cada servicio por su contexto root
- Es como tener múltiples sitios web en IIS, todos usando el puerto 80, pero con nombres diferentes
- **Ambos pueden estar activos al mismo tiempo en el puerto 8080**

**Estructura de URLs:**
```
http://localhost:8080/[CONTEXT_ROOT]/[SERVICIO]
```

**Ejemplos reales:**
- SOAP: `http://localhost:8080/conversion/WSConversion?wsdl`
- RESTful: `http://localhost:8080/api-rest/Conversion/celsiusAFahrenheit/25`
- Otra app: `http://localhost:8080/mi-app/...`

---

## 🔄 CICLO COMPLETO DE DESARROLLO

### Flujo completo (lo que debes hacer cada vez):

```
1. Desarrollar/Modificar código en NetBeans
   ↓
2. Compilar el proyecto (Generar .WAR)
   ↓
3. Iniciar Payara Server (si no está corriendo)
   ↓
4. Desplegar el .WAR en Payara
   ↓
5. Verificar que el servicio esté disponible
   ↓
6. Probar desde cliente (.NET, Java, Navegador)
```

### ¿Qué es un archivo .WAR?

- **WAR** = Web Application Archive
- Es un archivo .zip que contiene tu aplicación web completa
- Similar a publicar una carpeta en IIS
- Se genera en la carpeta `target/` de tu proyecto

---

## 🔌 MÚLTIPLES SERVICIOS EN EL MISMO PAYARA

### Escenario: Tienes 2 servicios

**Servicio 1: SOAP Java** (este proyecto)
- Proyecto: `CONUNI_SOAP_JAVA_GR09`
- Context Root: `/conversion`
- URL: `http://localhost:8080/conversion/WSConversion?wsdl`

**Servicio 2: RESTful Java**
- Proyecto: `CONUNI_RESTFUL_JAVA_GR09`
- Context Root: `/api-rest` (o el que configures)
- URL: `http://localhost:8080/api-rest/Conversion/celsiusAFahrenheit/25`

### ¿Cómo evitar conflictos?

1. **Cada proyecto debe tener un Context Root diferente**
   - Configurado en `src/main/webapp/WEB-INF/sun-web.xml`
   - O al desplegar: `asadmin deploy --contextroot /mi-contexto mi-app.war`

2. **Ambos servicios pueden estar desplegados al mismo tiempo**
   - Payara puede tener múltiples aplicaciones activas
   - Todas comparten el puerto 8080
   - Payara identifica por el Context Root

3. **En NetBeans:**
   - Puedes ejecutar ambos proyectos independientemente
   - Cada uno se despliega en su contexto root
   - No hay conflictos

### Ejemplo práctico:

```bash
# Desplegar Servicio SOAP
asadmin deploy --contextroot /conversion CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war

# Desplegar Servicio RESTful
asadmin deploy --contextroot /api-rest CONUNI_RESTFUL_JAVA_GR09-1.0-SNAPSHOT.war

# Ambos funcionan simultáneamente:
# http://localhost:8080/conversion/WSConversion?wsdl
# http://localhost:8080/api-rest/Conversion/...
```

---

## 🚀 PASO A PASO: DESPLEGAR TU PRIMER SERVICIO

### PREREQUISITOS

✅ Java JDK 17 o superior instalado  
✅ NetBeans instalado  
✅ Payara Server instalado en `C:\tools\payara6`  
✅ Proyecto SOAP creado en NetBeans

---

### PASO 1: Compilar y generar el archivo .WAR

**En NetBeans:**
1. Abre tu proyecto
2. Clic derecho en el proyecto → **Clean and Build** (o `Shift + F11`)
3. Espera a que termine la compilación

**Ubicación del .WAR generado:**
```
target\CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war
```

**Verificar:**
- Debe aparecer en `target/` sin errores
- El tamaño debe ser > 0 KB

---

### PASO 2: Iniciar Payara Server

**Opción A: Desde NetBeans**
- Si Payara está configurado en NetBeans, solo ejecuta el proyecto (F6)
- NetBeans iniciará Payara automáticamente

**Opción B: Desde línea de comandos**
```bash
cd C:\tools\payara6\bin
asadmin.bat start-domain domain1
```

**Verificar que Payara está corriendo:**
- Abre: `http://localhost:4848` (Consola de administración)
- Debe cargar la página de login

---

### PASO 3: Desplegar el .WAR en Payara

**Opción A: Desde NetBeans (Recomendado para desarrollo)**
1. Clic derecho en el proyecto → **Run** (F6)
2. NetBeans automáticamente:
   - Compila el proyecto
   - Genera el .WAR
   - Despliega en Payara
   - Inicia Payara si no está corriendo

**Opción B: Desde la consola de administración**
1. Abre: `http://localhost:4848`
2. Login (usuario: `admin`, contraseña: la que configuraste)
3. Ve a: **Applications** → **Deploy**
4. Selecciona: **"Packaged file to be uploaded to the server"**
5. Elige tu archivo: `target\CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war`
6. **Context Root:** Deja vacío (se usará `/conversion` del `sun-web.xml`) o especifica uno
7. Clic en **OK**

**Opción C: Desde línea de comandos**
```bash
cd C:\tools\payara6\bin
asadmin.bat deploy "D:\Darwin\Documents\PEPAS MONSTER G09\TI1.1 SOAP_JAVA_SINBDD_GR09\01.SERVIDOR\CONUNI_SOAP_JAVA_GR09\target\CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war"
```

---

### PASO 4: Verificar el despliegue

**Ver aplicaciones desplegadas:**
```bash
cd C:\tools\payara6\bin
asadmin.bat list-applications
```

**Debe mostrar:**
```
CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT  <ejb, web>  Enabled
```

**Abrir el WSDL en el navegador:**
```
http://localhost:8080/conversion/WSConversion?wsdl
```

**Si ves XML del WSDL** → ✅ **¡Despliegue exitoso!**

---

## 🛠️ SCRIPT AUTOMATIZADO (TODO EN UNO)

He creado **UN SOLO SCRIPT** que hace **TODO automáticamente**:

### `desplegar-todo.bat`

Este script hace:
1. ✅ Compila el proyecto (genera .WAR)
2. ✅ Inicia Payara Server (si no está corriendo)
3. ✅ Despliega el .WAR en Payara
4. ✅ Muestra las URLs del servicio

**Cómo usarlo:**
```bash
# Desde la carpeta del proyecto SOAP
.\desplegar-todo.bat
```

**Ubicación:** En la raíz del proyecto: `desplegar-todo.bat`

**IMPORTANTE:** 
- Si tienes 2 proyectos (SOAP y RESTful), necesitas ejecutar este script **en cada proyecto** (uno por uno)
- O puedes crear un script similar para el proyecto RESTful
- Ambos se despliegan en el mismo Payara Server sin conflictos

---

## 🧪 PROBAR EL SERVICIO

### Desde el Navegador

Abre:
```
http://localhost:8080/conversion/WSConversion?wsdl
```

Debes ver el XML del WSDL con todas las operaciones.

### Desde .NET (Visual Studio)

1. Clic derecho en tu proyecto .NET → **Add** → **Service Reference**
2. URL: `http://localhost:8080/conversion/WSConversion?wsdl`
3. Namespace: `ConversionService`
4. Clic en **Go** → **OK**

### Desde Java

```bash
wsimport -keep http://localhost:8080/conversion/WSConversion?wsdl
```

Esto genera las clases cliente que puedes usar en tu aplicación Java.

---

## 🌐 HACERLO ACCESIBLE DESDE OTRA COMPUTADORA

### Paso 1: Configurar el Firewall

**Windows PowerShell (como Administrador):**
```powershell
New-NetFirewallRule -DisplayName "Payara HTTP 8080" -Direction Inbound -LocalPort 8080 -Protocol TCP -Action Allow
```

O ejecuta el script:
```bash
.\scripts\configurar-firewall.bat
```

### Paso 2: Obtener la IP del servidor

```bash
ipconfig | findstr IPv4
```

Ejemplo: `192.168.1.100`

### Paso 3: URL remota

**Desde otra computadora:**
```
http://192.168.1.100:8080/conversion/WSConversion?wsdl
```

**Reemplaza `192.168.1.100` con la IP de tu servidor.**

---

## 📊 RESUMEN: ¿Qué debes tener al final?

### ✅ Checklist de despliegue exitoso

- [ ] Proyecto compilado sin errores
- [ ] Archivo .WAR en `target/`
- [ ] Payara Server corriendo (`http://localhost:4848` funciona)
- [ ] Aplicación desplegada (ver con `asadmin list-applications`)
- [ ] WSDL accesible: `http://localhost:8080/conversion/WSConversion?wsdl`
- [ ] Firewall configurado (puerto 8080 abierto)
- [ ] IP del servidor identificada
- [ ] WSDL accesible desde otra PC

### 📍 URLs importantes

**Local:**
- WSDL: `http://localhost:8080/conversion/WSConversion?wsdl`
- Endpoint: `http://localhost:8080/conversion/WSConversion`
- Admin: `http://localhost:4848`

**Remoto (ejemplo con IP 192.168.1.100):**
- WSDL: `http://192.168.1.100:8080/conversion/WSConversion?wsdl`
- Endpoint: `http://192.168.1.100:8080/conversion/WSConversion`

---

## 🆘 SOLUCIÓN DE PROBLEMAS

### Error: "Puerto 8080 ya está en uso"

**Solución:**
```bash
# Ver qué proceso usa el puerto
netstat -ano | findstr :8080

# Matar el proceso (reemplaza [PID] con el número)
taskkill /PID [PID] /F
```

### Error: "WSDL not found" o 404

**Verificar:**
1. ¿Payara está corriendo? → `http://localhost:4848`
2. ¿La aplicación está desplegada? → `asadmin list-applications`
3. ¿El Context Root es correcto? → Verificar en `sun-web.xml`

### Error: "asadmin no se reconoce como comando"

**Solución:**
- Usar la ruta completa: `C:\tools\payara6\bin\asadmin.bat`
- O agregar `C:\tools\payara6\bin` al PATH del sistema

### Error: No accede desde otra PC

**Verificar:**
1. ¿Firewall está configurado? → Ver sección "Hacerlo accesible"
2. ¿Ambas PCs están en la misma red?
3. ¿La IP es correcta? → `ipconfig | findstr IPv4`

### Error: "Context Root ya existe"

**Solución:**
- Cada aplicación debe tener un Context Root diferente
- Cambia el Context Root en `sun-web.xml` o al desplegar:
  ```bash
  asadmin deploy --contextroot /mi-nuevo-contexto mi-app.war
  ```

---

## 📚 CONCEPTOS CLAVE

### Context Root
- Es la "carpeta virtual" donde se despliega tu aplicación
- Ejemplo: `/conversion`, `/api-rest`, `/mi-app`
- Configurado en `src/main/webapp/WEB-INF/sun-web.xml`

### WSDL
- Web Services Description Language
- Describe todas las operaciones de tu servicio SOAP
- Se accede agregando `?wsdl` al final de la URL del servicio

### WAR (Web Application Archive)
- Archivo comprimido (.zip) con tu aplicación web
- Contiene: clases Java, web.xml, recursos, etc.
- Se despliega en Payara

### Payara Server
- Servidor de aplicaciones Java
- Similar a IIS para .NET
- Puede tener múltiples aplicaciones desplegadas simultáneamente

---

## 🎓 EJEMPLO PRÁCTICO COMPLETO

### Tienes 2 proyectos:

**1. SOAP Java** (`CONUNI_SOAP_JAVA_GR09`)
- Context Root: `/conversion`
- URL: `http://localhost:8080/conversion/WSConversion?wsdl`

**2. RESTful Java** (`CONUNI_RESTFUL_JAVA_GR09`)
- Context Root: `/api-rest` (o el que configures)
- URL: `http://localhost:8080/api-rest/Conversion/celsiusAFahrenheit/25`

### Proceso completo (rutas de ejemplo):

```bash
# Definir rutas (ajusta según tu instalación)
set SOAP_DIR=D:\Darwin\Documents\PEPAS MONSTER G09\TI1.1 SOAP_JAVA_SINBDD_GR09\01.SERVIDOR\CONUNI_SOAP_JAVA_GR09
set REST_DIR=D:\Darwin\Documents\PEPAS MONSTER G09\TI1.3 RESTFUL_JAVA_SINBDD_GR09\01.SERVIDOR\CONUNI_RESTFUL_JAVA_GR09

# 1. Compilar SOAP
cd "%SOAP_DIR%"
mvn clean package

# 2. Compilar RESTful
cd "%REST_DIR%"
mvn clean package

# 3. Iniciar Payara (solo una vez, desde NetBeans o línea de comandos)
cd C:\tools\payara6\bin
asadmin.bat start-domain domain1

# 4. Desplegar SOAP
asadmin.bat deploy --contextroot /conversion "%SOAP_DIR%\target\CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war"

# 5. Desplegar RESTful (con contexto root diferente)
asadmin.bat deploy --contextroot /api-rest "%REST_DIR%\target\CONUNI_RESTFUL_JAVA_GR09-1.0-SNAPSHOT.war"

# 6. Verificar que ambas estén desplegadas
asadmin.bat list-applications

# Debe mostrar ambas aplicaciones:
# CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT     Enabled
# CONUNI_RESTFUL_JAVA_GR09-1.0-SNAPSHOT  Enabled
```

**URLs resultantes:**
- SOAP: `http://localhost:8080/conversion/WSConversion?wsdl`
- RESTful: `http://localhost:8080/api-rest/Conversion/celsiusAFahrenheit/25`

**✅ Ambas funcionan simultáneamente en el puerto 8080, pero con Context Roots diferentes.**

**En NetBeans:**
- Puedes ejecutar ambos proyectos independientemente (F6 en cada uno)
- Cada uno se despliega en su contexto root
- No hay conflictos, ambos comparten el mismo Payara Server

---

## ✅ RESUMEN FINAL

### Lo que debes recordar:

1. **Payara puede tener múltiples servicios** - No hay conflicto, cada uno tiene su Context Root
2. **El puerto 8080 es compartido** - Como IIS con múltiples sitios en el puerto 80
3. **Ciclo de desarrollo:** Compilar → Desplegar → Probar
4. **Usa el script automatizado** (`desplegar-todo.bat`) para hacer todo de una vez

### Archivos importantes en este proyecto:

- **`README.md`** → Esta guía completa (el único documento que necesitas)
- **`sun-web.xml`** → Define el Context Root (`/conversion`)
- **`target/*.war`** → Archivo a desplegar (se genera al compilar)
- **`desplegar-todo.bat`** → Script único que hace TODO automáticamente

### Para desplegar ambos servicios (SOAP + RESTful):

1. **Ejecuta `desplegar-todo.bat` en el proyecto SOAP** → Despliega en `/conversion`
2. **Ejecuta `desplegar-todo.bat` en el proyecto RESTful** (ajusta el Context Root) → Despliega en `/api-rest`
3. **Ambos funcionan simultáneamente** en el mismo Payara Server

---

**¡Listo! Ahora tienes TODO lo necesario en un solo README para desplegar tus servicios SOAP y RESTful en Payara.** 🚀

