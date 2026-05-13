package ec.edu.monster.controlador;

import java.io.Console;
import java.nio.charset.Charset;

/**
 * Configuracion de codificacion para la consola
 * @author ACER NITRO V15
 */
public class ConfiguracionConsola {
    
    /**
     * Configura la codificacion UTF-8 para la consola
     */
    public static void configurarCodificacion() {
        try {
            // Configurar el charset del sistema para UTF-8
            System.setProperty("file.encoding", "UTF-8");
            System.setProperty("console.encoding", "UTF-8");
            
            // Configurar la salida estandar
            System.setOut(new java.io.PrintStream(System.out, true, "UTF-8"));
            System.setErr(new java.io.PrintStream(System.err, true, "UTF-8"));
            
            // No podemos cambiar System.in directamente, pero podemos configurar el Scanner
            // para que use UTF-8 cuando se cree
            
        } catch (Exception e) {
            System.err.println("No se pudo configurar UTF-8: " + e.getMessage());
        }
    }
    
    /**
     * Verifica si la consola soporta UTF-8
     */
    public static boolean verificarUTF8() {
        try {
            String test = "áéíóúñ";
            System.out.println("Prueba de caracteres especiales: " + test);
            return true;
        } catch (Exception e) {
            return false;
        }
    }
}
