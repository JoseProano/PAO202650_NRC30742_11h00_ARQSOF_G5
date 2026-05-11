# ERS - Especificación de Requisitos de Software
## CONUNI: Conversor Universal de Unidades — SOAP Java
### Grupo 09 — Monsters Inc.

---

## 1. Introducción

### 1.1 Propósito
Este documento describe los requisitos funcionales y no funcionales del sistema de Conversión Universal de Unidades (CONUNI) implementado con tecnología **SOAP (JAX-WS)** en **Java**, desplegado en servidor **Payara**.

### 1.2 Alcance
El sistema permite realizar conversiones de unidades en tres categorías:
- **Longitud**: Metros, Pies, Pulgadas, Kilómetros, Millas
- **Masa/Peso**: Kilogramos, Libras, Gramos, Onzas
- **Temperatura**: Celsius, Fahrenheit, Kelvin

### 1.3 Definiciones y Acrónimos
| Término | Definición |
|---------|-----------|
| SOAP | Simple Object Access Protocol |
| WSDL | Web Services Description Language |
| JAX-WS | Java API for XML Web Services |
| WS | Web Service |

---

## 2. Descripción General

### 2.1 Perspectiva del Producto
Sistema distribuido cliente-servidor basado en SOAP/XML que centraliza la lógica de conversión y autenticación en el servidor.

### 2.2 Funciones del Producto
- Autenticación de usuarios (credenciales fijas: MONSTER / MONSTER9)
- Conversión de unidades de Longitud
- Conversión de unidades de Masa/Peso
- Conversión de unidades de Temperatura

### 2.3 Características de los Usuarios
Usuarios con conocimientos básicos de informática que necesiten realizar conversiones de unidades.

### 2.4 Restricciones
- Sin base de datos (credenciales hardcoded en el servidor)
- Servidor desplegado en Payara Server
- Comunicación vía SOAP/XML

---

## 3. Requisitos Específicos

### 3.1 Requisitos Funcionales

#### RF-01: Autenticación
- El servidor valida credenciales (usuario: MONSTER, contraseña: MONSTER9)
- Retorna `true` o `false`

#### RF-02: Conversiones de Temperatura
| Operación | Fórmula |
|-----------|---------|
| Celsius → Fahrenheit | (C × 9/5) + 32 |
| Fahrenheit → Celsius | (F - 32) × 5/9 |
| Celsius → Kelvin | C + 273.15 |
| Kelvin → Celsius | K - 273.15 |
| Fahrenheit → Kelvin | (F - 32) × 5/9 + 273.15 |
| Kelvin → Fahrenheit | (K - 273.15) × 9/5 + 32 |

#### RF-03: Conversiones de Longitud
| Operación | Factor |
|-----------|--------|
| Metros ↔ Pies | 3.28084 |
| Metros ↔ Pulgadas | 39.3701 |
| Kilómetros ↔ Millas | 0.621371 |

#### RF-04: Conversiones de Masa
| Operación | Factor |
|-----------|--------|
| Kilogramos ↔ Libras | 2.20462 |
| Gramos ↔ Onzas | 0.035274 |

### 3.2 Requisitos No Funcionales

| ID | Requisito | Descripción |
|----|-----------|-------------|
| RNF-01 | Rendimiento | Respuesta < 2 segundos |
| RNF-02 | Disponibilidad | Servidor Payara |
| RNF-03 | Seguridad | Autenticación server-side |
| RNF-04 | Interoperabilidad | WSDL estándar |

---

## 4. Arquitectura del Sistema

### 4.1 Tecnologías
| Componente | Tecnología |
|------------|-----------|
| Servidor | Java (JAX-WS) — Payara Server |
| Cliente Consola | Java (Maven) |
| Cliente Escritorio | Java Swing |
| Cliente Web | JSP/Servlets — Payara Server |
| Cliente Móvil | Android (Kotlin) |

### 4.2 Estructura MVC
- **Modelo**: `ConversionService.java` — Lógica de negocio
- **Vista**: JSPs (web), Swing (escritorio), Consola (texto)
- **Controlador**: `WSConversion.java` — Endpoint SOAP

### 4.3 Endpoint SOAP
- URL WSDL: `http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl`
- Namespace: `http://servicios.monster.edu.ec/`

---

## 5. Casos de Uso

### CU-01: Iniciar Sesión
- **Actor**: Usuario
- **Precondición**: Servidor activo
- **Flujo**: Ingresar usuario y contraseña → Validar en servidor → Acceder al sistema
- **Postcondición**: Sesión autenticada

### CU-02: Realizar Conversión
- **Actor**: Usuario autenticado
- **Precondición**: Sesión activa
- **Flujo**: Seleccionar categoría → Ingresar valor → Seleccionar unidades → Obtener resultado
- **Postcondición**: Resultado mostrado
