package ec.edu.monster.controlador;

import ec.edu.monster.modelo.Conversion;
import ec.edu.monster.servicios.ClienteConversionSOAP;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.io.PrintWriter;

/**
 * Controlador principal para la aplicación web
 * @author ACER NITRO V15
 */
@WebServlet(name = "ControladorWeb", urlPatterns = {"/ControladorWeb", "/convertir"})
public class ControladorWeb extends HttpServlet {
    
    private ClienteConversionSOAP clienteSOAP;
    
    @Override
    public void init() throws ServletException {
        super.init();
        // NO inicializar el cliente SOAP aquí para evitar que bloquee la carga
        // Se inicializará solo cuando sea necesario (lazy initialization)
        System.out.println("INFO: ControladorWeb inicializado correctamente");
    }
    
    /**
     * Obtiene el cliente SOAP (lazy initialization)
     * Solo se crea cuando realmente se necesita
     */
    private ClienteConversionSOAP getClienteSOAP() {
        if (clienteSOAP == null) {
            clienteSOAP = new ClienteConversionSOAP();
        }
        return clienteSOAP;
    }
    
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        
        // Verificar si hay sesión activa
        HttpSession session = request.getSession(false);
        if (session == null || session.getAttribute("usuario") == null) {
            response.sendRedirect("login.jsp");
            return;
        }
        
