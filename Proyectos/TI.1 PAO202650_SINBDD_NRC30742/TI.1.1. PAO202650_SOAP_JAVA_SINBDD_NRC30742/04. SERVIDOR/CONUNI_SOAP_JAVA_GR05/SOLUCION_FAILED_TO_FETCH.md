# 🔧 SOLUCIÓN: Error "Failed to fetch"

## 🎯 El Problema

El error **"Failed to fetch"** generalmente ocurre por:

1. **CORS bloqueado** - El navegador bloquea la petición
2. **URL incorrecta** - Estás usando el WSDL como endpoint (debe ser sin `?wsdl`)
3. **Método HTTP incorrecto** - SOAP requiere POST, no GET

---

## ✅ SOLUCIONES

### Problema 1: Estás usando el WSDL como endpoint

**❌ INCORRECTO:**
```
http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```
Esta URL es solo para **ver el WSDL** (descripción), no para **llamar al servicio**.

**✅ CORRECTO - Endpoint del servicio:**
```
http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion
```
(Sin `?wsdl` al final)

---

### Problema 2: CORS bloqueado

Si estás usando un cliente web (JavaScript, HTML) desde otra URL, necesitas configurar CORS.

**Agregar filtro CORS en `web.xml`:**

```xml
<?xml version="1.0" encoding="UTF-8"?>
<web-app xmlns="https://jakarta.ee/xml/ns/jakartaee"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="https://jakarta.ee/xml/ns/jakartaee 
         https://jakarta.ee/xml/ns/jakartaee/web-app_6_0.xsd"
         version="6.0">
    
    <!-- ... tu configuración existente ... -->
    
    <!-- Filtro CORS para permitir peticiones desde cualquier origen -->
    <filter>
        <filter-name>CORSFilter</filter-name>
        <filter-class>org.apache.catalina.filters.CorsFilter</filter-class>
        <init-param>
            <param-name>cors.allowed.origins</param-name>
            <param-value>*</param-value>
        </init-param>
        <init-param>
            <param-name>cors.allowed.methods</param-name>
            <param-value>GET,POST,PUT,DELETE,OPTIONS,HEAD</param-value>
        </init-param>
        <init-param>
            <param-name>cors.allowed.headers</param-name>
            <param-value>Content-Type,X-Requested-With,accept,Origin,Access-Control-Request-Method,Access-Control-Request-Headers</param-value>
        </init-param>
        <init-param>
            <param-name>cors.exposed.headers</param-name>
            <param-value>Access-Control-Allow-Origin,Access-Control-Allow-Credentials</param-value>
        </init-param>
        <init-param>
            <param-name>cors.support.credentials</param-name>
            <param-value>true</param-value>
        </init-param>
    </filter>
    
    <filter-mapping>
        <filter-name>CORSFilter</filter-name>
        <url-pattern>/*</url-pattern>
    </filter-mapping>
    
</web-app>
```

**Después de agregar esto:**
1. Recompila el proyecto en NetBeans
2. Redespliega en Payara

---

### Problema 3: Estás usando GET en lugar de POST

**SOAP requiere POST**, no GET. Si estás probando desde el navegador directamente, el navegador hace GET por defecto.

**Para probar SOAP correctamente:**
- Usa un cliente SOAP (SOAP UI, Postman, etc.)
- O usa Add Service Reference en Visual Studio
- O usa wsimport en Java

---

## 🔍 DIAGNÓSTICO: ¿Qué estás usando?

### ¿Estás usando un cliente web (HTML/JavaScript)?

**Sí** → Necesitas configurar CORS (ver Problema 2)

**No** → El problema puede ser la URL

---

### ¿Estás probando desde el navegador directamente?

**Sí** → El navegador hace GET, pero SOAP necesita POST. Usa un cliente SOAP.

**No** → Verifica la URL del endpoint (sin `?wsdl`)

---

## 📝 URLs CORRECTAS

### Para ver el WSDL (descripción):
```
http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

### Para llamar al servicio (endpoint):
```
http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion
```
(Sin `?wsdl`)

---

## 🧪 CÓMO PROBAR CORRECTAMENTE

### Opción 1: Desde Visual Studio (.NET)

1. Clic derecho en el proyecto → **Add Service Reference**
2. URL: `http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl`
3. Namespace: `ConversionService`
4. Usa las clases generadas para llamar al servicio

### Opción 2: Desde Java

```bash
wsimport -keep http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl
```

### Opción 3: Desde SOAP UI o Postman

1. Crea una nueva petición SOAP
2. Endpoint: `http://192.168.100.6:8080/CONUNI_SOAP_JAVA_GR09/WSConversion`
3. Usa el WSDL para generar el mensaje SOAP

---

## ✅ RESUMEN

**El error "Failed to fetch" generalmente es porque:**

1. **Estás usando el WSDL como endpoint** → Usa el endpoint sin `?wsdl`
2. **CORS bloqueado** → Agrega el filtro CORS en `web.xml`
3. **Estás usando GET** → SOAP requiere POST (usa un cliente SOAP)

---

**¿Qué tipo de cliente estás usando? ¿HTML/JavaScript, .NET, Java, o estás probando desde el navegador?** 

Esto me ayudará a darte la solución exacta. 🔍


