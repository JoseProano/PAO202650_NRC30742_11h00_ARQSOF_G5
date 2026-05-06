package ec.edu.monster.modelo

import com.google.gson.annotations.SerializedName

/**
 * Modelo para respuestas de conversión RESTful
 */
data class ConversionResponse(
    @SerializedName("ValorOriginal")
    val valorOriginal: Double,
    @SerializedName("ValorConvertido")
    val valorConvertido: Double,
    @SerializedName("UnidadOrigen")
    val unidadOrigen: String,
    @SerializedName("UnidadDestino")
    val unidadDestino: String,
    @SerializedName("Categoria")
    val categoria: String,
    @SerializedName("Exito")
    val exito: Boolean,
    @SerializedName("Mensaje")
    val mensaje: String? = null,  // Campo del servidor .NET (mayúscula en JSON)
    @SerializedName("Timestamp")
    val timestamp: Long = 0
)

