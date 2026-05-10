# 🔧 SOLUCIÓN: Error 404 - Not Found

## 🎯 Problema

Tu aplicación está desplegada correctamente en Payara (lo veo en la consola), pero obtienes un error 404 al acceder.

**Esto significa que la URL que estás usando NO es la correcta.**

---

## ✅ SOLUCIÓN RÁPIDA: Encontrar la URL Correcta

### Método 1: Usar el botón "Launch" en Payara (MÁS FÁCIL)

1. Ve a la consola de Payara: `http://localhost:4848`
2. En la tabla de aplicaciones, busca `CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT`
3. Haz clic en el botón **"Launch"** (en la columna "Action")
4. Esto abrirá la URL correcta del servicio

---

### Método 2: Probar estas URLs (una de ellas funcionará)

Tu aplicación puede estar usando diferentes context roots. Prueba estas URLs en orden:

**Opción A: Con contexto root `/conversion` (si se respetó sun-web.xml):**
```
http://localhost:8080/conversion/WSConversion?wsdl
```

**Opción B: Con el nombre del WAR (si Payara ignoró sun-web.xml):**
```
http://localhost:8080/CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT/WSConversion?wsdl
```

**Opción C: Solo el nombre sin versión:**
```
http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

**Opción D: Sin contexto root (raíz):**
```
http://localhost:8080/WSConversion?wsdl
```

---

### Método 3: Verificar el contexto root real desde la línea de comandos

```bash
cd C:\tools\payara6\bin
asadmin.bat list-applications --long
```

Esto mostrará el contexto root real de cada aplicación.

---

## 🔍 VERIFICACIÓN: ¿Cuál es el problema?

### Paso 1: Verificar que Payara está corriendo
```
http://localhost:4848
```
Debe abrir la consola de administración.

### Paso 2: Verificar que la aplicación está desplegada
En la consola, ve a **Applications** → Debe aparecer `CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT` con estado **Enabled**

### Paso 3: Encontrar la URL correcta
Usa el botón **"Launch"** o prueba las URLs de arriba.

---

## 🛠️ SOLUCIÓN PERMANENTE: Redesplegar con Context Root Específico

Si ninguna URL funciona, redespliega especificando el contexto root explícitamente:

```bash
cd C:\tools\payara6\bin

# Eliminar la aplicación actual
asadmin.bat undeploy CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT

# Desplegar con contexto root específico
asadmin.bat deploy --contextroot /conversion "D:\Darwin\Documents\PEPAS MONSTER G09\TI1.1 SOAP_JAVA_SINBDD_GR09\01.SERVIDOR\CONUNI_SOAP_JAVA_GR09\target\CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war"
```

Después de redesplegar, la URL será:
```
http://localhost:8080/conversion/WSConversion?wsdl
```

---

## 📝 URLs PROBABLES (prueba estas en orden)

1. `http://localhost:8080/conversion/WSConversion?wsdl`
2. `http://localhost:8080/CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT/WSConversion?wsdl`
3. `http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl`
4. `http://localhost:8080/WSConversion?wsdl`

**Una de estas DEBE funcionar.**

---

## ✅ VERIFICACIÓN FINAL

Si encuentras la URL correcta, deberías ver un XML con el WSDL del servicio.

Si ninguna funciona, el problema puede ser:
1. La aplicación no se desplegó correctamente
2. El servicio SOAP no se generó correctamente
3. Hay un error en el despliegue

En ese caso, revisa los logs de Payara en:
```
C:\tools\payara6\glassfish\domains\domain1\logs\server.log
```

---

**¡Empieza con el botón "Launch" en la consola de Payara!** Es la forma más rápida de encontrar la URL correcta.


