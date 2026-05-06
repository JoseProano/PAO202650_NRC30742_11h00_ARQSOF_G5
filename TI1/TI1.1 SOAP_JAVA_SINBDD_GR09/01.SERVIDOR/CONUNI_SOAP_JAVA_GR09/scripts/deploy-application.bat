@echo off
REM Script para compilar, generar WAR y desplegar en Payara

echo ========================================
echo Desplegando aplicacion SOAP en Payara
echo ========================================

REM Cambiar estas rutas según tu instalación
set PAYARA_HOME=C:\tools\payara6
set PROJECT_DIR=%~dp0..
set WAR_FILE=%PROJECT_DIR%\target\CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war

cd /d "%PROJECT_DIR%"

echo.
echo Paso 1: Compilando proyecto...
call mvn clean package

if %ERRORLEVEL% NEQ 0 (
    echo ERROR: La compilacion fallo
    pause
    exit /b 1
)

if not exist "%WAR_FILE%" (
    echo ERROR: No se encontro el archivo WAR en: %WAR_FILE%
    pause
    exit /b 1
)

echo.
echo Paso 2: Desplegando aplicacion en Payara...
cd /d "%PAYARA_HOME%\bin"

REM Eliminar aplicacion anterior si existe
"%PAYARA_HOME%\bin\asadmin.bat" undeploy CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT 2>nul

REM Desplegar nueva version
"%PAYARA_HOME%\bin\asadmin.bat" deploy "%WAR_FILE%"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Aplicacion desplegada correctamente!
    echo ========================================
    echo.
    echo URLs del servicio:
    echo - WSDL: http://localhost:8080/conversion/WSConversion?wsdl
    echo - Endpoint: http://localhost:8080/conversion/WSConversion
    echo.
    echo Para acceder desde otra computadora, reemplaza localhost con la IP del servidor
    echo Ejemplo: http://192.168.1.100:8080/conversion/WSConversion?wsdl
    echo.
) else (
    echo.
    echo ERROR: No se pudo desplegar la aplicacion
    echo Verifica que Payara Server este corriendo
    echo.
)

pause

