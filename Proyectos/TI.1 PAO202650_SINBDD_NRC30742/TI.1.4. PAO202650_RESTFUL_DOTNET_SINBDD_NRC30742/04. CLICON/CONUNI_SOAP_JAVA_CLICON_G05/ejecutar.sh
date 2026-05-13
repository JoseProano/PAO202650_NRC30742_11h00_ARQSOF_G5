#!/bin/bash
echo "========================================"
echo "MONSTERS INC. CONVERTER - CLIENTE CONSOLA"
echo "========================================"
echo ""

# Configurar codificación UTF-8
export LANG=es_ES.UTF-8
export LC_ALL=es_ES.UTF-8

# Configurar colores ANSI
export TERM=xterm-256color

echo "Configurando terminal para colores y UTF-8..."
echo ""
echo "Prueba de caracteres especiales: áéíóúñ"
echo ""

# Verificar soporte de colores
if [ -t 1 ] && [ "$TERM" != "dumb" ]; then
    echo "Terminal: $(basename $SHELL) - Colores habilitados ✓"
else
    echo "Terminal: Sin soporte de colores"
fi
echo ""

echo "Ejecutando aplicación..."
echo ""
mvn exec:java -Dexec.mainClass="ec.edu.monster.prueba.PruebaClienteConversion"

echo ""
echo "Aplicación finalizada."
