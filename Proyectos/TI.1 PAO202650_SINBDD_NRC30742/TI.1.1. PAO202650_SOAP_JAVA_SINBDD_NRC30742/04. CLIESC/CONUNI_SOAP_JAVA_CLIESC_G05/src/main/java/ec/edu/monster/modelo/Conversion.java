package ec.edu.monster.modelo;

/**
 * Modelo para representar una conversión
 * @author ACER NITRO V15
 */
public class Conversion {
    private double valor;
    private String unidadOrigen;
    private String unidadDestino;
    private double resultado;
    private String operacion;
    private boolean exitosa;
    private String mensajeError;
    
    public Conversion() {
        this.exitosa = false;
    }
    
    public Conversion(double valor, String unidadOrigen, String unidadDestino, 
                     double resultado, String operacion) {
        this.valor = valor;
        this.unidadOrigen = unidadOrigen;
        this.unidadDestino = unidadDestino;
        this.resultado = resultado;
        this.operacion = operacion;
        this.exitosa = true;
    }
    
    // Getters y Setters
    public double getValor() { return valor; }
    public void setValor(double valor) { this.valor = valor; }
    
    public String getUnidadOrigen() { return unidadOrigen; }
    public void setUnidadOrigen(String unidadOrigen) { this.unidadOrigen = unidadOrigen; }
    
    public String getUnidadDestino() { return unidadDestino; }
    public void setUnidadDestino(String unidadDestino) { this.unidadDestino = unidadDestino; }
    
    public double getResultado() { return resultado; }
    public void setResultado(double resultado) { this.resultado = resultado; }
    
    public String getOperacion() { return operacion; }
    public void setOperacion(String operacion) { this.operacion = operacion; }
    
    public boolean isExitosa() { return exitosa; }
    public void setExitosa(boolean exitosa) { this.exitosa = exitosa; }
    
    public String getMensajeError() { return mensajeError; }
    public void setMensajeError(String mensajeError) { 
        this.mensajeError = mensajeError; 
        this.exitosa = false;
    }
    
    @Override
    public String toString() {
        if (exitosa) {
            return String.format("%.2f %s = %.2f %s", 
                               valor, unidadOrigen, resultado, unidadDestino);
        } else {
            return mensajeError != null ? mensajeError : "Error en la conversión";
        }
    }
}
