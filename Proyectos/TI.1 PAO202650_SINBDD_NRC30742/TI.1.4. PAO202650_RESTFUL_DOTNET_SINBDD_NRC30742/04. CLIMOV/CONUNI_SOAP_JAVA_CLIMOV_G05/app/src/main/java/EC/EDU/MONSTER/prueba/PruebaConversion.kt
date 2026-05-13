package EC.EDU.MONSTER.prueba

import EC.EDU.MONSTER.controlador.ControladorMovil
import EC.EDU.MONSTER.modelo.Conversion
import kotlinx.coroutines.runBlocking

/**
 * Clase de pruebas para el sistema de conversiones
 * Permite probar todas las funcionalidades del controlador
 */
class PruebaConversion {
    
    private val controlador = ControladorMovil()
    
    /**
     * Ejecuta todas las pruebas del sistema
     */
    fun ejecutarTodasLasPruebas() {
        println("=== INICIANDO PRUEBAS DEL SISTEMA DE CONVERSIONES ===")
        println()
        
        probarTemperatura()
        probarLongitud()
        probarPeso()
        probarVolumen()
        probarArea()
        
        println("=== PRUEBAS COMPLETADAS ===")
    }
    
    /**
     * Prueba las conversiones de temperatura
     */
    private fun probarTemperatura() {
        println("🌡️ PROBANDO CONVERSIONES DE TEMPERATURA")
        println("=" * 50)
        
        val pruebas = listOf(
            "celsiusAFahrenheit" to 100.0,
            "fahrenheitACelsius" to 212.0,
            "celsiusAKelvin" to 0.0,
            "kelvinACelsius" to 273.15
        )
        
        runBlocking {
            pruebas.forEach { (operacion, valor) ->
                try {
                    val resultado = controlador.convertirTemperatura(operacion, valor)
                    mostrarResultado("Temperatura", operacion, valor, resultado)
                } catch (e: Exception) {
                    println("❌ Error en $operacion: ${e.message}")
                }
            }
        }
        println()
    }
    
    /**
     * Prueba las conversiones de longitud
     */
    private fun probarLongitud() {
        println("📏 PROBANDO CONVERSIONES DE LONGITUD")
        println("=" * 50)
        
        val pruebas = listOf(
            "metrosAPies" to 1.0,
            "piesAMetros" to 3.28084,
            "kilometrosAMillas" to 1.0,
            "millasAKilometros" to 0.621371
        )
        
        runBlocking {
            pruebas.forEach { (operacion, valor) ->
                try {
                    val resultado = controlador.convertirLongitud(operacion, valor)
                    mostrarResultado("Longitud", operacion, valor, resultado)
                } catch (e: Exception) {
                    println("❌ Error en $operacion: ${e.message}")
                }
            }
        }
        println()
    }
    
    /**
     * Prueba las conversiones de peso
     */
    private fun probarPeso() {
        println("⚖️ PROBANDO CONVERSIONES DE PESO")
        println("=" * 50)
        
        val pruebas = listOf(
            "kilogramosALibras" to 1.0,
            "librasAKilogramos" to 2.20462,
            "gramosAOnzas" to 100.0,
            "onzasAGramos" to 3.5274
        )
        
        runBlocking {
            pruebas.forEach { (operacion, valor) ->
                try {
                    val resultado = controlador.convertirPeso(operacion, valor)
                    mostrarResultado("Peso", operacion, valor, resultado)
                } catch (e: Exception) {
                    println("❌ Error en $operacion: ${e.message}")
                }
            }
        }
        println()
    }
    
    /**
     * Prueba las conversiones de volumen
     */
    private fun probarVolumen() {
        println("🧪 PROBANDO CONVERSIONES DE VOLUMEN")
        println("=" * 50)
        
        val pruebas = listOf(
            "litrosAGalones" to 1.0,
            "galonesALitros" to 0.264172,
            "mililitrosAOnzasFluidas" to 1000.0,
            "onzasFluidasAMililitros" to 33.814
        )
        
        runBlocking {
            pruebas.forEach { (operacion, valor) ->
                try {
                    val resultado = controlador.convertirVolumen(operacion, valor)
                    mostrarResultado("Volumen", operacion, valor, resultado)
                } catch (e: Exception) {
                    println("❌ Error en $operacion: ${e.message}")
                }
            }
        }
        println()
    }
    
    /**
     * Prueba las conversiones de área
     */
    private fun probarArea() {
        println("📐 PROBANDO CONVERSIONES DE ÁREA")
        println("=" * 50)
        
        val pruebas = listOf(
            "metrosCuadradosAPiesCuadrados" to 1.0,
            "piesCuadradosAMetrosCuadrados" to 10.7639,
            "hectareasAAcres" to 1.0,
            "acresAHectareas" to 2.47105
        )
        
        runBlocking {
            pruebas.forEach { (operacion, valor) ->
                try {
                    val resultado = controlador.convertirArea(operacion, valor)
                    mostrarResultado("Área", operacion, valor, resultado)
                } catch (e: Exception) {
                    println("❌ Error en $operacion: ${e.message}")
                }
            }
        }
        println()
    }
    
    /**
     * Muestra el resultado de una prueba
     */
    private fun mostrarResultado(tipo: String, operacion: String, valor: Double, resultado: Conversion) {
        if (resultado.exitosa) {
            println("✅ $tipo - $operacion")
            println("   Entrada: $valor")
            println("   Resultado: ${resultado.getResultadoConSimbolos()}")
        } else {
            println("❌ $tipo - $operacion")
            println("   Error: ${resultado.mensajeError}")
        }
        println()
    }
    
    /**
     * Prueba la validación de valores
     */
    fun probarValidacion() {
        println("🔍 PROBANDO VALIDACIÓN DE VALORES")
        println("=" * 50)
        
        val valoresValidos = listOf("123.45", "0", "-10.5", "1000000")
        val valoresInvalidos = listOf("abc", "", "12.34.56", "texto123")
        
        println("Valores válidos:")
        valoresValidos.forEach { valor ->
            val esValido = controlador.validarValor(valor)
            println("  '$valor': ${if (esValido) "✅ Válido" else "❌ Inválido"}")
        }
        
        println("\nValores inválidos:")
        valoresInvalidos.forEach { valor ->
            val esValido = controlador.validarValor(valor)
            println("  '$valor': ${if (esValido) "❌ Válido (ERROR)" else "✅ Inválido"}")
        }
        
        println()
    }
    
    /**
     * Prueba las operaciones disponibles
     */
    fun probarOperacionesDisponibles() {
        println("📋 PROBANDO OPERACIONES DISPONIBLES")
        println("=" * 50)
        
        println("Temperatura:")
        controlador.getOperacionesTemperatura().forEach { (codigo, descripcion) ->
            println("  $codigo -> $descripcion")
        }
        
        println("\nLongitud:")
        controlador.getOperacionesLongitud().forEach { (codigo, descripcion) ->
            println("  $codigo -> $descripcion")
        }
        
        println("\nPeso:")
        controlador.getOperacionesPeso().forEach { (codigo, descripcion) ->
            println("  $codigo -> $descripcion")
        }
        
        println("\nVolumen:")
        controlador.getOperacionesVolumen().forEach { (codigo, descripcion) ->
            println("  $codigo -> $descripcion")
        }
        
        println("\nÁrea:")
        controlador.getOperacionesArea().forEach { (codigo, descripcion) ->
            println("  $codigo -> $descripcion")
        }
        
        println()
    }
}

/**
 * Función de extensión para repetir strings
 */
private operator fun String.times(n: Int): String = this.repeat(n)




