package ec.edu.monster.servicio

import android.content.Context
import android.content.SharedPreferences
import android.os.AsyncTask
import android.util.Log
import ec.edu.monster.modelo.Movimiento
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.RequestBody.Companion.toRequestBody
import okhttp3.logging.HttpLoggingInterceptor
import org.json.JSONArray
import org.json.JSONObject
import java.net.SocketTimeoutException
import java.net.UnknownHostException
import java.text.SimpleDateFormat
import java.util.*
import java.util.concurrent.TimeUnit

class EurekaRestService(private val context: Context? = null) {
    companion object {
        private const val TAG = "EurekaRestService"
        private const val PREF_NAME = "EurekaServicePrefs"
        private const val KEY_SERVER_IP = "server_ip"
        private const val DEFAULT_IP = "10.183.38.246"
        private const val ALTERNATIVE_IP = "0.0.0.0" // Para cambiar cuando cambies de red
        private const val PORT = "44385" // Puerto del servicio .NET RESTful
        private const val BASE_PATH = "/api/corebancario"
        private const val CONNECT_TIMEOUT = 30L // 30 segundos
        private const val READ_TIMEOUT = 30L // 30 segundos
        private val JSON_MEDIA_TYPE = "application/json; charset=utf-8".toMediaType()
    }

    private val client: OkHttpClient by lazy {
        val loggingInterceptor = HttpLoggingInterceptor { message ->
            Log.d(TAG, "OkHttp: $message")
        }.apply {
            level = HttpLoggingInterceptor.Level.BODY
        }
        
        OkHttpClient.Builder()
            .connectTimeout(CONNECT_TIMEOUT, TimeUnit.SECONDS)
            .readTimeout(READ_TIMEOUT, TimeUnit.SECONDS)
            .writeTimeout(READ_TIMEOUT, TimeUnit.SECONDS)
            .retryOnConnectionFailure(true)
            .addInterceptor(loggingInterceptor)
            .build()
    }
    
