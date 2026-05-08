using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using BDD_SOAP_DOTNET_CLICON_G09.ec.edu.monster.modelo;
using BDD_SOAP_DOTNET_CLICON_G09.WSEurekaReference;

namespace BDD_SOAP_DOTNET_CLICON_G09.ec.edu.monster.controlador
{
    public class CliCon_Controlador
    {
        private readonly WSEurekaClient servicio;

        public CliCon_Controlador()
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferSize = 2147483647;
            binding.ReaderQuotas.MaxDepth = 2147483647;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.ReaderQuotas.MaxArrayLength = 2147483647;
            binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            var endpoint = new EndpointAddress("http://10.183.38.246:51641/WSEureka.svc");
            servicio = new WSEurekaClient(binding, endpoint);
        }

        public List<CliCon_Movimiento> ObtenerMovimientos(string cuenta)
        {
            var movimientos = new List<CliCon_Movimiento>();
            try
            {
                // Escribir a archivo de log para debugging
                System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Consultando movimientos para cuenta: {cuenta}\n");
                
                WSEurekaReference.Movimiento[] datos = null;
                try
                {
                    System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Llamando a servicio.TraerMovimientos...\n");
                    datos = servicio.TraerMovimientos(cuenta);
                    System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Servicio retornó {datos?.Length ?? 0} movimientos\n");
                }
                catch (Exception exCall)
                {
                    System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] ERROR al llamar al servicio: {exCall.Message}\n");
                    System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Tipo: {exCall.GetType().Name}\n");
                    if (exCall.InnerException != null)
                    {
                        System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Inner: {exCall.InnerException.Message}\n");
                    }
                    throw;
                }

                if (datos != null && datos.Length > 0)
                {
                    System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Procesando {datos.Length} movimientos...\n");
                    int contador = 0;
                    foreach (var dato in datos)
                    {
                        try
                        {
                            contador++;
                            var movimiento = new CliCon_Movimiento
                            {
                                Cuenta = dato?.Cuenta ?? "",
                                NroMov = dato?.NroMov ?? 0,
                                Fecha = dato?.Fecha ?? DateTime.MinValue,
                                Tipo = dato?.Tipo ?? "",
                                Accion = dato?.Accion ?? "",
                                Importe = dato?.Importe ?? 0.0
                            };
                            movimientos.Add(movimiento);
                            System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Movimiento {contador} agregado: Cuenta={movimiento.Cuenta}, NroMov={movimiento.NroMov}\n");
                        }
                        catch (Exception exMov)
                        {
                            System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] ERROR al procesar movimiento {contador}: {exMov.Message}\n");
                        }
                    }
                }
                else
                {
                    System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] No hay datos o la lista está vacía\n");
                }
                
                System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Total de movimientos mapeados: {movimientos.Count}\n");
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] ERROR general: {ex.Message}\n");
                System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Stack: {ex.StackTrace}\n");
                if (ex.InnerException != null)
                {
                    System.IO.File.AppendAllText("debug.log", $"[{DateTime.Now}] Inner: {ex.InnerException.Message}\n");
                }
            }
            return movimientos;
        }

        public double ObtenerSaldo(string cuenta)
        {
            try
            {
                return servicio.TraerSaldoCuenta(cuenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener saldo: {ex.Message}");
                return 0.0;
            }
        }

        public void RegistrarDeposito(string cuenta, double importe)
        {
            try
            {
                string resultado = servicio.RegistrarDeposito(cuenta, importe, "0001");
                if (!resultado.Contains("exitosamente"))
                {
                    throw new Exception(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar depósito: {ex.Message}", ex);
            }
        }

        public void RegistrarRetiro(string cuenta, double importe)
        {
            try
            {
                string resultado = servicio.RegistrarRetiro(cuenta, importe, "0001");
                if (!resultado.Contains("exitosamente"))
                {
                    throw new Exception(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar el retiro: {ex.Message}", ex);
            }
        }

        public void RegistrarTransferencia(string cuentaOrigen, string cuentaDestino, double importe)
        {
            try
            {
                string resultado = servicio.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, "0001");
                if (!resultado.Contains("exitosamente"))
                {
                    throw new Exception(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar la transferencia: {ex.Message}", ex);
            }
        }
    }
}

