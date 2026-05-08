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
import org.xmlpull.v1.XmlPullParser
import org.xmlpull.v1.XmlPullParserFactory
import java.io.StringReader
import java.net.SocketTimeoutException
import java.net.UnknownHostException
import java.text.SimpleDateFormat
import java.util.*
import java.util.concurrent.TimeUnit

class EurekaSoapService(private val context: Context? = null) {
    companion object {
        private const val TAG = "EurekaSoapService"
        private const val NAMESPACE = "http://servicios.monster.edu.ec/"
        private const val SOAP_ACTION_PREFIX = "http://servicios.monster.edu.ec/"
        private const val PREF_NAME = "EurekaServicePrefs"
        private const val KEY_SERVER_IP = "server_ip"
        private const val DEFAULT_IP = "10.183.38.246"
        private const val ALTERNATIVE_IP = "0.0.0.0" // Para cambiar cuando cambies de red
        private const val PORT = "8080"
        private const val SERVICE_PATH = "/WS_EUREKABANK_SERVICIO/EurekaService"
        private const val COD_EMP_DEFAULT = "0001"
        private const val CONNECT_TIMEOUT = 30L // 30 segundos
        private const val READ_TIMEOUT = 30L // 30 segundos
        private val SOAP_MEDIA_TYPE = "text/xml; charset=utf-8".toMediaType()
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
        val url = "http://$finalIp:$PORT$SERVICE_PATH"
        Log.d(TAG, "URL del servicio: $url")
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
    fun probarConectividad(callback: SoapCallback<String>) {
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
    
    /**
     * Verifica la conexión al servidor (útil para diagnóstico)
     */
    fun verificarConexion(callback: SoapCallback<Boolean>) {
        object : AsyncTask<Void, Void, Pair<Boolean?, String?>>() {
            override fun doInBackground(vararg params: Void?): Pair<Boolean?, String?> {
                try {
                    val baseUrl = getBaseUrl()
                    val request = Request.Builder()
                        .url(baseUrl)
                        .get()
                        .build()
                    
                    val response = client.newCall(request).execute()
                    return Pair(response.isSuccessful, null)
                } catch (e: Exception) {
                    return Pair(false, e.message)
                }
            }
            
            override fun onPostExecute(result: Pair<Boolean?, String?>) {
                if (result.first == true) {
                    callback.onSuccess(true)
                } else {
                    callback.onError(result.second ?: "Error desconocido")
                }
            }
        }.execute()
    }

    interface SoapCallback<T> {
        fun onSuccess(result: T)
        fun onError(error: String)
    }

    fun leerMovimientos(cuenta: String, callback: SoapCallback<List<Movimiento>>) {
        LeerMovimientosTask(this, callback).execute(cuenta)
    }

    fun registrarDeposito(cuenta: String, importe: Double, callback: SoapCallback<Boolean>) {
        RegistrarDepositoTask(this, callback).execute(cuenta, importe.toString())
    }

    fun registrarRetiro(cuenta: String, importe: Double, callback: SoapCallback<Boolean>) {
        RegistrarRetiroTask(this, callback).execute(cuenta, importe.toString())
    }

    fun registrarTransferencia(
        cuentaOrigen: String,
        cuentaDestino: String,
        importe: Double,
        callback: SoapCallback<Boolean>
    ) {
        RegistrarTransferenciaTask(this, callback).execute(cuentaOrigen, cuentaDestino, importe.toString())
    }

    private fun createSoapRequest(methodName: String, vararg params: Pair<String, Any>): String {
        // Formato SOAP 1.1 document/literal exacto que espera JAX-WS
        // El método tiene namespace, pero los parámetros pueden no tenerlo
        val soapBody = StringBuilder()
        soapBody.append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")
        soapBody.append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" ")
        soapBody.append("xmlns:ser=\"$NAMESPACE\">")
        soapBody.append("<soapenv:Header/>")
        soapBody.append("<soapenv:Body>")
        soapBody.append("<ser:$methodName>")
        
        // Los parámetros SIN el prefijo ser:, solo el nombre del elemento
        // JAX-WS puede esperar que los parámetros estén sin namespace
        params.forEach { (name, value) ->
            soapBody.append("<$name>")
            soapBody.append(escapeXml(value.toString()))
            soapBody.append("</$name>")
        }
        
        soapBody.append("</ser:$methodName>")
        soapBody.append("</soapenv:Body>")
        soapBody.append("</soapenv:Envelope>")
        
        val xml = soapBody.toString()
        Log.d(TAG, "SOAP Request generado: $xml")
        return xml
    }

    private fun escapeXml(text: String): String {
        return text.replace("&", "&amp;")
            .replace("<", "&lt;")
            .replace(">", "&gt;")
            .replace("\"", "&quot;")
            .replace("'", "&apos;")
    }

    private fun parseMovimientosResponse(xmlResponse: String): List<Movimiento> {
        val movimientos = mutableListOf<Movimiento>()
        try {
            Log.d(TAG, "Parseando respuesta XML completa: $xmlResponse")
            
            val factory = XmlPullParserFactory.newInstance()
            factory.isNamespaceAware = true
            val parser = factory.newPullParser()
            parser.setInput(StringReader(xmlResponse))

            var eventType = parser.eventType
            var currentMovimiento: Movimiento? = null
            var currentTag: String? = null
            var dentroDeResponse = false

            while (eventType != XmlPullParser.END_DOCUMENT) {
                when (eventType) {
                    XmlPullParser.START_TAG -> {
                        val tagName = parser.name
                        val namespace = parser.namespace
                        
                        Log.d(TAG, "START_TAG: $tagName (namespace: $namespace)")
                        
                        // Detectar si estamos dentro de leerMovimientosResponse
                        if (tagName == "leerMovimientosResponse" || tagName.endsWith("leerMovimientosResponse")) {
                            dentroDeResponse = true
                            Log.d(TAG, "Dentro de leerMovimientosResponse")
                        }
                        
                        // Buscar elemento movimiento dentro de leerMovimientosResponse
                        if (dentroDeResponse) {
                            // El elemento movimiento debe estar directamente dentro de leerMovimientosResponse
                            // Según @XmlRootElement(name="movimiento") en el modelo
                            if (tagName == "movimiento") {
                                if (currentMovimiento == null) {
                                    currentMovimiento = Movimiento()
                                    Log.d(TAG, "Nuevo movimiento encontrado")
                                }
                            } else if (currentMovimiento != null) {
                                // Si ya tenemos un movimiento, cualquier tag hijo es un campo del movimiento
                                currentTag = tagName
                                Log.d(TAG, "Campo de movimiento: $tagName")
                            }
                        }
                    }
                    XmlPullParser.TEXT -> {
                        if (currentMovimiento != null && currentTag != null) {
                            val text = parser.text.trim()
                            if (text.isNotEmpty()) {
                                Log.d(TAG, "TEXT para tag '$currentTag': $text")
                                when (currentTag) {
                                    "cuencodigo", "chr_cuencodigo" -> currentMovimiento.cuencodigo = text
                                    "movinumero", "int_movinumero" -> currentMovimiento.movinumero = text.toIntOrNull() ?: 0
                                    "movifecha", "dtt_movifecha" -> {
                                        try {
                                            // Intentar diferentes formatos de fecha
                                            val formats = listOf(
                                                SimpleDateFormat("yyyy-MM-dd", Locale.getDefault()),
                                                SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.getDefault()),
                                                SimpleDateFormat("dd/MM/yyyy", Locale.getDefault())
                                            )
                                            var parsed = false
                                            for (format in formats) {
                                                try {
                                                    currentMovimiento.movifecha = format.parse(text)
                                                    parsed = true
                                                    break
                                                } catch (e: Exception) {
                                                    // Continuar con el siguiente formato
                                                }
                                            }
                                            if (!parsed) {
                                                Log.w(TAG, "No se pudo parsear la fecha: $text")
                                            }
                                        } catch (e: Exception) {
                                            Log.e(TAG, "Error parseando fecha: $text", e)
                                        }
                                    }
                                    "emplcodigo", "chr_emplcodigo" -> currentMovimiento.emplcodigo = text
                                    "tipocodigo", "chr_tipocodigo" -> currentMovimiento.tipocodigo = text
                                    "moviimporte", "dec_moviimporte" -> currentMovimiento.moviimporte = text.toDoubleOrNull() ?: 0.0
                                    "cuenreferencia", "chr_cuenreferencia" -> currentMovimiento.cuenreferencia = text
                                }
                            }
                        }
                    }
                    XmlPullParser.END_TAG -> {
                        val tagName = parser.name
                        
                        // Finalizar movimiento cuando se cierra el tag <movimiento>
                        if (tagName == "movimiento" && currentMovimiento != null) {
                            // Verificar si el movimiento tiene al menos un campo con datos
                            if (currentMovimiento.cuencodigo.isNotEmpty() || 
                                currentMovimiento.movinumero > 0 ||
                                currentMovimiento.moviimporte > 0) {
                                movimientos.add(currentMovimiento)
                                Log.d(TAG, "Movimiento agregado. Total: ${movimientos.size}")
                            } else {
                                Log.d(TAG, "Movimiento descartado (sin datos)")
                            }
                            currentMovimiento = null
                        }
                        
                        // Salir de response
                        if (tagName == "leerMovimientosResponse" || tagName.endsWith("leerMovimientosResponse")) {
                            dentroDeResponse = false
                        }
                        
                        currentTag = null
                    }
                }
                eventType = parser.next()
            }
            
            Log.d(TAG, "Parseo completado. Total movimientos: ${movimientos.size}")
        } catch (e: Exception) {
            Log.e(TAG, "Error parseando respuesta XML", e)
            e.printStackTrace()
        }
        return movimientos
    }

