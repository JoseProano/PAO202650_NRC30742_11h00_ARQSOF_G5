package ec.edu.monster.prueba;

import ec.edu.monster.controlador.ControladorConsola;
import ec.edu.monster.controlador.ConfiguracionConsola;

/**
 * Clase principal de la aplicación cliente de consola
 * @author ACER NITRO V15
 */
public class PruebaClienteConversion {
    
    public static void main(String[] args) {
        // Configurar codificación UTF-8 para mostrar tildes correctamente
        ConfiguracionConsola.configurarCodificacion();
        
        System.out.println("Iniciando Monsters Inc. Converter - Cliente Consola...");
        System.out.println("Sistema de autenticación activado");
        System.out.println("Usuario: MONSTER | Contraseña: MONSTER9");
        System.out.println();
        
        try {
            // Crear e iniciar el controlador de consola
            ControladorConsola controlador = new ControladorConsola();
            controlador.iniciar();
            
        } catch (Exception e) {
            System.err.println("Error al iniciar la aplicación:");
            System.err.println("   " + e.getMessage());
            System.err.println();
            System.err.println("Verifique que:");
            System.err.println("   - El servidor web esté ejecutándose en http://localhost:8080");
            System.err.println("   - El servicio WSConversion esté desplegado");
            System.err.println("   - La conexión de red esté disponible");
            System.exit(1);
        }
    }
}
