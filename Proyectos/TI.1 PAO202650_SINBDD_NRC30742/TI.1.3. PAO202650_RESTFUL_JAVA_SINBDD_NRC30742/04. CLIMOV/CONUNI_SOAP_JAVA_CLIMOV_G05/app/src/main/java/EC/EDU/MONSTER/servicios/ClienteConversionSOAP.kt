package EC.EDU.MONSTER.servicios

import EC.EDU.MONSTER.modelo.Conversion
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import java.io.BufferedReader
import java.io.InputStreamReader
import java.io.OutputStreamWriter
import java.net.HttpURLConnection
import java.net.URL
import java.nio.charset.StandardCharsets
import java.util.regex.Pattern

/**
 * Cliente SOAP para consumir el servicio de conversiones
 * Maneja la comunicación con el servidor web service
 */
class
ClienteConversionSOAP {
    
    companion object {
        private const val BASE_URL = "http://10.92.232.246:8080/CONUNI_SOAP_JAVA_GR09/WSConversion"
        private const val TIMEOUT = 30000 // 30 segundos
    }
    
    /**
     * Realiza una llamada SOAP genérica
     */
    suspend fun llamadaSOAP(method: String, valor: Double): Conversion = withContext(Dispatchers.IO) {
        println("=== INICIANDO LLAMADA SOAP ===")
        println("Método: $method")
        println("Valor: $valor")
        println("URL: $BASE_URL")

        val conversion = Conversion()
        conversion.valorOriginal = valor
        conversion.operacion = method

        try {
            val soapEnvelope = crearEnvelopeSOAP(method, valor)
            println("Envelope SOAP: $soapEnvelope")
            val soapResponse = enviarPeticionSOAP(soapEnvelope, method)
            println("Respuesta SOAP: $soapResponse")

            val resultado = parsearRespuestaSOAP(soapResponse, method)
            conversion.valorConvertido = resultado
            conversion.exitosa = true
            conversion.mensajeError = ""

            // Determinar unidades para el resultado formateado
            val unidades = determinarUnidades(method)
            conversion.unidadOriginal = unidades[0]
            conversion.unidadConvertida = unidades[1]

        } catch (e: Exception) {
            println("ERROR en llamada SOAP: ${e.message}")
            conversion.exitosa = false
            conversion.mensajeError = "Error de conexión: ${e.message}"
        }
        
        println("Resultado obtenido: ${conversion.getResultadoFormateado()}")
        conversion
    }
    
    /**
     * Crea el envelope SOAP para la llamada
     * Usa RPC/LITERAL según el WSDL del servidor
     */
    private fun crearEnvelopeSOAP(method: String, valor: Double): String {
        val namespace = "http://servicios.monster.edu.ec/"
        val paramName = determinarNombreParametro(method)
        
        return """<?xml version="1.0" encoding="UTF-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tns="$namespace">
    <soap:Body>
        <tns:$method>
            <$paramName>$valor</$paramName>
        </tns:$method>
    </soap:Body>
</soap:Envelope>"""
    }
    
    /**
     * Determina el nombre del parámetro basado en el método SOAP
     */
    private fun determinarNombreParametro(method: String): String {
        return when (method) {
            // Temperatura
            "celsiusAFahrenheit", "celsiusAKelvin" -> "celsius"
            "fahrenheitACelsius", "fahrenheitAKelvin" -> "fahrenheit"
            "kelvinACelsius", "kelvinAFahrenheit" -> "kelvin"
            
            // Longitud
            "metrosAPies", "metrosAPulgadas" -> "metros"
            "piesAMetros", "piesAPulgadas" -> "pies"
            "pulgadasAMetros", "pulgadasAPies" -> "pulgadas"
            "kilometrosAMillas" -> "kilometros"
            "millasAKilometros" -> "millas"
            
            // Peso
            "kilogramosALibras", "kilogramosAOnzas" -> "kilogramos"
            "librasAKilogramos", "librasAOnzas" -> "libras"
            "onzasAKilogramos", "onzasALibras" -> "onzas"
            "gramosAOnzas" -> "gramos"
            "onzasAGramos" -> "onzas"
            
            // Volumen
            "litrosAGalones", "litrosAMetrosCubicos" -> "litros"
            "galonesALitros", "galonesAMetrosCubicos" -> "galones"
            "metrosCubicosALitros", "metrosCubicosAGalones" -> "metrosCubicos"
            "mililitrosAOnzasFluidas" -> "mililitros"
            "onzasFluidasAMililitros" -> "onzasFluidas"
            
            // Área
            "metrosCuadradosAPiesCuadrados", "metrosCuadradosAHectareas" -> "metrosCuadrados"
            "piesCuadradosAMetrosCuadrados", "piesCuadradosAHectareas" -> "piesCuadrados"
            "hectareasAMetrosCuadrados", "hectareasAPiesCuadrados", "hectareasAAcres" -> "hectareas"
            "acresAHectareas" -> "acres"
            
            else -> "valor"
        }
    }
    
    /**
     * Envía la petición SOAP al servicio web
     */
    private fun enviarPeticionSOAP(soapEnvelope: String, method: String): String {
        val url = URL(BASE_URL)
        val connection = url.openConnection() as HttpURLConnection
        
        connection.requestMethod = "POST"
        connection.doOutput = true
        connection.setRequestProperty("Content-Type", "text/xml; charset=utf-8")
        connection.setRequestProperty("SOAPAction", "\"\"")  // Comillas vacías según WS-I BP
        connection.connectTimeout = TIMEOUT
        connection.readTimeout = TIMEOUT
        
        println("Conectando a: $BASE_URL")
        println("Método HTTP: POST")
        println("SOAPAction: \"\"")
        
        // Enviar el envelope SOAP
        OutputStreamWriter(connection.outputStream, StandardCharsets.UTF_8).use { writer ->
            writer.write(soapEnvelope)
            writer.flush()
            println("Envelope SOAP enviado")
        }
        
        // Verificar código de respuesta
        val responseCode = connection.responseCode
        println("Código de respuesta HTTP: $responseCode")
        
        if (responseCode != HttpURLConnection.HTTP_OK) {
            val errorResponse = StringBuilder()
            BufferedReader(InputStreamReader(connection.errorStream, StandardCharsets.UTF_8)).use { reader ->
                var line: String?
                while (reader.readLine().also { line = it } != null) {
                    errorResponse.append(line).append("\n")
                }
            }
            throw Exception("Error HTTP $responseCode: ${errorResponse.toString()}")
        }
        
        // Leer la respuesta
        val response = StringBuilder()
        BufferedReader(InputStreamReader(connection.inputStream, StandardCharsets.UTF_8)).use { reader ->
            var line: String?
            while (reader.readLine().also { line = it } != null) {
                response.append(line).append("\n")
            }
        }
        return response.toString()
    }
    
    /**
     * Parsea la respuesta SOAP para extraer el resultado
     */
    private fun parsearRespuestaSOAP(soapResponse: String, method: String): Double {
        val pattern = Pattern.compile("<return>(.*?)</return>")
        val matcher = pattern.matcher(soapResponse)
        
        return if (matcher.find()) {
            matcher.group(1)!!.toDouble()
        } else {
            throw Exception("No se pudo encontrar el resultado en la respuesta SOAP para el método: $method")
        }
    }
    
    /**
     * Determina las unidades de conversión basado en el método
     */
    private fun determinarUnidades(method: String): Array<String> {
        return when (method) {
            // Temperatura
            "celsiusAFahrenheit" -> arrayOf("celsius", "fahrenheit")
            "fahrenheitACelsius" -> arrayOf("fahrenheit", "celsius")
            "celsiusAKelvin" -> arrayOf("celsius", "kelvin")
            "kelvinACelsius" -> arrayOf("kelvin", "celsius")
            "fahrenheitAKelvin" -> arrayOf("fahrenheit", "kelvin")
            "kelvinAFahrenheit" -> arrayOf("kelvin", "fahrenheit")
            
            // Longitud
            "metrosAPies" -> arrayOf("metros", "pies")
            "piesAMetros" -> arrayOf("pies", "metros")
            "metrosAPulgadas" -> arrayOf("metros", "pulgadas")
            "pulgadasAMetros" -> arrayOf("pulgadas", "metros")
            "kilometrosAMillas" -> arrayOf("kilometros", "millas")
            "millasAKilometros" -> arrayOf("millas", "kilometros")
            
            // Peso
            "kilogramosALibras" -> arrayOf("kilogramos", "libras")
            "librasAKilogramos" -> arrayOf("libras", "kilogramos")
            "gramosAOnzas" -> arrayOf("gramos", "onzas")
            "onzasAGramos" -> arrayOf("onzas", "gramos")
            
            // Volumen
            "litrosAGalones" -> arrayOf("litros", "galones")
            "galonesALitros" -> arrayOf("galones", "litros")
            "mililitrosAOnzasFluidas" -> arrayOf("mililitros", "onzasFluidas")
            "onzasFluidasAMililitros" -> arrayOf("onzasFluidas", "mililitros")
            
            // Área
            "metrosCuadradosAPiesCuadrados" -> arrayOf("metrosCuadrados", "piesCuadrados")
            "piesCuadradosAMetrosCuadrados" -> arrayOf("piesCuadrados", "metrosCuadrados")
            "hectareasAAcres" -> arrayOf("hectareas", "acres")
            "acresAHectareas" -> arrayOf("acres", "hectareas")
            
            else -> arrayOf("unidad1", "unidad2")
        }
    }
    
    // ========== MÉTODOS DE TEMPERATURA ==========
    
    suspend fun celsiusAFahrenheit(celsius: Double): Conversion {
        return llamadaSOAP("celsiusAFahrenheit", celsius)
    }
    
    suspend fun fahrenheitACelsius(fahrenheit: Double): Conversion {
        return llamadaSOAP("fahrenheitACelsius", fahrenheit)
    }
    
    suspend fun celsiusAKelvin(celsius: Double): Conversion {
        return llamadaSOAP("celsiusAKelvin", celsius)
    }
    
    suspend fun kelvinACelsius(kelvin: Double): Conversion {
        return llamadaSOAP("kelvinACelsius", kelvin)
    }
    
    suspend fun fahrenheitAKelvin(fahrenheit: Double): Conversion {
        return llamadaSOAP("fahrenheitAKelvin", fahrenheit)
    }
    
    suspend fun kelvinAFahrenheit(kelvin: Double): Conversion {
        return llamadaSOAP("kelvinAFahrenheit", kelvin)
    }
    
    // ========== MÉTODOS DE LONGITUD ==========
    
    suspend fun metrosAPies(metros: Double): Conversion {
        return llamadaSOAP("metrosAPies", metros)
    }
    
    suspend fun piesAMetros(pies: Double): Conversion {
        return llamadaSOAP("piesAMetros", pies)
    }
    
    suspend fun metrosAPulgadas(metros: Double): Conversion {
        return llamadaSOAP("metrosAPulgadas", metros)
    }
    
    suspend fun pulgadasAMetros(pulgadas: Double): Conversion {
        return llamadaSOAP("pulgadasAMetros", pulgadas)
    }
    
    
    suspend fun kilometrosAMillas(kilometros: Double): Conversion {
        return llamadaSOAP("kilometrosAMillas", kilometros)
    }
    
    suspend fun millasAKilometros(millas: Double): Conversion {
        return llamadaSOAP("millasAKilometros", millas)
    }
    
    // ========== MÉTODOS DE PESO ==========
    
    suspend fun kilogramosALibras(kilogramos: Double): Conversion {
        return llamadaSOAP("kilogramosALibras", kilogramos)
    }
    
    suspend fun librasAKilogramos(libras: Double): Conversion {
        return llamadaSOAP("librasAKilogramos", libras)
    }
    
    
    suspend fun gramosAOnzas(gramos: Double): Conversion {
        return llamadaSOAP("gramosAOnzas", gramos)
    }
    
    suspend fun onzasAGramos(onzas: Double): Conversion {
        return llamadaSOAP("onzasAGramos", onzas)
    }
    
    // ========== MÉTODOS DE VOLUMEN ==========
    
    suspend fun litrosAGalones(litros: Double): Conversion {
        return llamadaSOAP("litrosAGalones", litros)
    }
    
    suspend fun galonesALitros(galones: Double): Conversion {
        return llamadaSOAP("galonesALitros", galones)
    }
    
    
    suspend fun mililitrosAOnzasFluidas(mililitros: Double): Conversion {
        return llamadaSOAP("mililitrosAOnzasFluidas", mililitros)
    }
    
    suspend fun onzasFluidasAMililitros(onzasFluidas: Double): Conversion {
        return llamadaSOAP("onzasFluidasAMililitros", onzasFluidas)
    }
    
    // ========== MÉTODOS DE ÁREA ==========
    
    suspend fun metrosCuadradosAPiesCuadrados(metrosCuadrados: Double): Conversion {
        return llamadaSOAP("metrosCuadradosAPiesCuadrados", metrosCuadrados)
    }
    
    suspend fun piesCuadradosAMetrosCuadrados(piesCuadrados: Double): Conversion {
        return llamadaSOAP("piesCuadradosAMetrosCuadrados", piesCuadrados)
    }
    
    
    suspend fun hectareasAAcres(hectareas: Double): Conversion {
        return llamadaSOAP("hectareasAAcres", hectareas)
    }
    
    suspend fun acresAHectareas(acres: Double): Conversion {
        return llamadaSOAP("acresAHectareas", acres)
    }
    
    /**
     * Conversión local como fallback cuando el servidor no está disponible
     */
    private fun conversionLocal(method: String, valor: Double): Double {
        return when (method) {
            // Temperatura
            "celsiusAFahrenheit" -> (valor * 9.0 / 5.0) + 32.0
            "fahrenheitACelsius" -> (valor - 32.0) * 5.0 / 9.0
            "celsiusAKelvin" -> valor + 273.15
            "kelvinACelsius" -> valor - 273.15
            "fahrenheitAKelvin" -> (valor - 32.0) * 5.0 / 9.0 + 273.15
            "kelvinAFahrenheit" -> (valor - 273.15) * 9.0 / 5.0 + 32.0
            
            // Longitud
            "metrosAPies" -> valor * 3.28084
            "piesAMetros" -> valor / 3.28084
            "metrosAPulgadas" -> valor * 39.3701
            "pulgadasAMetros" -> valor / 39.3701
            "kilometrosAMillas" -> valor * 0.621371
            "millasAKilometros" -> valor / 0.621371
            
            // Peso
            "kilogramosALibras" -> valor * 2.20462
            "librasAKilogramos" -> valor / 2.20462
            "gramosAOnzas" -> valor * 0.035274
            "onzasAGramos" -> valor / 0.035274
            
            // Volumen
            "litrosAGalones" -> valor * 0.264172
            "galonesALitros" -> valor / 0.264172
            "mililitrosAOnzasFluidas" -> valor * 0.033814
            "onzasFluidasAMililitros" -> valor / 0.033814
            
            // Área
            "metrosCuadradosAPiesCuadrados" -> valor * 10.7639
            "piesCuadradosAMetrosCuadrados" -> valor / 10.7639
            "hectareasAAcres" -> valor * 2.47105
            "acresAHectareas" -> valor / 2.47105
            
            else -> throw IllegalArgumentException("Método de conversión no soportado: $method")
        }
    }
}
