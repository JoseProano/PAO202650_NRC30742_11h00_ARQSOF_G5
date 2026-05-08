using System;
using System.Runtime.Serialization;

namespace BDD_RESTFUL_DOTNET_CLIWEB_G09.ec.edu.monster.modelo
{
    /// <summary>
    /// Modelo de datos para movimientos bancarios
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

        // Propiedades para compatibilidad
        public string getCuencodigo() => Cuenta;
        public int getMovinumero() => NroMov;
        public DateTime getMovifecha() => Fecha;
        public string getTipocodigo() => Tipocodigo;
        public string getAccion()
        {
            if (!string.IsNullOrEmpty(Accion))
                return Accion;
            
            if (Tipocodigo != null)
            {
                switch (Tipocodigo)
                {
                    case "001": case "003": case "005": case "008":
                        return "INGRESO";
                    case "002": case "004": case "006": case "007": case "009": case "010":
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



