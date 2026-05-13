package ec.edu.monster.servicios;

import ec.edu.monster.modelo.Conversion;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;

/**
 * Cliente para consumir el servicio SOAP de conversiones
 * @author ACER NITRO V15
 */
public class ClienteConversionSOAP {
    
    private static final String BASE_URL = "http://192.168.5.75:8080/CONUNI_SOAP_JAVA_GR09/WSConversion";
    private static final int TIMEOUT = 30000; // 30 segundos
    
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

        String soapResponse = enviarPeticionSOAPRaw(soapRequest);

        // El servidor devuelve <return>true</return> o <return>false</return>
        int startIndex = soapResponse.indexOf("<return>");
        int endIndex   = soapResponse.indexOf("</return>");
        if (startIndex == -1 || endIndex == -1) {
            throw new Exception("Respuesta de autenticación inválida del servidor");
        }
        String resultado = soapResponse.substring(startIndex + 8, endIndex).trim();
        return Boolean.parseBoolean(resultado);
    }

    /**
     * Envía una petición SOAP cruda y retorna la respuesta cruda.
     */
    private String enviarPeticionSOAPRaw(String soapRequest) throws Exception {
        URL url = new URL(BASE_URL);
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        
        connection.setRequestMethod("POST");
        connection.setRequestProperty("Content-Type", "text/xml;charset=UTF-8");
        connection.setRequestProperty("Accept", "text/xml");
        connection.setDoOutput(true);
        connection.setConnectTimeout(TIMEOUT);
        connection.setReadTimeout(TIMEOUT);
        
        try (OutputStream os = connection.getOutputStream()) {
            byte[] input = soapRequest.getBytes(StandardCharsets.UTF_8);
            os.write(input, 0, input.length);
        }
        
        int responseCode = connection.getResponseCode();
        
        if (responseCode == HttpURLConnection.HTTP_OK) {
            StringBuilder response = new StringBuilder();
            try (BufferedReader br = new BufferedReader(
                    new InputStreamReader(connection.getInputStream(), StandardCharsets.UTF_8))) {
                String responseLine;
                while ((responseLine = br.readLine()) != null) {
                    response.append(responseLine.trim());
                }
            }
            return response.toString();
        } else {
            throw new Exception("Error HTTP: " + responseCode);
        }
    }

    /**
     * Realiza una llamada SOAP al servicio web
     * @param method - Nombre del método SOAP
     * @param valor - Valor a convertir
     * @return Conversion con el resultado
     */
    private Conversion llamadaSOAP(String method, double valor) {
        Conversion resultado = new Conversion();
        resultado.setValorOriginal(valor);
        resultado.setOperacion(method);
        
        try {
            System.out.println("INFO:   === INICIANDO LLAMADA SOAP ===");
            System.out.println("INFO:   Método: " + method);
            System.out.println("INFO:   Valor: " + valor);
            System.out.println("INFO:   URL: " + BASE_URL);
            
            String soapEnvelope = crearEnvelopeSOAP(method, valor);
            System.out.println("INFO:   Envelope SOAP: " + soapEnvelope);
            
            String response = enviarPeticionSOAP(soapEnvelope, method);
            System.out.println("INFO:   Resultado obtenido: " + response);
            
            if (response.startsWith("ERROR:")) {
                resultado.setExitosa(false);
                resultado.setMensajeError(response);
            } else {
                double valorConvertido = Double.parseDouble(response);
                resultado.setValorConvertido(valorConvertido);
                resultado.setExitosa(true);
                resultado.setMensajeError("");
            }
            
        } catch (Exception e) {
            System.out.println("SEVERE:   ERROR en llamada SOAP: " + e.getMessage());
            resultado.setExitosa(false);
            resultado.setMensajeError("Error de conexión: " + e.getMessage());
        }
        
        return resultado;
    }
    
    
    /**
     * Crea el envelope SOAP para la llamada
     * @param method - Nombre del método
     * @param valor - Valor a convertir
     * @return String con el envelope SOAP
     */
    private String crearEnvelopeSOAP(String method, double valor) {
        // Obtener el nombre correcto del parámetro según el método
        String paramName = getParamName(method);
        
        // Envelope SOAP con el parámetro correcto
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
               "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tns=\"http://servicios.monster.edu.ec/\">\n" +
               "    <soap:Body>\n" +
               "        <tns:" + method + ">\n" +
               "            <" + paramName + ">" + valor + "</" + paramName + ">\n" +
               "        </tns:" + method + ">\n" +
               "    </soap:Body>\n" +
               "</soap:Envelope>";
    }
    
    /**
     * Obtiene el nombre del parámetro según la operación
     * @param operacion - Nombre de la operación
     * @return String con el nombre del parámetro
     */
    private String getParamName(String operacion) {
        switch (operacion) {
            // Temperatura
            case "celsiusAFahrenheit":
            case "celsiusAKelvin":
                return "celsius";
            case "fahrenheitACelsius":
            case "fahrenheitAKelvin":
                return "fahrenheit";
            case "kelvinACelsius":
            case "kelvinAFahrenheit":
                return "kelvin";
            // Longitud
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
            // Peso
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
     * Envía la petición SOAP al servicio web
     * @param soapEnvelope - Envelope SOAP
     * @param method - Método SOAP
     * @return String con la respuesta
     * @throws Exception si hay error
     */
    private String enviarPeticionSOAP(String soapEnvelope, String method) throws Exception {
        URL url = new URL(BASE_URL);
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        
        // Configurar la conexión para POST
        connection.setRequestMethod("POST");
        connection.setDoOutput(true); // IMPORTANTE: Permite escribir en la conexión
        connection.setDoInput(true); // Permite leer la respuesta
        
        // Configurar headers
        connection.setRequestProperty("Content-Type", "text/xml; charset=utf-8");
        connection.setRequestProperty("SOAPAction", "");
        connection.setRequestProperty("Content-Length", String.valueOf(soapEnvelope.getBytes(StandardCharsets.UTF_8).length));
        
        connection.setConnectTimeout(TIMEOUT);
        connection.setReadTimeout(TIMEOUT);
        
        System.out.println("INFO:   Conectando a: " + BASE_URL);
        System.out.println("INFO:   Método HTTP: POST");
        System.out.println("INFO:   SOAPAction: " + method);
        System.out.println("INFO:   Content-Type: text/xml; charset=utf-8");
        
        // Enviar el envelope SOAP
        try (OutputStreamWriter writer = new OutputStreamWriter(connection.getOutputStream(), StandardCharsets.UTF_8)) {
            writer.write(soapEnvelope);
            writer.flush();
            System.out.println("INFO:   Envelope SOAP enviado");
        }
        
        // Verificar código de respuesta
        int responseCode = connection.getResponseCode();
        System.out.println("INFO:   Código de respuesta HTTP: " + responseCode);
        
        if (responseCode != HttpURLConnection.HTTP_OK) {
            // Leer mensaje de error
            StringBuilder errorResponse = new StringBuilder();
            try (BufferedReader errorReader = new BufferedReader(
                    new InputStreamReader(connection.getErrorStream(), StandardCharsets.UTF_8))) {
                String line;
                while ((line = errorReader.readLine()) != null) {
                    errorResponse.append(line).append("\n");
                }
            }
            throw new Exception("Error HTTP " + responseCode + ": " + errorResponse.toString());
        }
        
        // Leer respuesta exitosa
        StringBuilder response = new StringBuilder();
        try (BufferedReader reader = new BufferedReader(
                new InputStreamReader(connection.getInputStream(), StandardCharsets.UTF_8))) {
            String line;
            while ((line = reader.readLine()) != null) {
                response.append(line).append("\n");
            }
        }
        
        // Parsear respuesta SOAP
        String responseText = response.toString();
        if (responseText.contains("<return>")) {
            int start = responseText.indexOf("<return>") + 8;
            int end = responseText.indexOf("</return>");
            return responseText.substring(start, end);
        } else {
            return "ERROR: No se pudo parsear la respuesta SOAP";
        }
    }
    
    // ========== MÉTODOS DE TEMPERATURA ==========
    
    public Conversion celsiusAFahrenheit(double valor) {
        return llamadaSOAP("celsiusAFahrenheit", valor);
    }
    
    public Conversion fahrenheitACelsius(double valor) {
        return llamadaSOAP("fahrenheitACelsius", valor);
    }
    
    public Conversion celsiusAKelvin(double valor) {
        return llamadaSOAP("celsiusAKelvin", valor);
    }
    
    public Conversion kelvinACelsius(double valor) {
        return llamadaSOAP("kelvinACelsius", valor);
    }
    
    public Conversion fahrenheitAKelvin(double valor) {
        return llamadaSOAP("fahrenheitAKelvin", valor);
    }
    
    public Conversion kelvinAFahrenheit(double valor) {
        return llamadaSOAP("kelvinAFahrenheit", valor);
    }
    
    // ========== MÉTODOS DE LONGITUD ==========
    
    public Conversion metrosAPies(double valor) {
        return llamadaSOAP("metrosAPies", valor);
    }
    
    public Conversion piesAMetros(double valor) {
        return llamadaSOAP("piesAMetros", valor);
    }
    
    public Conversion metrosAPulgadas(double valor) {
        return llamadaSOAP("metrosAPulgadas", valor);
    }
    
    public Conversion pulgadasAMetros(double valor) {
        return llamadaSOAP("pulgadasAMetros", valor);
    }
    
    public Conversion kilometrosAMillas(double valor) {
        return llamadaSOAP("kilometrosAMillas", valor);
    }
    
    public Conversion millasAKilometros(double valor) {
        return llamadaSOAP("millasAKilometros", valor);
    }
    
    // ========== MÉTODOS DE PESO ==========
    
    public Conversion kilogramosALibras(double valor) {
        return llamadaSOAP("kilogramosALibras", valor);
    }
    
    public Conversion librasAKilogramos(double valor) {
        return llamadaSOAP("librasAKilogramos", valor);
    }
    
    public Conversion gramosAOnzas(double valor) {
        return llamadaSOAP("gramosAOnzas", valor);
    }
    
    public Conversion onzasAGramos(double valor) {
        return llamadaSOAP("onzasAGramos", valor);
    }
}