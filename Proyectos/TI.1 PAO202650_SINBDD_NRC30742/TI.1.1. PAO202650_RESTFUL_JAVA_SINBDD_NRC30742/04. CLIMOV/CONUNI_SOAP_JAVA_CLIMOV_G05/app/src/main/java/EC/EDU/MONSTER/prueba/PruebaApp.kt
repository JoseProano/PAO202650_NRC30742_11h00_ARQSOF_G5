package EC.EDU.MONSTER.prueba

import android.content.Context
import android.util.Log
import EC.EDU.MONSTER.controlador.ControladorMovil
import kotlinx.coroutines.runBlocking

/**
 * Clase de pruebas para la aplicación móvil
 * Permite probar todas las funcionalidades del sistema
 */
class PruebaApp(private val context: Context) {
    
    private val controlador = ControladorMovil()
    private val tag = "PruebaApp"
    
    /**
     * Ejecuta todas las pruebas del sistema
     */
    fun ejecutarTodasLasPruebas() {
        Log.d(tag, "=== INICIANDO PRUEBAS DEL SISTEMA DE CONVERSIONES ===")
        
        probarTemperatura()
        probarLongitud()
        probarPeso()
        probarVolumen()
        probarArea()
        probarValidacion()
        probarOperacionesDisponibles()
        
        Log.d(tag, "=== PRUEBAS COMPLETADAS ===")
    }
    
    /**
     * Prueba las conversiones de temperatura
     */
    private fun probarTemperatura() {
        Log.d(tag, "🌡️ PROBANDO CONVERSIONES DE TEMPERATURA")
        
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
                    Log.e(tag, "❌ Error en $operacion: ${e.message}")
                }
            }
        }
    }
    
    /**
     * Prueba las conversiones de longitud
     */
    private fun probarLongitud() {
        Log.d(tag, "📏 PROBANDO CONVERSIONES DE LONGITUD")
        
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
                    Log.e(tag, "❌ Error en $operacion: ${e.message}")
                }
            }
        }
    }
    
    /**
     * Prueba las conversiones de peso
     */
    private fun probarPeso() {
        Log.d(tag, "⚖️ PROBANDO CONVERSIONES DE PESO")
        
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
                    Log.e(tag, "❌ Error en $operacion: ${e.message}")
                }
            }
        }
    }
    
    /**
     * Prueba las conversiones de volumen
     */
    private fun probarVolumen() {
        Log.d(tag, "🧪 PROBANDO CONVERSIONES DE VOLUMEN")
        
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
                    Log.e(tag, "❌ Error en $operacion: ${e.message}")
                }
            }
        }
    }
    
    /**
     * Prueba las conversiones de área
     */
    private fun probarArea() {
        Log.d(tag, "📐 PROBANDO CONVERSIONES DE ÁREA")
        
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
                    Log.e(tag, "❌ Error en $operacion: ${e.message}")
                }
            }
        }
    }
    
    /**
     * Prueba la validación de valores
     */
    private fun probarValidacion() {
        Log.d(tag, "🔍 PROBANDO VALIDACIÓN DE VALORES")
        
        val valoresValidos = listOf("123.45", "0", "-10.5", "1000000")
        val valoresInvalidos = listOf("abc", "", "12.34.56", "texto123")
        
        Log.d(tag, "Valores válidos:")
        valoresValidos.forEach { valor ->
            val esValido = controlador.validarValor(valor)
            Log.d(tag, "  '$valor': ${if (esValido) "✅ Válido" else "❌ Inválido"}")
        }
        
        Log.d(tag, "Valores inválidos:")
        valoresInvalidos.forEach { valor ->
            val esValido = controlador.validarValor(valor)
            Log.d(tag, "  '$valor': ${if (esValido) "❌ Válido (ERROR)" else "✅ Inválido"}")
        }
    }
    
    /**
     * Prueba las operaciones disponibles
     */
    private fun probarOperacionesDisponibles() {
        Log.d(tag, "📋 PROBANDO OPERACIONES DISPONIBLES")
        
        Log.d(tag, "Temperatura:")
        controlador.getOperacionesTemperatura().forEach { (codigo, descripcion) ->
            Log.d(tag, "  $codigo -> $descripcion")
        }
        
        Log.d(tag, "Longitud:")
        controlador.getOperacionesLongitud().forEach { (codigo, descripcion) ->
            Log.d(tag, "  $codigo -> $descripcion")
        }
        
        Log.d(tag, "Peso:")
        controlador.getOperacionesPeso().forEach { (codigo, descripcion) ->
            Log.d(tag, "  $codigo -> $descripcion")
        }
        
        Log.d(tag, "Volumen:")
        controlador.getOperacionesVolumen().forEach { (codigo, descripcion) ->
            Log.d(tag, "  $codigo -> $descripcion")
        }
        
        Log.d(tag, "Área:")
        controlador.getOperacionesArea().forEach { (codigo, descripcion) ->
            Log.d(tag, "  $codigo -> $descripcion")
        }
    }
    
    /**
     * Muestra el resultado de una prueba
     */
    private fun mostrarResultado(tipo: String, operacion: String, valor: Double, resultado: EC.EDU.MONSTER.modelo.Conversion) {
        if (resultado.exitosa) {
            Log.d(tag, "✅ $tipo - $operacion")
            Log.d(tag, "   Entrada: $valor")
            Log.d(tag, "   Resultado: ${resultado.getResultadoConSimbolos()}")
        } else {
            Log.e(tag, "❌ $tipo - $operacion")
            Log.e(tag, "   Error: ${resultado.mensajeError}")
        }
    }
}




