using System;
using System.Runtime.Serialization;

namespace BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.modelo
{
    /// <summary>
    /// Modelo de datos para movimientos bancarios (igual al Java)
    /// </summary>
    [DataContract(Namespace = "http://tempuri.org/")]
    public class Movimiento
    {
        [DataMember(Order = 0)]
        public string Cuenta { get; set; }

        [DataMember(Order = 1)]
        public int NroMov { get; set; }

        [DataMember(Order = 2)]
        public DateTime Fecha { get; set; }

        [DataMember(Order = 3)]
        public string Tipo { get; set; }

        [DataMember(Order = 4)]
        public string Accion { get; set; }

        [DataMember(Order = 5)]
        public double Importe { get; set; }

        [DataMember(Order = 6)]
        public string Tipocodigo { get; set; }

        [DataMember(Order = 7)]
        public string Emplcodigo { get; set; }

        [DataMember(Order = 8)]
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
