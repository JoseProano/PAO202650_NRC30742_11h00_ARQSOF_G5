namespace CONUNI_RESTFUL_DOTNET_GR9S.Models
{
    /// <summary>
    /// Modelo para las respuestas de conversión
    /// </summary>
    public class ConversionResponse
    {
        public double ValorOriginal { get; set; }
        public double ValorConvertido { get; set; }
        public string UnidadOrigen { get; set; }
        public string UnidadDestino { get; set; }
        public string Categoria { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public long Timestamp { get; set; }
        
        public ConversionResponse()
        {
            Timestamp = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        
        public ConversionResponse(double valorOriginal, double valorConvertido, 
                                string unidadOrigen, string unidadDestino, 
                                string categoria, bool exito, string mensaje)
        {
            ValorOriginal = valorOriginal;
            ValorConvertido = valorConvertido;
            UnidadOrigen = unidadOrigen;
            UnidadDestino = unidadDestino;
            Categoria = categoria;
            Exito = exito;
            Mensaje = mensaje;
            Timestamp = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        
        /// <summary>
        /// Método estático para crear respuesta exitosa
        /// </summary>
        public static ConversionResponse CrearExito(double valorOriginal, double valorConvertido,
                                                   string unidadOrigen, string unidadDestino, string categoria)
        {
            return new ConversionResponse(valorOriginal, valorConvertido, unidadOrigen, 
                                        unidadDestino, categoria, true, "Conversión exitosa");
        }
        
        /// <summary>
        /// Método estático para crear respuesta de error
        /// </summary>
        public static ConversionResponse CrearError(string mensaje)
        {
            var response = new ConversionResponse();
            response.Exito = false;
            response.Mensaje = mensaje;
            return response;
        }
    }
}


