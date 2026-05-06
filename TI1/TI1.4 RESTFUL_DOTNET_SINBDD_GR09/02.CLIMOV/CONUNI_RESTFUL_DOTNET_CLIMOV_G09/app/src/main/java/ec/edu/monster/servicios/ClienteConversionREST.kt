package ec.edu.monster.servicios

import android.util.Log
import ec.edu.monster.modelo.Conversion
import ec.edu.monster.modelo.ConversionRequest
import ec.edu.monster.modelo.ConversionResponse
import com.google.gson.Gson
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.RequestBody.Companion.toRequestBody
import java.security.cert.X509Certificate
import java.util.concurrent.TimeUnit
import javax.net.ssl.SSLContext
import javax.net.ssl.TrustManager
import javax.net.ssl.X509TrustManager

/**
 * Cliente RESTful para consumir el servicio de conversiones
 * Maneja la comunicación HTTP con el servidor RESTful .NET
 */
class ClienteConversionREST {
    
    companion object {
        // ============================================
        // CONFIGURACIÓN DE URL DEL SERVIDOR
        // ============================================
        // Configura la IP de tu PC en la red local
        private const val SERVER_IP = "10.92.232.246"
        private const val HTTP_PORT = "8086"
        private const val HTTPS_PORT = "8086"
        private const val USE_HTTPS = false
        
        // URL base del servicio
        private val BASE_URL = if (USE_HTTPS) {
            "https://$SERVER_IP:$HTTPS_PORT/api/conversion"
        } else {
            "http://$SERVER_IP:$HTTP_PORT/api/conversion"
        }
        
        // Para emulador Android: usar http://10.0.2.2:8086/api/conversion
        
        private const val TIMEOUT = 30000L // 30 segundos
        private val JSON_MEDIA_TYPE = "application/json; charset=utf-8".toMediaType()
    }
    
    private val client = if (USE_HTTPS) {
        createUnsafeOkHttpClient() // Cliente HTTPS que ignora certificados
    } else {
        createHttpClient() // Cliente HTTP simple
    }
    
    /**
     * Crea un cliente OkHttp simple para HTTP (sin SSL)
     */
    private fun createHttpClient(): OkHttpClient {
        return OkHttpClient.Builder()
            .connectTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
            .readTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
            .writeTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
            .build()
    }
    
    /**
     * Crea un cliente OkHttp que ignora certificados SSL (solo para desarrollo)
     * NO usar en producción
     */
    private fun createUnsafeOkHttpClient(): OkHttpClient {
        val trustAllCerts = arrayOf<TrustManager>(object : X509TrustManager {
            override fun checkClientTrusted(chain: Array<out X509Certificate>?, authType: String?) {}
            override fun checkServerTrusted(chain: Array<out X509Certificate>?, authType: String?) {}
            override fun getAcceptedIssuers(): Array<X509Certificate> = arrayOf()
        })
        
        val sslContext = SSLContext.getInstance("SSL")
        sslContext.init(null, trustAllCerts, java.security.SecureRandom())
        val sslSocketFactory = sslContext.socketFactory
        
        return OkHttpClient.Builder()
            .sslSocketFactory(sslSocketFactory, trustAllCerts[0] as X509TrustManager)
            .hostnameVerifier { _, _ -> true } // Aceptar cualquier hostname
            .connectTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
            .readTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
            .writeTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
            .build()
    }
    
    private val gson = Gson()
    
