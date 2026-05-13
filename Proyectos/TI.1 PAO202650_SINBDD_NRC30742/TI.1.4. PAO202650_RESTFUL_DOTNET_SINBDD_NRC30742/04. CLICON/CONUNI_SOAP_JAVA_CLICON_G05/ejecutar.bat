@echo off
echo ========================================
echo MONSTERS INC. CONVERTER - CLIENTE CONSOLA
echo ========================================
echo.

REM Configurar codificacion UTF-8
chcp 65001 >nul 2>&1

REM Configurar colores ANSI para Windows Terminal/PowerShell
set TERM=xterm-256color

echo Configurando terminal para colores y UTF-8...
echo.
echo Prueba de caracteres especiales: áéíóúñ
echo.

REM Verificar si estamos en PowerShell o CMD
echo %COMSPEC% | findstr /i powershell >nul
if %errorlevel% equ 0 (
    echo Terminal: PowerShell - Colores habilitados
) else (
    echo Terminal: CMD - Colores limitados
    echo Sugerencia: Use PowerShell o Windows Terminal para mejor experiencia
)
echo.

echo Ejecutando aplicacion...
echo.
mvn exec:java -Dexec.mainClass="ec.edu.monster.prueba.PruebaClienteConversion"

echo.
echo Aplicacion finalizada.
pause
