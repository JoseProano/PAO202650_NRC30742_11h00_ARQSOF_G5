using System;
using ec.edu.monster.modelo;
using ec.edu.monster.servicios;
using ec.edu.monster.vista;
using ec.edu.monster.prueba;

namespace ec.edu.monster.controlador
{
    /// <summary>
    /// Controlador para la aplicación de consola de conversiones - Patrón MVC
    /// Maneja la lógica de negocio y coordina entre la vista y el modelo
    /// Replicado desde Java
    /// </summary>
    public class ControladorConsola
    {
        private ClienteConversionSOAP clienteSOAP;
        private VistaMenu vistaMenu;
        private Autenticador autenticador;
        
        public ControladorConsola()
        {
            this.clienteSOAP = new ClienteConversionSOAP();
            this.vistaMenu = new VistaMenu();
            this.autenticador = new Autenticador();
        }
        
        /// <summary>
        /// Inicia la aplicación de consola
        /// </summary>
        public void Iniciar()
        {
            // Primero autenticar al usuario
            if (!autenticador.Autenticar())
            {
                Console.WriteLine("Cerrando aplicación por seguridad...");
                return;
            }
            
            // Si la autenticación es exitosa, mostrar el menú principal
            vistaMenu.MostrarBanner();
            vistaMenu.MostrarInfoColores();
            
            while (true)
            {
                vistaMenu.MostrarMenuPrincipal();
                int opcion = vistaMenu.LeerOpcion();
                
                if (opcion == 0)
                {
                    vistaMenu.MostrarDespedida();
                    break;
                }
                
                ProcesarOpcion(opcion);
                
                // Pausa antes de mostrar el menú nuevamente
                vistaMenu.MostrarPausa();
                UtilidadesConsola.LimpiarPantalla();
                vistaMenu.MostrarBanner();
            }
            
            CerrarRecursos();
        }
        
        /// <summary>
        /// Procesa la opción seleccionada
        /// </summary>
        private void ProcesarOpcion(int opcion)
        {
            Console.WriteLine();
            
            switch (opcion)
            {
                case 1:
                    ManejarTemperatura();
                    break;
                case 2:
                    ManejarLongitud();
                    break;
                case 3:
                    ManejarPeso();
                    break;
                case 4:
                    ManejarVolumen();
                    break;
                case 5:
                    ManejarArea();
                    break;
                case 6:
                    EjecutarPruebasAutomaticas();
                    break;
                case 7:
                    EjecutarPruebasIndividuales();
                    break;
                default:
                    vistaMenu.MostrarOpcionInvalida();
                    break;
            }
            
            Console.WriteLine("\n" + new string('=', 60));
        }
        
        /// <summary>
        /// Maneja las conversiones de temperatura
        /// </summary>
        private void ManejarTemperatura()
        {
            vistaMenu.MostrarMenuTemperatura();
            
            int subOpcion = vistaMenu.LeerOpcion();
            string operacion = ObtenerOperacionTemperatura(subOpcion);
            
            if (operacion != null)
            {
                double valor = vistaMenu.LeerValor();
                Conversion resultado = clienteSOAP.ConvertirTemperatura(operacion, valor);
                vistaMenu.MostrarResultado(resultado);
            }
            else
            {
                vistaMenu.MostrarOpcionInvalida();
            }
        }
        
        /// <summary>
        /// Maneja las conversiones de longitud
        /// </summary>
        private void ManejarLongitud()
        {
            vistaMenu.MostrarMenuLongitud();
            
            int subOpcion = vistaMenu.LeerOpcion();
            string operacion = ObtenerOperacionLongitud(subOpcion);
            
            if (operacion != null)
            {
                double valor = vistaMenu.LeerValor();
                Conversion resultado = clienteSOAP.ConvertirLongitud(operacion, valor);
                vistaMenu.MostrarResultado(resultado);
            }
            else
            {
                vistaMenu.MostrarOpcionInvalida();
            }
        }
        
        /// <summary>
        /// Maneja las conversiones de peso/masa
        /// </summary>
        private void ManejarPeso()
        {
            vistaMenu.MostrarMenuPeso();
            
            int subOpcion = vistaMenu.LeerOpcion();
            string operacion = ObtenerOperacionPeso(subOpcion);
            
            if (operacion != null)
            {
                double valor = vistaMenu.LeerValor();
                Conversion resultado = clienteSOAP.ConvertirPeso(operacion, valor);
                vistaMenu.MostrarResultado(resultado);
            }
            else
            {
                vistaMenu.MostrarOpcionInvalida();
            }
        }
        
        /// <summary>
        /// Maneja las conversiones de volumen
        /// </summary>
        private void ManejarVolumen()
        {
            vistaMenu.MostrarMenuVolumen();
            
            int subOpcion = vistaMenu.LeerOpcion();
            string operacion = ObtenerOperacionVolumen(subOpcion);
            
            if (operacion != null)
            {
                double valor = vistaMenu.LeerValor();
                Conversion resultado = clienteSOAP.ConvertirVolumen(operacion, valor);
                vistaMenu.MostrarResultado(resultado);
            }
            else
            {
                vistaMenu.MostrarOpcionInvalida();
            }
        }
        
