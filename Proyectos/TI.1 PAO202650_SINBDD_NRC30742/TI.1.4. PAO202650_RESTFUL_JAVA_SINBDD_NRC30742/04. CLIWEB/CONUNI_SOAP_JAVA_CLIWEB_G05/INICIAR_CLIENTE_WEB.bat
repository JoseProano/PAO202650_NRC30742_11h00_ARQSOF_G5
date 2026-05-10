@echo off
chcp 65001 >nul
title Monsters Inc. - Cliente Web SOAP
color 0A

echo.
echo ================================================================
echo.
echo    ███╗   ███╗ ██████╗ ███╗   ██╗███████╗████████╗███████╗██████╗ 
echo    ████╗ ████║██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗
echo    ██╔████╔██║██║   ██║██╔██╗ ██║███████╗   ██║   █████╗  ██████╔╝
echo    ██║╚██╔╝██║██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══╝  ██╔══██╗
echo    ██║ ╚═╝ ██║╚██████╔╝██║ ╚████║███████║   ██║   ███████╗██║  ██║
echo    ╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
echo.
echo ================================================================
echo.
color 0B
echo [INFO] Iniciando sistema Cliente Web SOAP...
echo.
color 0A
echo [PASO 1/4] Verificando Maven...
where mvn >nul 2>&1
if %errorlevel% neq 0 (
    color 0C
    echo [ERROR] Maven no encontrado. Por favor, instala Maven o agrega al PATH.
    pause
    exit /b 1
)
color 0A
echo [OK] Maven encontrado
echo.

color 0B
echo [PASO 2/4] Compilando proyecto...
echo.
color 0F
call mvn clean package -q -DskipTests
if %errorlevel% neq 0 (
    color 0C
    echo.
    echo [ERROR] Error al compilar el proyecto. Revisa los errores anteriores.
    pause
    exit /b 1
)
color 0A
echo [OK] Proyecto compilado exitosamente
echo.

color 0B
echo [PASO 3/4] Abriendo puerto 8082...
echo.
color 0A
echo [OK] Servidor Tomcat iniciando en puerto 8082
echo.

color 0B
echo [PASO 4/4] Abriendo navegador...
echo.
timeout /t 3 /nobreak >nul
start http://localhost:8082/CONUNI_SOAP_JAVA_CLIWEB_G09
color 0A
echo [OK] Navegador abierto
echo.
echo ================================================================
color 0E
echo.
echo    ╔═══════════════════════════════════════════════════════════╗
echo    ║                                                           ║
echo    ║   SERVIDOR INICIADO CORRECTAMENTE                         ║
echo    ║                                                           ║
echo    ║   URL: http://localhost:8082/CONUNI_SOAP_JAVA_CLIWEB_G09 ║
echo    ║                                                           ║
echo    ║   Presiona Ctrl+C para detener el servidor                ║
echo    ║                                                           ║
echo    ╚═══════════════════════════════════════════════════════════╝
echo.
color 0F
echo ================================================================
echo.

call mvn tomcat7:run -Dtomcat.port=8082


