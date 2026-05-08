using System;
using System.Runtime.Serialization;

namespace WS_EB_DOTNET_SOAP_S.ec.edu.monster.modelo
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
    }
}

