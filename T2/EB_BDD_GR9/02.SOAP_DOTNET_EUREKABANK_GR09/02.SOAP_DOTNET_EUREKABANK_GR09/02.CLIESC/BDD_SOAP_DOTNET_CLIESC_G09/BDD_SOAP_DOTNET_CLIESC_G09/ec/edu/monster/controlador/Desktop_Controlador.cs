using System.Collections.Generic;
using BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.modelo;
using BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.servicio;

namespace BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.controlador
{
    /// <summary>
    /// Controlador que actúa como intermediario entre la vista y el servicio
    /// </summary>
    public class Desktop_Controlador
    {
        private readonly EurekaService service;

        public Desktop_Controlador()
        {
            this.service = new EurekaService();
        }

        /// <summary>
        /// Obtiene los movimientos de una cuenta
        /// </summary>
        public List<Movimiento> TraerMovimientos(string cuenta)
        {
            return service.TraerMovimientos(cuenta);
        }

        /// <summary>
        /// Registra un depósito
        /// </summary>
        public int RegDeposito(string cuenta, double importe)
        {
            return service.RegDeposito(cuenta, importe);
        }

        /// <summary>
        /// Registra un retiro
        /// </summary>
        public int RegRetiro(string cuenta, double importe)
        {
            return service.RegRetiro(cuenta, importe);
        }

        /// <summary>
        /// Registra una transferencia
        /// </summary>
        public int RegTransferencia(string cuentaOrigen, string cuentaDestino, double importe)
        {
            return service.RegTransferencia(cuentaOrigen, cuentaDestino, importe);
        }

        /// <summary>
        /// Valida el ingreso de un usuario
        /// </summary>
        public string ValidarIngreso(string usuario, string password)
        {
            return service.ValidarIngreso(usuario, password);
        }
    }
}

