@echo off
echo ========================================
echo   MONSTERS INC. CONVERTER DESKTOP
echo ========================================
echo.

echo Verificando Java...
java -version
if %errorlevel% neq 0 (
    echo ERROR: Java no esta instalado o no esta en el PATH
    pause
    exit /b 1
)

echo.
echo Compilando proyecto...
call mvn clean compile
if %errorlevel% neq 0 (
    echo ERROR: Fallo la compilacion
    pause
    exit /b 1
)

echo.
echo Generando clases SOAP...
call mvn jaxws:wsimport
if %errorlevel% neq 0 (
    echo ADVERTENCIA: No se pudieron generar las clases SOAP
    echo Asegurese de que el servidor este ejecutandose
)

echo.
echo Ejecutando aplicacion...
call mvn exec:java -Dexec.mainClass="ec.edu.monster.prueba.AplicacionDesktop"

pause
