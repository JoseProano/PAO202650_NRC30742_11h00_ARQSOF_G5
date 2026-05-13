package ec.edu.monster.servicios;

import ec.edu.monster.modelo.Conversion;
import ec.edu.monster.servicios.generated.WSConversion;
import ec.edu.monster.servicios.generated.WSConversion_Service;
import jakarta.xml.ws.WebServiceException;

/**
 * Cliente SOAP para consumir el servicio de conversiones
 * @author ACER NITRO V15
 */
public class ClienteConversionSOAP {
    
    // URL del servicio
    private static final String URL_SERVICIO = "http://192.168.5.75:8080/CONUNI_SOAP_JAVA_GR09/WSConversion?wsdl";
    
    private WSConversion servicio;
    
    public ClienteConversionSOAP() {
        try {
            // Crear la instancia del servicio SOAP
            WSConversion_Service service = new WSConversion_Service();
            this.servicio = service.getWSConversionPort();
        } catch (Exception e) {
            System.err.println("❌ ERROR: No se pudo conectar al servicio SOAP");
            System.err.println("   🔧 Solución: Asegúrate de que el servidor esté encendido en:");
            System.err.println("   📍 URL: " + URL_SERVICIO);
            System.err.println("   ⚠️  Detalle del error: " + e.getMessage());
            this.servicio = null;
        }
    }

    // ========== MÉTODO DE AUTENTICACIÓN ==========

    /**
     * Llama a la operación {@code login} del servidor SOAP para validar credenciales.
     * El cliente NUNCA almacena ni conoce las credenciales correctas.
     *
     * @param usuario   nombre de usuario ingresado
     * @param contrasena contraseña ingresada
     * @return {@code true} si el servidor confirma las credenciales
     * @throws Exception si hay error de comunicación con el servidor
     */
    public boolean login(String usuario, String contrasena) throws Exception {
        if (servicio == null) {
            throw new Exception("Servicio SOAP no disponible. Verifique que el servidor esté en línea.");
        }
        return servicio.login(usuario, contrasena);
    }