    /**
     * Realiza una conversión genérica usando POST /api/conversion/convertir
     */
    suspend fun convertir(request: ConversionRequest): Conversion = withContext(Dispatchers.IO) {
        val conversion = Conversion()
        conversion.valorOriginal = request.valor
        conversion.operacion = "${request.unidadOrigen}To${request.unidadDestino}"
        conversion.tipoConversion = request.categoria
        
        val url = "$BASE_URL/convertir"
        Log.d("ClienteConversionREST", "Intentando conectar a: $url")
        Log.d("ClienteConversionREST", "Request: ${gson.toJson(request)}")
        
        try {
            val requestBody = gson.toJson(request).toRequestBody(JSON_MEDIA_TYPE)
            val httpRequest = Request.Builder()
                .url(url)
                .post(requestBody)
                .addHeader("Content-Type", "application/json")
                .build()
            
            Log.d("ClienteConversionREST", "Enviando petición HTTP...")
            val response = client.newCall(httpRequest).execute()
            Log.d("ClienteConversionREST", "Respuesta recibida: ${response.code} - ${response.message}")
            
            if (response.isSuccessful) {
                val responseBody = response.body?.string() ?: throw Exception("Respuesta vacía")
                Log.d("ClienteConversionREST", "Response body: $responseBody")
                val conversionResponse = gson.fromJson(responseBody, ConversionResponse::class.java)
                
                if (conversionResponse.exito) {
                    conversion.valorOriginal = conversionResponse.valorOriginal
                    conversion.valorConvertido = conversionResponse.valorConvertido
                    conversion.unidadOriginal = conversionResponse.unidadOrigen
                    conversion.unidadConvertida = conversionResponse.unidadDestino
                    conversion.exitosa = true
                    conversion.mensajeError = conversionResponse.mensaje ?: ""
                    Log.d("ClienteConversionREST", "Conversión exitosa: ${conversion.valorOriginal} ${conversion.unidadOriginal} = ${conversion.valorConvertido} ${conversion.unidadConvertida}")
                } else {
                    conversion.exitosa = false
                    conversion.mensajeError = conversionResponse.mensaje ?: "Error desconocido"
                    Log.e("ClienteConversionREST", "Error en respuesta: ${conversionResponse.mensaje}")
                }
            } else {
                conversion.exitosa = false
                val errorBody = response.body?.string() ?: "Sin detalles"
                conversion.mensajeError = "Error HTTP ${response.code}: ${response.message}"
                Log.e("ClienteConversionREST", "Error HTTP ${response.code}: ${response.message}")
                Log.e("ClienteConversionREST", "Error body: $errorBody")
            }
        } catch (e: java.net.ConnectException) {
            conversion.exitosa = false
            conversion.mensajeError = "No se puede conectar al servidor. Verifica que esté corriendo en $url"
            Log.e("ClienteConversionREST", "Error de conexión: ${e.message}", e)
            Log.e("ClienteConversionREST", "Stack trace: ${e.stackTraceToString()}")
        } catch (e: java.net.SocketTimeoutException) {
            conversion.exitosa = false
            conversion.mensajeError = "Timeout al conectar. El servidor no responde en $url"
            Log.e("ClienteConversionREST", "Timeout: ${e.message}", e)
        } catch (e: Exception) {
            conversion.exitosa = false
            conversion.mensajeError = "Error de conexión: ${e.message}"
            Log.e("ClienteConversionREST", "Error general: ${e.javaClass.simpleName} - ${e.message}", e)
            Log.e("ClienteConversionREST", "Stack trace completo: ${e.stackTraceToString()}")
        }
        
        conversion
    }
    
    // ========== MÉTODOS DE TEMPERATURA ==========
    
    suspend fun celsiusAFahrenheit(celsius: Double): Conversion {
        return convertir(ConversionRequest(celsius, "celsius", "fahrenheit", "temperatura"))
    }
    
    suspend fun fahrenheitACelsius(fahrenheit: Double): Conversion {
        return convertir(ConversionRequest(fahrenheit, "fahrenheit", "celsius", "temperatura"))
    }
    
    suspend fun celsiusAKelvin(celsius: Double): Conversion {
        return convertir(ConversionRequest(celsius, "celsius", "kelvin", "temperatura"))
    }
    
    suspend fun kelvinACelsius(kelvin: Double): Conversion {
        return convertir(ConversionRequest(kelvin, "kelvin", "celsius", "temperatura"))
    }
    
