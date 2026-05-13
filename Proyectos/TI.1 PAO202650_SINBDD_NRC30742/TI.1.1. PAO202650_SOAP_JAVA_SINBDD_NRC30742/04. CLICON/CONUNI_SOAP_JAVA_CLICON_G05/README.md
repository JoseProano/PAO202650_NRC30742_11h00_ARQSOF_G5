# Monsters Inc. Converter - Cliente Consola

## 🎯 Descripción
Cliente Java de consola que consume el Web Service SOAP de conversiones de Monsters Inc.

## 🚀 Cómo ejecutar

### Opción 1: Usando IDE (NetBeans, IntelliJ, Eclipse)
1. Abre el proyecto en tu IDE
2. Compila el proyecto
3. Ejecuta la clase `PruebaClienteConversion`

### Opción 2: Usando línea de comandos
```bash
# Compilar
javac -cp src/main/java src/main/java/ec/edu/monster/*/*.java

# Ejecutar aplicación principal
java -cp src/main/java ec.edu.monster.prueba.PruebaClienteConversion

# Ejecutar pruebas automáticas
java -cp src/main/java ec.edu.monster.prueba.PruebaClienteSOAP
```

## 📋 Requisitos
- Java 17 o superior
- Servidor web ejecutándose en `http://localhost:8080`
- Servicio WSConversion desplegado y funcionando

## 🎮 Características
- ✅ Menú interactivo de consola
- ✅ 24 conversiones diferentes
- ✅ Pruebas automáticas
- ✅ Manejo de errores robusto
- ✅ Interfaz Monsters Inc.

## 🔧 Solución de problemas
Si tienes errores de compilación:
1. Asegúrate de que el servidor web esté ejecutándose
2. Verifica que el servicio WSConversion esté desplegado
3. Limpia y recompila el proyecto
