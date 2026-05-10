# 📱 Acceso Remoto desde el Celular (Como IIS)

## 🎯 El Problema

Tu servicio funciona en `localhost` pero no desde el celular con la IP. Esto es **exactamente igual que IIS** - necesitas configurar firewall y red.

---

## ✅ PASOS PARA ARREGLARLO

### PASO 1: Obtener y Verificar tu IP

```bash
ipconfig | findstr IPv4
```

**Importante:** Usa la IP de la interfaz WiFi (no Ethernet, no Loopback).

Deberías ver algo como:
```
IPv4 Address. . . . . . . . . . . . : 192.168.1.100
```

**Anota esta IP** - es la que usarás desde el celular.

---

### PASO 2: Configurar el Firewall de Windows

**Esto es CRÍTICO** - sin esto, el celular no podrá conectarse.

**Opción A: Script Automatizado**
```bash
.\scripts\configurar-firewall.bat
```
(Ejecutar como Administrador)

**Opción B: Manual**
```powershell
# Ejecutar PowerShell como Administrador
New-NetFirewallRule -DisplayName "Payara HTTP 8080" -Direction Inbound -LocalPort 8080 -Protocol TCP -Action Allow
```

**Verificar que la regla se creó:**
```powershell
Get-NetFirewallRule -DisplayName "Payara HTTP 8080"
```

---

### PASO 3: Verificar que Payara escucha en TODAS las interfaces

Por defecto, Payara debería escuchar en `0.0.0.0:8080` (todas las interfaces), pero vamos a verificar:

**Verificar en la consola de Payara:**
1. Abre: `http://localhost:4848`
2. Ve a: **Configurations** → **server-config** → **Network Config** → **Network Listeners** → **http-listener-1**
3. Verifica que el **Address** sea `0.0.0.0` o esté vacío (significa todas las interfaces)
4. El **Port** debe ser `8080`

**Si está en `127.0.0.1` o `localhost`**, cámbialo a `0.0.0.0`:

**Desde línea de comandos:**
```bash
cd C:\tools\payara6\bin
asadmin.bat set configs.config.server-config.network-config.network-listeners.network-listener.http-listener-1.address=0.0.0.0
asadmin.bat restart-domain domain1
```

---

### PASO 4: Verificar que ambos dispositivos están en la misma red

**En tu computadora:**
```bash
ipconfig | findstr IPv4
```

**En tu celular:**
- Ve a Configuración → WiFi → Tu red → Ver detalles
- La IP del celular debe estar en el mismo rango
- Ejemplo:
  - Computadora: `192.168.1.100`
  - Celular: `192.168.1.105` ✅ (mismo rango)
  - Celular: `10.0.0.5` ❌ (diferente red)

**⚠️ IMPORTANTE:** 
- NO uses datos móviles en el celular
- Ambos deben estar en la misma red WiFi

---

### PASO 5: Probar desde tu computadora primero (con la IP)

**Antes de probar desde el celular, prueba desde tu computadora usando la IP:**

```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**Reemplaza `192.168.1.100` con tu IP real.**

**Si funciona aquí pero no en el celular** → problema de red/firewall  
**Si NO funciona ni aquí** → problema de configuración de Payara

---

### PASO 6: Probar desde el Celular

**URL completa:**
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**Reemplaza `192.168.1.100` con la IP de tu computadora.**

---

## 🔍 DIAGNÓSTICO PASO A PASO

### Test 1: ¿Funciona en localhost?
```
http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```
✅ SÍ → Payara funciona  
❌ NO → Problema con el despliegue

### Test 2: ¿Funciona con la IP desde la misma computadora?
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```
✅ SÍ → Firewall/red está bien  
❌ NO → Problema de firewall o Payara solo escucha en localhost

### Test 3: ¿Funciona desde el celular?
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```
✅ SÍ → ¡Funciona!  
❌ NO → Problema de red (diferentes redes, firewall bloquea, etc.)

---

## 🛠️ SOLUCIÓN SI NO FUNCIONA

### Problema 1: Firewall bloquea

**Verificar reglas de firewall:**
```powershell
Get-NetFirewallRule -DisplayName "Payara*"
```

**Si no existe, crear:**
```powershell
New-NetFirewallRule -DisplayName "Payara HTTP 8080" -Direction Inbound -LocalPort 8080 -Protocol TCP -Action Allow
```

### Problema 2: Payara solo escucha en localhost

**Verificar:**
```bash
cd C:\tools\payara6\bin
asadmin.bat get configs.config.server-config.network-config.network-listeners.network-listener.http-listener-1.address
```

**Si muestra `127.0.0.1`, cambiarlo:**
```bash
asadmin.bat set configs.config.server-config.network-config.network-listeners.network-listener.http-listener-1.address=0.0.0.0
asadmin.bat restart-domain domain1
```

### Problema 3: Diferentes redes

- Verifica que ambos estén en la misma WiFi
- NO uses datos móviles en el celular
- Verifica el rango de IPs (deben ser similares)

### Problema 4: Puerto bloqueado por router

Algunos routers bloquean conexiones entrantes. En ese caso:
- Usa una IP estática (no cambia)
- O configura port forwarding en el router (si es necesario)

---

## 📋 CHECKLIST COMPLETO

- [ ] IP de la computadora obtenida: `ipconfig | findstr IPv4`
- [ ] Firewall configurado: puerto 8080 abierto
- [ ] Payara escucha en `0.0.0.0:8080` (todas las interfaces)
- [ ] Celular y computadora en la misma red WiFi
- [ ] Probado desde computadora con la IP (funciona)
- [ ] Probado desde celular con la IP

---

## 🎯 RESUMEN: URL Final

**Desde el celular:**
```
http://[IP_DE_TU_COMPUTADORA]:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**Ejemplo:**
```
http://192.168.1.100:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

---

## 🆘 SI SIGUE SIN FUNCIONAR

1. **Verifica que Payara esté corriendo:**
   ```
   http://localhost:4848
   ```

2. **Revisa los logs de Payara:**
   ```
   C:\tools\payara6\glassfish\domains\domain1\logs\server.log
   ```

3. **Prueba con ping desde el celular:**
   - Instala una app de ping en el celular
   - Haz ping a la IP de tu computadora
   - Si no responde → problema de red

4. **Verifica que el puerto esté abierto:**
   ```powershell
   Test-NetConnection -ComputerName localhost -Port 8080
   ```

---

**¡Es exactamente como IIS! Solo necesitas configurar el firewall y asegurarte de que Payara escuche en todas las interfaces.** 🔥