    private fun getServerIp(): String {
        return if (context != null) {
            val prefs: SharedPreferences = context.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE)
            prefs.getString(KEY_SERVER_IP, DEFAULT_IP) ?: DEFAULT_IP
        } else {
            DEFAULT_IP
        }
    }
    
    private fun getBaseUrl(): String {
        val ip = getServerIp()
        // Si la IP es 0.0.0.0, usar la IP por defecto
        val finalIp = if (ip == ALTERNATIVE_IP) DEFAULT_IP else ip
        // Construir URL sin espacios ni caracteres especiales
        val url = "http://$finalIp:$PORT$BASE_PATH"
        Log.d(TAG, "URL del servicio REST: $url")
        Log.d(TAG, "IP final: $finalIp, Puerto: $PORT")
        return url
    }
    
    fun setServerIp(ip: String) {
        context?.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE)
            ?.edit()
            ?.putString(KEY_SERVER_IP, ip)
            ?.apply()
    }
    
    fun getCurrentServerIp(): String = getServerIp()
    
    fun getCurrentServiceUrl(): String = getBaseUrl()
    
    /**
     * Prueba la conectividad básica al servidor
     */
    fun probarConectividad(callback: RestCallback<String>) {
        object : AsyncTask<Void, Void, Pair<String?, String?>>() {
            override fun doInBackground(vararg params: Void?): Pair<String?, String?> {
                try {
                    val baseUrl = getBaseUrl()
                    val testUrl = "http://${getServerIp()}:$PORT"
                    
                    Log.d(TAG, "Probando conectividad a: $testUrl")
                    
                    val request = Request.Builder()
                        .url(testUrl)
                        .get()
                        .build()
                    
                    val response = client.newCall(request).execute()
                    return Pair("HTTP ${response.code}", null)
                } catch (e: Exception) {
                    return Pair(null, "${e.javaClass.simpleName}: ${e.message}")
                }
            }
            
            override fun onPostExecute(result: Pair<String?, String?>) {
                if (result.first != null) {
                    callback.onSuccess(result.first!!)
                } else {
                    callback.onError(result.second ?: "Error desconocido")
                }
            }
        }.execute()
    }

    interface RestCallback<T> {
        fun onSuccess(result: T)
        fun onError(error: String)
    }

    fun leerMovimientos(cuenta: String, callback: RestCallback<List<Movimiento>>) {
        LeerMovimientosTask(this, callback).execute(cuenta)
    }

    fun registrarDeposito(cuenta: String, importe: Double, callback: RestCallback<Boolean>) {
        RegistrarDepositoTask(this, callback).execute(cuenta, importe.toString())
    }

    fun registrarRetiro(cuenta: String, importe: Double, callback: RestCallback<Boolean>) {
        RegistrarRetiroTask(this, callback).execute(cuenta, importe.toString())
    }

    fun registrarTransferencia(
        cuentaOrigen: String,
        cuentaDestino: String,
        importe: Double,
        callback: RestCallback<Boolean>
    ) {
        RegistrarTransferenciaTask(this, callback).execute(cuentaOrigen, cuentaDestino, importe.toString())
    }

    private fun executeRestRequest(url: String, method: String = "GET", body: RequestBody? = null): String? {
        val baseUrl = getBaseUrl()
        try {
            Log.d(TAG, "=== INICIANDO PETICIÓN REST ===")
            Log.d(TAG, "URL: $url")
            Log.d(TAG, "Method: $method")
            
            val requestBuilder = Request.Builder()
                .url(url)
                .addHeader("Accept", "application/json")
            
            // Para métodos POST, PUT, etc., OkHttp requiere un body (incluso si está vacío)
            val finalBody = if (method == "GET" || method == "DELETE") {
                body // GET y DELETE pueden no tener body
            } else {
                // Para POST, PUT, etc., usar el body proporcionado o crear uno vacío
                body ?: "{}".toRequestBody(JSON_MEDIA_TYPE)
            }
            
            if (finalBody != null) {
                requestBuilder.addHeader("Content-Type", "application/json")
                requestBuilder.method(method, finalBody)
            } else {
                requestBuilder.method(method, null)
            }
            
            val request = requestBuilder.build()

            Log.d(TAG, "Ejecutando petición REST...")
            Log.d(TAG, "Request URL: ${request.url}")
            Log.d(TAG, "Request Headers: ${request.headers}")
            
            val response = client.newCall(request).execute()
            Log.d(TAG, "Respuesta recibida: HTTP ${response.code}")
            
            if (!response.isSuccessful) {
                val errorBody = response.body?.string() ?: "Sin detalles"
                Log.e(TAG, "Error HTTP ${response.code}: $errorBody")
                throw Exception("Error HTTP ${response.code}: ${response.message}")
            }
            
            val responseBody = response.body?.string()
            Log.d(TAG, "Respuesta completa del servidor: $responseBody")
            Log.d(TAG, "Longitud de respuesta: ${responseBody?.length ?: 0} caracteres")
            return responseBody
            
        } catch (e: SocketTimeoutException) {
            Log.e(TAG, "Timeout de conexión", e)
            throw Exception("Timeout: No se pudo conectar al servidor en $baseUrl. Verifica que el servidor esté corriendo y accesible.")
        } catch (e: UnknownHostException) {
            Log.e(TAG, "Host desconocido", e)
            throw Exception("Host no encontrado: $baseUrl. Verifica la IP del servidor y que estés en la misma red.")
        } catch (e: java.net.ConnectException) {
            Log.e(TAG, "Error de conexión", e)
            val errorMsg = e.message ?: "Sin detalles"
            Log.e(TAG, "Detalles del error: $errorMsg")
            Log.e(TAG, "Stack trace completo:", e)
            throw Exception("No se pudo conectar al servidor: $baseUrl\n\nError: $errorMsg\n\nVerifica:\n1. El servidor esté corriendo\n2. La IP sea correcta (actual: ${getCurrentServerIp()})\n3. Ambos dispositivos en la misma red WiFi\n4. Prueba desde navegador Android: $baseUrl")
        } catch (e: Exception) {
            Log.e(TAG, "Error en petición REST", e)
            throw Exception("Error en la petición REST: ${e.message}\nURL: $baseUrl")
        }
    }

    private fun parseMovimientosResponse(jsonResponse: String): List<Movimiento> {
        val movimientos = mutableListOf<Movimiento>()
        try {
            Log.d(TAG, "Parseando respuesta JSON completa: $jsonResponse")
            
            val jsonArray = JSONArray(jsonResponse)
            
            for (i in 0 until jsonArray.length()) {
                val jsonObject = jsonArray.getJSONObject(i)
                val movimiento = Movimiento()
                
                // Mapear campos del JSON del servidor .NET a los campos del modelo Android
                // El servidor .NET devuelve: Cuenta, NroMov, Fecha, Tipo, Importe, Referencia, Accion, Tipocodigo, Emplcodigo
                // El modelo Android espera: cuencodigo, movinumero, movifecha, tipocodigo, moviimporte, cuenreferencia
                
                movimiento.cuencodigo = jsonObject.optString("Cuenta", jsonObject.optString("cuenta", ""))
                movimiento.movinumero = jsonObject.optInt("NroMov", jsonObject.optInt("nroMov", 0))
                movimiento.tipocodigo = jsonObject.optString("Tipocodigo", jsonObject.optString("tipocodigo", jsonObject.optString("Tipo", "")))
                movimiento.moviimporte = jsonObject.optDouble("Importe", jsonObject.optDouble("importe", 0.0))
                movimiento.cuenreferencia = jsonObject.optString("Referencia", jsonObject.optString("referencia", ""))
                movimiento.emplcodigo = jsonObject.optString("Emplcodigo", jsonObject.optString("emplcodigo", ""))
                
                // Parsear fecha - el servidor .NET devuelve "Fecha" en formato ISO
                val fechaStr = jsonObject.optString("Fecha", "")
                if (fechaStr.isNotEmpty()) {
                    try {
                        // Remover la 'Z' al final si existe
                        val fechaClean = fechaStr.replace("Z", "").trim()
                        
                        // Intentar diferentes formatos de fecha
                        val formats = listOf(
                            SimpleDateFormat("yyyy-MM-dd", Locale.getDefault()),
                            SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.getDefault()),
                            SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.getDefault()),
                            SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS", Locale.getDefault()),
                            SimpleDateFormat("dd/MM/yyyy", Locale.getDefault())
                        )
                        var parsed = false
                        for (format in formats) {
                            try {
                                movimiento.movifecha = format.parse(fechaClean)
                                parsed = true
                                Log.d(TAG, "Fecha parseada correctamente: $fechaClean -> ${movimiento.movifecha}")
                                break
                            } catch (e: Exception) {
                                // Continuar con el siguiente formato
                            }
                        }
                        if (!parsed) {
                            Log.w(TAG, "No se pudo parsear la fecha: $fechaStr")
                        }
                    } catch (e: Exception) {
                        Log.e(TAG, "Error parseando fecha: $fechaStr", e)
                    }
                }
                
                movimientos.add(movimiento)
                Log.d(TAG, "Movimiento agregado: cuenta=${movimiento.cuencodigo}, nro=${movimiento.movinumero}, importe=${movimiento.moviimporte}, tipo=${movimiento.tipocodigo}, fecha=${movimiento.movifecha}")
            }
            
            Log.d(TAG, "Parseo completado. Total movimientos: ${movimientos.size}")
        } catch (e: Exception) {
            Log.e(TAG, "Error parseando respuesta JSON", e)
            e.printStackTrace()
        }
        return movimientos
    }

    private class LeerMovimientosTask(
        private val service: EurekaRestService,
        private val callback: RestCallback<List<Movimiento>>
    ) : AsyncTask<String, Void, Pair<List<Movimiento>?, String?>>() {
        override fun doInBackground(vararg params: String?): Pair<List<Movimiento>?, String?> {
            try {
                val cuenta = params[0] ?: return Pair(null, "Cuenta no válida")
                Log.d(TAG, "Consultando movimientos para cuenta: $cuenta")
                
                val baseUrl = service.getBaseUrl()
                val url = "$baseUrl/movimientos/$cuenta"
                
                val response = service.executeRestRequest(url, "GET")
                    ?: return Pair(null, "Respuesta vacía del servidor")

                Log.d(TAG, "Respuesta recibida del servidor, parseando...")
                val movimientos = service.parseMovimientosResponse(response)
                Log.d(TAG, "Movimientos parseados: ${movimientos.size}")
                
                if (movimientos.isEmpty() && response.isNotEmpty()) {
                    Log.w(TAG, "El servidor devolvió una respuesta válida pero sin movimientos. Verifica que la cuenta $cuenta tenga movimientos en la base de datos.")
                }
                
                return Pair(movimientos, null)
            } catch (e: Exception) {
                Log.e(TAG, "Error en LeerMovimientosTask", e)
                return Pair(null, e.message ?: "Error desconocido")
            }
        }

        override fun onPostExecute(result: Pair<List<Movimiento>?, String?>) {
            if (result.first != null) {
                callback.onSuccess(result.first!!)
            } else {
                callback.onError(result.second ?: "Error desconocido")
            }
        }
    }

    private class RegistrarDepositoTask(
        private val service: EurekaRestService,
        private val callback: RestCallback<Boolean>
    ) : AsyncTask<String, Void, Pair<Boolean?, String?>>() {
        override fun doInBackground(vararg params: String?): Pair<Boolean?, String?> {
            try {
                val cuenta = params[0] ?: return Pair(false, "Cuenta no válida")
                val importe = params[1]?.toDoubleOrNull() ?: return Pair(false, "Importe no válido")
                
                val baseUrl = service.getBaseUrl()
                val url = "$baseUrl/deposito?cuenta=$cuenta&importe=$importe"
                
                val response = service.executeRestRequest(url, "POST")
                
                // Parsear respuesta JSON {"estado": 1 o -1}
                val jsonResponse = JSONObject(response ?: "{}")
                val estado = jsonResponse.optInt("estado", -1)
                
                return Pair(estado == 1, null)
            } catch (e: Exception) {
                return Pair(false, e.message ?: "Error desconocido")
            }
        }

        override fun onPostExecute(result: Pair<Boolean?, String?>) {
            if (result.first == true) {
                callback.onSuccess(true)
            } else {
                callback.onError(result.second ?: "Error desconocido")
            }
        }
    }

    private class RegistrarRetiroTask(
        private val service: EurekaRestService,
        private val callback: RestCallback<Boolean>
    ) : AsyncTask<String, Void, Pair<Boolean?, String?>>() {
        override fun doInBackground(vararg params: String?): Pair<Boolean?, String?> {
            try {
                val cuenta = params[0] ?: return Pair(false, "Cuenta no válida")
                val importe = params[1]?.toDoubleOrNull() ?: return Pair(false, "Importe no válido")
                
                val baseUrl = service.getBaseUrl()
                val url = "$baseUrl/retiro?cuenta=$cuenta&importe=$importe"
                
                val response = service.executeRestRequest(url, "POST")
                
                // Parsear respuesta JSON {"estado": 1 o -1}
                val jsonResponse = JSONObject(response ?: "{}")
                val estado = jsonResponse.optInt("estado", -1)
                
                return Pair(estado == 1, null)
            } catch (e: Exception) {
                return Pair(false, e.message ?: "Error desconocido")
            }
        }

        override fun onPostExecute(result: Pair<Boolean?, String?>) {
            if (result.first == true) {
                callback.onSuccess(true)
            } else {
                callback.onError(result.second ?: "Error desconocido")
            }
        }
    }

    private class RegistrarTransferenciaTask(
        private val service: EurekaRestService,
        private val callback: RestCallback<Boolean>
    ) : AsyncTask<String, Void, Pair<Boolean?, String?>>() {
        override fun doInBackground(vararg params: String?): Pair<Boolean?, String?> {
            try {
                val cuentaOrigen = params[0] ?: return Pair(false, "Cuenta origen no válida")
                val cuentaDestino = params[1] ?: return Pair(false, "Cuenta destino no válida")
                val importe = params[2]?.toDoubleOrNull() ?: return Pair(false, "Importe no válido")
                
                val baseUrl = service.getBaseUrl()
                val url = "$baseUrl/transferencia?cuentaOrigen=$cuentaOrigen&cuentaDestino=$cuentaDestino&importe=$importe"
                
                val response = service.executeRestRequest(url, "POST")
                
                // Parsear respuesta JSON {"estado": 1 o -1}
                val jsonResponse = JSONObject(response ?: "{}")
                val estado = jsonResponse.optInt("estado", -1)
                
                return Pair(estado == 1, null)
            } catch (e: Exception) {
                return Pair(false, e.message ?: "Error desconocido")
            }
        }

        override fun onPostExecute(result: Pair<Boolean?, String?>) {
            if (result.first == true) {
                callback.onSuccess(true)
            } else {
                callback.onError(result.second ?: "Error desconocido")
            }
        }
    }
}

