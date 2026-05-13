# ✅ PASOS SIGUIENTES - Acceso Remoto

## 📊 Estado Actual

✅ **Firewall configurado** - Puerto 8080 abierto  
✅ **Tu IP:** `192.168.100.6`

---

## 🎯 PASOS A SEGUIR

### PASO 1: Verificar que Payara esté corriendo

Abre en tu navegador:
```
http://localhost:4848
```

Debe abrir la consola de administración de Payara.

---

### PASO 2: Probar desde tu computadora (con la IP)

**IMPORTANTE:** Prueba primero desde tu computadora usando la IP (no localhost).

Abre en tu navegador:
```
http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**¿Qué esperar?**
- ✅ Si funciona → Verás el XML del WSDL
- ❌ Si no funciona → Problema de configuración de Payara (ver paso 3)

---

### PASO 3: Verificar que Payara escuche en todas las interfaces

Si el paso 2 no funcionó, Payara puede estar escuchando solo en `localhost`.

**Verificar:**
```bash
cd C:\tools\payara6\bin
asadmin.bat get configs.config.server-config.network-config.network-listeners.network-listener.http-listener-1.address
```

**Si muestra `127.0.0.1` o está vacío, configurarlo:**
```bash
asadmin.bat set configs.config.server-config.network-config.network-listeners.network-listener.http-listener-1.address=0.0.0.0
asadmin.bat restart-domain domain1
```

**Espera 30-60 segundos a que Payara reinicie.**

---

### PASO 4: Probar nuevamente desde tu computadora

Después de reiniciar Payara, prueba nuevamente:
```
http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

Debe funcionar ahora.

---

### PASO 5: Probar desde el Celular

**Requisitos:**
- ✅ Celular y computadora en la **misma red WiFi**
- ✅ NO uses datos móviles en el celular

**URL desde el celular:**
```
http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**Abre esta URL en el navegador del celular.**

---

## 🔍 DIAGNÓSTICO

### Si funciona en localhost pero NO con la IP:

**Problema:** Payara está escuchando solo en `localhost` (127.0.0.1)

**Solución:** Ejecuta el Paso 3 para configurarlo en `0.0.0.0`

---

### Si funciona con la IP desde tu computadora pero NO desde el celular:

**Problema:** Red diferente o firewall del router

**Verificar:**
1. ¿Celular y computadora en la misma WiFi?
2. ¿El celular tiene datos móviles desactivados?
3. ¿La IP del celular está en el mismo rango? (192.168.100.x)

---

### Si NO funciona ni en localhost:

**Problema:** Payara no está corriendo o la aplicación no está desplegada

**Solución:**
1. Verificar que Payara esté corriendo: `http://localhost:4848`
2. Verificar que la aplicación esté desplegada en la consola de Payara

---

## 📝 RESUMEN DE URLs

### Desde tu computadora:
- **Localhost:** `http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl`
- **Con IP:** `http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl`

### Desde el celular:
- **Con IP:** `http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl`

---

## ✅ CHECKLIST

- [ ] Payara corriendo (`http://localhost:4848`)
- [ ] Probado desde computadora con IP (funciona)
- [ ] Payara escucha en `0.0.0.0:8080` (si el paso anterior no funcionó)
- [ ] Celular y computadora en misma WiFi
- [ ] Probado desde celular con IP

---

**¡Empieza con el Paso 1 y vamos paso a paso!** 🚀


