package ec.edu.monster.servicios;

/**
 * Servicio de autenticación centralizado en el servidor.
 * Las credenciales residen ÚNICAMENTE aquí, en el servidor.
 * Los clientes deben llamar al WebService para validar credenciales.
 *
 * @author GR09 - Arquitectura de Software
 */
public class AutenticacionService {

    // Credenciales almacenadas SOLO en el servidor
    private static final String USUARIO_VALIDO    = "MONSTER";
    private static final String CONTRASENA_VALIDA = "MONSTER9";

    /**
     * Valida las credenciales de un usuario.
     *
     * @param usuario   nombre de usuario enviado por el cliente
     * @param contrasena contraseña enviada por el cliente
     * @return {@code true} si las credenciales son correctas, {@code false} en caso contrario
     */
    public boolean validarCredenciales(String usuario, String contrasena) {
        if (usuario == null || contrasena == null) {
            return false;
        }
        return USUARIO_VALIDO.equals(usuario.trim().toUpperCase())
                && CONTRASENA_VALIDA.equals(contrasena.trim());
    }
}
