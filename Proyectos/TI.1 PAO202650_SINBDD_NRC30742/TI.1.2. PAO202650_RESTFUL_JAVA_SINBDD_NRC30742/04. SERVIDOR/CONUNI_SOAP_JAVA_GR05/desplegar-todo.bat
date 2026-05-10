@echo off
REM ========================================
REM SCRIPT ÚNICO: Compila, Inicia Payara y Despliega
REM ========================================
REM Este script hace TODO automáticamente:
REM 1. Compila el proyecto (genera .WAR)
REM 2. Inicia Payara Server (si no está corriendo)
REM 3. Despliega el .WAR en Payara
REM 4. Muestra las URLs del servicio
REM ========================================

setlocal enabledelayedexpansion

echo.
echo ========================================
echo   DESPLEGAR SERVICIO SOAP EN PAYARA
echo ========================================
echo.

REM ========================================
REM CONFIGURACIÓN - AJUSTA SEGÚN TU INSTALACIÓN
REM ========================================
set PAYARA_HOME=C:\tools\payara6
set DOMAIN_NAME=domain1
set PROJECT_DIR=%~dp0
set WAR_NAME=CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT.war
set WAR_FILE=%PROJECT_DIR%target\%WAR_NAME%
set CONTEXT_ROOT=/conversion

echo [INFO] Configuración:
echo   - Payara: %PAYARA_HOME%
echo   - Dominio: %DOMAIN_NAME%
echo   - Proyecto: %PROJECT_DIR%
echo   - WAR: %WAR_FILE%
echo   - Context Root: %CONTEXT_ROOT%
echo.

REM ========================================
REM PASO 1: COMPILAR PROYECTO (Generar .WAR)
REM ========================================
echo [1/4] Compilando proyecto y generando .WAR...
echo.

cd /d "%PROJECT_DIR%"

call mvn clean package

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [ERROR] La compilación falló!
    echo Verifica los errores de compilación en NetBeans.
    pause
    exit /b 1
)

if not exist "%WAR_FILE%" (
    echo.
    echo [ERROR] No se encontró el archivo WAR en: %WAR_FILE%
    echo Verifica que la compilación se completó correctamente.
    pause
    exit /b 1
)

echo [OK] .WAR generado exitosamente: %WAR_FILE%
echo.

REM ========================================
REM PASO 2: VERIFICAR/INICIAR PAYARA SERVER
REM ========================================
echo [2/4] Verificando si Payara Server está corriendo...
echo.

cd /d "%PAYARA_HOME%\bin"

REM Verificar si Payara está corriendo (puerto 4848)
powershell -Command "$response = Invoke-WebRequest -Uri 'http://localhost:4848' -UseBasicParsing -TimeoutSec 2 -ErrorAction SilentlyContinue; if ($response.StatusCode -eq 200) { exit 0 } else { exit 1 }" >nul 2>&1

if %ERRORLEVEL% EQU 0 (
    echo [OK] Payara Server ya está corriendo.
    echo.
) else (
    echo [INFO] Payara Server no está corriendo. Iniciando...
    echo.
    
    start /B "" "%PAYARA_HOME%\bin\asadmin.bat" start-domain %DOMAIN_NAME% >nul 2>&1
    
    REM Esperar a que Payara inicie (máximo 60 segundos)
    echo [INFO] Esperando a que Payara Server inicie (esto puede tomar 30-60 segundos)...
    
    set TIMEOUT=60
    set COUNTER=0
    
    :WAIT_FOR_PAYARA
    timeout /t 2 /nobreak >nul 2>&1
    set /a COUNTER+=2
    
    powershell -Command "$response = Invoke-WebRequest -Uri 'http://localhost:4848' -UseBasicParsing -TimeoutSec 2 -ErrorAction SilentlyContinue; if ($response.StatusCode -eq 200) { exit 0 } else { exit 1 }" >nul 2>&1
    
    if %ERRORLEVEL% EQU 0 (
        echo [OK] Payara Server iniciado correctamente!
        echo.
    ) else (
        if !COUNTER! LSS !TIMEOUT! (
            goto WAIT_FOR_PAYARA
        ) else (
            echo [ERROR] Timeout esperando a que Payara inicie.
            echo Verifica que Payara esté instalado correctamente en: %PAYARA_HOME%
            pause
            exit /b 1
        )
    )
)

REM ========================================
REM PASO 3: DESPLEGAR APLICACIÓN EN PAYARA
REM ========================================
echo [3/4] Desplegando aplicación en Payara Server...
echo.

REM Eliminar aplicación anterior si existe
echo [INFO] Eliminando aplicación anterior (si existe)...
"%PAYARA_HOME%\bin\asadmin.bat" undeploy CONUNI_SOAP_JAVA_GR09-1.0-SNAPSHOT 2>nul

REM Desplegar nueva versión
echo [INFO] Desplegando nueva versión...
"%PAYARA_HOME%\bin\asadmin.bat" deploy --contextroot %CONTEXT_ROOT% "%WAR_FILE%"

if %ERRORLEVEL% EQU 0 (
    echo [OK] Aplicación desplegada correctamente!
    echo.
) else (
    echo.
    echo [ERROR] No se pudo desplegar la aplicación.
    echo Verifica que:
    echo   1. Payara Server esté corriendo
    echo   2. El archivo .WAR existe
    echo   3. No hay otra aplicación con el mismo nombre
    echo.
    pause
    exit /b 1
)

REM ========================================
REM PASO 4: VERIFICAR Y MOSTRAR URLs
REM ========================================
echo [4/4] Verificando despliegue...
echo.

REM Obtener IP local
for /f "tokens=2 delims=:" %%a in ('ipconfig ^| findstr /i "IPv4"') do (
    set IP=%%a
    set IP=!IP:~1!
    goto :FOUND_IP
)
:FOUND_IP

echo ========================================
echo   DESPLIEGUE COMPLETADO EXITOSAMENTE!
echo ========================================
echo.
echo URLs del servicio:
echo.
echo [LOCAL]
echo   WSDL:     http://localhost:8080%CONTEXT_ROOT%/WSConversion?wsdl
echo   Endpoint: http://localhost:8080%CONTEXT_ROOT%/WSConversion
echo.
echo [REMOTO - desde otra computadora]
if defined IP (
    echo   WSDL:     http://%IP%:8080%CONTEXT_ROOT%/WSConversion?wsdl
    echo   Endpoint: http://%IP%:8080%CONTEXT_ROOT%/WSConversion
) else (
    echo   IP: (ejecuta 'ipconfig' para obtener tu IP)
    echo   WSDL:     http://[TU_IP]:8080%CONTEXT_ROOT%/WSConversion?wsdl
    echo   Endpoint: http://[TU_IP]:8080%CONTEXT_ROOT%/WSConversion
)
echo.
echo [CONSOLA DE ADMINISTRACIÓN]
echo   Admin: http://localhost:4848
echo.
echo ========================================
echo.
echo Para probar el servicio:
echo   1. Abre el WSDL en el navegador
echo   2. O usa Add Service Reference en Visual Studio
echo   3. O usa wsimport en Java
echo.
echo Para detener Payara Server:
echo   cd %PAYARA_HOME%\bin
echo   asadmin.bat stop-domain %DOMAIN_NAME%
echo.

pause


