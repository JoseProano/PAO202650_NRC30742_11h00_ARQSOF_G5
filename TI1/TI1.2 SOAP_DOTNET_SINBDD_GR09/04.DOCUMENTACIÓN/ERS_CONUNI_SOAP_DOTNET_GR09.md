# ERS - Especificación de Requisitos de Software
## CONUNI: Conversor Universal de Unidades — SOAP .NET (WCF)
### Grupo 09 — Monsters Inc.

---

## 1. Introducción

### 1.1 Propósito
Este documento describe los requisitos del sistema CONUNI implementado con tecnología **SOAP (WCF)** en **.NET Framework 4.7.2**, desplegado en **IIS Express**.

### 1.2 Alcance
El sistema permite realizar conversiones de unidades en tres categorías:
- **Longitud**: Metros, Pies, Pulgadas, Kilómetros, Millas
- **Masa/Peso**: Kilogramos, Libras, Gramos, Onzas
- **Temperatura**: Celsius, Fahrenheit, Kelvin

### 1.3 Definiciones y Acrónimos
| Término | Definición |
|---------|-----------|
| WCF | Windows Communication Foundation |
| IIS | Internet Information Services |
| SVC | WCF Service file |

---

## 2. Descripción General

### 2.1 Perspectiva del Producto
Sistema distribuido basado en SOAP/WCF que centraliza la lógica en el servidor .NET.

### 2.2 Funciones del Producto
- Autenticación centralizada (MONSTER / MONSTER9)
- Conversión de unidades de Longitud, Masa y Temperatura

### 2.3 Restricciones
- .NET Framework 4.7.2
- Servidor desplegado en IIS / IIS Express
- Compatible con Visual Studio 2026

---

## 3. Requisitos Específicos

### 3.1 Requisitos Funcionales

#### RF-01: Autenticación
- `Login(string usuario, string contrasena)` → `bool`
- Credenciales: MONSTER / MONSTER9

#### RF-02: Conversiones de Temperatura
| Operación | Método WCF |
|-----------|-----------|
| Celsius → Fahrenheit | `CelsiusAFahrenheit(double)` |
| Fahrenheit → Celsius | `FahrenheitACelsius(double)` |
| Celsius → Kelvin | `CelsiusAKelvin(double)` |
| Kelvin → Celsius | `KelvinACelsius(double)` |
| Fahrenheit → Kelvin | `FahrenheitAKelvin(double)` |
| Kelvin → Fahrenheit | `KelvinAFahrenheit(double)` |

#### RF-03: Conversiones de Longitud
| Operación | Método WCF |
|-----------|-----------|
| Metros ↔ Pies | `MetrosAPies / PiesAMetros` |
| Metros ↔ Pulgadas | `MetrosAPulgadas / PulgadasAMetros` |
| Kilómetros ↔ Millas | `KilometrosAMillas / MillasAKilometros` |

#### RF-04: Conversiones de Masa
| Operación | Método WCF |
|-----------|-----------|
| Kilogramos ↔ Libras | `KilogramosALibras / LibrasAKilogramos` |
| Gramos ↔ Onzas | `GramosAOnzas / OnzasAGramos` |

### 3.2 Requisitos No Funcionales
| ID | Requisito | Descripción |
|----|-----------|-------------|
| RNF-01 | Rendimiento | Respuesta < 2 segundos |
| RNF-02 | Disponibilidad | IIS / IIS Express |
| RNF-03 | Seguridad | Autenticación server-side |
| RNF-04 | Compatibilidad | Visual Studio 2026, .NET 4.7.2 |

---

## 4. Arquitectura del Sistema

### 4.1 Tecnologías
| Componente | Tecnología |
|------------|-----------|
| Servidor | C# WCF (.NET 4.7.2) — IIS Express |
| Cliente Consola | C# Console App (.NET 4.7.2) |
| Cliente Escritorio | C# Windows Forms (.NET 4.7.2) |
| Cliente Web | ASP.NET Core 8.0 MVC |
| Cliente Móvil | Android (Kotlin) |

### 4.2 Estructura
- **Interfaz**: `IWSConversion.cs` — Contrato de servicio `[ServiceContract]`
- **Implementación**: `Service1.svc.cs` — Lógica de negocio
- **Endpoint**: `Service1.svc`

### 4.3 Endpoint SOAP
- URL WSDL: `http://localhost:PORT/Service1.svc?wsdl`
- Namespace: `CONUNI_SOAP_DOTNET_GR09`

---

## 5. Casos de Uso
Idénticos al proyecto SOAP Java (CU-01: Login, CU-02: Conversión).
