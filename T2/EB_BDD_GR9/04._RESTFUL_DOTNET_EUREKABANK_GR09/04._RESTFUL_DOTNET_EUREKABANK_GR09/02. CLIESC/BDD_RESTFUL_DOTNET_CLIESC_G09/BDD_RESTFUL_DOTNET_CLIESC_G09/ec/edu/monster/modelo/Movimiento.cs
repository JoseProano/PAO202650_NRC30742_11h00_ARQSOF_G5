using System;

namespace BDD_RESTFUL_DOTNET_CLIESC_G09.ec.edu.monster.modelo
{
    /// <summary>
    /// Modelo de datos para movimientos bancarios (igual al Java)
    /// </summary>
    public class Movimiento
    {
        public string Cuenta { get; set; }
        public int NroMov { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Accion { get; set; }
        public double Importe { get; set; }
        public string Tipocodigo { get; set; }
        public string Emplcodigo { get; set; }
        public string Referencia { get; set; }

        // Propiedades para compatibilidad con el código Java
        public string getCuencodigo() => Cuenta;
        public int getMovinumero() => NroMov;
        public DateTime getMovifecha() => Fecha;
        public string getTipocodigo() => Tipocodigo;
        public string getAccion()
        {
            // Si Accion ya está establecida, retornarla
            if (!string.IsNullOrEmpty(Accion))
                return Accion;
            
            // Si no, calcularla basándose en tipocodigo (igual que en Java)
            if (Tipocodigo != null)
            {
                switch (Tipocodigo)
                {
                    case "001": // Apertura
                    case "003": // Depósito
                    case "005": // Interés
                    case "008": // Transferencia entrada
                        return "INGRESO";
                    case "002": // Cancelar
                    case "004": // Retiro
                    case "006": // Mantenimiento
                    case "007": // ITF
                    case "009": // Transferencia salida
                    case "010": // Cargo por movimiento
                        return "SALIDA";
                    default:
                        return "N/A";
                }
            }
            return "N/A";
        }
        public double getMoviimporte() => Importe;
    }
}



