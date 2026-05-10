@echo off
REM Script para configurar el Firewall de Windows para Payara
REM Requiere ejecutarse como Administrador

echo ========================================
echo Configurando Firewall de Windows
echo ========================================
echo.
echo Este script requiere privilegios de Administrador
echo.

REM Verificar si se ejecuta como administrador
net session >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Este script debe ejecutarse como Administrador
    echo Clic derecho en el archivo y selecciona "Ejecutar como administrador"
    pause
    exit /b 1
)

echo Configurando reglas de firewall...

REM Permitir puerto 8080 (HTTP)
echo Configurando regla para puerto 8080 (HTTP)...
netsh advfirewall firewall add rule name="Payara HTTP 8080" dir=in action=allow protocol=TCP localport=8080

REM Permitir puerto 4848 (Admin Console - opcional)
echo Configurando regla para puerto 4848 (Admin Console)...
netsh advfirewall firewall add rule name="Payara Admin 4848" dir=in action=allow protocol=TCP localport=4848

REM Permitir puerto 8181 (HTTPS - opcional)
echo Configurando regla para puerto 8181 (HTTPS)...
netsh advfirewall firewall add rule name="Payara HTTPS 8181" dir=in action=allow protocol=TCP localport=8181

echo.
echo ========================================
echo Reglas de firewall configuradas!
echo ========================================
echo.
echo Reglas creadas:
echo - Payara HTTP 8080 (Puerto 8080)
echo - Payara Admin 4848 (Puerto 4848)
echo - Payara HTTPS 8181 (Puerto 8181)
echo.
echo Para verificar las reglas, ejecuta:
echo netsh advfirewall firewall show rule name="Payara HTTP 8080"
echo.
pause


