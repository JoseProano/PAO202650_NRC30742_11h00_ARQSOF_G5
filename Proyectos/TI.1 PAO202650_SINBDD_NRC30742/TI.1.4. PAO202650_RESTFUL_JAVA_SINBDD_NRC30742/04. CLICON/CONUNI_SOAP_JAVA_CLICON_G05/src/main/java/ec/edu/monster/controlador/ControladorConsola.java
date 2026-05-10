package ec.edu.monster.controlador;

import ec.edu.monster.modelo.Conversion;
import ec.edu.monster.servicios.ClienteConversionSOAP;
import ec.edu.monster.vista.VistaMenu;

/**
 * Controlador para la aplicación de consola de conversiones - Patrón MVC
 * Maneja la lógica de negocio y coordina entre la vista y el modelo
 * @author ACER NITRO V15
 */
public class ControladorConsola {
    
    private ClienteConversionSOAP clienteSOAP;
    private VistaMenu vistaMenu;
    private Autenticador autenticador;
    
    public ControladorConsola() {
        this.clienteSOAP = new ClienteConversionSOAP();
        this.vistaMenu = new VistaMenu();
        this.autenticador = new Autenticador();
    }
    
    /**
     * Inicia la aplicación de consola
     */
    public void iniciar() {
        // Primero autenticar al usuario
        if (!autenticador.autenticar()) {
            System.out.println("Cerrando aplicación por seguridad...");
            return;
        }
        
        // Si la autenticación es exitosa, mostrar el menú principal
        vistaMenu.mostrarBanner();
        vistaMenu.mostrarInfoColores();
        
        while (true) {
            vistaMenu.mostrarMenuPrincipal();
            int opcion = vistaMenu.leerOpcion();
            
            if (opcion == 0) {
                vistaMenu.mostrarDespedida();
                break;
            }
            
            procesarOpcion(opcion);
            
            // Pausa antes de mostrar el menú nuevamente
            vistaMenu.mostrarPausa();
            UtilidadesConsola.limpiarPantalla();
            vistaMenu.mostrarBanner();
        }
        
        cerrarRecursos();
    }
    
    /**
     * Procesa la opción seleccionada
     */
    private void procesarOpcion(int opcion) {
        System.out.println();
        
        switch (opcion) {
            case 1:
                manejarTemperatura();
                break;
            case 2:
                manejarLongitud();
                break;
            case 3:
                manejarPeso();
                break;
            case 4:
                manejarVolumen();
                break;
            case 5:
                manejarArea();
                break;
            case 6:
                ejecutarPruebasAutomaticas();
                break;
            default:
                vistaMenu.mostrarOpcionInvalida();
        }
        
        System.out.println("\n" + "=".repeat(60));
    }
    
    /**
     * Maneja las conversiones de temperatura
     */
    private void manejarTemperatura() {
        vistaMenu.mostrarMenuTemperatura();
        
        int subOpcion = vistaMenu.leerOpcion();
        String operacion = obtenerOperacionTemperatura(subOpcion);
        
        if (operacion != null) {
            double valor = vistaMenu.leerValor();
            Conversion resultado = clienteSOAP.convertirTemperatura(operacion, valor);
            vistaMenu.mostrarResultado(resultado);
        } else {
            vistaMenu.mostrarOpcionInvalida();
        }
    }
    
    /**
     * Maneja las conversiones de longitud
     */
    private void manejarLongitud() {
        vistaMenu.mostrarMenuLongitud();
        
        int subOpcion = vistaMenu.leerOpcion();
        String operacion = obtenerOperacionLongitud(subOpcion);
        
        if (operacion != null) {
            double valor = vistaMenu.leerValor();
            Conversion resultado = clienteSOAP.convertirLongitud(operacion, valor);
            vistaMenu.mostrarResultado(resultado);
        } else {
            vistaMenu.mostrarOpcionInvalida();
        }
    }
    
    /**
     * Maneja las conversiones de peso/masa
     */
    private void manejarPeso() {
        vistaMenu.mostrarMenuPeso();
        
        int subOpcion = vistaMenu.leerOpcion();
        String operacion = obtenerOperacionPeso(subOpcion);
        
        if (operacion != null) {
            double valor = vistaMenu.leerValor();
            Conversion resultado = clienteSOAP.convertirPeso(operacion, valor);
            vistaMenu.mostrarResultado(resultado);
        } else {
            vistaMenu.mostrarOpcionInvalida();
        }
    }
    
