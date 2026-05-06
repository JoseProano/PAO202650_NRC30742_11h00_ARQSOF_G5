# 🔧 SOLUCIÓN: URL Correcta para Acceso Remoto

## 🎯 Problema

Payara te está mostrando URLs de otra aplicación (`CONUNI_SOAP_JAVA_CLIWEB_G09`) en lugar de tu aplicación (`CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT`).

## ✅ SOLUCIÓN: URL Correcta para tu Servicio

### Paso 1: Obtener tu IP Local

**En PowerShell o CMD:**
```bash
ipconfig | findstr IPv4
```

Te mostrará algo como:
```
IPv4 Address. . . . . . . . . . . . : 192.168.1.100
```

**O usa este comando:**
```bash
.\scripts\obtener-ip.bat
```

### Paso 2: URLs Correctas para tu Servicio SOAP

**Tu aplicación correcta es:** `CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT`

**Prueba estas URLs (en orden):**

**Opción 1: Con contexto root del WAR**
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT/WSConversion?wsdl
```

**Opción 2: Con contexto root /conversion (si se respetó sun-web.xml)**
```
http://192.168.1.100:8080/conversion/WSConversion?wsdl
```

**Reemplaza `192.168.1.100` con la IP que obtuviste en el Paso 1.**

---

## 🔍 Verificar el Context Root Real

**Desde la línea de comandos:**
```bash
cd C:\tools\payara6\bin
asadmin.bat list-applications --long
```

Busca `CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT` y verás su contexto root.

---

## 📱 Acceso desde el Celular

### Paso 1: Asegúrate de que ambos dispositivos estén en la misma red WiFi

- Tu computadora y tu celular deben estar conectados a la misma red WiFi
- NO uses datos móviles en el celular

### Paso 2: Obtener la IP de tu computadora

```bash
ipconfig | findstr IPv4
```

Ejemplo: `192.168.1.100`

### Paso 3: Probar desde el celular

Abre el navegador del celular y prueba estas URLs (reemplaza con tu IP):

**Opción 1:**
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT/WSConversion?wsdl
```

**Opción 2:**
```
http://192.168.1.100:8080/conversion/WSConversion?wsdl
```

**Opción 3 (si las anteriores no funcionan):**
```
http://192.168.1.100:8080/WSConversion?wsdl
```

### Paso 4: Verificar Firewall

**Asegúrate de que el firewall permita el puerto 8080:**

```powershell
# Ejecutar como Administrador
New-NetFirewallRule -DisplayName "Payara HTTP 8080" -Direction Inbound -LocalPort 8080 -Protocol TCP -Action Allow
```

O ejecuta:
```bash
.\scripts\configurar-firewall.bat
```

---

## 🛠️ Si Ninguna URL Funciona: Redesplegar Correctamente

### Opción A: Redesplegar con Context Root Específico

```bash
cd C:\tools\payara6\bin

# 1. Eliminar aplicación actual
asadmin.bat undeploy CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT

# 2. Desplegar con contexto root /conversion
asadmin.bat deploy --contextroot /conversion "D:\Darwin\Documents\PEPAS MONSTER G09\TI1.1 SOAP_JAVA_SINBDD_GR09\01.SERVIDOR\CONUNI_SOAP_JAVA_GR09\target\CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war"
```

Después de esto, la URL será:
```
http://192.168.1.100:8080/conversion/WSConversion?wsdl
```

### Opción B: Usar el Script Automatizado

```bash
.\desplegar-todo.bat
```

Este script redesplegará la aplicación con el contexto root correcto.

---

## ✅ Checklist para Acceso Remoto

- [ ] Obtener IP de la computadora: `ipconfig | findstr IPv4`
- [ ] Celular y computadora en la misma red WiFi
- [ ] Firewall configurado (puerto 8080 abierto)
- [ ] Aplicación desplegada correctamente
- [ ] Probar URL con la IP en lugar de localhost
- [ ] Agregar `?wsdl` al final de la URL

---

## 📝 Resumen de URLs

**Local (desde tu computadora):**
- `http://localhost:8080/CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT/WSConversion?wsdl`
- `http://localhost:8080/conversion/WSConversion?wsdl`

**Remoto (desde celular u otra PC):**
- `http://[TU_IP]:8080/CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT/WSConversion?wsdl`
- `http://[TU_IP]:8080/conversion/WSConversion?wsdl`

**Reemplaza `[TU_IP]` con la IP que obtuviste con `ipconfig`.**

---

## 🆘 Si Sigue Sin Funcionar

1. **Verifica que Payara esté corriendo:**
   ```
   http://localhost:4848
   ```

2. **Verifica que la aplicación esté desplegada:**
   - En la consola de Payara, ve a Applications
   - Debe aparecer `CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT` con estado Enabled

3. **Verifica los logs de Payara:**
   ```
   C:\tools\payara6\glassfish\domains\domain1\logs\server.log
   ```

4. **Prueba desde la misma computadora primero:**
   - Si funciona en localhost pero no en el celular → problema de red/firewall
   - Si no funciona ni en localhost → problema de despliegue

---

**¡Empieza obteniendo tu IP y probando las URLs con la IP en lugar de localhost!**


