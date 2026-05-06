package ec.edu.monster.servicio;

import ec.edu.monster.modelo.ConversionRequest;
import ec.edu.monster.modelo.ConversionResponse;
import jakarta.ws.rs.client.Client;
import jakarta.ws.rs.client.ClientBuilder;
import jakarta.ws.rs.client.Entity;
import jakarta.ws.rs.client.WebTarget;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

import java.io.InputStream;
import java.util.Properties;

/**
 * Cliente RESTful para consumir el servicio de conversiones
 * Equivalente al ClienteConversionSOAP pero usando REST
 * @author ACER NITRO V15
 */
public class ConversionApiClient {
    
    private static final String CONFIG_FILE = "config.properties";
    private static final String API_BASE_URL_KEY = "api.base.url";
    private final Client client;
    private final WebTarget baseTarget;
    private final String baseUrl;
    
    public ConversionApiClient() {
        this.baseUrl = cargarUrlDesdeConfiguracion();
        if (baseUrl == null || baseUrl.trim().isEmpty()) {
            throw new RuntimeException("Error: La URL del servicio API no está configurada en config.properties");
        }
        this.client = ClientBuilder.newClient();
        this.baseTarget = client.target(baseUrl);
    }
    
    /**
     * Carga la URL base del servicio desde el archivo de configuración
     */
    private String cargarUrlDesdeConfiguracion() {
        try {
            Properties props = new Properties();
            InputStream is = getClass().getClassLoader().getResourceAsStream(CONFIG_FILE);
            
            if (is == null) {
                System.err.println("Advertencia: No se encontró el archivo " + CONFIG_FILE);
                return null;
            }
            
            props.load(is);
            is.close();
            
            String url = props.getProperty(API_BASE_URL_KEY);
            if (url != null) {
                url = url.trim();
            }
            return url;
        } catch (Exception e) {
            System.err.println("Error al cargar la configuración: " + e.getMessage());
            return null;
        }
    }
    
    // Conversiones de temperatura
    public ConversionResponse convertirTemperatura(String operacion, double valor) {
        try {
            String unidadOrigen = "";
            String unidadDestino = "";
            String categoria = "temperatura";
            
            switch (operacion) {
                case "celsiusAFahrenheit":
                    unidadOrigen = "celsius";
                    unidadDestino = "fahrenheit";
                    break;
                case "fahrenheitACelsius":
                    unidadOrigen = "fahrenheit";
                    unidadDestino = "celsius";
                    break;
                case "celsiusAKelvin":
                    unidadOrigen = "celsius";
                    unidadDestino = "kelvin";
                    break;
                case "kelvinACelsius":
                    unidadOrigen = "kelvin";
                    unidadDestino = "celsius";
                    break;
                case "fahrenheitAKelvin":
                    unidadOrigen = "fahrenheit";
                    unidadDestino = "kelvin";
                    break;
                case "kelvinAFahrenheit":
                    unidadOrigen = "kelvin";
                    unidadDestino = "fahrenheit";
                    break;
                default:
                    ConversionResponse error = new ConversionResponse();
                    error.setExito(false);
                    error.setMensaje("Operación no válida");
                    return error;
            }
            
            ConversionRequest request = new ConversionRequest(valor, unidadOrigen, unidadDestino, categoria);
            String url = baseUrl + "/convertir";
            
            System.out.println("\n=========================================");
            System.out.println("LLAMADA REST API - TEMPERATURA");
            System.out.println("=========================================");
            System.out.println("URL: " + url);
            System.out.println("REQUEST JSON:");
            System.out.println("   {");
            System.out.println("     \"valor\": " + valor + ",");
            System.out.println("     \"unidadOrigen\": \"" + unidadOrigen + "\",");
            System.out.println("     \"unidadDestino\": \"" + unidadDestino + "\",");
            System.out.println("     \"categoria\": \"" + categoria + "\"");
            System.out.println("   }");
            System.out.println("Enviando POST a REST API...");
            
            Response response = baseTarget.path("/convertir")
                    .request(MediaType.APPLICATION_JSON)
                    .post(Entity.entity(request, MediaType.APPLICATION_JSON));
            
            System.out.println("Status Code: " + response.getStatus());
            
            if (response.getStatus() == 200) {
                ConversionResponse result = response.readEntity(ConversionResponse.class);
                System.out.println("RESPUESTA EXITOSA:");
                System.out.println("   {");
                System.out.println("     \"exito\": " + result.isExito() + ",");
                System.out.println("     \"valorOriginal\": " + result.getValorOriginal() + ",");
                System.out.println("     \"valorConvertido\": " + result.getValorConvertido() + ",");
                System.out.println("     \"unidadOrigen\": \"" + result.getUnidadOrigen() + "\",");
                System.out.println("     \"unidadDestino\": \"" + result.getUnidadDestino() + "\"");
                System.out.println("   }");
                System.out.println("=========================================\n");
                return result;
            } else {
                System.out.println("ERROR DEL SERVIDOR - Status: " + response.getStatus());
                ConversionResponse error = new ConversionResponse();
                error.setExito(false);
                error.setMensaje("Algo monstruoso ocurrió. El servidor respondió con un error (" + response.getStatus() + "). Inténtalo más tarde.");
                System.out.println("=========================================\n");
                return error;
            }
        } catch (Exception e) {
            System.out.println("\nERROR DE CONEXION REST");
            System.out.println("El servidor no responde. Verifica que este encendido en:");
            System.out.println("URL: " + baseUrl);
            System.out.println("Detalle: " + e.getMessage());
            System.out.println("=========================================\n");
            ConversionResponse error = new ConversionResponse();
            error.setExito(false);
            error.setMensaje("Algo monstruoso pasó. El servidor está temporalmente inactivo. Inténtalo más tarde.");
            return error;
        }
    }
    
