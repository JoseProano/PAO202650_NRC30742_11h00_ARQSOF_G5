package ec.edu.monster.prueba;

import ec.edu.monster.controlador.ControladorDesktop;
import ec.edu.monster.vista.VentanaPrincipal;
import ec.edu.monster.vista.VentanaLogin;
import javax.swing.SwingUtilities;

/**
 * Clase principal para ejecutar la aplicación de escritorio
 * @author ACER NITRO V15
 */
public class AplicacionDesktop {
    
    public static void main(String[] args) {
        // Ejecutar en el hilo de eventos de Swing
        SwingUtilities.invokeLater(() -> {
            try {
                // Mostrar ventana de login primero
                VentanaLogin ventanaLogin = new VentanaLogin();
                ventanaLogin.setVisible(true);
                
                System.out.println("Monsters Inc. Converter iniciado correctamente");
                
            } catch (Exception e) {
                System.err.println("Error al iniciar la aplicación: " + e.getMessage());
                e.printStackTrace();
                
                // Mostrar mensaje de error
                javax.swing.JOptionPane.showMessageDialog(
                    null, 
                    "Error al iniciar la aplicación:\n" + e.getMessage(),
                    "Error de Inicio",
                    javax.swing.JOptionPane.ERROR_MESSAGE
                );
            }
        });
    }
}