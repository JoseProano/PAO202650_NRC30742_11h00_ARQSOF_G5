@echo off
REM Script para configurar IIS Express para escuchar en localhost y en IP 10.183.38.246
REM Debe ejecutarse como Administrador

echo ========================================
echo Configurando IIS Express
echo ========================================
echo.

REM Verificar si se ejecuta como administrador
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo ERROR: Este script debe ejecutarse como Administrador
    echo.
    echo Por favor, haz clic derecho en este archivo y selecciona
    echo "Ejecutar como administrador"
    echo.
    pause
    exit /b 1
)

echo Paso 1: Configurando reservas de URL...
echo.

REM Cambiar al directorio del script
cd /d "%~dp0"

REM Eliminar reservas existentes si existen
netsh http delete urlacl "http://10.183.38.246:51641/" >nul 2>&1
netsh http delete urlacl "http://*:51641/" >nul 2>&1

REM Agregar reserva para todas las interfaces
echo   Agregando reserva para todas las interfaces en puerto 51641...
netsh http add urlacl url="http://*:51641/" user="Everyone"
if %errorLevel% equ 0 (
    echo   Reserva agregada exitosamente
) else (
    echo   La reserva ya existe o hubo un error
)

echo.
echo === Configuracion completada ===
echo.
echo El servicio estara disponible en:
echo   http://localhost:51641/
echo   http://10.183.38.246:51641/
echo   http://localhost:51641/WSEureka.svc
echo   http://10.183.38.246:51641/WSEureka.svc
echo.
echo Nota: Abre el proyecto en Visual Studio y ejecuta este script nuevamente
echo       para configurar el applicationhost.config si es necesario
echo.
pause
