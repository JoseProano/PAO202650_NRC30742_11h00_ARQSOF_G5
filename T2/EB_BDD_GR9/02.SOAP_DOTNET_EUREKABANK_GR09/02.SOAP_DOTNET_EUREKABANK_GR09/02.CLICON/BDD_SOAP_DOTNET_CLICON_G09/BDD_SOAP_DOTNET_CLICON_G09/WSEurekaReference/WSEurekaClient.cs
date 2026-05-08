using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace BDD_SOAP_DOTNET_CLICON_G09.WSEurekaReference
{
    /// <summary>
    /// Cliente proxy para el servicio WCF EurekaBank
    /// </summary>
    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface IWSEureka
    {
        [OperationContract]
        string ValidarIngreso(string usuario, string password);

        [OperationContract]
        string ProbarConexionBD();

        [OperationContract]
        Movimiento[] TraerMovimientos(string cuenta);

        [OperationContract]
        string RegistrarDeposito(string cuenta, double importe, string codEmp);

        [OperationContract]
        string RegistrarRetiro(string cuenta, double importe, string codEmp);

        [OperationContract]
        string RegistrarTransferencia(string cuentaOrigen, string cuentaDestino, double importe, string codEmp);

        [OperationContract]
        double TraerSaldoCuenta(string cuenta);
    }

    /// <summary>
    /// DTO para recibir datos del servicio WCF
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

    /// <summary>
    /// Cliente WCF generado manualmente
    /// </summary>
    public class WSEurekaClient : ClientBase<IWSEureka>, IWSEureka
    {
        public WSEurekaClient(BasicHttpBinding binding, EndpointAddress remoteAddress) 
            : base(binding, remoteAddress)
        {
        }

        public string ValidarIngreso(string usuario, string password)
        {
            return Channel.ValidarIngreso(usuario, password);
        }

        public string ProbarConexionBD()
        {
            return Channel.ProbarConexionBD();
        }

        public Movimiento[] TraerMovimientos(string cuenta)
        {
            return Channel.TraerMovimientos(cuenta);
        }

        public string RegistrarDeposito(string cuenta, double importe, string codEmp)
        {
            return Channel.RegistrarDeposito(cuenta, importe, codEmp);
        }

        public string RegistrarRetiro(string cuenta, double importe, string codEmp)
        {
            return Channel.RegistrarRetiro(cuenta, importe, codEmp);
        }

        public string RegistrarTransferencia(string cuentaOrigen, string cuentaDestino, double importe, string codEmp)
        {
            return Channel.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, codEmp);
        }

        public double TraerSaldoCuenta(string cuenta)
        {
            return Channel.TraerSaldoCuenta(cuenta);
        }
    }
}

