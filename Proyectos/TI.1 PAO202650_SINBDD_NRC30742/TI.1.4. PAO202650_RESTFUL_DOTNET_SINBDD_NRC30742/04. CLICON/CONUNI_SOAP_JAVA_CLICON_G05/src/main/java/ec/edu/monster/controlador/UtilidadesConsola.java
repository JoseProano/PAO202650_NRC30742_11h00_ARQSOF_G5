package ec.edu.monster.controlador;

/**
 * Utilidades para la consola
 * @author ACER NITRO V15
 */
public class UtilidadesConsola {
    
    /**
     * Limpia la pantalla de la consola
     */
    public static void limpiarPantalla() {
        try {
            // Para Windows
            if (System.getProperty("os.name").toLowerCase().contains("windows")) {
                new ProcessBuilder("cmd", "/c", "cls").inheritIO().start().waitFor();
            } else {
                // Para Unix/Linux/Mac
                System.out.print("\033[H\033[2J");
                System.out.flush();
            }
        } catch (Exception e) {
            // Si falla, simplemente imprime muchas líneas en blanco
            for (int i = 0; i < 50; i++) {
                System.out.println();
            }
        }
    }
    
    /**
     * Pausa la ejecución por un tiempo determinado
     */
    public static void pausar(int segundos) {
        try {
            Thread.sleep(segundos * 1000);
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
    }
    
    /**
     * Muestra una línea separadora
     */
    public static void mostrarSeparador() {
        System.out.println("=".repeat(60));
    }
    
    /**
     * Muestra una línea separadora más pequeña
     */
    public static void mostrarSeparadorPequeño() {
        System.out.println("-".repeat(40));
    }
    
    /**
     * Centra un texto en la consola
     */
    public static void centrarTexto(String texto) {
        int ancho = 60;
        int espacios = (ancho - texto.length()) / 2;
        System.out.println(" ".repeat(Math.max(0, espacios)) + texto);
    }
}
