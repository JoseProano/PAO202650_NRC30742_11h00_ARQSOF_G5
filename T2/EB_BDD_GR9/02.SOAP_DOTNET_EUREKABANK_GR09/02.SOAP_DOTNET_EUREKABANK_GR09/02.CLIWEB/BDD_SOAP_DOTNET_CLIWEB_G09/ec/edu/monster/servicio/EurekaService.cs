using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BDD_SOAP_DOTNET_CLIWEB_G09.ec.edu.monster.modelo;

namespace BDD_SOAP_DOTNET_CLIWEB_G09.ec.edu.monster.servicio
{
    /// <summary>
    /// Cliente SOAP para el servicio EurekaBank
    /// </summary>
    public class EurekaService
    {
        // URL del servicio SOAP - ajustar según corresponda
        private static readonly string SERVICE_URL = "http://10.183.38.246:51641/WSEureka.svc";
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
            catch (Exception ex)
            {
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
        /// Registra un depósito
        /// </summary>
        public string RegistrarDeposito(string cuenta, double importe)
        {
            ChannelFactory<IWSEureka> channelFactory = null;
            try
            {
                var binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.None;
                var endpoint = new EndpointAddress(SERVICE_URL);
                channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                string resultado = client.RegistrarDeposito(cuenta, importe, COD_EMP_DEFAULT);
                
                if (channelFactory.State == CommunicationState.Opened)
                {
                    channelFactory.Close();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar depósito: {ex.Message}", ex);
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
        /// Registra un retiro
        /// </summary>
        public string RegistrarRetiro(string cuenta, double importe)
        {
            ChannelFactory<IWSEureka> channelFactory = null;
            try
            {
                var binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.None;
                var endpoint = new EndpointAddress(SERVICE_URL);
                channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                string resultado = client.RegistrarRetiro(cuenta, importe, COD_EMP_DEFAULT);
                
                if (channelFactory.State == CommunicationState.Opened)
                {
                    channelFactory.Close();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar retiro: {ex.Message}", ex);
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
        /// Registra una transferencia
        /// </summary>
        public string RegistrarTransferencia(string cuentaOrigen, string cuentaDestino, double importe)
        {
            ChannelFactory<IWSEureka> channelFactory = null;
            try
            {
                var binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.None;
                var endpoint = new EndpointAddress(SERVICE_URL);
                channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                string resultado = client.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, COD_EMP_DEFAULT);
                
                if (channelFactory.State == CommunicationState.Opened)
                {
                    channelFactory.Close();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar transferencia: {ex.Message}", ex);
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
        /// Valida el ingreso de un usuario
        /// </summary>
        public string ValidarIngreso(string usuario, string password)
        {
            ChannelFactory<IWSEureka> channelFactory = null;
            try
            {
                var binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.None;
                var endpoint = new EndpointAddress(SERVICE_URL);
                channelFactory = new ChannelFactory<IWSEureka>(binding, endpoint);
                var client = channelFactory.CreateChannel();

                string resultado = client.ValidarIngreso(usuario, password);
                
                if (channelFactory.State == CommunicationState.Opened)
                {
                    channelFactory.Close();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar ingreso: {ex.Message}", ex);
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
    }

    /// <summary>
    /// Interfaz del servicio WCF
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

