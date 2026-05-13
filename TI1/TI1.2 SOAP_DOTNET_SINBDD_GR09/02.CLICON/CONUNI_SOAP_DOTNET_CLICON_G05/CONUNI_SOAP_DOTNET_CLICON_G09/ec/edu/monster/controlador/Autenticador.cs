using System;
using ec.edu.monster.servicios;
using ec.edu.monster.vista;

namespace ec.edu.monster.controlador
{
    /// <summary>
    /// Clase para manejar la autenticación del sistema.
    /// <para>
    /// Las credenciales NO se almacenan en el cliente. La validación
    /// se delega al servidor a través de la operación SOAP <c>login</c>.
    /// </para>
    /// </summary>
    public class Autenticador
    {
        private const int MAX_INTENTOS = 3;

        private readonly VistaLogin          _vistaLogin;
        private readonly ClienteConversionSOAP _clienteSOAP;

        public Autenticador()
        {
            _vistaLogin  = new VistaLogin();
            _clienteSOAP = new ClienteConversionSOAP();
        }

        /// <summary>
        /// Realiza el proceso de autenticación contra el servidor SOAP.
        /// </summary>
        /// <returns><c>true</c> si la autenticación es exitosa</returns>
        public bool Autenticar()
        {
            _vistaLogin.MostrarBannerLogin();

            for (int intento = 1; intento <= MAX_INTENTOS; intento++)
            {
                _vistaLogin.MostrarIntento(intento, MAX_INTENTOS);

                string usuario   = _vistaLogin.LeerUsuario();
                string contrasena = _vistaLogin.LeerContrasenaOculta();

                if (ValidarCredencialesEnServidor(usuario, contrasena))
                {
                    _vistaLogin.MostrarMensajeExito();
                    return true;
                }
                else
                {
                    _vistaLogin.MostrarMensajeError(intento, MAX_INTENTOS);
                }
            }

            _vistaLogin.MostrarMensajeBloqueo();
            return false;
        }

        /// <summary>
        /// Delega la validación de credenciales al servidor SOAP.
        /// El cliente nunca conoce las credenciales correctas.
        /// </summary>
        private bool ValidarCredencialesEnServidor(string usuario, string contrasena)
        {
            try
            {
                return _clienteSOAP.Login(usuario, contrasena);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al conectar con el servidor para autenticar: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Cierra el autenticador y libera recursos.
        /// </summary>
        public void Cerrar()
        {
            _vistaLogin?.Cerrar();
            _clienteSOAP?.Cerrar();
        }
    }
}
