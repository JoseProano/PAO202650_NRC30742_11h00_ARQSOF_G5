package ec.edu.monster.prueba;

import ec.edu.monster.modelo.Conversion;
import ec.edu.monster.servicios.ClienteConversionSOAP;

/**
 * Clase de prueba para el cliente SOAP de conversiones
 * @author ACER NITRO V15
 */
public class PruebaClienteSOAP {
    
    public static void main(String[] args) {
        System.out.println("PRUEBAS DEL CLIENTE SOAP - MONSTERS INC. CONVERTER");
        System.out.println("=".repeat(60));
        
        ClienteConversionSOAP cliente = new ClienteConversionSOAP();
        
        // Pruebas de temperatura
        System.out.println("\nPRUEBAS DE TEMPERATURA:");
        System.out.println("-".repeat(40));
        
        Conversion temp1 = cliente.convertirTemperatura("celsiusAFahrenheit", 25.0);
        mostrarResultado(temp1);
        
        Conversion temp2 = cliente.convertirTemperatura("fahrenheitACelsius", 77.0);
        mostrarResultado(temp2);
        
        Conversion temp3 = cliente.convertirTemperatura("celsiusAKelvin", 0.0);
        mostrarResultado(temp3);
        
        // Pruebas de longitud
        System.out.println("\nPRUEBAS DE LONGITUD:");
        System.out.println("-".repeat(40));
        
        Conversion long1 = cliente.convertirLongitud("metrosAPies", 10.0);
        mostrarResultado(long1);
        
        Conversion long2 = cliente.convertirLongitud("piesAMetros", 32.8);
        mostrarResultado(long2);
        
        Conversion long3 = cliente.convertirLongitud("kilometrosAMillas", 100.0);
        mostrarResultado(long3);
        
        // Pruebas de peso
        System.out.println("\nPRUEBAS DE PESO/MASA:");
        System.out.println("-".repeat(40));
        
        Conversion peso1 = cliente.convertirPeso("kilogramosALibras", 5.0);
        mostrarResultado(peso1);
        
        Conversion peso2 = cliente.convertirPeso("librasAKilogramos", 11.0);
        mostrarResultado(peso2);
        
        Conversion peso3 = cliente.convertirPeso("gramosAOnzas", 1000.0);
        mostrarResultado(peso3);
        

        System.out.println("\nTODAS LAS PRUEBAS COMPLETADAS EXITOSAMENTE");
        System.out.println("=".repeat(60));
    }
    
    private static void mostrarResultado(Conversion conversion) {
        if (conversion.isExitosa()) {
            System.out.println("ÉXITO: " + conversion.toString());
        } else {
            System.out.println("ERROR: " + conversion.toString());
        }
    }
}