package ec.edu.monster.servicios;

import ec.edu.monster.modelo.Conversion;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.net.URL;
import java.net.URLConnection;
import java.nio.charset.StandardCharsets;
import java.io.InputStream;
import java.io.OutputStream;

/**
 * Cliente SOAP para consumir el servicio de conversiones
 * @author ACER NITRO V15
 */
public class ClienteConversionSOAP {
    
    private static final String WS_URL = "http://localhost:8080/CONUNI_SOAP_JAVA_GR09/WSConversion";

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
        String soapRequest =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" " +
                                "xmlns:tns=\"http://servicios.monster.edu.ec/\">\n" +
                "    <soap:Body>\n" +
                "        <tns:login>\n" +
                "            <usuario>" + usuario + "</usuario>\n" +
                "            <contrasena>" + contrasena + "</contrasena>\n" +
                "        </tns:login>\n" +
                "    </soap:Body>\n" +
                "</soap:Envelope>";

        String soapResponse = enviarPeticionSOAP(soapRequest);

        // El servidor devuelve <return>true</return> o <return>false</return>
        int startIndex = soapResponse.indexOf("<return>");
        int endIndex   = soapResponse.indexOf("</return>");
        if (startIndex == -1 || endIndex == -1) {
            throw new Exception("Respuesta de autenticación inválida del servidor");
        }
        String resultado = soapResponse.substring(startIndex + 8, endIndex).trim();
        return Boolean.parseBoolean(resultado);
    }


    public Conversion convertirTemperatura(String operacion, double valor) {
        Conversion conversion = new Conversion("Temperatura", operacion, valor, 
            getUnidadOriginal(operacion), getUnidadDestino(operacion));
        
        try {
            double resultado = realizarConversion(operacion, valor);
            conversion.setValorConvertido(resultado);
            conversion.setExitosa(true);
        } catch (Exception e) {
            conversion.setExitosa(false);
            conversion.setMensajeError("Error en conversión de temperatura: " + e.getMessage());
        }
        
        return conversion;
    }
    
    /**
     * Realiza una conversión de longitud
     */
    public Conversion convertirLongitud(String operacion, double valor) {
        Conversion conversion = new Conversion("Longitud", operacion, valor, 
            getUnidadOriginal(operacion), getUnidadDestino(operacion));
        
        try {
            double resultado = realizarConversion(operacion, valor);
            conversion.setValorConvertido(resultado);
            conversion.setExitosa(true);
        } catch (Exception e) {
            conversion.setExitosa(false);
            conversion.setMensajeError("Error en conversión de longitud: " + e.getMessage());
        }
        
        return conversion;
    }
    
    /**
     * Realiza una conversión de peso/masa
     */
    public Conversion convertirPeso(String operacion, double valor) {
        Conversion conversion = new Conversion("Peso/Masa", operacion, valor, 
            getUnidadOriginal(operacion), getUnidadDestino(operacion));
        
        try {
            double resultado = realizarConversion(operacion, valor);
            conversion.setValorConvertido(resultado);
            conversion.setExitosa(true);
        } catch (Exception e) {
            conversion.setExitosa(false);
            conversion.setMensajeError("Error en conversión de peso: " + e.getMessage());
        }
        
        return conversion;
    }
    

    
    /**
     * Realiza la llamada SOAP al servicio web
     */
    private double realizarConversion(String operacion, double valor) throws Exception {
        // Crear el mensaje SOAP
        String soapRequest = crearSOAPRequest(operacion, valor);
        
        // Enviar la petición HTTP
        String soapResponse = enviarPeticionSOAP(soapRequest);
        
        // Parsear la respuesta
        return parsearRespuestaSOAP(soapResponse);
    }
    
    /**
     * Crea el XML SOAP request
     */
    private String crearSOAPRequest(String operacion, double valor) {
        String paramName = getParamName(operacion);
        
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
               "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tns=\"http://servicios.monster.edu.ec/\">\n" +
               "    <soap:Body>\n" +
               "        <tns:" + operacion + ">\n" +
               "            <" + paramName + ">" + valor + "</" + paramName + ">\n" +
               "        </tns:" + operacion + ">\n" +
               "    </soap:Body>\n" +
               "</soap:Envelope>";
    }
    
    /**
     * Envía la petición SOAP al servicio web
     */
    private String enviarPeticionSOAP(String soapRequest) throws Exception {
        URL url = new URL(WS_URL);
        URLConnection connection = url.openConnection();
        
        connection.setDoOutput(true);
        connection.setRequestProperty("Content-Type", "text/xml; charset=utf-8");
        connection.setRequestProperty("SOAPAction", "");
        
        // Enviar la petición
        try (OutputStream outputStream = connection.getOutputStream()) {
            outputStream.write(soapRequest.getBytes(StandardCharsets.UTF_8));
        }
        
        // Leer la respuesta
        StringBuilder response = new StringBuilder();
        try (InputStream inputStream = connection.getInputStream()) {
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = inputStream.read(buffer)) != -1) {
                response.append(new String(buffer, 0, bytesRead, StandardCharsets.UTF_8));
            }
        }
        
        return response.toString();
    }
    
    /**
     * Parsea la respuesta SOAP y extrae el resultado
     */
    private double parsearRespuestaSOAP(String soapResponse) throws Exception {
        // Buscar el tag <return>
        int startIndex = soapResponse.indexOf("<return>");
        int endIndex = soapResponse.indexOf("</return>");
        
        if (startIndex == -1 || endIndex == -1) {
            throw new Exception("No se encontró el resultado en la respuesta SOAP");
        }
        
        String resultStr = soapResponse.substring(startIndex + 8, endIndex);
        return Double.parseDouble(resultStr.trim());
    }
    
    /**
     * Obtiene el nombre del parámetro para la operación
     */
    private String getParamName(String operacion) {
        switch (operacion) {
            case "celsiusAFahrenheit":
            case "celsiusAKelvin":
                return "celsius";
            case "fahrenheitACelsius":
            case "fahrenheitAKelvin":
                return "fahrenheit";
            case "kelvinACelsius":
            case "kelvinAFahrenheit":
                return "kelvin";
            case "metrosAPies":
            case "metrosAPulgadas":
                return "metros";
            case "piesAMetros":
                return "pies";
            case "pulgadasAMetros":
                return "pulgadas";
            case "kilometrosAMillas":
                return "kilometros";
            case "millasAKilometros":
                return "millas";
            case "kilogramosALibras":
                return "kilogramos";
            case "librasAKilogramos":
                return "libras";
            case "gramosAOnzas":
                return "gramos";
            case "onzasAGramos":
                return "onzas";
            default:
                return "valor";
        }
    }
    
    /**
     * Obtiene la unidad original para la operación
     */
    private String getUnidadOriginal(String operacion) {
        if (operacion.contains("celsiusA")) return "°C";
        if (operacion.contains("fahrenheitA")) return "°F";
        if (operacion.contains("kelvinA")) return "K";
        if (operacion.contains("metrosA")) return "m";
        if (operacion.contains("piesA")) return "ft";
        if (operacion.contains("pulgadasA")) return "in";
        if (operacion.contains("kilometrosA")) return "km";
        if (operacion.contains("millasA")) return "mi";
        if (operacion.contains("kilogramosA")) return "kg";
        if (operacion.contains("librasA")) return "lb";
        if (operacion.contains("gramosA")) return "g";
        if (operacion.contains("onzasA")) return "oz";
        return "unidad";
    }
    
    /**
     * Obtiene la unidad destino para la operación
     */
    private String getUnidadDestino(String operacion) {
        if (operacion.contains("AFahrenheit")) return "°F";
        if (operacion.contains("ACelsius")) return "°C";
        if (operacion.contains("AKelvin")) return "K";
        if (operacion.contains("APies")) return "ft";
        if (operacion.contains("AMetros")) return "m";
        if (operacion.contains("APulgadas")) return "in";
        if (operacion.contains("AMillas")) return "mi";
        if (operacion.contains("AKilometros")) return "km";
        if (operacion.contains("ALibras")) return "lb";
        if (operacion.contains("AKilogramos")) return "kg";
        if (operacion.contains("AOnzas")) return "oz";
        if (operacion.contains("AGramos")) return "g";
        return "unidad";
    }
}