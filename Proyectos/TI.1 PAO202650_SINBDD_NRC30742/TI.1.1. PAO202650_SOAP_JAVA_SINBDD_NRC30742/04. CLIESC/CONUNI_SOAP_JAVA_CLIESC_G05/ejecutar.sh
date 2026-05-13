#!/bin/bash

echo "========================================"
echo "   MONSTERS INC. CONVERTER DESKTOP"
echo "========================================"
echo

echo "Verificando Java..."
java -version
if [ $? -ne 0 ]; then
    echo "ERROR: Java no está instalado o no está en el PATH"
    exit 1
fi

echo
echo "Compilando proyecto..."
mvn clean compile
if [ $? -ne 0 ]; then
    echo "ERROR: Falló la compilación"
    exit 1
fi

echo
echo "Generando clases SOAP..."
mvn jaxws:wsimport
if [ $? -ne 0 ]; then
    echo "ADVERTENCIA: No se pudieron generar las clases SOAP"
    echo "Asegúrese de que el servidor esté ejecutándose"
fi

echo
echo "Ejecutando aplicación..."
mvn exec:java -Dexec.mainClass="ec.edu.monster.prueba.AplicacionDesktop"