    // Conversiones de longitud
    public ConversionResponse convertirLongitud(String operacion, double valor) {
        try {
            String unidadOrigen = "";
            String unidadDestino = "";
            String categoria = "longitud";
            
            switch (operacion) {
                case "metrosAPies":
                    unidadOrigen = "metros";
                    unidadDestino = "pies";
                    break;
                case "piesAMetros":
                    unidadOrigen = "pies";
                    unidadDestino = "metros";
                    break;
                case "metrosAPulgadas":
                    unidadOrigen = "metros";
                    unidadDestino = "pulgadas";
                    break;
                case "pulgadasAMetros":
                    unidadOrigen = "pulgadas";
                    unidadDestino = "metros";
                    break;
                case "kilometrosAMillas":
                    unidadOrigen = "kilometros";
                    unidadDestino = "millas";
                    break;
                case "millasAKilometros":
                    unidadOrigen = "millas";
                    unidadDestino = "kilometros";
                    break;
                default:
                    ConversionResponse error = new ConversionResponse();
                    error.setExito(false);
                    error.setMensaje("Operación no válida");
                    return error;
            }
            
            ConversionRequest request = new ConversionRequest(valor, unidadOrigen, unidadDestino, categoria);
            String url = baseUrl + "/convertir";
            
            System.out.println("\n=========================================");
            System.out.println("LLAMADA REST API - LONGITUD");
            System.out.println("=========================================");
            System.out.println("URL: " + url);
            System.out.println("REQUEST JSON:");
            System.out.println("   {");
            System.out.println("     \"valor\": " + valor + ",");
            System.out.println("     \"unidadOrigen\": \"" + unidadOrigen + "\",");
            System.out.println("     \"unidadDestino\": \"" + unidadDestino + "\",");
            System.out.println("     \"categoria\": \"" + categoria + "\"");
            System.out.println("   }");
            System.out.println("Enviando POST a REST API...");
            
            Response response = baseTarget.path("/convertir")
                    .request(MediaType.APPLICATION_JSON)
                    .post(Entity.entity(request, MediaType.APPLICATION_JSON));
            
            System.out.println("Status Code: " + response.getStatus());
            
            if (response.getStatus() == 200) {
                ConversionResponse result = response.readEntity(ConversionResponse.class);
                System.out.println("RESPUESTA EXITOSA:");
                System.out.println("   {");
                System.out.println("     \"exito\": " + result.isExito() + ",");
                System.out.println("     \"valorOriginal\": " + result.getValorOriginal() + ",");
                System.out.println("     \"valorConvertido\": " + result.getValorConvertido() + ",");
                System.out.println("     \"unidadOrigen\": \"" + result.getUnidadOrigen() + "\",");
                System.out.println("     \"unidadDestino\": \"" + result.getUnidadDestino() + "\"");
                System.out.println("   }");
                System.out.println("=========================================\n");
                return result;
            } else {
                System.out.println("ERROR DEL SERVIDOR - Status: " + response.getStatus());
                ConversionResponse error = new ConversionResponse();
                error.setExito(false);
                error.setMensaje("Algo monstruoso ocurrió. El servidor respondió con un error (" + response.getStatus() + "). Inténtalo más tarde.");
                System.out.println("=========================================\n");
                return error;
            }
        } catch (Exception e) {
            System.out.println("\nERROR DE CONEXION REST");
            System.out.println("El servidor no responde. Verifica que este encendido en:");
            System.out.println("URL: " + baseUrl);
            System.out.println("Detalle: " + e.getMessage());
            System.out.println("=========================================\n");
            ConversionResponse error = new ConversionResponse();
            error.setExito(false);
            error.setMensaje("Algo monstruoso pasó. El servidor está temporalmente inactivo. Inténtalo más tarde.");
            return error;
        }
    }
    
