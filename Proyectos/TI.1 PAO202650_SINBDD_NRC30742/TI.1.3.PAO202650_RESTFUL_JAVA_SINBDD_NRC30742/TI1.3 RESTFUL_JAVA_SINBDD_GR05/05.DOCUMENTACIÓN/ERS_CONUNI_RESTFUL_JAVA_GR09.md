# ERS - Especificación de Requisitos de Software
## CONUNI: Conversor Universal de Unidades — RESTful Java
### Grupo 09 — Monsters Inc.

---

## 1. Introducción

### 1.1 Propósito
Este documento describe los requisitos del sistema CONUNI implementado con tecnología **REST (JAX-RS)** en **Java**, desplegado en servidor **Payara**.

### 1.2 Alcance
El sistema permite realizar conversiones de unidades en tres categorías:
- **Longitud**: Metros, Pies, Pulgadas, Kilómetros, Millas
- **Masa/Peso**: Kilogramos, Libras, Gramos, Onzas
- **Temperatura**: Celsius, Fahrenheit, Kelvin

### 1.3 Definiciones y Acrónimos
| Término | Definición |
|---------|-----------|
| REST | Representational State Transfer |
| JAX-RS | Java API for RESTful Web Services |
| JSON | JavaScript Object Notation |
| API | Application Programming Interface |

---

## 2. Descripción General

### 2.1 Perspectiva del Producto
Sistema distribuido REST que expone endpoints HTTP para conversión de unidades, con autenticación centralizada.

### 2.2 Funciones del Producto
- Autenticación vía POST /api/auth/login
- Conversiones de Longitud, Masa y Temperatura via HTTP GET
- Conversión genérica via HTTP POST

### 2.3 Restricciones
- Sin base de datos
- Servidor Payara
- Formato JSON

---

## 3. Requisitos Específicos

### 3.1 Requisitos Funcionales

#### RF-01: Autenticación
- `POST /api/auth/login` con body `{"usuario": "MONSTER", "contrasena": "MONSTER9"}`
- Retorna `{"autenticado": true/false}`

#### RF-02: Endpoints de Temperatura
| Endpoint | Descripción |
|----------|-------------|
| `GET /api/conversion/temperatura/celsius-to-fahrenheit?celsius=X` | °C → °F |
| `GET /api/conversion/temperatura/fahrenheit-to-celsius?fahrenheit=X` | °F → °C |
| `GET /api/conversion/temperatura/celsius-to-kelvin?celsius=X` | °C → K |
| `GET /api/conversion/temperatura/kelvin-to-celsius?kelvin=X` | K → °C |
| `GET /api/conversion/temperatura/fahrenheit-to-kelvin?fahrenheit=X` | °F → K |
| `GET /api/conversion/temperatura/kelvin-to-fahrenheit?kelvin=X` | K → °F |

#### RF-03: Endpoints de Longitud
| Endpoint | Descripción |
|----------|-------------|
| `GET /api/conversion/longitud/metros-to-pies?metros=X` | m → ft |
| `GET /api/conversion/longitud/pies-to-metros?pies=X` | ft → m |
| `GET /api/conversion/longitud/metros-to-pulgadas?metros=X` | m → in |
| `GET /api/conversion/longitud/pulgadas-to-metros?pulgadas=X` | in → m |
| `GET /api/conversion/longitud/kilometros-to-millas?kilometros=X` | km → mi |
| `GET /api/conversion/longitud/millas-to-kilometros?millas=X` | mi → km |

#### RF-04: Endpoints de Masa
| Endpoint | Descripción |
|----------|-------------|
| `GET /api/conversion/peso/kilogramos-to-libras?kilogramos=X` | kg → lb |
| `GET /api/conversion/peso/libras-to-kilogramos?libras=X` | lb → kg |
| `GET /api/conversion/peso/gramos-to-onzas?gramos=X` | g → oz |
| `GET /api/conversion/peso/onzas-to-gramos?onzas=X` | oz → g |

#### RF-05: Conversión Genérica
- `POST /api/conversion/convertir` con body JSON:
```json
{
  "valor": 100,
  "unidadOrigen": "celsius",
  "unidadDestino": "fahrenheit",
  "categoria": "temperatura"
}
```

### 3.2 Requisitos No Funcionales
| ID | Requisito | Descripción |
|----|-----------|-------------|
| RNF-01 | Rendimiento | Respuesta < 2 segundos |
| RNF-02 | Formato | JSON (application/json) |
| RNF-03 | CORS | Habilitado para todos los orígenes |
| RNF-04 | Servidor | Payara Server |

---

## 4. Arquitectura del Sistema

### 4.1 Tecnologías
| Componente | Tecnología |
|------------|-----------|
| Servidor | Java (JAX-RS / Jakarta EE) — Payara Server |
| Cliente Consola | Java (HttpClient) |
| Cliente Escritorio | Java Swing |
| Cliente Web | JSP/Servlets — Payara Server |
| Cliente Móvil | Android (Kotlin) |

### 4.2 Estructura MVC
- **Modelo**: `Conversion.java`, `ConversionRequest.java`, `ConversionResponse.java`
- **Servicio**: `ConversionService.java` — Lógica de negocio
- **Controlador**: `ConversionController.java` — Endpoints REST
- **Autenticación**: `AuthController.java`

### 4.3 Base URL
- `http://localhost:8080/CONUNI_RESTFUL_JAVA_GR09/api/`

---

## 5. Formato de Respuesta
```json
{
  "exito": true,
  "valorOriginal": 100.0,
  "resultado": 212.0,
  "unidadOrigen": "celsius",
  "unidadDestino": "fahrenheit",
  "categoria": "temperatura",
  "mensaje": "Conversión exitosa"
}
```
