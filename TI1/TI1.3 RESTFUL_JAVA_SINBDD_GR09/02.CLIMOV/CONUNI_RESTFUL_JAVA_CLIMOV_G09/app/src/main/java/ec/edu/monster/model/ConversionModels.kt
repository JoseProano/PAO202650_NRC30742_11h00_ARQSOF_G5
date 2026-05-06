package ec.edu.monster.model

data class ConversionRequest(
    val valor: Double,
    val unidadOrigen: String,
    val unidadDestino: String,
    val categoria: String
)

data class ConversionResponse(
    val exito: Boolean? = null,
    val valorConvertido: Double? = null,
    val resultado: Double? = null,
    val mensaje: String? = null
)



