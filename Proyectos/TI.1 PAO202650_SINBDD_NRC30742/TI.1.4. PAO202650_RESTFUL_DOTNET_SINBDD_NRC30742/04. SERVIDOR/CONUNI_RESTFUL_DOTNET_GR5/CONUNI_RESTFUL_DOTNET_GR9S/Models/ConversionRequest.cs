namespace CONUNI_RESTFUL_DOTNET_GR9S.Models
{
    /// <summary>
    /// Modelo para las peticiones de conversión
    /// </summary>
    public class ConversionRequest
    {
        public double Valor { get; set; }
        public string UnidadOrigen { get; set; }
        public string UnidadDestino { get; set; }
        public string Categoria { get; set; }
        
        public ConversionRequest()
        {
        }
        
        public ConversionRequest(double valor, string unidadOrigen, string unidadDestino, string categoria)
        {
            Valor = valor;
            UnidadOrigen = unidadOrigen;
            UnidadDestino = unidadDestino;
            Categoria = categoria;
        }
    }
}