    // Conversiones de peso/masa
    public ConversionResponse convertirPeso(String operacion, double valor) {
        try {
            String unidadOrigen = "";
            String unidadDestino = "";
            String categoria = "peso";
            
            switch (operacion) {
                case "kilogramosALibras":
                    unidadOrigen = "kilogramos";
                    unidadDestino = "libras";
                    break;
                case "librasAKilogramos":
                    unidadOrigen = "libras";
                    unidadDestino = "kilogramos";
                    break;
                case "gramosAOnzas":
                    unidadOrigen = "gramos";
                    unidadDestino = "onzas";
                    break;
                case "onzasAGramos":
                    unidadOrigen = "onzas";
                    unidadDestino = "gramos";
                    break;
                default:
                    ConversionResponse error = new ConversionResponse();
                    error.setExito(false);
                    error.setMensaje("Operación no válida");
                    return error;
            }
            
            ConversionRequest request = new ConversionRequest(valor, unidadOrigen, unidadDestino, categoria);
            String url = baseUrl + "/convertir";
            
            System.out.println("\n=========================================");
            System.out.println("LLAMADA REST API - PESO");
            System.out.println("=========================================");
            System.out.println("URL: " + url);
            System.out.println("REQUEST JSON:");
            System.out.println("   {");
            System.out.println("     \"valor\": " + valor + ",");
            System.out.println("     \"unidadOrigen\": \"" + unidadOrigen + "\",");
            System.out.println("     \"unidadDestino\": \"" + unidadDestino + "\",");
            System.out.println("     \"categoria\": \"" + categoria + "\"");
            System.out.println("   }");
            System.out.println("Enviando POST a REST API...");
            
            Response response = baseTarget.path("/convertir")
                    .request(MediaType.APPLICATION_JSON)
                    .post(Entity.entity(request, MediaType.APPLICATION_JSON));
            
            System.out.println("Status Code: " + response.getStatus());
            
            if (response.getStatus() == 200) {
                ConversionResponse result = response.readEntity(ConversionResponse.class);
                System.out.println("RESPUESTA EXITOSA:");
                System.out.println("   {");
                System.out.println("     \"exito\": " + result.isExito() + ",");
                System.out.println("     \"valorOriginal\": " + result.getValorOriginal() + ",");
                System.out.println("     \"valorConvertido\": " + result.getValorConvertido() + ",");
                System.out.println("     \"unidadOrigen\": \"" + result.getUnidadOrigen() + "\",");
                System.out.println("     \"unidadDestino\": \"" + result.getUnidadDestino() + "\"");
                System.out.println("   }");
                System.out.println("=========================================\n");
                return result;
            } else {
                System.out.println("ERROR DEL SERVIDOR - Status: " + response.getStatus());
                ConversionResponse error = new ConversionResponse();
                error.setExito(false);
                error.setMensaje("Algo monstruoso ocurrió. El servidor respondió con un error (" + response.getStatus() + "). Inténtalo más tarde.");
                System.out.println("=========================================\n");
                return error;
            }
        } catch (Exception e) {
            System.out.println("\nERROR DE CONEXION REST");
            System.out.println("El servidor no responde. Verifica que este encendido en:");
            System.out.println("URL: " + baseUrl);
            System.out.println("Detalle: " + e.getMessage());
            System.out.println("=========================================\n");
            ConversionResponse error = new ConversionResponse();
            error.setExito(false);
            error.setMensaje("Algo monstruoso pasó. El servidor está temporalmente inactivo. Inténtalo más tarde.");
            return error;
        }
    }
    
