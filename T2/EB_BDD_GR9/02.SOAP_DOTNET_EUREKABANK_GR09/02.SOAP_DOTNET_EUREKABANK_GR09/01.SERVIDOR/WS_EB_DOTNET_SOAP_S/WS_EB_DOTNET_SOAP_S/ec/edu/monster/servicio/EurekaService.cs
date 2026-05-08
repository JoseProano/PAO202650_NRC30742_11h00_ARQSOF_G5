using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using WS_EB_DOTNET_SOAP_S.ec.edu.monster.modelo;

namespace WS_EB_DOTNET_SOAP_S.ec.edu.monster.servicio
{
    /// <summary>
    /// Servicio que contiene la lógica de negocio para EurekaBank
    /// </summary>
    public class EurekaService
    {
        private readonly string connectionString;

        public EurekaService()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["EUREKABANK"].ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("La cadena de conexión 'EUREKABANK' no está configurada en Web.config");
                }
                
                // Intentar probar la conexión al inicializar
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        System.Diagnostics.Debug.WriteLine($"Conexión a base de datos exitosa: {connectionString}");
                    }
                }
                catch (Exception connEx)
                {
                    System.Diagnostics.Debug.WriteLine($"ADVERTENCIA: No se pudo conectar a la base de datos: {connEx.Message}");
                    System.Diagnostics.Debug.WriteLine($"Cadena de conexión usada: {connectionString}");
                    System.Diagnostics.Debug.WriteLine($"Sugerencia: Verifica que SQL Server esté instalado y corriendo.");
                    System.Diagnostics.Debug.WriteLine($"Opciones:");
                    System.Diagnostics.Debug.WriteLine($"1. SQL Server Express: Server=.\\SQLEXPRESS");
                    System.Diagnostics.Debug.WriteLine($"2. SQL Server LocalDB: Server=(localdb)\\MSSQLLocalDB");
                    System.Diagnostics.Debug.WriteLine($"3. SQL Server por nombre: Server=localhost o Server=nombre_instancia");
                    // No lanzar excepción aquí, solo registrar el error
                    // La conexión se intentará nuevamente cuando se use
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar la cadena de conexión: {ex.Message}", ex);
            }
        }

        private const string USUARIO = "MONSTER";
        private const string PASSWORD_HASH = "6C3F6757E773775FD059E2F025BD14BA"; // MD5 de "MONSTER9"

        /// <summary>
        /// Valida el ingreso de un usuario
        /// </summary>
        public bool ValidarIngreso(string usuario, string password)
        {
            string hashIngresado = CrearHashMD5(password);
            return USUARIO.Equals(usuario, StringComparison.OrdinalIgnoreCase) && 
                   PASSWORD_HASH.Equals(hashIngresado, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Crea un hash MD5 de una contraseña
        /// </summary>
        public static string CrearHashMD5(string password)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Lee los movimientos de una cuenta
        /// </summary>
        public List<Movimiento> LeerMovimientos(string cuenta)
        {
            var movimientos = new List<Movimiento>();
            try
            {
                // Limpiar espacios en blanco
                cuenta = cuenta?.Trim() ?? string.Empty;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Consulta simple y directa - exactamente como funciona en SQL Server
                    var query = @"
                        SELECT 
                            m.chr_cuencodigo,
                            m.int_movinumero,
                            m.dtt_movifecha,
                            m.chr_tipocodigo,
                            ISNULL(t.vch_tipodescripcion, '') AS vch_tipodescripcion,
                            ISNULL(t.vch_tipoaccion, '') AS vch_tipoaccion,
                            CAST(m.dec_moviimporte AS FLOAT) AS dec_moviimporte,
                            m.chr_emplcodigo,
                            m.chr_cuenreferencia
                        FROM dbo.Movimiento m
                        LEFT JOIN dbo.TipoMovimiento t ON t.chr_tipocodigo = m.chr_tipocodigo
                        WHERE m.chr_cuencodigo = @Cuenta
                        ORDER BY m.int_movinumero DESC";

                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Cuenta", cuenta);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                var movimiento = new Movimiento
                                {
                                    Cuenta = reader["chr_cuencodigo"] != DBNull.Value ? reader["chr_cuencodigo"].ToString().Trim() : string.Empty,
                                    NroMov = reader["int_movinumero"] != DBNull.Value ? Convert.ToInt32(reader["int_movinumero"]) : 0,
                                    Fecha = reader["dtt_movifecha"] != DBNull.Value ? Convert.ToDateTime(reader["dtt_movifecha"]) : DateTime.MinValue,
                                    Tipocodigo = reader["chr_tipocodigo"] != DBNull.Value ? reader["chr_tipocodigo"].ToString().Trim() : string.Empty,
                                    Tipo = reader["vch_tipodescripcion"] != DBNull.Value ? reader["vch_tipodescripcion"].ToString().Trim() : string.Empty,
                                    Accion = reader["vch_tipoaccion"] != DBNull.Value ? reader["vch_tipoaccion"].ToString().Trim() : string.Empty,
                                    Importe = reader["dec_moviimporte"] != DBNull.Value ? Convert.ToDouble(reader["dec_moviimporte"]) : 0.0,
                                    Emplcodigo = reader["chr_emplcodigo"] != DBNull.Value ? reader["chr_emplcodigo"].ToString().Trim() : string.Empty,
                                    Referencia = reader["chr_cuenreferencia"] != DBNull.Value ? reader["chr_cuenreferencia"].ToString().Trim() : string.Empty
                                };
                                movimientos.Add(movimiento);
                            }
                            catch (Exception exMov)
                            {
                                // Continuar con el siguiente movimiento si hay error
                                continue;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
            return movimientos;
        }

        /// <summary>
        /// Lee el saldo de una cuenta
        /// </summary>
        public double LeerSaldoCuenta(string cuenta)
        {
            double saldo = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT dec_cuensaldo FROM dbo.cuenta WHERE chr_cuencodigo = @Cuenta";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Cuenta", cuenta);
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    saldo = Convert.ToDouble(result);
            }
            return saldo;
        }

        /// <summary>
        /// Registra un depósito en una cuenta
        /// </summary>
        public void RegistrarDeposito(string cuenta, double importe, string codEmp)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Paso 1: Leer datos de la cuenta (WITH (UPDLOCK) para bloqueo pesimista)
                        var querySelect = @"
                            SELECT dec_cuensaldo, int_cuencontmov 
                            FROM dbo.cuenta WITH (UPDLOCK)
                            WHERE chr_cuencodigo = @Cuenta AND vch_cuenestado = 'ACTIVO'";
                        var selectCommand = new SqlCommand(querySelect, connection, transaction);
                        selectCommand.Parameters.AddWithValue("@Cuenta", cuenta);

                        double saldo;
                        int cont;

                        using (var reader = selectCommand.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("ERROR, cuenta no existe, o no está activa");

                            saldo = Convert.ToDouble(reader["dec_cuensaldo"]);
                            cont = Convert.ToInt32(reader["int_cuencontmov"]);
                        }

                        // Paso 2: Obtener el siguiente número de movimiento
                        var queryMaxMov = @"
                            SELECT COALESCE(MAX(int_movinumero), 0) + 1 AS siguiente_numero 
                            FROM dbo.movimiento 
                            WHERE chr_cuencodigo = @Cuenta";
                        var maxCommand = new SqlCommand(queryMaxMov, connection, transaction);
                        maxCommand.Parameters.AddWithValue("@Cuenta", cuenta);
                        var maxResult = maxCommand.ExecuteScalar();
                        int siguienteNumero = 1;
                        if (maxResult != null && maxResult != DBNull.Value)
                        {
                            siguienteNumero = Convert.ToInt32(maxResult);
                        }

                        // Paso 3: Actualizar la cuenta
                        saldo += importe;
                        cont++;
                        var queryUpdate = @"
                            UPDATE dbo.cuenta
                            SET dec_cuensaldo = @Saldo, int_cuencontmov = @Cont
                            WHERE chr_cuencodigo = @Cuenta AND vch_cuenestado = 'ACTIVO'";
                        var updateCommand = new SqlCommand(queryUpdate, connection, transaction);
                        updateCommand.Parameters.AddWithValue("@Saldo", saldo);
                        updateCommand.Parameters.AddWithValue("@Cont", cont);
                        updateCommand.Parameters.AddWithValue("@Cuenta", cuenta);
                        updateCommand.ExecuteNonQuery();

                        // Paso 4: Registrar movimiento (tipo '003' depósito)
                        var queryInsert = @"
                            INSERT INTO dbo.movimiento (chr_cuencodigo, int_movinumero, dtt_movifecha, chr_emplcodigo, chr_tipocodigo, dec_moviimporte)
                            VALUES (@Cuenta, @NroMov, GETDATE(), @CodEmp, '003', @Importe)";
                        var insertCommand = new SqlCommand(queryInsert, connection, transaction);
                        insertCommand.Parameters.AddWithValue("@Cuenta", cuenta);
                        insertCommand.Parameters.AddWithValue("@NroMov", siguienteNumero);
                        insertCommand.Parameters.AddWithValue("@CodEmp", codEmp);
                        insertCommand.Parameters.AddWithValue("@Importe", importe);
                        insertCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Registra un retiro de una cuenta
        /// </summary>
        public void RegistrarRetiro(string cuenta, double importe, string codEmp)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Paso 1: Leer datos de la cuenta (WITH (UPDLOCK) para bloqueo pesimista)
                        var querySelect = @"
                            SELECT dec_cuensaldo, int_cuencontmov 
                            FROM dbo.cuenta WITH (UPDLOCK)
                            WHERE chr_cuencodigo = @Cuenta AND vch_cuenestado = 'ACTIVO'";
                        var selectCommand = new SqlCommand(querySelect, connection, transaction);
                        selectCommand.Parameters.AddWithValue("@Cuenta", cuenta);

                        double saldo;
                        int cont;

                        using (var reader = selectCommand.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("ERROR, cuenta no existe, o no está activa");

                            saldo = Convert.ToDouble(reader["dec_cuensaldo"]);
                            cont = Convert.ToInt32(reader["int_cuencontmov"]);
                        }

                        // Validar saldo suficiente
                        if (saldo < importe)
                            throw new Exception("ERROR, saldo insuficiente");

                        // Paso 2: Obtener el siguiente número de movimiento
                        var queryMaxMov = @"
                            SELECT COALESCE(MAX(int_movinumero), 0) + 1 AS siguiente_numero 
                            FROM dbo.movimiento 
                            WHERE chr_cuencodigo = @Cuenta";
                        var maxCommand = new SqlCommand(queryMaxMov, connection, transaction);
                        maxCommand.Parameters.AddWithValue("@Cuenta", cuenta);
                        var maxResult = maxCommand.ExecuteScalar();
                        int siguienteNumero = 1;
                        if (maxResult != null && maxResult != DBNull.Value)
                        {
                            siguienteNumero = Convert.ToInt32(maxResult);
                        }

                        // Paso 3: Actualizar la cuenta (debitar)
                        saldo -= importe;
                        cont++;
                        var queryUpdate = @"
                            UPDATE dbo.cuenta
                            SET dec_cuensaldo = @Saldo, int_cuencontmov = @Cont
                            WHERE chr_cuencodigo = @Cuenta AND vch_cuenestado = 'ACTIVO'";
                        var updateCommand = new SqlCommand(queryUpdate, connection, transaction);
                        updateCommand.Parameters.AddWithValue("@Saldo", saldo);
                        updateCommand.Parameters.AddWithValue("@Cont", cont);
                        updateCommand.Parameters.AddWithValue("@Cuenta", cuenta);
                        updateCommand.ExecuteNonQuery();

                        // Paso 4: Registrar movimiento (tipo '004' retiro) - importe positivo
                        var queryInsert = @"
                            INSERT INTO dbo.movimiento (chr_cuencodigo, int_movinumero, dtt_movifecha, chr_emplcodigo, chr_tipocodigo, dec_moviimporte)
                            VALUES (@Cuenta, @NroMov, GETDATE(), @CodEmp, '004', @Importe)";
                        var insertCommand = new SqlCommand(queryInsert, connection, transaction);
                        insertCommand.Parameters.AddWithValue("@Cuenta", cuenta);
                        insertCommand.Parameters.AddWithValue("@NroMov", siguienteNumero);
                        insertCommand.Parameters.AddWithValue("@CodEmp", codEmp);
                        insertCommand.Parameters.AddWithValue("@Importe", importe);
                        insertCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Registra una transferencia entre cuentas
        /// </summary>
        public void RegistrarTransferencia(string cuentaOrigen, string cuentaDestino, double importe, string codEmp)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Validar que las cuentas sean diferentes
                        if (cuentaOrigen == cuentaDestino)
                            throw new Exception("ERROR, no se puede transferir a la misma cuenta");

                        // === PASO 1: Leer datos de la cuenta origen (WITH (UPDLOCK)) ===
                        var cmdSelectOrigen = new SqlCommand(@"
                            SELECT dec_cuensaldo, int_cuencontmov 
                            FROM dbo.cuenta WITH (UPDLOCK)
                            WHERE chr_cuencodigo = @Cuenta AND vch_cuenestado = 'ACTIVO'", connection, transaction);
                        cmdSelectOrigen.Parameters.AddWithValue("@Cuenta", cuentaOrigen);

                        double saldoOrigen;
                        int contOrigen;

                        using (var reader = cmdSelectOrigen.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("ERROR, cuenta origen no existe o no está activa");
                            saldoOrigen = Convert.ToDouble(reader["dec_cuensaldo"]);
                            contOrigen = Convert.ToInt32(reader["int_cuencontmov"]);
                        }

                        // Validar saldo suficiente
                        if (saldoOrigen < importe)
                            throw new Exception("ERROR, saldo insuficiente en la cuenta origen");

                        // === PASO 2: Leer datos de la cuenta destino (WITH (UPDLOCK)) ===
                        var cmdSelectDestino = new SqlCommand(@"
                            SELECT dec_cuensaldo, int_cuencontmov 
                            FROM dbo.cuenta WITH (UPDLOCK)
                            WHERE chr_cuencodigo = @Cuenta AND vch_cuenestado = 'ACTIVO'", connection, transaction);
                        cmdSelectDestino.Parameters.AddWithValue("@Cuenta", cuentaDestino);

                        double saldoDestino;
                        int contDestino;

                        using (var reader = cmdSelectDestino.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("ERROR, cuenta destino no existe o no está activa");
                            saldoDestino = Convert.ToDouble(reader["dec_cuensaldo"]);
                            contDestino = Convert.ToInt32(reader["int_cuencontmov"]);
                        }

                        // === PASO 3: Obtener siguiente número de movimiento para cuenta origen ===
                        var queryMaxMovOrigen = @"
                            SELECT COALESCE(MAX(int_movinumero), 0) + 1 AS siguiente_numero 
                            FROM dbo.movimiento 
                            WHERE chr_cuencodigo = @Cuenta";
                        var maxCommandOrigen = new SqlCommand(queryMaxMovOrigen, connection, transaction);
                        maxCommandOrigen.Parameters.AddWithValue("@Cuenta", cuentaOrigen);
                        var maxResultOrigen = maxCommandOrigen.ExecuteScalar();
                        int siguienteNumeroOrigen = 1;
                        if (maxResultOrigen != null && maxResultOrigen != DBNull.Value)
                        {
                            siguienteNumeroOrigen = Convert.ToInt32(maxResultOrigen);
                        }

                        // === PASO 4: Obtener siguiente número de movimiento para cuenta destino ===
                        var queryMaxMovDestino = @"
                            SELECT COALESCE(MAX(int_movinumero), 0) + 1 AS siguiente_numero 
                            FROM dbo.movimiento 
                            WHERE chr_cuencodigo = @Cuenta";
                        var maxCommandDestino = new SqlCommand(queryMaxMovDestino, connection, transaction);
                        maxCommandDestino.Parameters.AddWithValue("@Cuenta", cuentaDestino);
                        var maxResultDestino = maxCommandDestino.ExecuteScalar();
                        int siguienteNumeroDestino = 1;
                        if (maxResultDestino != null && maxResultDestino != DBNull.Value)
                        {
                            siguienteNumeroDestino = Convert.ToInt32(maxResultDestino);
                        }

                        // === PASO 5: Actualizar cuenta origen (debitar) ===
                        saldoOrigen -= importe;
                        contOrigen++;
                        var cmdUpdateOrigen = new SqlCommand(@"
                            UPDATE dbo.cuenta SET dec_cuensaldo = @Saldo, int_cuencontmov = @Cont
                            WHERE chr_cuencodigo = @Cuenta AND vch_cuenestado = 'ACTIVO'", connection, transaction);
                        cmdUpdateOrigen.Parameters.AddWithValue("@Saldo", saldoOrigen);
                        cmdUpdateOrigen.Parameters.AddWithValue("@Cont", contOrigen);
                        cmdUpdateOrigen.Parameters.AddWithValue("@Cuenta", cuentaOrigen);
                        cmdUpdateOrigen.ExecuteNonQuery();

                        // === PASO 6: Actualizar cuenta destino (acreditar) ===
                        saldoDestino += importe;
                        contDestino++;
                        var cmdUpdateDestino = new SqlCommand(@"
                            UPDATE dbo.cuenta SET dec_cuensaldo = @Saldo, int_cuencontmov = @Cont
                            WHERE chr_cuencodigo = @Cuenta AND vch_cuenestado = 'ACTIVO'", connection, transaction);
                        cmdUpdateDestino.Parameters.AddWithValue("@Saldo", saldoDestino);
                        cmdUpdateDestino.Parameters.AddWithValue("@Cont", contDestino);
                        cmdUpdateDestino.Parameters.AddWithValue("@Cuenta", cuentaDestino);
                        cmdUpdateDestino.ExecuteNonQuery();

                        // === PASO 7: Registrar movimiento en cuenta origen (tipo '009' - Transferencia SALIDA) ===
                        var cmdInsertRetiro = new SqlCommand(@"
                            INSERT INTO dbo.movimiento (chr_cuencodigo, int_movinumero, dtt_movifecha, chr_emplcodigo, chr_tipocodigo, dec_moviimporte, chr_cuenreferencia)
                            VALUES (@Cuenta, @NroMov, GETDATE(), @CodEmp, '009', @Importe, @Referencia)", connection, transaction);
                        cmdInsertRetiro.Parameters.AddWithValue("@Cuenta", cuentaOrigen);
                        cmdInsertRetiro.Parameters.AddWithValue("@NroMov", siguienteNumeroOrigen);
                        cmdInsertRetiro.Parameters.AddWithValue("@CodEmp", codEmp);
                        cmdInsertRetiro.Parameters.AddWithValue("@Importe", importe);
                        cmdInsertRetiro.Parameters.AddWithValue("@Referencia", cuentaDestino);
                        cmdInsertRetiro.ExecuteNonQuery();

                        // === PASO 8: Registrar movimiento en cuenta destino (tipo '008' - Transferencia INGRESO) ===
                        var cmdInsertDeposito = new SqlCommand(@"
                            INSERT INTO dbo.movimiento (chr_cuencodigo, int_movinumero, dtt_movifecha, chr_emplcodigo, chr_tipocodigo, dec_moviimporte, chr_cuenreferencia)
                            VALUES (@Cuenta, @NroMov, GETDATE(), @CodEmp, '008', @Importe, @Referencia)", connection, transaction);
                        cmdInsertDeposito.Parameters.AddWithValue("@Cuenta", cuentaDestino);
                        cmdInsertDeposito.Parameters.AddWithValue("@NroMov", siguienteNumeroDestino);
                        cmdInsertDeposito.Parameters.AddWithValue("@CodEmp", codEmp);
                        cmdInsertDeposito.Parameters.AddWithValue("@Importe", importe);
                        cmdInsertDeposito.Parameters.AddWithValue("@Referencia", cuentaOrigen);
                        cmdInsertDeposito.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

