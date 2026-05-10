import java.net.HttpURLConnection;
import java.net.URL;
import java.io.OutputStreamWriter;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;

public class TestClient {
    public static void main(String[] args) {
        try {
            String url = "http://192.168.0.13:8080/CONUNI_SOAP_JAVA_GR09/WSConversion";
            String soapEnvelope = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                "<soap:Body>" +
                "<celsiusAFahrenheit xmlns=\"http://servicios.monster.edu.ec/\">" +
                "<celsius>100.0</celsius>" +
                "</celsiusAFahrenheit>" +
                "</soap:Body>" +
                "</soap:Envelope>";
            
            URL obj = new URL(url);
            HttpURLConnection con = (HttpURLConnection) obj.openConnection();
            
            con.setRequestMethod("POST");
            con.setDoOutput(true);
            con.setRequestProperty("Content-Type", "text/xml; charset=utf-8");
            con.setRequestProperty("SOAPAction", "\"\"");
            
            System.out.println("Enviando petición SOAP...");
            System.out.println("URL: " + url);
            System.out.println("SOAP: " + soapEnvelope);
            
            OutputStreamWriter out = new OutputStreamWriter(con.getOutputStream(), StandardCharsets.UTF_8);
            out.write(soapEnvelope);
            out.close();
            
            int responseCode = con.getResponseCode();
            System.out.println("Código de respuesta: " + responseCode);
            
            BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
            String inputLine;
            StringBuffer response = new StringBuffer();
            
            while ((inputLine = in.readLine()) != null) {
                response.append(inputLine);
            }
            in.close();
            
            System.out.println("Respuesta: " + response.toString());
            
        } catch (Exception e) {
            System.err.println("Error: " + e.getMessage());
            e.printStackTrace();
        }
    }
}