    // Conversiones de volumen
    public ConversionResponse convertirVolumen(String operacion, double valor) {
        try {
            String unidadOrigen = "";
            String unidadDestino = "";
            String categoria = "volumen";
            
            switch (operacion) {
                case "litrosAGalones":
                    unidadOrigen = "litros";
                    unidadDestino = "galones";
                    break;
                case "galonesALitros":
                    unidadOrigen = "galones";
                    unidadDestino = "litros";
                    break;
                case "mililitrosAOnzasFluidas":
                    unidadOrigen = "mililitros";
                    unidadDestino = "onzasFluidas";
                    break;
                case "onzasFluidasAMililitros":
                    unidadOrigen = "onzasFluidas";
                    unidadDestino = "mililitros";
                    break;
                default:
                    ConversionResponse error = new ConversionResponse();
                    error.setExito(false);
                    error.setMensaje("Operación no válida");
                    return error;
            }
            
            ConversionRequest request = new ConversionRequest(valor, unidadOrigen, unidadDestino, categoria);
            String url = baseUrl + "/convertir";
            
            System.out.println("\n=========================================");
            System.out.println("LLAMADA REST API - VOLUMEN");
            System.out.println("=========================================");
            System.out.println("URL: " + url);
            System.out.println("REQUEST JSON:");
            System.out.println("   {");
            System.out.println("     \"valor\": " + valor + ",");
            System.out.println("     \"unidadOrigen\": \"" + unidadOrigen + "\",");
            System.out.println("     \"unidadDestino\": \"" + unidadDestino + "\",");
            System.out.println("     \"categoria\": \"" + categoria + "\"");
            System.out.println("   }");
            System.out.println("Enviando POST a REST API...");
            
            Response response = baseTarget.path("/convertir")
                    .request(MediaType.APPLICATION_JSON)
                    .post(Entity.entity(request, MediaType.APPLICATION_JSON));
            
            System.out.println("Status Code: " + response.getStatus());
            
            if (response.getStatus() == 200) {
                ConversionResponse result = response.readEntity(ConversionResponse.class);
                System.out.println("RESPUESTA EXITOSA:");
                System.out.println("   {");
                System.out.println("     \"exito\": " + result.isExito() + ",");
                System.out.println("     \"valorOriginal\": " + result.getValorOriginal() + ",");
                System.out.println("     \"valorConvertido\": " + result.getValorConvertido() + ",");
                System.out.println("     \"unidadOrigen\": \"" + result.getUnidadOrigen() + "\",");
                System.out.println("     \"unidadDestino\": \"" + result.getUnidadDestino() + "\"");
                System.out.println("   }");
                System.out.println("=========================================\n");
                return result;
            } else {
                System.out.println("ERROR DEL SERVIDOR - Status: " + response.getStatus());
                ConversionResponse error = new ConversionResponse();
                error.setExito(false);
                error.setMensaje("Algo monstruoso ocurrió. El servidor respondió con un error (" + response.getStatus() + "). Inténtalo más tarde.");
                System.out.println("=========================================\n");
                return error;
            }
        } catch (Exception e) {
            System.out.println("\nERROR DE CONEXION REST");
            System.out.println("El servidor no responde. Verifica que este encendido en:");
            System.out.println("URL: " + baseUrl);
            System.out.println("Detalle: " + e.getMessage());
            System.out.println("=========================================\n");
            ConversionResponse error = new ConversionResponse();
            error.setExito(false);
            error.setMensaje("Algo monstruoso pasó. El servidor está temporalmente inactivo. Inténtalo más tarde.");
            return error;
        }
    }
    