        // Si hay sesión, redirigir al sistema principal
        response.sendRedirect("index.jsp");
    }
    
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        
        // Configurar respuesta JSON
        response.setContentType("application/json");
        response.setCharacterEncoding("UTF-8");
        response.setStatus(HttpServletResponse.SC_OK);
        
        PrintWriter out = null;
        
        try {
            out = response.getWriter();
            
            // Debug: Imprimir todos los parámetros recibidos
            System.out.println("=== PETICIÓN POST RECIBIDA ===");
            System.out.println("Parámetros recibidos:");
            request.getParameterMap().forEach((key, values) -> {
                System.out.println("  " + key + " = " + String.join(", ", values));
            });
            
            // Obtener parámetros de la petición
            String accion = request.getParameter("accion");
            System.out.println("Acción solicitada: " + accion);
            
            if (accion == null || accion.trim().isEmpty()) {
                System.out.println("ERROR: Acción no especificada");
                enviarError(out, "Acción no especificada");
                return;
            }
            
            switch (accion) {
                case "login":
                    System.out.println("Procesando login...");
                    procesarLogin(request, response, out);
                    break;
                case "logout":
                    System.out.println("Procesando logout...");
                    procesarLogout(request, response, out);
                    break;
                case "convertir":
                    System.out.println("Procesando conversión...");
                    procesarConversion(request, response, out);
                    break;
                case "verificarServicio":
                    System.out.println("Verificando servicio SOAP...");
                    verificarServicio(request, response, out);
                    break;
                default:
                    System.out.println("ERROR: Acción no válida: " + accion);
                    enviarError(out, "Acción no válida: " + accion);
                    break;
            }
            
        } catch (Exception e) {
            System.err.println("ERROR EN CONTROLADOR: " + e.getMessage());
            e.printStackTrace();
            
            // Asegurar que siempre se devuelva JSON válido
            if (out != null) {
                try {
                    enviarError(out, "Error interno del servidor: " + e.getMessage());
                } catch (Exception ex) {
                    System.err.println("Error al enviar respuesta de error: " + ex.getMessage());
                    response.setStatus(HttpServletResponse.SC_INTERNAL_SERVER_ERROR);
                    response.setContentType("text/plain");
                    response.getWriter().print("Error interno del servidor");
                }
            } else {
                response.setStatus(HttpServletResponse.SC_INTERNAL_SERVER_ERROR);
                response.setContentType("text/plain");
                response.getWriter().print("Error interno del servidor");
            }
        } finally {
            if (out != null) {
                out.flush();
                out.close();
            }
        }
    }
    
    /**
     * Procesa el login del usuario
     */
    private void procesarLogin(HttpServletRequest request, HttpServletResponse response, PrintWriter out) {
        try {
            String usuario = request.getParameter("usuario");
            String contrasena = request.getParameter("contrasena");
            
            System.out.println("Usuario: " + usuario);
            System.out.println("Contraseña: " + (contrasena != null ? "***" : "null"));
            
            if (usuario == null || contrasena == null) {
                out.print("{\"exitoso\": false, \"mensaje\": \"Usuario y contraseña son requeridos\"}");
                return;
            }
            
            if ("MONSTER".equals(usuario) && "MONSTER9".equals(contrasena)) {
                // Crear sesión
                HttpSession session = request.getSession(true);
                session.setAttribute("usuario", usuario);
                session.setMaxInactiveInterval(30 * 60); // 30 minutos
                
                System.out.println("Login exitoso para usuario: " + usuario);
                out.print("{\"exitoso\": true, \"mensaje\": \"Login exitoso\"}");
            } else {
                System.out.println("Credenciales incorrectas");
                out.print("{\"exitoso\": false, \"mensaje\": \"Usuario o contraseña incorrectos\"}");
            }
        } catch (Exception e) {
            System.err.println("Error en procesarLogin: " + e.getMessage());
            e.printStackTrace();
            out.print("{\"exitoso\": false, \"mensaje\": \"Error al procesar el login: " + e.getMessage() + "\"}");
        }
    }
    
    /**
     * Procesa el logout del usuario
     */
    private void procesarLogout(HttpServletRequest request, HttpServletResponse response, PrintWriter out) {
        try {
            HttpSession session = request.getSession(false);
            if (session != null) {
                session.invalidate();
            }
            out.print("{\"exitoso\": true, \"mensaje\": \"Sesión cerrada\"}");
        } catch (Exception e) {
            System.err.println("Error en procesarLogout: " + e.getMessage());
            e.printStackTrace();
            out.print("{\"exitoso\": false, \"mensaje\": \"Error al cerrar sesión: " + e.getMessage() + "\"}");
        }
    }
    
    /**
     * Procesa las conversiones
     */
    private void procesarConversion(HttpServletRequest request, HttpServletResponse response, PrintWriter out) {
        try {
            String tipoConversion = request.getParameter("tipo");
            String operacion = request.getParameter("operacion");
            String valorStr = request.getParameter("valor");
            
            System.out.println("Parámetros de conversión:");
            System.out.println("  tipo: " + tipoConversion);
            System.out.println("  operacion: " + operacion);
            System.out.println("  valor: " + valorStr);
            
            if (tipoConversion == null || operacion == null || valorStr == null) {
                System.out.println("ERROR: Parámetros incompletos");
                enviarError(out, "Parámetros incompletos");
                return;
            }
            
            double valor = Double.parseDouble(valorStr);
            System.out.println("Valor parseado: " + valor);
            
            Conversion resultado = ejecutarConversion(tipoConversion, operacion, valor);
            System.out.println("Resultado obtenido: " + resultado.getResultadoFormateado());
            
            out.print(resultado.toJSON());
            
        } catch (NumberFormatException e) {
            System.out.println("ERROR: Valor no numérico");
            enviarError(out, "El valor debe ser numérico");
        } catch (Exception e) {
            System.out.println("ERROR: " + e.getMessage());
            enviarError(out, "Error en la conversión: " + e.getMessage());
        }
    }
    
    /**
     * Ejecuta la conversión según el tipo y operación
     */
    private Conversion ejecutarConversion(String tipoConversion, String operacion, double valor) {
        Conversion resultado = new Conversion();
        
        try {
            switch (tipoConversion) {
                case "temperatura":
                    resultado = convertirTemperatura(operacion, valor);
                    break;
                case "longitud":
                    resultado = convertirLongitud(operacion, valor);
                    break;
                case "peso":
                    resultado = convertirPeso(operacion, valor);
                    break;
                case "volumen":
                    resultado = convertirVolumen(operacion, valor);
                    break;
                case "area":
                    resultado = convertirArea(operacion, valor);
                    break;
                default:
                    resultado.setExitosa(false);
                    resultado.setMensajeError("Tipo de conversión no válido");
                    break;
            }
        } catch (Exception e) {
            resultado.setExitosa(false);
            resultado.setMensajeError("Error en la conversión: " + e.getMessage());
        }
        
        return resultado;
    }
    
    /**
     * Convierte temperatura
     */
    private Conversion convertirTemperatura(String operacion, double valor) {
        switch (operacion) {
            case "celsiusAFahrenheit":
                return getClienteSOAP().celsiusAFahrenheit(valor);
            case "fahrenheitACelsius":
                return getClienteSOAP().fahrenheitACelsius(valor);
            case "celsiusAKelvin":
                return getClienteSOAP().celsiusAKelvin(valor);
            case "kelvinACelsius":
                return getClienteSOAP().kelvinACelsius(valor);
            case "fahrenheitAKelvin":
                return getClienteSOAP().fahrenheitAKelvin(valor);
            case "kelvinAFahrenheit":
                return getClienteSOAP().kelvinAFahrenheit(valor);
            default:
                Conversion error = new Conversion();
                error.setExitosa(false);
                error.setMensajeError("Operación de temperatura no válida: " + operacion);
                return error;
        }
    }
    
    /**
     * Convierte longitud
     */
    private Conversion convertirLongitud(String operacion, double valor) {
        switch (operacion) {
            case "metrosAPies":
                return getClienteSOAP().metrosAPies(valor);
            case "piesAMetros":
                return getClienteSOAP().piesAMetros(valor);
            case "metrosAPulgadas":
                return getClienteSOAP().metrosAPulgadas(valor);
            case "pulgadasAMetros":
                return getClienteSOAP().pulgadasAMetros(valor);
            case "kilometrosAMillas":
                return getClienteSOAP().kilometrosAMillas(valor);
            case "millasAKilometros":
                return getClienteSOAP().millasAKilometros(valor);
            default:
                Conversion error = new Conversion();
                error.setExitosa(false);
                error.setMensajeError("Operación de longitud no válida: " + operacion);
                return error;
        }
    }
    
    /**
     * Convierte peso
     */
    private Conversion convertirPeso(String operacion, double valor) {
        switch (operacion) {
            case "kilogramosALibras":
                return getClienteSOAP().kilogramosALibras(valor);
            case "librasAKilogramos":
                return getClienteSOAP().librasAKilogramos(valor);
            case "gramosAOnzas":
                return getClienteSOAP().gramosAOnzas(valor);
            case "onzasAGramos":
                return getClienteSOAP().onzasAGramos(valor);
            default:
                Conversion error = new Conversion();
                error.setExitosa(false);
                error.setMensajeError("Operación de peso no válida: " + operacion);
                return error;
        }
    }
    
    /**
     * Convierte volumen
     */
    private Conversion convertirVolumen(String operacion, double valor) {
        switch (operacion) {
            case "litrosAGalones":
                return getClienteSOAP().litrosAGalones(valor);
            case "galonesALitros":
                return getClienteSOAP().galonesALitros(valor);
            case "mililitrosAOnzasFluidas":
                return getClienteSOAP().mililitrosAOnzasFluidas(valor);
            case "onzasFluidasAMililitros":
                return getClienteSOAP().onzasFluidasAMililitros(valor);
            default:
                Conversion error = new Conversion();
                error.setExitosa(false);
                error.setMensajeError("Operación de volumen no válida: " + operacion);
                return error;
        }
    }
    
    /**
     * Convierte área
     */
    private Conversion convertirArea(String operacion, double valor) {
        switch (operacion) {
            case "metrosCuadradosAPiesCuadrados":
                return getClienteSOAP().metrosCuadradosAPiesCuadrados(valor);
            case "piesCuadradosAMetrosCuadrados":
                return getClienteSOAP().piesCuadradosAMetrosCuadrados(valor);
            case "hectareasAAcres":
                return getClienteSOAP().hectareasAAcres(valor);
            case "acresAHectareas":
                return getClienteSOAP().acresAHectareas(valor);
            default:
                Conversion error = new Conversion();
                error.setExitosa(false);
                error.setMensajeError("Operación de área no válida: " + operacion);
                return error;
        }
    }
    
    
    /**
     * Verifica si el servicio SOAP está disponible
     */
    private void verificarServicio(HttpServletRequest request, HttpServletResponse response, PrintWriter out) {
        try {
            // Intentar una conversión simple para verificar
            Conversion test = getClienteSOAP().celsiusAFahrenheit(0.0);
            boolean disponible = test.isExitosa() || test.getMensajeError() != null;
            
            out.print("{\"disponible\": " + disponible + "}");
        } catch (Exception e) {
            System.err.println("Error al verificar servicio: " + e.getMessage());
            out.print("{\"disponible\": false}");
        }
    }
    
    /**
     * Envía un error en formato JSON
     */
    private void enviarError(PrintWriter out, String mensaje) {
        try {
            // Escapar comillas y caracteres especiales para JSON
            String mensajeEscapado = mensaje.replace("\"", "\\\"")
                                           .replace("\n", "\\n")
                                           .replace("\r", "\\r")
                                           .replace("\\", "\\\\");
            out.print("{\"exitoso\": false, \"mensaje\": \"" + mensajeEscapado + "\"}");
        } catch (Exception e) {
            System.err.println("Error al enviar error: " + e.getMessage());
            out.print("{\"exitoso\": false, \"mensaje\": \"Error interno\"}");
        }
    }
}