    suspend fun fahrenheitAKelvin(fahrenheit: Double): Conversion {
        return convertir(ConversionRequest(fahrenheit, "fahrenheit", "kelvin", "temperatura"))
    }
    
    suspend fun kelvinAFahrenheit(kelvin: Double): Conversion {
        return convertir(ConversionRequest(kelvin, "kelvin", "fahrenheit", "temperatura"))
    }
    
    // ========== MÉTODOS DE LONGITUD ==========
    
    suspend fun metrosAPies(metros: Double): Conversion {
        return convertir(ConversionRequest(metros, "metros", "pies", "longitud"))
    }
    
    suspend fun piesAMetros(pies: Double): Conversion {
        return convertir(ConversionRequest(pies, "pies", "metros", "longitud"))
    }
    
    suspend fun metrosAPulgadas(metros: Double): Conversion {
        return convertir(ConversionRequest(metros, "metros", "pulgadas", "longitud"))
    }
    
    suspend fun pulgadasAMetros(pulgadas: Double): Conversion {
        return convertir(ConversionRequest(pulgadas, "pulgadas", "metros", "longitud"))
    }
    
    suspend fun kilometrosAMillas(kilometros: Double): Conversion {
        return convertir(ConversionRequest(kilometros, "kilometros", "millas", "longitud"))
    }
    
    suspend fun millasAKilometros(millas: Double): Conversion {
        return convertir(ConversionRequest(millas, "millas", "kilometros", "longitud"))
    }
    
    // ========== MÉTODOS DE PESO ==========
    
    suspend fun kilogramosALibras(kilogramos: Double): Conversion {
        return convertir(ConversionRequest(kilogramos, "kilogramos", "libras", "peso"))
    }
    
    suspend fun librasAKilogramos(libras: Double): Conversion {
        return convertir(ConversionRequest(libras, "libras", "kilogramos", "peso"))
    }
    
    suspend fun gramosAOnzas(gramos: Double): Conversion {
        return convertir(ConversionRequest(gramos, "gramos", "onzas", "peso"))
    }
    
    suspend fun onzasAGramos(onzas: Double): Conversion {
        return convertir(ConversionRequest(onzas, "onzas", "gramos", "peso"))
    }
    
    // ========== MÉTODOS DE VOLUMEN ==========
    
    suspend fun litrosAGalones(litros: Double): Conversion {
        return convertir(ConversionRequest(litros, "litros", "galones", "volumen"))
    }
    
    suspend fun galonesALitros(galones: Double): Conversion {
        return convertir(ConversionRequest(galones, "galones", "litros", "volumen"))
    }
    
    suspend fun mililitrosAOnzasFluidas(mililitros: Double): Conversion {
        return convertir(ConversionRequest(mililitros, "mililitros", "onzasFluidas", "volumen"))
    }
    
    suspend fun onzasFluidasAMililitros(onzasFluidas: Double): Conversion {
        return convertir(ConversionRequest(onzasFluidas, "onzasFluidas", "mililitros", "volumen"))
    }
    
    // ========== MÉTODOS DE ÁREA ==========
    
    suspend fun metrosCuadradosAPiesCuadrados(metrosCuadrados: Double): Conversion {
        return convertir(ConversionRequest(metrosCuadrados, "metrosCuadrados", "piesCuadrados", "area"))
    }
    
    suspend fun piesCuadradosAMetrosCuadrados(piesCuadrados: Double): Conversion {
        return convertir(ConversionRequest(piesCuadrados, "piesCuadrados", "metrosCuadrados", "area"))
    }
    
    suspend fun hectareasAAcres(hectareas: Double): Conversion {
        return convertir(ConversionRequest(hectareas, "hectareas", "acres", "area"))
    }
    
    suspend fun acresAHectareas(acres: Double): Conversion {
        return convertir(ConversionRequest(acres, "acres", "hectareas", "area"))
    }
}

