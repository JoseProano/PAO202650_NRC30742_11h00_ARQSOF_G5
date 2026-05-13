package ec.edu.monster.vista;

import ec.edu.monster.modelo.Conversion;
import ec.edu.monster.controlador.UtilidadesConsola;

/**
 * Vista para el menú principal y conversiones - Patrón MVC
 * Maneja la presentación del menú y resultados de conversiones
 * @author ACER NITRO V15
 */
public class VistaMenu {
    
    // Colores para la consola
    public static final String CELESTE = "\033[38;5;51m";
    public static final String VERDE = "\033[92m";
    public static final String ROJO = "\033[91m";
    public static final String AMARILLO = "\033[93m";
    public static final String RESET = "\033[0m";
    
    private java.util.Scanner scanner;
    private boolean soportaColores;
    
    /**
     * Detecta si el terminal soporta colores
     */
    private boolean detectarSoporteColores() {
        try {
            String os = System.getProperty("os.name").toLowerCase();
            
            if (os.contains("windows")) {
                // En Windows 10/11, verificar si VirtualTerminalLevel está habilitado
                // O simplemente intentar siempre, ya que Windows 10+ lo soporta
                try {
                    // Verificar si estamos en Windows Terminal, PowerShell o CMD con ANSI habilitado
                    String term = System.getenv("TERM");
                    String wtSession = System.getenv("WT_SESSION");
                    String psModulePath = System.getenv("PSModulePath");
                    
                    // Si hay WT_SESSION, estamos en Windows Terminal
                    if (wtSession != null) {
                        return true;
                    }
                    
                    // Si hay TERM configurado, usarlo
                    if (term != null && (term.contains("xterm") || term.contains("256color"))) {
                        return true;
                    }
                    
                    // Windows 10 build 1511+ soporta ANSI si está habilitado
                    // Intentar siempre en Windows, el sistema lo manejará
                    return true;
                } catch (Exception e) {
                    // Si falla, intentar de todas formas
                    return true;
                }
            }
            
            // Para Linux/Mac, verificar TERM
            String term = System.getenv("TERM");
            return term != null && !term.equals("dumb");
        } catch (Exception e) {
            // En caso de error, intentar usar colores de todas formas
            return true;
        }
    }
    
    /**
     * Aplica color solo si el terminal lo soporta
     */
    private String aplicarColor(String color, String texto) {
        return soportaColores ? color + texto + RESET : texto;
    }
    
    public VistaMenu() {
        // Detectar soporte de colores
        this.soportaColores = detectarSoporteColores();
        
        // Crear Scanner con codificacion UTF-8
        try {
            this.scanner = new java.util.Scanner(System.in, "UTF-8");
        } catch (Exception e) {
            this.scanner = new java.util.Scanner(System.in);
        }
    }
    
    /**
     * Muestra el banner principal de la aplicación
     */
    public void mostrarBanner() {
        mostrarEncabezado();
    }
    
