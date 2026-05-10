@echo off
REM Script para obtener la IP del servidor

echo ========================================
echo Informacion de Red del Servidor
echo ========================================
echo.

echo Direcciones IP configuradas:
echo.
ipconfig | findstr /i "IPv4"

echo.
echo ========================================
echo Para usar desde otra computadora:
echo.
echo Reemplaza "localhost" en las URLs con una de las IPs mostradas arriba
echo.
echo Ejemplo:
echo   Local:  http://localhost:8080/conversion/WSConversion?wsdl
echo   Remoto: http://192.168.1.100:8080/conversion/WSConversion?wsdl
echo.
echo ========================================
echo.

pause


