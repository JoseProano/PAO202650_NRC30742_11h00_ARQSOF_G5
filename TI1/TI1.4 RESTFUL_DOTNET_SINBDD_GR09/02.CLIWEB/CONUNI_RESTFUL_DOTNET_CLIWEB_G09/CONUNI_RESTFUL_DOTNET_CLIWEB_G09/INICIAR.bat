@echo off
chcp 65001 >nul
title Cliente Web RESTFUL .NET - Monsters Inc.
color 0A

cls
echo.
echo ╔═══════════════════════════════════════════════════════════╗
echo ║      CLIENTE WEB RESTFUL .NET - MONSTERS INC.             ║
echo ╚═══════════════════════════════════════════════════════════╝
echo.

color 0B
echo [1/4] Verificando .NET SDK...
where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    color 0C
    echo [ERROR] .NET SDK no encontrado. Instala .NET SDK o agrega al PATH.
    pause
    exit /b 1
)
color 0A
echo [OK] .NET SDK encontrado
echo.

color 0B
echo [2/4] Restaurando paquetes NuGet (esto puede tardar unos segundos)...
color 0F
call dotnet restore >nul 2>&1
if %errorlevel% neq 0 (
    color 0C
    echo [ERROR] Error al restaurar paquetes. Ejecuta 'dotnet restore' para ver detalles.
    pause
    exit /b 1
)
color 0A
echo [OK] Paquetes restaurados
echo.

color 0B
echo [3/4] Compilando proyecto...
color 0F
call dotnet build --no-restore -q >nul 2>&1
if %errorlevel% neq 0 (
    color 0C
    echo [ERROR] Error al compilar. Ejecuta 'dotnet build' para ver detalles.
    pause
    exit /b 1
)
color 0A
echo [OK] Compilación exitosa
echo.

color 0B
echo [4/4] Iniciando servidor en puerto 5191...
color 0A
echo [OK] Servidor iniciando...
echo.

color 0B
echo Abriendo navegador en 3 segundos...
timeout /t 3 /nobreak >nul
start http://localhost:5191
color 0A
echo [OK] Navegador abierto
echo.

color 0E
echo ╔═══════════════════════════════════════════════════════════╗
echo ║  ✓ SERVIDOR INICIADO                                     ║
echo ║  ✓ URL: http://localhost:5191                            ║
echo ║  ✓ Presiona Ctrl+C para detener                          ║
echo ╚═══════════════════════════════════════════════════════════╝
echo.
color 0F

call dotnet run --no-build