    /**
     * Muestra el encabezado principal
     */
    private void mostrarEncabezado() {
        System.out.println();
        System.out.println(aplicarColor(CELESTE, "████████╗███████╗███╗   ███╗██████╗ ███████╗██████╗  █████╗ ████████╗██╗   ██╗██████╗  █████╗ "));
        System.out.println(aplicarColor(CELESTE, "╚══██╔══╝██╔════╝████╗ ████║██╔══██╗██╔════╝██╔══██╗██╔══██╗╚══██╔══╝██║   ██║██╔══██╗██╔══██╗"));
        System.out.println(aplicarColor(CELESTE, "   ██║   █████╗  ██╔████╔██║██████╔╝█████╗  ██████╔╝███████║   ██║   ██║   ██║██████╔╝███████║"));
        System.out.println(aplicarColor(CELESTE, "   ██║   ██╔══╝  ██║╚██╔╝██║██╔══██╗██╔══╝  ██╔══██╗██╔══██║   ██║   ██║   ██║██╔══██╗██╔══██║"));
        System.out.println(aplicarColor(CELESTE, "   ██║   ███████╗██║ ╚═╝ ██║██║  ██║███████╗██║  ██║██║  ██║   ██║   ╚██████╔╝██║  ██║██║  ██║"));
        System.out.println(aplicarColor(CELESTE, "   ╚═╝   ╚══════╝╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝"));
        System.out.println();
        System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(VERDE, "                SISTEMA DE CONVERSIÓN DE UNIDADES               "));
        System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
    }
    
    /**
     * Muestra el menú principal
     */
    public void mostrarMenuPrincipal() {
        System.out.println(aplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(CELESTE, "                         MENÚ PRINCIPAL                            "));
        System.out.println(aplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[1]") + " Conversiones de " + aplicarColor(AMARILLO, "Temperatura") + "                                         " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[2]") + " Conversiones de " + aplicarColor(AMARILLO, "Longitud") + "                                       " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[3]") + " Conversiones de " + aplicarColor(AMARILLO, "Peso/Masa") + "                                    " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[4]") + " Conversiones de " + aplicarColor(AMARILLO, "Volumen") + "                                    " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[5]") + " Conversiones de " + aplicarColor(AMARILLO, "Área") + "                                              " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[6]") + " Pruebas Automáticas                                                      " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(ROJO, "[0]") + " Salir                                                                   " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.print(aplicarColor(VERDE, "Seleccione una opción: "));
    }
    
    /**
     * Lee la opción del usuario
     */
    public int leerOpcion() {
        try {
            String entrada = scanner.nextLine().trim();
            return Integer.parseInt(entrada);
        } catch (NumberFormatException e) {
            return -1; // Para forzar mensaje "Opción no válida"
        }
    }
    
    /**
     * Muestra el menú de conversiones de temperatura
     */
    public void mostrarMenuTemperatura() {
        System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(VERDE, "                     CONVERSIONES DE TEMPERATURA                             "));
        System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.println(aplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(CELESTE, "                              MENÚ TEMPERATURA                                "));
        System.out.println(aplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[1]") + " " + aplicarColor(AMARILLO, "Celsius") + " → " + aplicarColor(AMARILLO, "Fahrenheit") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[2]") + " " + aplicarColor(AMARILLO, "Fahrenheit") + " → " + aplicarColor(AMARILLO, "Celsius") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[3]") + " " + aplicarColor(AMARILLO, "Celsius") + " → " + aplicarColor(AMARILLO, "Kelvin") + "                                          " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[4]") + " " + aplicarColor(AMARILLO, "Kelvin") + " → " + aplicarColor(AMARILLO, "Celsius") + "                                          " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[5]") + " " + aplicarColor(AMARILLO, "Fahrenheit") + " → " + aplicarColor(AMARILLO, "Kelvin") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[6]") + " " + aplicarColor(AMARILLO, "Kelvin") + " → " + aplicarColor(AMARILLO, "Fahrenheit") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.print(aplicarColor(VERDE, "Seleccione la conversión: "));
    }
    
    /**
     * Muestra el menú de conversiones de longitud
     */
    public void mostrarMenuLongitud() {
        System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(VERDE, "                    CONVERSIONES DE LONGITUD                                 "));
        System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.println(aplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(CELESTE, "                         MENÚ LONGITUD                                   "));
        System.out.println(aplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[1]") + " " + aplicarColor(AMARILLO, "Metros") + " → " + aplicarColor(AMARILLO, "Pies") + "                                            " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[2]") + " " + aplicarColor(AMARILLO, "Pies") + " → " + aplicarColor(AMARILLO, "Metros") + "                                            " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[3]") + " " + aplicarColor(AMARILLO, "Metros") + " → " + aplicarColor(AMARILLO, "Pulgadas") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[4]") + " " + aplicarColor(AMARILLO, "Pulgadas") + " → " + aplicarColor(AMARILLO, "Metros") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[5]") + " " + aplicarColor(AMARILLO, "Kilómetros") + " → " + aplicarColor(AMARILLO, "Millas") + "                                      " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[6]") + " " + aplicarColor(AMARILLO, "Millas") + " → " + aplicarColor(AMARILLO, "Kilómetros") + "                                      " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.print(aplicarColor(VERDE, "Seleccione la conversión: "));
    }
    
    /**
     * Muestra el menú de conversiones de peso/masa
     */
    public void mostrarMenuPeso() {
        System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(VERDE, "                     CONVERSIONES DE PESO/MASA                                 "));
        System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.println(aplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(CELESTE, "                         MENÚ PESO/MASA                                  "));
        System.out.println(aplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[1]") + " " + aplicarColor(AMARILLO, "Kilogramos") + " → " + aplicarColor(AMARILLO, "Libras") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[2]") + " " + aplicarColor(AMARILLO, "Libras") + " → " + aplicarColor(AMARILLO, "Kilogramos") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[3]") + " " + aplicarColor(AMARILLO, "Gramos") + " → " + aplicarColor(AMARILLO, "Onzas") + "                                            " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[4]") + " " + aplicarColor(AMARILLO, "Onzas") + " → " + aplicarColor(AMARILLO, "Gramos") + "                                            " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.print(aplicarColor(VERDE, "Seleccione la conversión: "));
    }
    
    /**
     * Muestra el menú de conversiones de volumen
     */
    public void mostrarMenuVolumen() {
        System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(VERDE, "                     CONVERSIONES DE VOLUMEN                                  "));
        System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.println(aplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(CELESTE, "                              MENÚ VOLUMEN                                    "));
        System.out.println(aplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[1]") + " " + aplicarColor(AMARILLO, "Litros") + " → " + aplicarColor(AMARILLO, "Galones") + "                                         " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[2]") + " " + aplicarColor(AMARILLO, "Galones") + " → " + aplicarColor(AMARILLO, "Litros") + "                                         " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[3]") + " " + aplicarColor(AMARILLO, "Mililitros") + " → " + aplicarColor(AMARILLO, "Onzas fluidas") + "                               " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[4]") + " " + aplicarColor(AMARILLO, "Onzas fluidas") + " → " + aplicarColor(AMARILLO, "Mililitros") + "                               " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.print(aplicarColor(VERDE, "Seleccione la conversión: "));
    }
    
    /**
     * Muestra el menú de conversiones de área
     */
    public void mostrarMenuArea() {
        System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(VERDE, "                     CONVERSIONES DE ÁREA                                     "));
        System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.println(aplicarColor(CELESTE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(CELESTE, "                              MENÚ ÁREA                                       "));
        System.out.println(aplicarColor(CELESTE, "╠══════════════════════════════════════════════════════════════════════════════╣"));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[1]") + " " + aplicarColor(AMARILLO, "Metros cuadrados") + " → " + aplicarColor(AMARILLO, "Pies cuadrados") + "                            " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[2]") + " " + aplicarColor(AMARILLO, "Pies cuadrados") + " → " + aplicarColor(AMARILLO, "Metros cuadrados") + "                            " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[3]") + " " + aplicarColor(AMARILLO, "Hectáreas") + " → " + aplicarColor(AMARILLO, "Acres") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "") + "  " + aplicarColor(VERDE, "[4]") + " " + aplicarColor(AMARILLO, "Acres") + " → " + aplicarColor(AMARILLO, "Hectáreas") + "                                        " + aplicarColor(CELESTE, ""));
        System.out.println(aplicarColor(CELESTE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.print(aplicarColor(VERDE, "Seleccione la conversión: "));
    }
    
    /**
     * Lee el valor a convertir
     */
    public double leerValor() {
        while (true) {
            System.out.println();
            System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
            System.out.println(aplicarColor(VERDE, "                         INGRESO DE VALOR                                  "));
            System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
            System.out.println();
            System.out.print(aplicarColor(AMARILLO, "Ingrese el valor a convertir: "));
            try {
                String entrada = scanner.nextLine().replace(",", "."); // reemplaza ',' por '.'
                double valor = Double.parseDouble(entrada);
                
                if (valor < 0) {
                    mostrarErrorEntrada("No se permiten valores negativos");
                    continue;
                }
                
                return valor;
            } catch (NumberFormatException e) {
                mostrarErrorEntrada("Debe ingresar un valor numérico válido");
            }
        }
    }
    
    /**
     * Muestra error de entrada
     */
    public void mostrarErrorEntrada(String mensaje) {
        System.out.println(aplicarColor(ROJO, "ERROR: " + mensaje));
    }
    
    /**
     * Muestra el resultado de una conversión
     */
    public void mostrarResultado(Conversion conversion) {
        System.out.println();
        System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(VERDE, "                             RESULTADO                                        "));
        System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        
        if (conversion.isExitosa()) {
            System.out.println("  " + aplicarColor(AMARILLO, conversion.toString()));
            System.out.println(aplicarColor(CELESTE, "Operación: " + conversion.getOperacion()));
        } else {
            System.out.println(aplicarColor(ROJO, "ERROR: " + conversion.toString()));
        }
        System.out.println();
    }
    
    /**
     * Muestra mensaje de opción inválida
     */
    public void mostrarOpcionInvalida() {
        System.out.println(aplicarColor(ROJO, "ERROR: Opción no válida. Por favor, elija una opción del menú."));
    }
    
    /**
     * Muestra mensaje de despedida
     */
    public void mostrarDespedida() {
        System.out.println();
        System.out.println(aplicarColor(VERDE, "╔══════════════════════════════════════════════════════════════════════════════╗"));
        System.out.println(aplicarColor(VERDE, "                             HASTA PRONTO                                      "));
        System.out.println(aplicarColor(VERDE, "╚══════════════════════════════════════════════════════════════════════════════╝"));
        System.out.println();
        System.out.println(aplicarColor(CELESTE, "Gracias por utilizar el sistema Monster de conversión de unidades."));
        System.out.println(aplicarColor(AMARILLO, "¡Que tenga un excelente día!"));
    }
    
    /**
     * Muestra mensaje de pausa
     */
    public void mostrarPausa() {
        System.out.print(aplicarColor(AMARILLO, "\nPresione cualquier tecla para continuar..."));
        scanner.nextLine();
    }
    
    /**
     * Muestra el banner de pruebas automáticas
     */
    public void mostrarBannerPruebas() {
        System.out.println(aplicarColor(AMARILLO, "EJECUTANDO PRUEBAS AUTOMÁTICAS..."));
        System.out.println();
    }
    
    /**
     * Muestra el título de pruebas de temperatura
     */
    public void mostrarTituloPruebasTemperatura() {
        System.out.println(aplicarColor(CELESTE, "Probando conversiones de temperatura:"));
    }
    
    /**
     * Muestra el título de pruebas de longitud
     */
    public void mostrarTituloPruebasLongitud() {
        System.out.println(aplicarColor(CELESTE, "Probando conversiones de longitud:"));
    }
    
    /**
     * Muestra el título de pruebas de peso
     */
    public void mostrarTituloPruebasPeso() {
        System.out.println(aplicarColor(CELESTE, "Probando conversiones de peso:"));
    }
    
    /**
     * Muestra mensaje de pruebas completadas
     */
    public void mostrarPruebasCompletadas() {
        System.out.println(aplicarColor(VERDE, "Pruebas automáticas completadas exitosamente!"));
    }
    
    /**
     * Muestra información sobre el soporte de colores (versión silenciosa)
     */
    public void mostrarInfoColores() {
        // No mostrar nada - información silenciosa
    }
    
    /**
     * Cierra los recursos de la vista
     */
    public void cerrar() {
        if (scanner != null) {
            scanner.close();
        }
    }
}
