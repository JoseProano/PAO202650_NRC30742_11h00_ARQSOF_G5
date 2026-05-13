# ✅ URLs CORRECTAS para tu Servicio SOAP

## 🎯 Tu Aplicación

**Nombre:** `CONUNI_SOAP_JAVA_GR09`  
**Context Root:** `/CONUNI_SOAP_JAVA_GR09`  
**Servicio SOAP:** `WSConversion`

---

## 📍 URLs Completas

### Desde tu Computadora (Localhost)

**WSDL del Servicio:**
```
http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**Endpoint del Servicio:**
```
http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion
```

---

### Desde el Celular u Otra PC (Acceso Remoto)

**⚠️ IMPORTANTE:** `DESKTOP-H5VAQE8` NO funcionará desde el celular (es un nombre de host que no se resuelve).

**Necesitas usar la IP de tu computadora:**

#### Paso 1: Obtener tu IP

```bash
ipconfig | findstr IPv4
```

Te mostrará algo como:
```
IPv4 Address. . . . . . . . . . . . : 192.168.1.100
```

#### Paso 2: URLs con la IP

**WSDL del Servicio:**
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**Endpoint del Servicio:**
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09/WSConversion
```

**Reemplaza `192.168.1.100` con la IP que obtuviste.**

---

## 📱 Acceso desde el Celular

### Requisitos

1. ✅ Celular y computadora en la **misma red WiFi**
2. ✅ Firewall configurado (puerto 8080 abierto)

### Configurar Firewall (si no lo has hecho)

```powershell
# Ejecutar como Administrador
New-NetFirewallRule -DisplayName "Payara HTTP 8080" -Direction Inbound -LocalPort 8080 -Protocol TCP -Action Allow
```

O ejecuta:
```bash
.\scripts\configurar-firewall.bat
```

### Probar desde el Celular

1. Obtén tu IP: `ipconfig | findstr IPv4`
2. Abre el navegador del celular
3. Prueba esta URL:
   ```
   http://[TU_IP]:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
   ```

**Ejemplo:**
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

Debes ver el XML del WSDL.

---

## 🔍 Diferencia entre las URLs

Payara te muestra:
- `http://DESKTOP-H5VAQE8:8080/CONUNI_SOAP_JAVA_GR09` ← **Solo el contexto root**

**Pero necesitas agregar el servicio SOAP:**
- `http://DESKTOP-H5VAQE8:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl` ← **URL completa del WSDL**

---

## ✅ Resumen de URLs

### Local (desde tu computadora):
```
http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

### Remoto (desde celular u otra PC):
```
http://[TU_IP]:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**Reemplaza `[TU_IP]` con la IP que obtuviste con `ipconfig`.**

---

## 🆘 Si No Funciona desde el Celular

1. **Verifica que estén en la misma red WiFi**
2. **Verifica el firewall:** ejecuta `.\scripts\configurar-firewall.bat`
3. **Verifica que Payara esté corriendo:** `http://localhost:4848`
4. **Prueba primero desde la misma computadora:**
   - `http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl`
   - Si funciona aquí pero no en el celular → problema de red/firewall
5. **Obtén la IP correcta:** `ipconfig | findstr IPv4`

---

**¡Usa la URL completa con `/WSConversion?wsdl` al final!** 🚀


