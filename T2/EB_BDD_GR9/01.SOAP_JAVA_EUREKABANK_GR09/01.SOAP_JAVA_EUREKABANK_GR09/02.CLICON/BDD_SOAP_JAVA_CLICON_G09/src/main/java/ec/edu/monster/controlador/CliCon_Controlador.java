/*
 * Controlador para el cliente consola EurekaBank
 * Intermediario entre la vista y el servicio
 */
package ec.edu.monster.controlador;

import ec.edu.monster.modelo.Movimiento;
import ec.edu.monster.servicio.EurekaService;
import java.util.List;

/**
 * Controlador que maneja la lógica de negocio entre la vista y el servicio
 * @author Grupo Monster G09
 */
public class CliCon_Controlador {
    
    /**
     * Obtiene los movimientos de una cuenta
     * @param cuenta Número de cuenta
     * @return Lista de movimientos
     */
    public List<Movimiento> traerMovimientos(String cuenta) {
        EurekaService service = new EurekaService();
        return service.traerMovimientos(cuenta);
    }
    
    /**
     * Registra un depósito en una cuenta
     * @param cuenta Número de cuenta
     * @param importe Importe a depositar
     * @return 1 si fue exitoso, 0 en caso contrario
     */
    public int regDeposito(String cuenta, double importe) {
        EurekaService service = new EurekaService();
        return service.regDeposito(cuenta, importe);
    }
    
    /**
     * Registra un retiro de una cuenta
     * @param cuenta Número de cuenta
     * @param importe Importe a retirar
     * @return 1 si fue exitoso, 0 en caso contrario
     */
    public int regRetiro(String cuenta, double importe) {
        EurekaService service = new EurekaService();
        return service.regRetiro(cuenta, importe);
    }
    
    /**
     * Registra una transferencia entre cuentas
     * @param cuentaOrigen Cuenta origen
     * @param cuentaDestino Cuenta destino
     * @param importe Importe a transferir
     * @return 1 si fue exitoso, 0 en caso contrario
     */
    public int regTransferencia(String cuentaOrigen, String cuentaDestino, double importe) {
        EurekaService service = new EurekaService();
        return service.regTransferencia(cuentaOrigen, cuentaDestino, importe);
    }
}

