using System;
using System.Collections.Generic;
using System.ServiceModel;
using WS_EB_DOTNET_SOAP_S.ec.edu.monster.modelo;

namespace WS_EB_DOTNET_SOAP_S.ec.edu.monster.ws
{
    /// <summary>
    /// Interfaz del servicio WCF EurekaBank
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
}