    private fun executeSoapRequest(soapAction: String, soapBody: String, methodName: String = ""): String? {
        val baseUrl = getBaseUrl()
        try {
            Log.d(TAG, "=== INICIANDO PETICIÓN SOAP ===")
            Log.d(TAG, "URL: $baseUrl")
            Log.d(TAG, "SOAPAction: $soapAction")
            Log.d(TAG, "MethodName: $methodName")
            Log.d(TAG, "SOAP Body (primeros 500 chars): ${soapBody.take(500)}")
            
            val requestBody = soapBody.toRequestBody(SOAP_MEDIA_TYPE)
            val requestBuilder = Request.Builder()
                .url(baseUrl)
                .addHeader("Content-Type", "text/xml; charset=utf-8")
                .addHeader("Accept", "text/xml")
            
            // Para JAX-WS document/literal, el SOAPAction puede estar vacío según el WSDL
            // Pero algunos servidores lo requieren, probar sin SOAPAction primero
            // NO agregar SOAPAction para document/literal style
            Log.d(TAG, "No agregando SOAPAction (document/literal style)")
            
            val request = requestBuilder.post(requestBody).build()

            Log.d(TAG, "Ejecutando petición SOAP...")
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
            throw Exception("No se pudo conectar al servidor: $baseUrl\n\nError: $errorMsg\n\nVerifica:\n1. El servidor esté corriendo (EurekaServicePublisher)\n2. La IP sea correcta (actual: ${getCurrentServerIp()})\n3. Ambos dispositivos en la misma red WiFi\n4. Prueba desde navegador Android: $baseUrl?wsdl")
        } catch (e: Exception) {
            Log.e(TAG, "Error en petición SOAP", e)
            throw Exception("Error en la petición SOAP: ${e.message}\nURL: $baseUrl")
        }
    }

