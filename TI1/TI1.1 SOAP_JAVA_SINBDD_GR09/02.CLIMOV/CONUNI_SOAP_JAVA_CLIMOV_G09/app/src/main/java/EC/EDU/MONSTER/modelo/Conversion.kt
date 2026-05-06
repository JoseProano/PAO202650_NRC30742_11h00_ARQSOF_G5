package EC.EDU.MONSTER.modelo

/**
 * Modelo de datos para las conversiones
 * Representa el resultado de una conversión realizada
 */
data class Conversion(
    var valorOriginal: Double = 0.0,
    var unidadOriginal: String = "",
    var valorConvertido: Double = 0.0,
    var unidadConvertida: String = "",
    var operacion: String = "",
    var exitosa: Boolean = false,
    var mensajeError: String = "",
    var tipoConversion: String = ""
) {
    
    /**
     * Obtiene el resultado formateado para mostrar al usuario
     */
    fun getResultadoFormateado(): String {
        return if (exitosa) {
            String.format("%.2f %s = %.2f %s", 
                valorOriginal, unidadOriginal, valorConvertido, unidadConvertida)
        } else {
            "ERROR: $mensajeError"
        }
    }
    
    /**
     * Obtiene el símbolo de la unidad original
     */
    fun getSimboloOriginal(): String {
        return when (unidadOriginal.lowercase()) {
            "celsius" -> "°C"
            "fahrenheit" -> "°F"
            "kelvin" -> "K"
            "metros" -> "m"
            "pies" -> "ft"
            "pulgadas" -> "in"
            "kilometros" -> "km"
            "millas" -> "mi"
            "kilogramos" -> "kg"
            "libras" -> "lb"
            "gramos" -> "g"
            "onzas" -> "oz"
            "litros" -> "L"
            "galones" -> "gal"
            "mililitros" -> "mL"
            "onzasfluidas" -> "fl oz"
            "metroscuadrados" -> "m²"
            "piescuadrados" -> "ft²"
            "hectareas" -> "ha"
            "acres" -> "ac"
            else -> unidadOriginal
        }
    }
    
    /**
     * Obtiene el símbolo de la unidad convertida
     */
    fun getSimboloConvertido(): String {
        return when (unidadConvertida.lowercase()) {
            "celsius" -> "°C"
            "fahrenheit" -> "°F"
            "kelvin" -> "K"
            "metros" -> "m"
            "pies" -> "ft"
            "pulgadas" -> "in"
            "kilometros" -> "km"
            "millas" -> "mi"
            "kilogramos" -> "kg"
            "libras" -> "lb"
            "gramos" -> "g"
            "onzas" -> "oz"
            "litros" -> "L"
            "galones" -> "gal"
            "mililitros" -> "mL"
            "onzasfluidas" -> "fl oz"
            "metroscuadrados" -> "m²"
            "piescuadrados" -> "ft²"
            "hectareas" -> "ha"
            "acres" -> "ac"
            else -> unidadConvertida
        }
    }
    
    /**
     * Obtiene el resultado con símbolos para mostrar
     */
    fun getResultadoConSimbolos(): String {
        return if (exitosa) {
            String.format("%.2f %s = %.2f %s", 
                valorOriginal, getSimboloOriginal(), 
                valorConvertido, getSimboloConvertido())
        } else {
            "ERROR: $mensajeError"
        }
    }
}




