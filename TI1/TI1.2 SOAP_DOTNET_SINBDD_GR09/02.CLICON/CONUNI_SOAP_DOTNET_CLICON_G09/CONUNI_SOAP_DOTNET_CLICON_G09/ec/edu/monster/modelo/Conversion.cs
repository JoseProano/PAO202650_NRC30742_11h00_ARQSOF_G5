using System;

namespace ec.edu.monster.modelo
{
    /// <summary>
    /// Modelo para representar una conversión
    /// Replicado desde Java - Patrón MVC
    /// </summary>
    public class Conversion
    {
        public string TipoConversion { get; set; }
        public string Operacion { get; set; }
        public double ValorOriginal { get; set; }
        public double ValorConvertido { get; set; }
        public string UnidadOriginal { get; set; }
        public string UnidadDestino { get; set; }
        public bool Exitosa { get; set; }
        public string MensajeError { get; set; }

        // Constructores
        public Conversion()
        {
        }

        public Conversion(string tipoConversion, string operacion, double valorOriginal, 
                         string unidadOriginal, string unidadDestino)
        {
            TipoConversion = tipoConversion;
            Operacion = operacion;
            ValorOriginal = valorOriginal;
            UnidadOriginal = unidadOriginal;
            UnidadDestino = unidadDestino;
            Exitosa = false;
        }

        public override string ToString()
        {
            if (Exitosa)
            {
                return $"{TipoConversion}: {ValorOriginal:F4} {UnidadOriginal} = {ValorConvertido:F4} {UnidadDestino}";
            }
            else
            {
                return $"Error en {TipoConversion}: {MensajeError}";
            }
        }
    }
}


