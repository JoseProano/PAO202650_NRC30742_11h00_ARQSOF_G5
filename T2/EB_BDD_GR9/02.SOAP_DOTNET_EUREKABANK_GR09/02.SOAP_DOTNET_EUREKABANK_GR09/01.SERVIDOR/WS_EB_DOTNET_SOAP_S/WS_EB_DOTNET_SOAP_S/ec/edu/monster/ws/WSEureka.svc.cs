using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WS_EB_DOTNET_SOAP_S.ec.edu.monster.servicio;
using WS_EB_DOTNET_SOAP_S.ec.edu.monster.modelo;

namespace WS_EB_DOTNET_SOAP_S.ec.edu.monster.ws
{
    /// <summary>
    /// Implementación del servicio WCF EurekaBank
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WSEureka : IWSEureka
    {
        private readonly EurekaService servicio = new EurekaService();

        public string ValidarIngreso(string usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
                return "Error: Usuario o contraseña vacíos.";

            return servicio.ValidarIngreso(usuario, password) ? "Exitoso" : "Denegado";
        }

        public string ProbarConexionBD()
        {
            try
            {
                // Intentar leer el saldo de una cuenta de prueba para verificar la conexión
                servicio.LeerSaldoCuenta("00100001");
                return "Conexión exitosa a la base de datos.";
            }
            catch (Exception ex)
            {
                return $"Error al probar conexión: {ex.Message}";
            }
        }

        public Movimiento[] TraerMovimientos(string cuenta)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== TraerMovimientos - Inicio ===");
                System.Diagnostics.Debug.WriteLine($"Cuenta recibida: '{cuenta}' (Longitud: {cuenta?.Length ?? 0})");
                
                if (!EsCuentaValida(cuenta))
                {
                    System.Diagnostics.Debug.WriteLine($"Cuenta inválida: '{cuenta}'");
                    return new Movimiento[0];
                }

                System.Diagnostics.Debug.WriteLine($"Cuenta válida, llamando a LeerMovimientos...");
                var resultado = servicio.LeerMovimientos(cuenta);
                System.Diagnostics.Debug.WriteLine($"LeerMovimientos retornó {resultado?.Count ?? 0} movimientos");
                
                // Log detallado de cada movimiento
                if (resultado != null && resultado.Count > 0)
                {
                    for (int i = 0; i < resultado.Count; i++)
                    {
                        var mov = resultado[i];
                        System.Diagnostics.Debug.WriteLine($"Movimiento[{i}]: Cuenta={mov.Cuenta}, NroMov={mov.NroMov}, Tipo={mov.Tipo}, Importe={mov.Importe}");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"ADVERTENCIA: La lista de movimientos está vacía o es null");
                }
                
                System.Diagnostics.Debug.WriteLine($"=== TraerMovimientos - Fin ===");
                
                // Convertir lista a array para mejor serialización
                return resultado?.ToArray() ?? new Movimiento[0];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en TraerMovimientos: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // En lugar de lanzar la excepción, retornar array vacío para evitar problemas de serialización
                System.Diagnostics.Debug.WriteLine($"Retornando array vacío debido al error");
                return new Movimiento[0];
            }
        }

        public string RegistrarDeposito(string cuenta, double importe, string codEmp)
        {
            if (!EsCuentaValida(cuenta))
                return "Error: La cuenta debe tener 8 dígitos numéricos.";

            if (importe <= 0)
                return "Error: El importe debe ser mayor a 0.";

            if (string.IsNullOrWhiteSpace(codEmp))
                codEmp = "0001";

            try
            {
                servicio.RegistrarDeposito(cuenta, importe, codEmp);
                return "Depósito registrado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string RegistrarRetiro(string cuenta, double importe, string codEmp)
        {
            if (!EsCuentaValida(cuenta))
                return "Error: La cuenta debe tener 8 dígitos numéricos.";

            if (importe <= 0)
                return "Error: El importe debe ser mayor a 0.";

            if (string.IsNullOrWhiteSpace(codEmp))
                codEmp = "0001";

            try
            {
                servicio.RegistrarRetiro(cuenta, importe, codEmp);
                return "Retiro registrado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string RegistrarTransferencia(string cuentaOrigen, string cuentaDestino, double importe, string codEmp)
        {
            if (!EsCuentaValida(cuentaOrigen) || !EsCuentaValida(cuentaDestino))
                return "Error: Ambas cuentas deben contener exactamente 8 dígitos numéricos.";

            if (cuentaOrigen == cuentaDestino)
                return "Error: La cuenta origen y destino no pueden ser iguales.";

            if (importe <= 0)
                return "Error: El importe debe ser mayor a 0.";

            if (string.IsNullOrWhiteSpace(codEmp))
                codEmp = "0001";

            try
            {
                servicio.RegistrarTransferencia(cuentaOrigen, cuentaDestino, importe, codEmp);
                return "Transferencia registrada exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public double TraerSaldoCuenta(string cuenta)
        {
            if (!EsCuentaValida(cuenta))
                return 0;

            return servicio.LeerSaldoCuenta(cuenta);
        }

        /// <summary>
        /// Valida que una cuenta tenga el formato correcto (8 dígitos)
        /// </summary>
        private bool EsCuentaValida(string cuenta)
        {
            return !string.IsNullOrWhiteSpace(cuenta)
                   && cuenta.Length == 8
                   && cuenta.All(char.IsDigit);
        }
    }
}