    // Conversiones de área
    public ConversionResponse convertirArea(String operacion, double valor) {
        try {
            String unidadOrigen = "";
            String unidadDestino = "";
            String categoria = "area";
            
            switch (operacion) {
                case "metrosCuadradosAPiesCuadrados":
                    unidadOrigen = "metrosCuadrados";
                    unidadDestino = "piesCuadrados";
                    break;
                case "piesCuadradosAMetrosCuadrados":
                    unidadOrigen = "piesCuadrados";
                    unidadDestino = "metrosCuadrados";
                    break;
                case "hectareasAAcres":
                    unidadOrigen = "hectareas";
                    unidadDestino = "acres";
                    break;
                case "acresAHectareas":
                    unidadOrigen = "acres";
                    unidadDestino = "hectareas";
                    break;
                default:
                    ConversionResponse error = new ConversionResponse();
                    error.setExito(false);
                    error.setMensaje("Operación no válida");
                    return error;
            }
            
            ConversionRequest request = new ConversionRequest(valor, unidadOrigen, unidadDestino, categoria);
            String url = baseUrl + "/convertir";
            
            System.out.println("\n=========================================");
            System.out.println("LLAMADA REST API - AREA");
            System.out.println("=========================================");
            System.out.println("URL: " + url);
            System.out.println("REQUEST JSON:");
            System.out.println("   {");
            System.out.println("     \"valor\": " + valor + ",");
            System.out.println("     \"unidadOrigen\": \"" + unidadOrigen + "\",");
            System.out.println("     \"unidadDestino\": \"" + unidadDestino + "\",");
            System.out.println("     \"categoria\": \"" + categoria + "\"");
            System.out.println("   }");
            System.out.println("Enviando POST a REST API...");
            
            Response response = baseTarget.path("/convertir")
                    .request(MediaType.APPLICATION_JSON)
                    .post(Entity.entity(request, MediaType.APPLICATION_JSON));
            
            System.out.println("Status Code: " + response.getStatus());
            
            if (response.getStatus() == 200) {
                ConversionResponse result = response.readEntity(ConversionResponse.class);
                System.out.println("RESPUESTA EXITOSA:");
                System.out.println("   {");
                System.out.println("     \"exito\": " + result.isExito() + ",");
                System.out.println("     \"valorOriginal\": " + result.getValorOriginal() + ",");
                System.out.println("     \"valorConvertido\": " + result.getValorConvertido() + ",");
                System.out.println("     \"unidadOrigen\": \"" + result.getUnidadOrigen() + "\",");
                System.out.println("     \"unidadDestino\": \"" + result.getUnidadDestino() + "\"");
                System.out.println("   }");
                System.out.println("=========================================\n");
                return result;
            } else {
                System.out.println("ERROR DEL SERVIDOR - Status: " + response.getStatus());
                ConversionResponse error = new ConversionResponse();
                error.setExito(false);
                error.setMensaje("Error del servidor: " + response.getStatus());
                System.out.println("=========================================\n");
                return error;
            }
        } catch (Exception e) {
            System.out.println("\nERROR DE CONEXION REST");
            System.out.println("El servidor no responde. Verifica que este encendido en:");
            System.out.println("URL: " + baseUrl);
            System.out.println("Detalle: " + e.getMessage());
            System.out.println("=========================================\n");
            ConversionResponse error = new ConversionResponse();
            error.setExito(false);
            error.setMensaje("ERROR DE CONEXION REST\n" +
                            "El servidor no responde. Verifica que este encendido en:\n" +
                            "URL: " + baseUrl + "\n" +
                            "Detalle: " + e.getMessage());
            return error;
        }
    }
    
    public void close() {
        if (client != null) {
            client.close();
        }
    }
}