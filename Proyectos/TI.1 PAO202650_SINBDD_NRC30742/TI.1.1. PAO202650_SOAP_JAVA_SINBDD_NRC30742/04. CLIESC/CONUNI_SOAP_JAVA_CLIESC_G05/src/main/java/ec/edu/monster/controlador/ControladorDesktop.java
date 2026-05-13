package ec.edu.monster.controlador;

import ec.edu.monster.modelo.Conversion;
import ec.edu.monster.servicios.ClienteConversionSOAP;
import ec.edu.monster.vista.VentanaPrincipal;

/**
 * Controlador para la aplicación de escritorio
 * Maneja la lógica de negocio y coordina entre la vista y el modelo
 * @author grupo 5
 */
public class ControladorDesktop {
    
    private static ControladorDesktop instancia;
    private VentanaPrincipal ventana;
    private ClienteConversionSOAP clienteSOAP;
    
    private ControladorDesktop() {
        this.clienteSOAP = new ClienteConversionSOAP();
    }
    
    /**
     * Obtiene la instancia única del controlador (Singleton)
     */
    public static ControladorDesktop getInstance() {
        if (instancia == null) {
            instancia = new ControladorDesktop();
        }
        return instancia;
    }
    
    /**
     * Establece la ventana principal
     */
    public void setVentana(VentanaPrincipal ventana) {
        this.ventana = ventana;
    }
    
    /**
     * Obtiene la ventana principal
     */
    public VentanaPrincipal getVentana() {
        return this.ventana;
    }
    
    /**
     * Convierte temperatura
     */
    public void convertirTemperatura(String operacion, double valor) {
        Conversion resultado = clienteSOAP.convertirTemperatura(operacion, valor);
        mostrarResultado(resultado);
    }
    
    /**
     * Convierte longitud
     */
    public void convertirLongitud(String operacion, double valor) {
        Conversion resultado = clienteSOAP.convertirLongitud(operacion, valor);
        mostrarResultado(resultado);
    }
    
    /**
     * Convierte peso/masa
     */
    public void convertirPeso(String operacion, double valor) {
        Conversion resultado = clienteSOAP.convertirPeso(operacion, valor);
        mostrarResultado(resultado);
    }
    
    /**
     * Muestra el resultado en el panel correspondiente
     * NOTA: Ahora cada panel maneja su propio resultado localmente
     */
    private void mostrarResultado(Conversion conversion) {
        // Los paneles ahora manejan sus propios resultados localmente
        // Este método se mantiene por compatibilidad pero no hace nada
        System.out.println("Resultado de conversión: " + conversion.toString());
    }
    
    /**
     * Limpia el resultado
     * NOTA: Ahora cada panel maneja su propio resultado localmente
     */
    public void limpiarResultado() {
        // Los paneles ahora manejan sus propios resultados localmente
        // Este método se mantiene por compatibilidad pero no hace nada
        System.out.println("Limpiando resultado...");
    }
}
