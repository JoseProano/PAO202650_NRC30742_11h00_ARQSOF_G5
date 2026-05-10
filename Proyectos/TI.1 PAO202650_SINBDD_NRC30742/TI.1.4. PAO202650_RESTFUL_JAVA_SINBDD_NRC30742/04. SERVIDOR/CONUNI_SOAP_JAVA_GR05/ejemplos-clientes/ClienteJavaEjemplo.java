package ec.edu.monster.client;

import java.net.URL;
import javax.xml.namespace.QName;
import jakarta.xml.ws.Service;

/**
 * Ejemplo de cliente Java para consumir el servicio SOAP WSConversion
 * 
 * IMPORTANTE: Antes de ejecutar este código, debes generar las clases cliente
 * usando wsimport:
 * 
 * wsimport -keep -d client-classes -s client-src -p ec.edu.monster.client http://localhost:8080/conversion/WSConversion?wsdl
 * 
 * O importar el proyecto en un IDE como NetBeans/IntelliJ y usar "Web Service Client"
 */
public class ClienteJavaEjemplo {
    
    // URL del WSDL (ajusta según tu servidor)
    private static final String WSDL_URL = "http://localhost:8080/conversion/WSConversion?wsdl";
    // Para acceso remoto, cambiar a: "http://192.168.1.100:8080/conversion/WSConversion?wsdl"
    
    public static void main(String[] args) {
        try {
            System.out.println("========================================");
            System.out.println("Cliente Java - Servicio WSConversion");
            System.out.println("========================================\n");
            
            // Crear URL del WSDL
            URL wsdlUrl = new URL(WSDL_URL);
            
            // Namespace y nombre del servicio (verificar en el WSDL generado)
            QName serviceName = new QName("http://servicios.monster.edu.ec/", "WSConversionService");
            QName portName = new QName("http://servicios.monster.edu.ec/", "WSConversionPort");
            
            // Crear servicio
            Service service = Service.create(wsdlUrl, serviceName);
            
            // Obtener el puerto (ajustar según las clases generadas por wsimport)
            // WSConversion port = service.getPort(portName, WSConversion.class);
            
            // Ejemplos de uso (descomentar después de generar las clases)
            /*
            System.out.println("=== Pruebas de Conversión ===\n");
            
            // Conversión de temperatura
            System.out.println("1. Conversión de Temperatura:");
            double celsius = 25.0;
            double fahrenheit = port.celsiusAFahrenheit(celsius);
            System.out.println("   " + celsius + "°C = " + fahrenheit + "°F");
            
            double kelvin = port.celsiusAKelvin(celsius);
            System.out.println("   " + celsius + "°C = " + kelvin + "K\n");
            
            // Conversión de longitud
            System.out.println("2. Conversión de Longitud:");
            double metros = 100.0;
            double pies = port.metrosAPies(metros);
            System.out.println("   " + metros + " metros = " + pies + " pies");
            
            double millas = port.kilometrosAMillas(metros / 1000.0);
            System.out.println("   " + (metros / 1000.0) + " km = " + millas + " millas\n");
            
            // Conversión de peso
            System.out.println("3. Conversión de Peso:");
            double kg = 70.0;
            double libras = port.kilogramosALibras(kg);
            System.out.println("   " + kg + " kg = " + libras + " libras\n");
            
            // Conversión de volumen
            System.out.println("4. Conversión de Volumen:");
            double litros = 50.0;
            double galones = port.litrosAGalones(litros);
            System.out.println("   " + litros + " litros = " + galones + " galones\n");
            
            System.out.println("========================================");
            System.out.println("Todas las pruebas completadas!");
            System.out.println("========================================");
            */
            
            System.out.println("NOTA: Este es un ejemplo básico.");
            System.out.println("Para usar este código, debes generar las clases cliente con wsimport.");
            System.out.println("Ver comentarios en el código para instrucciones.");
            
        } catch (Exception e) {
            System.err.println("Error al conectar con el servicio:");
            e.printStackTrace();
        }
    }
}