    public Conversion convertirTemperatura(String operacion, double valor) {
        if (servicio == null) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ SERVICIO SOAP NO DISPONIBLE\n" +
                                "🔧 El servidor está apagado. Enciende el servidor en:\n" +
                                "📍 " + URL_SERVICIO);
            return error;
        }
        
        try {
            double resultado = 0;
            String unidadOrigen = "";
            String unidadDestino = "";
            
            switch (operacion) {
                case "celsiusAFahrenheit":
                    resultado = servicio.celsiusAFahrenheit(valor);
                    unidadOrigen = "°C";
                    unidadDestino = "°F";
                    break;
                case "fahrenheitACelsius":
                    resultado = servicio.fahrenheitACelsius(valor);
                    unidadOrigen = "°F";
                    unidadDestino = "°C";
                    break;
                case "celsiusAKelvin":
                    resultado = servicio.celsiusAKelvin(valor);
                    unidadOrigen = "°C";
                    unidadDestino = "K";
                    break;
                case "kelvinACelsius":
                    resultado = servicio.kelvinACelsius(valor);
                    unidadOrigen = "K";
                    unidadDestino = "°C";
                    break;
                case "fahrenheitAKelvin":
                    resultado = servicio.fahrenheitAKelvin(valor);
                    unidadOrigen = "°F";
                    unidadDestino = "K";
                    break;
                case "kelvinAFahrenheit":
                    resultado = servicio.kelvinAFahrenheit(valor);
                    unidadOrigen = "K";
                    unidadDestino = "°F";
                    break;
                default:
                    return new Conversion();
            }
            
            return new Conversion(valor, unidadOrigen, unidadDestino, resultado, operacion);
            
        } catch (WebServiceException e) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ ERROR DE CONEXIÓN SOAP\n" +
                                "🔧 El servidor no responde. Verifica que esté encendido en:\n" +
                                "📍 " + URL_SERVICIO + "\n" +
                                "⚠️  Detalle: " + e.getMessage());
            return error;
        } catch (Exception e) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ ERROR EN LA CONVERSIÓN\n" +
                                "⚠️  Detalle: " + e.getMessage());
            return error;
        }
    }
    
    // Conversiones de longitud
    public Conversion convertirLongitud(String operacion, double valor) {
        if (servicio == null) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ SERVICIO SOAP NO DISPONIBLE\n" +
                                "🔧 El servidor está apagado. Enciende el servidor en:\n" +
                                "📍 " + URL_SERVICIO);
            return error;
        }
        
        try {
            double resultado = 0;
            String unidadOrigen = "";
            String unidadDestino = "";
            
            switch (operacion) {
                case "metrosAPies":
                    resultado = servicio.metrosAPies(valor);
                    unidadOrigen = "m";
                    unidadDestino = "ft";
                    break;
                case "piesAMetros":
                    resultado = servicio.piesAMetros(valor);
                    unidadOrigen = "ft";
                    unidadDestino = "m";
                    break;
                case "metrosAPulgadas":
                    resultado = servicio.metrosAPulgadas(valor);
                    unidadOrigen = "m";
                    unidadDestino = "in";
                    break;
                case "pulgadasAMetros":
                    resultado = servicio.pulgadasAMetros(valor);
                    unidadOrigen = "in";
                    unidadDestino = "m";
                    break;
                case "kilometrosAMillas":
                    resultado = servicio.kilometrosAMillas(valor);
                    unidadOrigen = "km";
                    unidadDestino = "mi";
                    break;
                case "millasAKilometros":
                    resultado = servicio.millasAKilometros(valor);
                    unidadOrigen = "mi";
                    unidadDestino = "km";
                    break;
                default:
                    return new Conversion();
            }
            
            return new Conversion(valor, unidadOrigen, unidadDestino, resultado, operacion);
            
        } catch (WebServiceException e) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ ERROR DE CONEXIÓN SOAP\n" +
                                "🔧 El servidor no responde. Verifica que esté encendido en:\n" +
                                "📍 " + URL_SERVICIO + "\n" +
                                "⚠️  Detalle: " + e.getMessage());
            return error;
        } catch (Exception e) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ ERROR EN LA CONVERSIÓN\n" +
                                "⚠️  Detalle: " + e.getMessage());
            return error;
        }
    }
    
    // Conversiones de peso/masa
    public Conversion convertirPeso(String operacion, double valor) {
        if (servicio == null) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ SERVICIO SOAP NO DISPONIBLE\n" +
                                "🔧 El servidor está apagado. Enciende el servidor en:\n" +
                                "📍 " + URL_SERVICIO);
            return error;
        }
        
        try {
            double resultado = 0;
            String unidadOrigen = "";
            String unidadDestino = "";
            
            switch (operacion) {
                case "kilogramosALibras":
                    resultado = servicio.kilogramosALibras(valor);
                    unidadOrigen = "kg";
                    unidadDestino = "lb";
                    break;
                case "librasAKilogramos":
                    resultado = servicio.librasAKilogramos(valor);
                    unidadOrigen = "lb";
                    unidadDestino = "kg";
                    break;
                case "gramosAOnzas":
                    resultado = servicio.gramosAOnzas(valor);
                    unidadOrigen = "g";
                    unidadDestino = "oz";
                    break;
                case "onzasAGramos":
                    resultado = servicio.onzasAGramos(valor);
                    unidadOrigen = "oz";
                    unidadDestino = "g";
                    break;
                default:
                    return new Conversion();
            }
            
            return new Conversion(valor, unidadOrigen, unidadDestino, resultado, operacion);
            
        } catch (WebServiceException e) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ ERROR DE CONEXIÓN SOAP\n" +
                                "🔧 El servidor no responde. Verifica que esté encendido en:\n" +
                                "📍 " + URL_SERVICIO + "\n" +
                                "⚠️  Detalle: " + e.getMessage());
            return error;
        } catch (Exception e) {
            Conversion error = new Conversion();
            error.setMensajeError("❌ ERROR EN LA CONVERSIÓN\n" +
                                "⚠️  Detalle: " + e.getMessage());
            return error;
        }
    }
}
