package ec.edu.monster.controlador;

import ec.edu.monster.servicios.ClienteConversionSOAP;
import ec.edu.monster.vista.VistaLogin;

/**
 * Clase para manejar la autenticación del sistema.
 * <p>
 * Las credenciales NO se almacenan en el cliente. La validación
 * se delega al servidor a través de la operación SOAP {@code login}.
 * </p>
 *
 * @author GR09 - Arquitectura de Software
 */
public class Autenticador {

    private static final int MAX_INTENTOS = 3;

    private final VistaLogin          vistaLogin;
    private final ClienteConversionSOAP clienteSOAP;

    public Autenticador() {
        this.vistaLogin   = new VistaLogin();
        this.clienteSOAP  = new ClienteConversionSOAP();
    }

    /**
     * Realiza el proceso de autenticación contra el servidor.
     *
     * @return {@code true} si la autenticación es exitosa, {@code false} en caso contrario
     */
    public boolean autenticar() {
        vistaLogin.mostrarBannerLogin();

        for (int intento = 1; intento <= MAX_INTENTOS; intento++) {
            vistaLogin.mostrarIntento(intento, MAX_INTENTOS);

            String usuario   = vistaLogin.leerUsuario();
            String contrasena = vistaLogin.leerContrasenaOculta();

            if (validarCredencialesEnServidor(usuario, contrasena)) {
                vistaLogin.mostrarMensajeExito();
                return true;
            } else {
                vistaLogin.mostrarMensajeError(intento, MAX_INTENTOS);
            }
        }

        vistaLogin.mostrarMensajeBloqueo();
        return false;
    }

    /**
     * Delega la validación de credenciales al servidor SOAP.
     * El cliente nunca conoce las credenciales correctas.
     */
    private boolean validarCredencialesEnServidor(String usuario, String contrasena) {
        try {
            return clienteSOAP.login(usuario, contrasena);
        } catch (Exception e) {
            System.err.println("Error al conectar con el servidor para autenticar: " + e.getMessage());
            return false;
        }
    }

    /**
     * Cierra el autenticador y libera recursos.
     */
    public void cerrar() {
        if (vistaLogin != null) {
            vistaLogin.cerrar();
        }
    }
}