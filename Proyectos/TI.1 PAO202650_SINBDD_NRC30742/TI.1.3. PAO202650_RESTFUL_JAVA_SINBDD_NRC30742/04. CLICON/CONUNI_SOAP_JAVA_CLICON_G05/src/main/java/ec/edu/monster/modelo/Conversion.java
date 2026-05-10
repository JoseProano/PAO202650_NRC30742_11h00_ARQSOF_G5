package ec.edu.monster.modelo;

/**
 * Modelo para representar una conversión
 * @author ACER NITRO V15
 */
public class Conversion {
    private String tipoConversion;
    private String operacion;
    private double valorOriginal;
    private double valorConvertido;
    private String unidadOriginal;
    private String unidadDestino;
    private boolean exitosa;
    private String mensajeError;

    // Constructores
    public Conversion() {
    }

    public Conversion(String tipoConversion, String operacion, double valorOriginal, 
                     String unidadOriginal, String unidadDestino) {
        this.tipoConversion = tipoConversion;
        this.operacion = operacion;
        this.valorOriginal = valorOriginal;
        this.unidadOriginal = unidadOriginal;
        this.unidadDestino = unidadDestino;
        this.exitosa = false;
    }

    // Getters y Setters
    public String getTipoConversion() {
        return tipoConversion;
    }

    public void setTipoConversion(String tipoConversion) {
        this.tipoConversion = tipoConversion;
    }

    public String getOperacion() {
        return operacion;
    }

    public void setOperacion(String operacion) {
        this.operacion = operacion;
    }

    public double getValorOriginal() {
        return valorOriginal;
    }

    public void setValorOriginal(double valorOriginal) {
        this.valorOriginal = valorOriginal;
    }

    public double getValorConvertido() {
        return valorConvertido;
    }

    public void setValorConvertido(double valorConvertido) {
        this.valorConvertido = valorConvertido;
    }

    public String getUnidadOriginal() {
        return unidadOriginal;
    }

    public void setUnidadOriginal(String unidadOriginal) {
        this.unidadOriginal = unidadOriginal;
    }

    public String getUnidadDestino() {
        return unidadDestino;
    }

    public void setUnidadDestino(String unidadDestino) {
        this.unidadDestino = unidadDestino;
    }

    public boolean isExitosa() {
        return exitosa;
    }

    public void setExitosa(boolean exitosa) {
        this.exitosa = exitosa;
    }

    public String getMensajeError() {
        return mensajeError;
    }

    public void setMensajeError(String mensajeError) {
        this.mensajeError = mensajeError;
    }

    @Override
    public String toString() {
        if (exitosa) {
            return String.format("%s: %.4f %s = %.4f %s", 
                tipoConversion, valorOriginal, unidadOriginal, 
                valorConvertido, unidadDestino);
        } else {
            return String.format("Error en %s: %s", tipoConversion, mensajeError);
        }
    }
}