    /**
     * Maneja las conversiones de volumen
     */
    private void manejarVolumen() {
        vistaMenu.mostrarMenuVolumen();
        
        int subOpcion = vistaMenu.leerOpcion();
        String operacion = obtenerOperacionVolumen(subOpcion);
        
        if (operacion != null) {
            double valor = vistaMenu.leerValor();
            Conversion resultado = clienteSOAP.convertirVolumen(operacion, valor);
            vistaMenu.mostrarResultado(resultado);
        } else {
            vistaMenu.mostrarOpcionInvalida();
        }
    }
    
    /**
     * Maneja las conversiones de área
     */
    private void manejarArea() {
        vistaMenu.mostrarMenuArea();
        
        int subOpcion = vistaMenu.leerOpcion();
        String operacion = obtenerOperacionArea(subOpcion);
        
        if (operacion != null) {
            double valor = vistaMenu.leerValor();
            Conversion resultado = clienteSOAP.convertirArea(operacion, valor);
            vistaMenu.mostrarResultado(resultado);
        } else {
            vistaMenu.mostrarOpcionInvalida();
        }
    }
    
    /**
     * Ejecuta pruebas automáticas
     */
    private void ejecutarPruebasAutomaticas() {
        vistaMenu.mostrarBannerPruebas();
        
        // Pruebas de temperatura
        vistaMenu.mostrarTituloPruebasTemperatura();
        Conversion temp1 = clienteSOAP.convertirTemperatura("celsiusAFahrenheit", 25.0);
        vistaMenu.mostrarResultado(temp1);
        
        Conversion temp2 = clienteSOAP.convertirTemperatura("fahrenheitACelsius", 77.0);
        vistaMenu.mostrarResultado(temp2);
        
        // Pruebas de longitud
        vistaMenu.mostrarTituloPruebasLongitud();
        Conversion long1 = clienteSOAP.convertirLongitud("metrosAPies", 10.0);
        vistaMenu.mostrarResultado(long1);
        
        Conversion long2 = clienteSOAP.convertirLongitud("piesAMetros", 32.8);
        vistaMenu.mostrarResultado(long2);
        
        // Pruebas de peso
        vistaMenu.mostrarTituloPruebasPeso();
        Conversion peso1 = clienteSOAP.convertirPeso("kilogramosALibras", 5.0);
        vistaMenu.mostrarResultado(peso1);
        
        Conversion peso2 = clienteSOAP.convertirPeso("librasAKilogramos", 11.0);
        vistaMenu.mostrarResultado(peso2);
        
        vistaMenu.mostrarPruebasCompletadas();
    }
    
    // Métodos auxiliares para obtener operaciones
    private String obtenerOperacionTemperatura(int opcion) {
        switch (opcion) {
            case 1: return "celsiusAFahrenheit";
            case 2: return "fahrenheitACelsius";
            case 3: return "celsiusAKelvin";
            case 4: return "kelvinACelsius";
            case 5: return "fahrenheitAKelvin";
            case 6: return "kelvinAFahrenheit";
            default: return null;
        }
    }
    
    private String obtenerOperacionLongitud(int opcion) {
        switch (opcion) {
            case 1: return "metrosAPies";
            case 2: return "piesAMetros";
            case 3: return "metrosAPulgadas";
            case 4: return "pulgadasAMetros";
            case 5: return "kilometrosAMillas";
            case 6: return "millasAKilometros";
            default: return null;
        }
    }
    
    private String obtenerOperacionPeso(int opcion) {
        switch (opcion) {
            case 1: return "kilogramosALibras";
            case 2: return "librasAKilogramos";
            case 3: return "gramosAOnzas";
            case 4: return "onzasAGramos";
            default: return null;
        }
    }
    
    private String obtenerOperacionVolumen(int opcion) {
        switch (opcion) {
            case 1: return "litrosAGalones";
            case 2: return "galonesALitros";
            case 3: return "mililitrosAOnzasFluidas";
            case 4: return "onzasFluidasAMililitros";
            default: return null;
        }
    }
    
    private String obtenerOperacionArea(int opcion) {
        switch (opcion) {
            case 1: return "metrosCuadradosAPiesCuadrados";
            case 2: return "piesCuadradosAMetrosCuadrados";
            case 3: return "hectareasAAcres";
            case 4: return "acresAHectareas";
            default: return null;
        }
    }
    
    /**
     * Cierra los recursos
     */
    private void cerrarRecursos() {
        if (vistaMenu != null) {
            vistaMenu.cerrar();
        }
        if (autenticador != null) {
            autenticador.cerrar();
        }
    }
}