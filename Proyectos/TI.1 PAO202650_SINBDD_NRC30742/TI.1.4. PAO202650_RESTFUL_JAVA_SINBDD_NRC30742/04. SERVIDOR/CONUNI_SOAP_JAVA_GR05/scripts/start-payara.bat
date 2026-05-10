@echo off
REM Script para iniciar Payara Server
REM Ajusta la ruta según tu instalación de Payara

echo ========================================
echo Iniciando Payara Server...
echo ========================================

REM Cambiar esta ruta según tu instalación
set PAYARA_HOME=C:\tools\payara6
set DOMAIN_NAME=domain1

cd /d "%PAYARA_HOME%\bin"

echo Ejecutando: %PAYARA_HOME%\bin\asadmin.bat start-domain %DOMAIN_NAME%
"%PAYARA_HOME%\bin\asadmin.bat" start-domain %DOMAIN_NAME%

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Payara Server iniciado correctamente!
    echo ========================================
    echo.
    echo URLs importantes:
    echo - Consola Admin: http://localhost:4848
    echo - Aplicacion: http://localhost:8080/conversion
    echo - WSDL: http://localhost:8080/conversion/WSConversion?wsdl
    echo.
    echo Para detener el servidor, ejecuta: stop-payara.bat
    echo.
) else (
    echo.
    echo ERROR: No se pudo iniciar Payara Server
    echo Verifica que:
    echo 1. Payara este instalado en: %PAYARA_HOME%
    echo 2. El dominio %DOMAIN_NAME% exista
    echo 3. No haya otro proceso usando los puertos 8080 o 4848
    echo.
)

pause

