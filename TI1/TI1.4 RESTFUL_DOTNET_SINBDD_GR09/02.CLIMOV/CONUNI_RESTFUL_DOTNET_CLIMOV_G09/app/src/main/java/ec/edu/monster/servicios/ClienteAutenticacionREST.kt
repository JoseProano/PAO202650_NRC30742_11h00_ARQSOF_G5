package ec.edu.monster.servicios

import com.google.gson.Gson
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.RequestBody.Companion.toRequestBody
import java.util.concurrent.TimeUnit

data class ResultadoLoginREST(
    val autenticado: Boolean,
    val mensaje: String
)

object ClienteAutenticacionREST {

    private const val SERVER_IP = "192.168.100.2"
    private const val HTTP_PORT = "44385"
    private const val BASE_URL = "http://$SERVER_IP:$HTTP_PORT/api/auth"
    private const val TIMEOUT = 30000L
    private val JSON_MEDIA_TYPE = "application/json; charset=utf-8".toMediaType()

    private val client: OkHttpClient = OkHttpClient.Builder()
        .connectTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
        .readTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
        .writeTimeout(TIMEOUT, TimeUnit.MILLISECONDS)
        .build()

    private val gson = Gson()

    suspend fun login(usuario: String, contrasena: String): ResultadoLoginREST = withContext(Dispatchers.IO) {
        val requestJson = gson.toJson(
            mapOf(
                "usuario" to usuario,
                "contrasena" to contrasena
            )
        )

        val request = Request.Builder()
            .url("$BASE_URL/login")
            .post(requestJson.toRequestBody(JSON_MEDIA_TYPE))
            .addHeader("Content-Type", "application/json")
            .build()

        try {
            client.newCall(request).execute().use { response ->
                val responseBody = response.body?.string().orEmpty()
                val parsed = if (responseBody.isNotBlank()) {
                    gson.fromJson(responseBody, LoginResponseDto::class.java)
                } else {
                    null
                }

                when {
                    response.isSuccessful && parsed?.autenticado == true -> {
                        ResultadoLoginREST(true, parsed.mensaje ?: "Autenticación exitosa")
                    }
                    response.code == 401 -> {
                        ResultadoLoginREST(false, parsed?.mensaje ?: "Credenciales incorrectas")
                    }
                    else -> {
                        ResultadoLoginREST(false, parsed?.mensaje ?: "Error HTTP ${response.code}")
                    }
                }
            }
        } catch (e: Exception) {
            ResultadoLoginREST(false, "No se pudo conectar al servidor: ${e.message}")
        }
    }

    private data class LoginResponseDto(
        val autenticado: Boolean = false,
        val mensaje: String? = null
    )
}