        /// <summary>
        /// Maneja las conversiones de área
        /// </summary>
        private void ManejarArea()
        {
            vistaMenu.MostrarMenuArea();
            
            int subOpcion = vistaMenu.LeerOpcion();
            string operacion = ObtenerOperacionArea(subOpcion);
            
            if (operacion != null)
            {
                double valor = vistaMenu.LeerValor();
                Conversion resultado = clienteSOAP.ConvertirArea(operacion, valor);
                vistaMenu.MostrarResultado(resultado);
            }
            else
            {
                vistaMenu.MostrarOpcionInvalida();
            }
        }
        
        /// <summary>
        /// Ejecuta pruebas automáticas
        /// </summary>
        private void EjecutarPruebasAutomaticas()
        {
            vistaMenu.MostrarBannerPruebas();
            
            // Pruebas de temperatura
            vistaMenu.MostrarTituloPruebasTemperatura();
            Conversion temp1 = clienteSOAP.ConvertirTemperatura("celsiusAFahrenheit", 25.0);
            vistaMenu.MostrarResultado(temp1);
            
            Conversion temp2 = clienteSOAP.ConvertirTemperatura("fahrenheitACelsius", 77.0);
            vistaMenu.MostrarResultado(temp2);
            
            // Pruebas de longitud
            vistaMenu.MostrarTituloPruebasLongitud();
            Conversion long1 = clienteSOAP.ConvertirLongitud("metrosAPies", 10.0);
            vistaMenu.MostrarResultado(long1);
            
            Conversion long2 = clienteSOAP.ConvertirLongitud("piesAMetros", 32.8);
            vistaMenu.MostrarResultado(long2);
            
            // Pruebas de peso
            vistaMenu.MostrarTituloPruebasPeso();
            Conversion peso1 = clienteSOAP.ConvertirPeso("kilogramosALibras", 5.0);
            vistaMenu.MostrarResultado(peso1);
            
            Conversion peso2 = clienteSOAP.ConvertirPeso("librasAKilogramos", 11.0);
            vistaMenu.MostrarResultado(peso2);
            
            vistaMenu.MostrarPruebasCompletadas();
        }
        
        /// <summary>
        /// Ejecuta pruebas individuales
        /// </summary>
        private void EjecutarPruebasIndividuales()
        {
            vistaMenu.MostrarBannerPruebas();
            
            Console.WriteLine("Seleccione el tipo de prueba:");
            Console.WriteLine("[1] Pruebas del Cliente de Conversiones");
            Console.WriteLine("[2] Pruebas de la Aplicación Completa");
            Console.WriteLine("[0] Volver al menú principal");
            Console.WriteLine();
            
            Console.Write("Seleccione una opción: ");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out int opcion))
            {
                switch (opcion)
                {
                    case 1:
                        PruebaClienteConversion.EjecutarPruebas();
                        break;
                    case 2:
                        PruebaClienteSOAP.EjecutarPruebasCompletas();
                        break;
                    case 0:
                        return;
                    default:
                        vistaMenu.MostrarOpcionInvalida();
                        break;
                }
            }
            else
            {
                vistaMenu.MostrarOpcionInvalida();
            }
        }
        
        // Métodos auxiliares para obtener operaciones
        private string ObtenerOperacionTemperatura(int opcion)
        {
            switch (opcion)
            {
                case 1: return "celsiusAFahrenheit";
                case 2: return "fahrenheitACelsius";
                case 3: return "celsiusAKelvin";
                case 4: return "kelvinACelsius";
                case 5: return "fahrenheitAKelvin";
                case 6: return "kelvinAFahrenheit";
                default: return null;
            }
        }
        
        private string ObtenerOperacionLongitud(int opcion)
        {
            switch (opcion)
            {
                case 1: return "metrosAPies";
                case 2: return "piesAMetros";
                case 3: return "metrosAPulgadas";
                case 4: return "pulgadasAMetros";
                case 5: return "kilometrosAMillas";
                case 6: return "millasAKilometros";
                default: return null;
            }
        }
        
        private string ObtenerOperacionPeso(int opcion)
        {
            switch (opcion)
            {
                case 1: return "kilogramosALibras";
                case 2: return "librasAKilogramos";
                case 3: return "gramosAOnzas";
                case 4: return "onzasAGramos";
                default: return null;
            }
        }
        
        private string ObtenerOperacionVolumen(int opcion)
        {
            switch (opcion)
            {
                case 1: return "litrosAGalones";
                case 2: return "galonesALitros";
                case 3: return "mililitrosAOnzasFluidas";
                case 4: return "onzasFluidasAMililitros";
                default: return null;
            }
        }
        
        private string ObtenerOperacionArea(int opcion)
        {
            switch (opcion)
            {
                case 1: return "metrosCuadradosAPiesCuadrados";
                case 2: return "piesCuadradosAMetrosCuadrados";
                case 3: return "hectareasAAcres";
                case 4: return "acresAHectareas";
                default: return null;
            }
        }
        
        /// <summary>
        /// Cierra los recursos
        /// </summary>
        private void CerrarRecursos()
        {
            vistaMenu?.Cerrar();
            autenticador?.Cerrar();
            clienteSOAP?.Cerrar();
        }
    }
}
