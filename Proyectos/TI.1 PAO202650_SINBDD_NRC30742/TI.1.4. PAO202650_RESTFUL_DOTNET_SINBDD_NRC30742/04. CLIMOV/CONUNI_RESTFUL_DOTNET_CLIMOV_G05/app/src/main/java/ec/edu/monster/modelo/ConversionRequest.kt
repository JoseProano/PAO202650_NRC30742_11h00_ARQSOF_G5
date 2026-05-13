package ec.edu.monster.modelo

/**
 * Modelo para peticiones de conversión RESTful
 */
data class ConversionRequest(
    val valor: Double,
    val unidadOrigen: String,
    val unidadDestino: String,
    val categoria: String
)


