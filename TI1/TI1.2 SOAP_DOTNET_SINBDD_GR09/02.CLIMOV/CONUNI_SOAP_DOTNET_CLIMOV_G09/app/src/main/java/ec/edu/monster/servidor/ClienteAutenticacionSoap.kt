package ec.edu.monster.servidor

import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import org.ksoap2.SoapEnvelope
import org.ksoap2.serialization.SoapObject
import org.ksoap2.serialization.SoapPrimitive
import org.ksoap2.serialization.SoapSerializationEnvelope
import org.ksoap2.transport.HttpTransportSE

data class ResultadoLoginSoap(
    val autenticado: Boolean,
    val mensaje: String
)

object ClienteAutenticacionSoap {

    private const val NAMESPACE = "http://tempuri.org/"
    private const val SERVER_IP = "192.168.100.2"
    private const val SERVER_PORT = "62533"
    private const val URL = "http://$SERVER_IP:$SERVER_PORT/Service1.svc"
    private const val OPERATION = "Login"

    suspend fun login(usuario: String, contrasena: String): ResultadoLoginSoap = withContext(Dispatchers.IO) {
        val request = SoapObject(NAMESPACE, OPERATION).apply {
            addProperty("usuario", usuario)
            addProperty("contrasena", contrasena)
        }

        val envelope = SoapSerializationEnvelope(SoapEnvelope.VER11).apply {
            dotNet = true
            setOutputSoapObject(request)
        }

        val transport = HttpTransportSE(URL, 60000)

        try {
            transport.call("$NAMESPACE$OPERATION", envelope)
            val response = envelope.response as? SoapPrimitive
            val autenticado = response?.toString()?.trim()?.toBoolean() ?: false
            ResultadoLoginSoap(
                autenticado = autenticado,
                mensaje = if (autenticado) "Autenticación exitosa" else "Credenciales incorrectas"
            )
        } catch (e: Exception) {
            ResultadoLoginSoap(false, e.message ?: "No se pudo conectar al servidor SOAP")
        }
    }
}