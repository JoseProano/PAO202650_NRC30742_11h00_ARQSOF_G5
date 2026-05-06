package ec.edu.monster.controlador

import ec.edu.monster.modelo.Conversion
import ec.edu.monster.servicios.ClienteConversionREST

/**
 * Controlador principal para la aplicación móvil
 * Maneja la lógica de negocio y coordina entre la vista y el modelo
 */
class ControladorMovil {
    
    private val clienteREST = ClienteConversionREST()
    
    /**
     * Convierte temperatura
     */
    suspend fun convertirTemperatura(operacion: String, valor: Double): Conversion {
        return when (operacion) {
            "celsiusAFahrenheit" -> clienteREST.celsiusAFahrenheit(valor)
            "fahrenheitACelsius" -> clienteREST.fahrenheitACelsius(valor)
            "celsiusAKelvin" -> clienteREST.celsiusAKelvin(valor)
            "kelvinACelsius" -> clienteREST.kelvinACelsius(valor)
            "fahrenheitAKelvin" -> clienteREST.fahrenheitAKelvin(valor)
            "kelvinAFahrenheit" -> clienteREST.kelvinAFahrenheit(valor)
            else -> {
                val error = Conversion()
                error.exitosa = false
                error.mensajeError = "Operación de temperatura no válida: $operacion"
                error
            }
        }
    }
    
    /**
     * Convierte longitud
     */
    suspend fun convertirLongitud(operacion: String, valor: Double): Conversion {
        return when (operacion) {
            "metrosAPies" -> clienteREST.metrosAPies(valor)
            "piesAMetros" -> clienteREST.piesAMetros(valor)
            "metrosAPulgadas" -> clienteREST.metrosAPulgadas(valor)
            "pulgadasAMetros" -> clienteREST.pulgadasAMetros(valor)
            "kilometrosAMillas" -> clienteREST.kilometrosAMillas(valor)
            "millasAKilometros" -> clienteREST.millasAKilometros(valor)
            else -> {
                val error = Conversion()
                error.exitosa = false
                error.mensajeError = "Operación de longitud no válida: $operacion"
                error
            }
        }
    }
    
    /**
     * Convierte peso/masa
     */
    suspend fun convertirPeso(operacion: String, valor: Double): Conversion {
        return when (operacion) {
            "kilogramosALibras" -> clienteREST.kilogramosALibras(valor)
            "librasAKilogramos" -> clienteREST.librasAKilogramos(valor)
            "gramosAOnzas" -> clienteREST.gramosAOnzas(valor)
            "onzasAGramos" -> clienteREST.onzasAGramos(valor)
            else -> {
                val error = Conversion()
                error.exitosa = false
                error.mensajeError = "Operación de peso no válida: $operacion"
                error
            }
        }
    }
    
    /**
     * Convierte volumen
     */
    suspend fun convertirVolumen(operacion: String, valor: Double): Conversion {
        return when (operacion) {
            "litrosAGalones" -> clienteREST.litrosAGalones(valor)
            "galonesALitros" -> clienteREST.galonesALitros(valor)
            "mililitrosAOnzasFluidas" -> clienteREST.mililitrosAOnzasFluidas(valor)
            "onzasFluidasAMililitros" -> clienteREST.onzasFluidasAMililitros(valor)
            else -> {
                val error = Conversion()
                error.exitosa = false
                error.mensajeError = "Operación de volumen no válida: $operacion"
                error
            }
        }
    }
    
    /**
     * Convierte área
     */
    suspend fun convertirArea(operacion: String, valor: Double): Conversion {
        return when (operacion) {
            "metrosCuadradosAPiesCuadrados" -> clienteREST.metrosCuadradosAPiesCuadrados(valor)
            "piesCuadradosAMetrosCuadrados" -> clienteREST.piesCuadradosAMetrosCuadrados(valor)
            "hectareasAAcres" -> clienteREST.hectareasAAcres(valor)
            "acresAHectareas" -> clienteREST.acresAHectareas(valor)
            else -> {
                val error = Conversion()
                error.exitosa = false
                error.mensajeError = "Operación de área no válida: $operacion"
                error
            }
        }
    }
    
    /**
     * Valida si un valor es numérico válido
     */
    fun validarValor(valor: String): Boolean {
        return try {
            valor.toDouble()
            true
        } catch (e: NumberFormatException) {
            false
        }
    }
    
    /**
     * Obtiene las operaciones disponibles para cada tipo de conversión
     */
    fun getOperacionesTemperatura(): List<Pair<String, String>> {
        return listOf(
            "celsiusAFahrenheit" to "Celsius -> Fahrenheit",
            "fahrenheitACelsius" to "Fahrenheit -> Celsius",
            "celsiusAKelvin" to "Celsius -> Kelvin",
            "kelvinACelsius" to "Kelvin -> Celsius",
            "fahrenheitAKelvin" to "Fahrenheit -> Kelvin",
            "kelvinAFahrenheit" to "Kelvin -> Fahrenheit"
        )
    }
    
    fun getOperacionesLongitud(): List<Pair<String, String>> {
        return listOf(
            "metrosAPies" to "Metros -> Pies",
            "piesAMetros" to "Pies -> Metros",
            "metrosAPulgadas" to "Metros -> Pulgadas",
            "pulgadasAMetros" to "Pulgadas -> Metros",
            "kilometrosAMillas" to "Kilómetros -> Millas",
            "millasAKilometros" to "Millas -> Kilómetros"
        )
    }
    
    fun getOperacionesPeso(): List<Pair<String, String>> {
        return listOf(
            "kilogramosALibras" to "Kilogramos -> Libras",
            "librasAKilogramos" to "Libras -> Kilogramos",
            "gramosAOnzas" to "Gramos -> Onzas",
            "onzasAGramos" to "Onzas -> Gramos"
        )
    }
    
    fun getOperacionesVolumen(): List<Pair<String, String>> {
        return listOf(
            "litrosAGalones" to "Litros -> Galones",
            "galonesALitros" to "Galones -> Litros",
            "mililitrosAOnzasFluidas" to "Mililitros -> Onzas Fluidas",
            "onzasFluidasAMililitros" to "Onzas Fluidas -> Mililitros"
        )
    }
    
    fun getOperacionesArea(): List<Pair<String, String>> {
        return listOf(
            "metrosCuadradosAPiesCuadrados" to "Metros² -> Pies²",
            "piesCuadradosAMetrosCuadrados" to "Pies² -> Metros²",
            "hectareasAAcres" to "Hectáreas -> Acres",
            "acresAHectareas" to "Acres -> Hectáreas"
        )
    }
}


