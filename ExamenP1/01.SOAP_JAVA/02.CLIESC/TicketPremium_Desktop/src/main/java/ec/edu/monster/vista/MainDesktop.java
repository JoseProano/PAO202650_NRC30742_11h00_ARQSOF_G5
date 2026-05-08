package ec.edu.monster.vista;

/**
 * Clase principal para lanzar la aplicación de escritorio.
 */
public class MainDesktop {
    public static void main(String[] args) {
        javax.swing.SwingUtilities.invokeLater(() -> {
            new VentanaPrincipal().setVisible(true);
        });
    }
}
