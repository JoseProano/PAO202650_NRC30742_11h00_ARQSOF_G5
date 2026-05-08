package ec.edu.monster.modelo;

import java.io.Serializable;

/**
 * Modelo que representa un partido de fútbol.
 */
public class PartidoFutbol implements Serializable {

    private int codigo;
    private String equipoLocal;
    private String equipoVisita;
    private String fecha;
    private String lugar;

    public PartidoFutbol() {}

    public PartidoFutbol(int codigo, String equipoLocal, String equipoVisita, String fecha, String lugar) {
        this.codigo = codigo;
        this.equipoLocal = equipoLocal;
        this.equipoVisita = equipoVisita;
        this.fecha = fecha;
        this.lugar = lugar;
    }

    public int getCodigo() { return codigo; }
    public void setCodigo(int codigo) { this.codigo = codigo; }
    public String getEquipoLocal() { return equipoLocal; }
    public void setEquipoLocal(String equipoLocal) { this.equipoLocal = equipoLocal; }
    public String getEquipoVisita() { return equipoVisita; }
    public void setEquipoVisita(String equipoVisita) { this.equipoVisita = equipoVisita; }
    public String getFecha() { return fecha; }
    public void setFecha(String fecha) { this.fecha = fecha; }
    public String getLugar() { return lugar; }
    public void setLugar(String lugar) { this.lugar = lugar; }
}
