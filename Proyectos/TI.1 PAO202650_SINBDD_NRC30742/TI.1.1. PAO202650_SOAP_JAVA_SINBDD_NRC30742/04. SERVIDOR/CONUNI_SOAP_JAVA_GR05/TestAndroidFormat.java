import java.net.HttpURLConnection;
import java.net.URL;
import java.io.OutputStreamWriter;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;

public class TestAndroidFormat {
    public static void main(String[] args) {
        try {
            String url = "http://192.168.0.13:8080/CONUNI_SOAP_JAVA_GR09/WSConversion";
            
            // Envelope con el formato que usa Android (igual al HTML que funciona)
            String soapEnvelope = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tns=\"http://servicios.monster.edu.ec/\">" +
                "<soap:Body>" +
                "<tns:kelvinAFahrenheit>" +
                "<kelvin>100.0</kelvin>" +
                "</tns:kelvinAFahrenheit>" +
                "</soap:Body>" +
                "</soap:Envelope>";
            
            URL obj = new URL(url);
            HttpURLConnection con = (HttpURLConnection) obj.openConnection();
            
            con.setRequestMethod("POST");
            con.setDoOutput(true);
            con.setRequestProperty("Content-Type", "text/xml; charset=utf-8");
            con.setRequestProperty("SOAPAction", "\"\"");
            con.setConnectTimeout(10000);
            con.setReadTimeout(10000);
            
            System.out.println("=== PRUEBA FORMATO ANDROID ===");
            System.out.println("URL: " + url);
            System.out.println("SOAPAction: \"\"");
            System.out.println("SOAP Envelope:");
            System.out.println(soapEnvelope);
            System.out.println("=========================");
            
            OutputStreamWriter out = new OutputStreamWriter(con.getOutputStream(), StandardCharsets.UTF_8);
            out.write(soapEnvelope);
            out.close();
            
            int responseCode = con.getResponseCode();
            System.out.println("Código de respuesta: " + responseCode);
            
            if (responseCode == 200) {
                BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
                String inputLine;
                StringBuffer response = new StringBuffer();
                
                while ((inputLine = in.readLine()) != null) {
                    response.append(inputLine);
                }
                in.close();
                
                System.out.println("EXITO! Respuesta: " + response.toString());
            } else {
                BufferedReader in = new BufferedReader(new InputStreamReader(con.getErrorStream()));
                String inputLine;
                StringBuffer response = new StringBuffer();
                
                while ((inputLine = in.readLine()) != null) {
                    response.append(inputLine);
                }
                in.close();
                
                System.out.println("ERROR! Respuesta: " + response.toString());
            }
            
        } catch (Exception e) {
            System.err.println("EXCEPCION: " + e.getMessage());
            e.printStackTrace();
        }
    }
}