    private class LeerMovimientosTask(
        private val service: EurekaSoapService,
        private val callback: SoapCallback<List<Movimiento>>
    ) : AsyncTask<String, Void, Pair<List<Movimiento>?, String?>>() {
        override fun doInBackground(vararg params: String?): Pair<List<Movimiento>?, String?> {
            try {
                val cuenta = params[0] ?: return Pair(null, "Cuenta no válida")
                Log.d(TAG, "Consultando movimientos para cuenta: $cuenta")
                
                val methodName = "leerMovimientos"
                val soapAction = SOAP_ACTION_PREFIX + methodName

                val soapBody = service.createSoapRequest(methodName, "cuenta" to cuenta)
                val response = service.executeSoapRequest(soapAction, soapBody, methodName)
                    ?: return Pair(null, "Respuesta vacía del servidor")

                Log.d(TAG, "Respuesta recibida del servidor, parseando...")
                val movimientos = service.parseMovimientosResponse(response)
                Log.d(TAG, "Movimientos parseados: ${movimientos.size}")
                
                // Si no hay movimientos pero la respuesta no está vacía, puede ser que el servidor devolvió un array vacío
                if (movimientos.isEmpty() && response.contains("leerMovimientosResponse")) {
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
        private val service: EurekaSoapService,
        private val callback: SoapCallback<Boolean>
    ) : AsyncTask<String, Void, Pair<Boolean?, String?>>() {
        override fun doInBackground(vararg params: String?): Pair<Boolean?, String?> {
            try {
                val cuenta = params[0] ?: return Pair(false, "Cuenta no válida")
                val importe = params[1]?.toDoubleOrNull() ?: return Pair(false, "Importe no válido")
                val methodName = "registrarDeposito"
                val soapAction = SOAP_ACTION_PREFIX + methodName

                val soapBody = service.createSoapRequest(
                    methodName,
                    "cuenta" to cuenta,
                    "importe" to importe,
                    "codEmp" to COD_EMP_DEFAULT
                )

                service.executeSoapRequest(soapAction, soapBody, methodName)
                return Pair(true, null)
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
        private val service: EurekaSoapService,
        private val callback: SoapCallback<Boolean>
    ) : AsyncTask<String, Void, Pair<Boolean?, String?>>() {
        override fun doInBackground(vararg params: String?): Pair<Boolean?, String?> {
            try {
                val cuenta = params[0] ?: return Pair(false, "Cuenta no válida")
                val importe = params[1]?.toDoubleOrNull() ?: return Pair(false, "Importe no válido")
                val methodName = "registrarRetiro"
                val soapAction = SOAP_ACTION_PREFIX + methodName

                val soapBody = service.createSoapRequest(
                    methodName,
                    "cuenta" to cuenta,
                    "importe" to importe,
                    "codEmp" to COD_EMP_DEFAULT
                )

                service.executeSoapRequest(soapAction, soapBody, methodName)
                return Pair(true, null)
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
        private val service: EurekaSoapService,
        private val callback: SoapCallback<Boolean>
    ) : AsyncTask<String, Void, Pair<Boolean?, String?>>() {
        override fun doInBackground(vararg params: String?): Pair<Boolean?, String?> {
            try {
                val cuentaOrigen = params[0] ?: return Pair(false, "Cuenta origen no válida")
                val cuentaDestino = params[1] ?: return Pair(false, "Cuenta destino no válida")
                val importe = params[2]?.toDoubleOrNull() ?: return Pair(false, "Importe no válido")
                val methodName = "registrarTransferencia"
                val soapAction = SOAP_ACTION_PREFIX + methodName

                val soapBody = service.createSoapRequest(
                    methodName,
                    "cuentaOrigen" to cuentaOrigen,
                    "cuentaDestino" to cuentaDestino,
                    "importe" to importe,
                    "codEmp" to COD_EMP_DEFAULT
                )

                service.executeSoapRequest(soapAction, soapBody, methodName)
                return Pair(true, null)
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
