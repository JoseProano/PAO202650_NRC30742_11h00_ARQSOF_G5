namespace CONUNI_RESTFUL_DOTNET_GR9S.Models
{
    /// <summary>
    /// Modelo de datos para movimientos bancarios
    /// </summary>
    public class Movimiento
    {
        public string Cuenta { get; set; } = string.Empty;
        public int NroMov { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public double Importe { get; set; }
        public string Tipocodigo { get; set; } = string.Empty;
        public string Emplcodigo { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
    }
}

