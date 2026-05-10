# 🎯 Monsters Inc. Converter - Android App

## 📱 Descripción
Aplicación móvil Android para conversiones universales desarrollada con el patrón MVC (Model-View-Controller) y consumo de servicios SOAP.

## 🏗️ Arquitectura
- **Modelo**: `Conversion.kt` - Clase de datos para representar conversiones
- **Vista**: Fragmentos para cada tipo de conversión + MainActivity
- **Controlador**: `ControladorMovil.kt` - Lógica de negocio y coordinación
- **Servicios**: `ClienteConversionSOAP.kt` - Cliente SOAP para consumir el web service

## 🚀 Características
- ✅ **5 tipos de conversión**: Temperatura, Longitud, Peso, Volumen, Área
- ✅ **Consumo de servicios SOAP** (usando `10.0.2.2` para Android)
- ✅ **Corrutinas** para operaciones asíncronas
- ✅ **Validación de datos** en tiempo real
- ✅ **Manejo de errores** robusto
- ✅ **Interfaz de usuario** moderna con Material Design
- ✅ **Sistema de pruebas** completo

## 📦 Dependencias
- Android SDK 24+
- Kotlin Coroutines
- Material Design Components
- Navigation Component
- OkHttp para networking

## 🔧 Instalación
1. Clona el repositorio
2. Abre el proyecto en Android Studio
3. Sincroniza las dependencias
4. Ejecuta la aplicación

## 🌐 Configuración del Servidor
- **URL del servidor**: `http://10.0.2.2:8080/CONUNI_SOAP_JAVA_GR09/WSConversion`
- **Nota**: `10.0.2.2` es la IP del host local desde el emulador Android

## 📱 Uso
1. **Inicio**: Pantalla principal con botones de navegación
2. **Selección**: Elige el tipo de conversión deseado
3. **Conversión**: Ingresa el valor y selecciona la operación
4. **Resultado**: Visualiza el resultado de la conversión

## 🧪 Pruebas
- **PruebaConversion.kt**: Pruebas unitarias del sistema
- **PruebaApp.kt**: Pruebas de integración con Android

## 🎨 Colores del Tema
- **Monster Blue**: `#21a9da`
- **Monster Purple**: `#8c6bcd`
- **Monster Pink**: `#f66971`
- **Monster Yellow**: `#f5d880`
- **Monster Light Blue**: `#9fdcfa`

## 📋 Tipos de Conversión

### 🌡️ Temperatura
- Celsius ↔ Fahrenheit
- Celsius ↔ Kelvin
- Fahrenheit ↔ Kelvin

### 📏 Longitud
- Metros ↔ Pies
- Metros ↔ Pulgadas
- Pies ↔ Pulgadas
- Kilómetros ↔ Millas

### ⚖️ Peso/Masa
- Kilogramos ↔ Libras
- Kilogramos ↔ Onzas
- Libras ↔ Onzas
- Gramos ↔ Onzas

### 🧪 Volumen
- Litros ↔ Galones
- Litros ↔ Metros Cúbicos
- Galones ↔ Metros Cúbicos
- Mililitros ↔ Onzas Fluidas

### 📐 Área
- Metros² ↔ Pies²
- Metros² ↔ Hectáreas
- Pies² ↔ Hectáreas
- Hectáreas ↔ Acres

## 🔧 Desarrollo
- **Lenguaje**: Kotlin
- **Patrón**: MVC
- **UI**: Material Design
- **Networking**: SOAP + OkHttp
- **Async**: Kotlin Coroutines

## 📄 Licencia
Monsters Inc. Converter v1.0 - Enterprise Edition

---
**Desarrollado con ❤️ para conversiones universales**




