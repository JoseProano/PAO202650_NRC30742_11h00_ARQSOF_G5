using System;
using System.Collections.Generic;
using BDD_RESTFUL_DOTNET_CLICON_G09.ec.edu.monster.modelo;
using BDD_RESTFUL_DOTNET_CLICON_G09.ec.edu.monster.servicio;

namespace BDD_RESTFUL_DOTNET_CLICON_G09.ec.edu.monster.controlador
{
    public class CliCon_Controlador
    {
        private readonly EurekaService servicio;

        public CliCon_Controlador()
        {
            servicio = new EurekaService();
        }

        public List<CliCon_Movimiento> ObtenerMovimientos(string cuenta)
        {
            var movimientos = new List<CliCon_Movimiento>();
            try
            {
                var datos = servicio.TraerMovimientos(cuenta);

                if (datos != null && datos.Count > 0)
                {
                    foreach (var dato in datos)
                    {
                        try
                        {
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
                        }
                        catch (Exception)
                        {
                            // Continuar con el siguiente movimiento si hay error
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Retornar lista vacía en caso de error
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
                int resultado = servicio.RegistrarDeposito(cuenta, importe, "0001");
                if (resultado != 1)
                {
                    throw new Exception("El depósito no se pudo realizar correctamente");
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
                int resultado = servicio.RegistrarRetiro(cuenta, importe, "0001");
                if (resultado != 1)
                {
                    throw new Exception("El retiro no se pudo realizar correctamente");
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
                int resultado = servicio.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, "0001");
                if (resultado != 1)
                {
                    throw new Exception("La transferencia no se pudo realizar correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar la transferencia: {ex.Message}", ex);
            }
        }
    }
}



