using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.modelo;

namespace BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.servicio
{
    /// <summary>
    /// Cliente SOAP para el servicio EurekaBank
    /// </summary>
    public class EurekaService
    {
        // URL del servicio SOAP (ajustar según tu configuración)
        // Por defecto IIS Express usa un puerto dinámico, ajustar según corresponda
        private static readonly string SERVICE_URL = "http://10.183.38.246:51641/WSEureka.svc";
        
        // Código de empleado por defecto para operaciones
        private static readonly string COD_EMP_DEFAULT = "0001";

        /// <summary>
        /// Obtiene los movimientos de una cuenta
        /// </summary>
        public List<Movimiento> TraerMovimientos(string cuenta)
        {
            ChannelFactory<IWSEureka> channelFactory = null;
            try
            {
                var binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = 2147483647;
                binding.MaxBufferSize = 2147483647;
                binding.ReaderQuotas.MaxDepth = 2147483647;
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;
                binding.ReaderQuotas.MaxArrayLength = 2147483647;
                binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                binding.Security.Mode = BasicHttpSecurityMode.None;

                var endpoint = new EndpointAddress(SERVICE_URL);
                channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                Movimiento[] movimientos = client.TraerMovimientos(cuenta);
                
                if (channelFactory.State == CommunicationState.Opened)
                {
                    channelFactory.Close();
                }

                if (movimientos != null)
                {
                    return new List<Movimiento>(movimientos);
                }
                return new List<Movimiento>();
            }
            catch (EndpointNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al consultar movimientos: {ex.Message}");
                throw new Exception($"No se pudo conectar al servicio SOAP en {SERVICE_URL}. " +
                    $"Verifique que el servicio esté corriendo y que la URL sea correcta. " +
                    $"Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al consultar movimientos: {ex.Message}");
                throw new Exception($"Error al consultar movimientos: {ex.Message}", ex);
            }
            finally
            {
                if (channelFactory != null && channelFactory.State != CommunicationState.Closed)
                {
                    try
                    {
                        channelFactory.Abort();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Registra un depósito en una cuenta
        /// </summary>
        public int RegDeposito(string cuenta, double importe)
        {
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(SERVICE_URL);
                var channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                string resultado = client.RegistrarDeposito(cuenta, importe, COD_EMP_DEFAULT);
                channelFactory.Close();

                // El servicio retorna "OK" o un mensaje de error
                return resultado.Contains("OK") || resultado.Contains("exitoso") ? 1 : 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al registrar depósito: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Registra un retiro de una cuenta
        /// </summary>
        public int RegRetiro(string cuenta, double importe)
        {
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(SERVICE_URL);
                var channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                string resultado = client.RegistrarRetiro(cuenta, importe, COD_EMP_DEFAULT);
                channelFactory.Close();

                return resultado.Contains("OK") || resultado.Contains("exitoso") ? 1 : 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al registrar retiro: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Registra una transferencia entre cuentas
        /// </summary>
        public int RegTransferencia(string cuentaOrigen, string cuentaDestino, double importe)
        {
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(SERVICE_URL);
                var channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                string resultado = client.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, COD_EMP_DEFAULT);
                channelFactory.Close();

                return resultado.Contains("OK") || resultado.Contains("exitoso") ? 1 : 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al registrar transferencia: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Valida el ingreso de un usuario
        /// </summary>
        public string ValidarIngreso(string usuario, string password)
        {
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(SERVICE_URL);
                var channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                string resultado = client.ValidarIngreso(usuario, password);
                channelFactory.Close();

                return resultado;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al validar ingreso: {ex.Message}");
                return "Error";
            }
        }
    }

    /// <summary>
    /// Interfaz del servicio WCF (debe coincidir con IWSEureka del servidor)
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

