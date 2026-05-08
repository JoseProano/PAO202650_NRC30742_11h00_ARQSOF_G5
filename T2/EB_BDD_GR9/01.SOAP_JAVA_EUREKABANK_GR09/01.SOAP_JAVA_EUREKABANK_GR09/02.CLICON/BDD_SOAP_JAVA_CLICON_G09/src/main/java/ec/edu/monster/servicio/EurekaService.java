/*
 * Cliente SOAP para el servicio EurekaBank
 * Adaptado para consumir el servicio web SOAP
 */
package ec.edu.monster.servicio;

import ec.edu.monster.modelo.Movimiento;
import jakarta.xml.ws.Service;
import java.net.URL;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import javax.xml.namespace.QName;

/**
 * Clase servicio que actúa como cliente SOAP para EurekaBank
 * @author Grupo Monster G09
 */
public class EurekaService {
    
    // URL del servicio SOAP (ajustar según tu configuración)
    private static final String WSDL_URL = "http://10.183.38.246:8080/WS_EUREKABANK_SERVICIO/EurekaService?wsdl";
    
    // Namespace del servicio
    private static final String TARGET_NAMESPACE = "http://servicios.monster.edu.ec/";
    private static final String SERVICE_NAME = "EurekaService";
    private static final String PORT_NAME = "EurekaServicePort";
    
    // Código de empleado por defecto para operaciones
    private static final String COD_EMP_DEFAULT = "0001";
    
    /**
     * Obtiene los movimientos de una cuenta
     * @param cuenta Número de cuenta
     * @return Lista de movimientos
     */
    public List<Movimiento> traerMovimientos(String cuenta) {
        List<Movimiento> lista = new ArrayList<>();
        
        try {
            // Crear el servicio usando la URL del WSDL
            URL url = new URL(WSDL_URL);
            QName serviceName = new QName(TARGET_NAMESPACE, SERVICE_NAME);
            QName portName = new QName(TARGET_NAMESPACE, PORT_NAME);
            
            Service service = Service.create(url, serviceName);
            
            // Obtener el port usando la interfaz
            EurekaServicePort port = service.getPort(portName, EurekaServicePort.class);
            
            // Invocar el método
            Movimiento[] movimientos = port.leerMovimientos(cuenta);
            
            // Convertir array a lista
            if (movimientos != null) {
                lista = Arrays.asList(movimientos);
            }
            
        } catch (Exception e) {
            System.err.println("Error al consultar movimientos: " + e.getMessage());
            e.printStackTrace();
        }
        
        return lista;
    }
    
    /**
     * Registra un depósito en una cuenta
     * @param cuenta Número de cuenta
     * @param importe Importe a depositar
     * @return 1 si fue exitoso, 0 en caso contrario
     */
    public int regDeposito(String cuenta, double importe) {
        try {
            // Crear el servicio usando la URL del WSDL
            URL url = new URL(WSDL_URL);
            QName serviceName = new QName(TARGET_NAMESPACE, SERVICE_NAME);
            QName portName = new QName(TARGET_NAMESPACE, PORT_NAME);
            
            Service service = Service.create(url, serviceName);
            
            // Obtener el port usando la interfaz
            EurekaServicePort port = service.getPort(portName, EurekaServicePort.class);
            
            // Invocar el método
            port.registrarDeposito(cuenta, importe, COD_EMP_DEFAULT);
            
            return 1;
            
        } catch (Exception e) {
            System.err.println("Error al registrar depósito: " + e.getMessage());
            e.printStackTrace();
            return 0;
        }
    }
    
    /**
     * Registra un retiro de una cuenta
     * @param cuenta Número de cuenta
     * @param importe Importe a retirar
     * @return 1 si fue exitoso, 0 en caso contrario
     */
    public int regRetiro(String cuenta, double importe) {
        try {
            // Crear el servicio usando la URL del WSDL
            URL url = new URL(WSDL_URL);
            QName serviceName = new QName(TARGET_NAMESPACE, SERVICE_NAME);
            QName portName = new QName(TARGET_NAMESPACE, PORT_NAME);
            
            Service service = Service.create(url, serviceName);
            
            // Obtener el port usando la interfaz
            EurekaServicePort port = service.getPort(portName, EurekaServicePort.class);
            
            // Invocar el método
            port.registrarRetiro(cuenta, importe, COD_EMP_DEFAULT);
            
            return 1;
            
        } catch (Exception e) {
            System.err.println("Error al registrar retiro: " + e.getMessage());
            e.printStackTrace();
            return 0;
        }
    }
    
    /**
     * Registra una transferencia entre cuentas
     * @param cuentaOrigen Cuenta origen
     * @param cuentaDestino Cuenta destino
     * @param importe Importe a transferir
     * @return 1 si fue exitoso, 0 en caso contrario
     */
    public int regTransferencia(String cuentaOrigen, String cuentaDestino, double importe) {
        try {
            // Crear el servicio usando la URL del WSDL
            URL url = new URL(WSDL_URL);
            QName serviceName = new QName(TARGET_NAMESPACE, SERVICE_NAME);
            QName portName = new QName(TARGET_NAMESPACE, PORT_NAME);
            
            Service service = Service.create(url, serviceName);
            
            // Obtener el port usando la interfaz
            EurekaServicePort port = service.getPort(portName, EurekaServicePort.class);
            
            // Invocar el método
            port.registrarTransferencia(cuentaOrigen, cuentaDestino, importe, COD_EMP_DEFAULT);
            
            return 1;
            
        } catch (Exception e) {
            System.err.println("Error al registrar transferencia: " + e.getMessage());
            e.printStackTrace();
            return 0;
        }
    }
}
