@echo off
REM Script para detener Payara Server

echo ========================================
echo Deteniendo Payara Server...
echo ========================================

REM Cambiar esta ruta según tu instalación
set PAYARA_HOME=C:\tools\payara6
set DOMAIN_NAME=domain1

cd /d "%PAYARA_HOME%\bin"

echo Ejecutando: %PAYARA_HOME%\bin\asadmin.bat stop-domain %DOMAIN_NAME%
"%PAYARA_HOME%\bin\asadmin.bat" stop-domain %DOMAIN_NAME%

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Payara Server detenido correctamente!
    echo ========================================
) else (
    echo.
    echo ERROR: No se pudo detener Payara Server
    echo Verifica que el servidor este corriendo
    echo.
)

pause

