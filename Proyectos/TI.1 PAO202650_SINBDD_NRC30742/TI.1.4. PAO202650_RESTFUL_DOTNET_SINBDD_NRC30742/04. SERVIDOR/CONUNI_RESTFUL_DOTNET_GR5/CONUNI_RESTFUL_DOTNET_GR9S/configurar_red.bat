@echo off
setlocal enabledelayedexpansion
echo ============================================
echo Configurando IIS Express para red local
echo HTTP y HTTPS
echo ============================================
echo.

REM Verificar que se ejecuta como administrador
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo ERROR: Este script debe ejecutarse como Administrador
    echo Clic derecho en el archivo y seleccionar "Ejecutar como administrador"
    pause
    exit /b 1
)

REM Obtener la IP local
echo Obteniendo IP local...
for /f "tokens=2 delims=:" %%a in ('ipconfig ^| findstr /c:"IPv4"') do (
    set LOCAL_IP=%%a
    set LOCAL_IP=!LOCAL_IP:~1!
    goto :found_ip
)

:found_ip
echo IP local detectada: %LOCAL_IP%
echo.

REM Puertos del servidor
set HTTP_PORT=44385
set HTTPS_PORT=44384

echo ============================================
echo Configurando HTTP (Puerto %HTTP_PORT%)
echo ============================================
echo Configurando URL ACL para HTTP...
netsh http add urlacl url=http://%LOCAL_IP%:%HTTP_PORT%/ user=Everyone >nul 2>&1
if %errorLevel% neq 0 (
    echo ADVERTENCIA: URL ACL HTTP ya existe o no se pudo agregar
) else (
    echo URL ACL HTTP agregada correctamente
)

echo Agregando regla de firewall HTTP...
netsh advfirewall firewall delete rule name="IIS Express HTTP RESTful" >nul 2>&1
netsh advfirewall firewall add rule name="IIS Express HTTP RESTful" dir=in action=allow protocol=TCP localport=%HTTP_PORT% >nul 2>&1
if %errorLevel% neq 0 (
    echo ADVERTENCIA: No se pudo agregar la regla de firewall HTTP
) else (
    echo Regla de firewall HTTP agregada correctamente
)
echo.

echo ============================================
echo Configurando HTTPS (Puerto %HTTPS_PORT%)
echo ============================================
echo Configurando URL ACL para HTTPS...
netsh http add urlacl url=https://%LOCAL_IP%:%HTTPS_PORT%/ user=Everyone >nul 2>&1
if %errorLevel% neq 0 (
    echo ADVERTENCIA: URL ACL HTTPS ya existe o no se pudo agregar
) else (
    echo URL ACL HTTPS agregada correctamente
)

echo Agregando regla de firewall HTTPS...
netsh advfirewall firewall delete rule name="IIS Express HTTPS RESTful" >nul 2>&1
netsh advfirewall firewall add rule name="IIS Express HTTPS RESTful" dir=in action=allow protocol=TCP localport=%HTTPS_PORT% >nul 2>&1
if %errorLevel% neq 0 (
    echo ADVERTENCIA: No se pudo agregar la regla de firewall HTTPS
) else (
    echo Regla de firewall HTTPS agregada correctamente
)
echo.

echo ============================================
echo Configuracion completada!
echo ============================================
echo.
echo El servidor ahora deberia ser accesible desde:
echo   - HTTP Localhost: http://localhost:%HTTP_PORT%
echo   - HTTP Red local: http://%LOCAL_IP%:%HTTP_PORT%
echo   - HTTPS Localhost: https://localhost:%HTTPS_PORT%
echo   - HTTPS Red local: https://%LOCAL_IP%:%HTTPS_PORT%
echo.
echo IMPORTANTE: Ahora necesitas:
echo   1. Configurar el proyecto para usar HTTP en Visual Studio
echo   2. Agregar binding HTTP en applicationhost.config
echo   3. O usar HTTPS si prefieres (más complejo)
echo.
echo Verificando puertos...
echo.
echo Puerto HTTP (%HTTP_PORT%):
netstat -an | findstr :%HTTP_PORT%
echo.
echo Puerto HTTPS (%HTTPS_PORT%):
netstat -an | findstr :%HTTPS_PORT%
echo.
echo ============================================
echo RECOMENDACION: Usa HTTP para desarrollo
echo El cliente movil ya esta configurado para HTTP
echo ============================================
echo.
pause

