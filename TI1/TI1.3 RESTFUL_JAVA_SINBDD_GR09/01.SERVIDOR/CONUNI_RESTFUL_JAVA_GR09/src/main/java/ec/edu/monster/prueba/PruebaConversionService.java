package ec.edu.monster.prueba;

import ec.edu.monster.servicios.ConversionService;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;

/**
 * Pruebas unitarias para el servicio de conversión
 * Prueba la lógica de negocio directamente sin HTTP
 * @author ACER NITRO V15
 */
public class PruebaConversionService {
    
    private ConversionService conversionService;
    
    public PruebaConversionService() {
        this.conversionService = new ConversionService();
    }
    
    /**
     * Método principal para ejecutar todas las pruebas unitarias
     */
    public static void main(String[] args) {
        System.out.println("=== MONSTERS INC. CONVERTER - PRUEBAS UNITARIAS ===");
        System.out.println("Iniciando pruebas del servicio de conversión...\n");
        
        PruebaConversionService tester = new PruebaConversionService();
        
        try {
            // Probar conversiones de temperatura
            tester.probarConversionesTemperatura();
            
            // Probar conversiones de longitud
            tester.probarConversionesLongitud();
            
            // Probar conversiones de peso
            tester.probarConversionesPeso();
            
            // Probar conversiones de volumen
            tester.probarConversionesVolumen();
            
            // Probar conversiones de área
            tester.probarConversionesArea();
            
            // Probar casos edge
            tester.probarCasosEdge();
            
            System.out.println("\n=== TODAS LAS PRUEBAS UNITARIAS COMPLETADAS ===");
            
        } catch (Exception e) {
            System.err.println("Error durante las pruebas: " + e.getMessage());
            e.printStackTrace();
        }
    }
    
    /**
     * Prueba todas las conversiones de temperatura
     */
    public void probarConversionesTemperatura() {
        System.out.println("🌡️ Probando conversiones de temperatura...");
        
        // Celsius a Fahrenheit
        double resultado = conversionService.celsiusAFahrenheit(100.0);
        System.out.printf("✅ Celsius a Fahrenheit: 100°C → %.2f°F%n", resultado);
        assert Math.abs(resultado - 212.0) < 0.01 : "Error en Celsius a Fahrenheit";
        
        // Fahrenheit a Celsius
        resultado = conversionService.fahrenheitACelsius(212.0);
        System.out.printf("✅ Fahrenheit a Celsius: 212°F → %.2f°C%n", resultado);
        assert Math.abs(resultado - 100.0) < 0.01 : "Error en Fahrenheit a Celsius";
        
        // Celsius a Kelvin
        resultado = conversionService.celsiusAKelvin(0.0);
        System.out.printf("✅ Celsius a Kelvin: 0°C → %.2fK%n", resultado);
        assert Math.abs(resultado - 273.15) < 0.01 : "Error en Celsius a Kelvin";
        
        // Kelvin a Celsius
        resultado = conversionService.kelvinACelsius(273.15);
        System.out.printf("✅ Kelvin a Celsius: 273.15K → %.2f°C%n", resultado);
        assert Math.abs(resultado - 0.0) < 0.01 : "Error en Kelvin a Celsius";
        
        // Fahrenheit a Kelvin
        resultado = conversionService.fahrenheitAKelvin(32.0);
        System.out.printf("✅ Fahrenheit a Kelvin: 32°F → %.2fK%n", resultado);
        assert Math.abs(resultado - 273.15) < 0.01 : "Error en Fahrenheit a Kelvin";
        
        // Kelvin a Fahrenheit
        resultado = conversionService.kelvinAFahrenheit(273.15);
        System.out.printf("✅ Kelvin a Fahrenheit: 273.15K → %.2f°F%n", resultado);
        assert Math.abs(resultado - 32.0) < 0.01 : "Error en Kelvin a Fahrenheit";
        
        System.out.println();
    }
    
    /**
     * Prueba todas las conversiones de longitud
     */
    public void probarConversionesLongitud() {
        System.out.println("📏 Probando conversiones de longitud...");
        
        // Metros a pies
        double resultado = conversionService.metrosAPies(1.0);
        System.out.printf("✅ Metros a pies: 1m → %.4f ft%n", resultado);
        assert Math.abs(resultado - 3.28084) < 0.0001 : "Error en Metros a pies";
        
        // Pies a metros
        resultado = conversionService.piesAMetros(3.28084);
        System.out.printf("✅ Pies a metros: 3.28084 ft → %.4f m%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Pies a metros";
        
        // Metros a pulgadas
        resultado = conversionService.metrosAPulgadas(1.0);
        System.out.printf("✅ Metros a pulgadas: 1m → %.4f in%n", resultado);
        assert Math.abs(resultado - 39.3701) < 0.0001 : "Error en Metros a pulgadas";
        
        // Pulgadas a metros
        resultado = conversionService.pulgadasAMetros(39.3701);
        System.out.printf("✅ Pulgadas a metros: 39.3701 in → %.4f m%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Pulgadas a metros";
        
        // Kilómetros a millas
        resultado = conversionService.kilometrosAMillas(1.0);
        System.out.printf("✅ Kilómetros a millas: 1km → %.4f mi%n", resultado);
        assert Math.abs(resultado - 0.621371) < 0.0001 : "Error en Kilómetros a millas";
        
        // Millas a kilómetros
        resultado = conversionService.millasAKilometros(0.621371);
        System.out.printf("✅ Millas a kilómetros: 0.621371 mi → %.4f km%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Millas a kilómetros";
        
        System.out.println();
    }
    
    /**
     * Prueba todas las conversiones de peso
     */
    public void probarConversionesPeso() {
        System.out.println("⚖️ Probando conversiones de peso...");
        
        // Kilogramos a libras
        double resultado = conversionService.kilogramosALibras(1.0);
        System.out.printf("✅ Kilogramos a libras: 1kg → %.4f lb%n", resultado);
        assert Math.abs(resultado - 2.20462) < 0.0001 : "Error en Kilogramos a libras";
        
        // Libras a kilogramos
        resultado = conversionService.librasAKilogramos(2.20462);
        System.out.printf("✅ Libras a kilogramos: 2.20462 lb → %.4f kg%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Libras a kilogramos";
        
        // Gramos a onzas
        resultado = conversionService.gramosAOnzas(28.3495);
        System.out.printf("✅ Gramos a onzas: 28.3495g → %.4f oz%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Gramos a onzas";
        
        // Onzas a gramos
        resultado = conversionService.onzasAGramos(1.0);
        System.out.printf("✅ Onzas a gramos: 1oz → %.4f g%n", resultado);
        assert Math.abs(resultado - 28.3495) < 0.0001 : "Error en Onzas a gramos";
        
        System.out.println();
    }
    
    /**
     * Prueba todas las conversiones de volumen
     */
    public void probarConversionesVolumen() {
        System.out.println("🧪 Probando conversiones de volumen...");
        
        // Litros a galones
        double resultado = conversionService.litrosAGalones(1.0);
        System.out.printf("✅ Litros a galones: 1L → %.4f gal%n", resultado);
        assert Math.abs(resultado - 0.264172) < 0.0001 : "Error en Litros a galones";
        
        // Galones a litros
        resultado = conversionService.galonesALitros(0.264172);
        System.out.printf("✅ Galones a litros: 0.264172 gal → %.4f L%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Galones a litros";
        
        // Mililitros a onzas fluidas
        resultado = conversionService.mililitrosAOnzasFluidas(29.5735);
        System.out.printf("✅ Mililitros a onzas fluidas: 29.5735mL → %.4f fl oz%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Mililitros a onzas fluidas";
        
        // Onzas fluidas a mililitros
        resultado = conversionService.onzasFluidasAMililitros(1.0);
        System.out.printf("✅ Onzas fluidas a mililitros: 1fl oz → %.4f mL%n", resultado);
        assert Math.abs(resultado - 29.5735) < 0.0001 : "Error en Onzas fluidas a mililitros";
        
        System.out.println();
    }
    
    /**
     * Prueba todas las conversiones de área
     */
    public void probarConversionesArea() {
        System.out.println("📐 Probando conversiones de área...");
        
        // Metros cuadrados a pies cuadrados
        double resultado = conversionService.metrosCuadradosAPiesCuadrados(1.0);
        System.out.printf("✅ Metros cuadrados a pies cuadrados: 1m² → %.4f ft²%n", resultado);
        assert Math.abs(resultado - 10.7639) < 0.0001 : "Error en Metros cuadrados a pies cuadrados";
        
        // Pies cuadrados a metros cuadrados
        resultado = conversionService.piesCuadradosAMetrosCuadrados(10.7639);
        System.out.printf("✅ Pies cuadrados a metros cuadrados: 10.7639 ft² → %.4f m²%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Pies cuadrados a metros cuadrados";
        
        // Hectáreas a acres
        resultado = conversionService.hectareasAAcres(1.0);
        System.out.printf("✅ Hectáreas a acres: 1ha → %.4f ac%n", resultado);
        assert Math.abs(resultado - 2.47105) < 0.0001 : "Error en Hectáreas a acres";
        
        // Acres a hectáreas
        resultado = conversionService.acresAHectareas(2.47105);
        System.out.printf("✅ Acres a hectáreas: 2.47105 ac → %.4f ha%n", resultado);
        assert Math.abs(resultado - 1.0) < 0.0001 : "Error en Acres a hectáreas";
        
        System.out.println();
    }
    
    /**
     * Prueba casos edge y valores especiales
     */
    public void probarCasosEdge() {
        System.out.println("🔍 Probando casos edge...");
        
        // Temperaturas extremas
        double resultado = conversionService.celsiusAFahrenheit(-40.0);
        System.out.printf("✅ Celsius a Fahrenheit (-40°C): %.2f°F%n", resultado);
        assert Math.abs(resultado - (-40.0)) < 0.01 : "Error en conversión de -40°C";
        
        // Cero absoluto
        resultado = conversionService.celsiusAKelvin(-273.15);
        System.out.printf("✅ Celsius a Kelvin (-273.15°C): %.2fK%n", resultado);
        assert Math.abs(resultado - 0.0) < 0.01 : "Error en conversión a cero absoluto";
        
        // Valores muy pequeños
        resultado = conversionService.metrosAPulgadas(0.001);
        System.out.printf("✅ Metros a pulgadas (0.001m): %.6f in%n", resultado);
        
        // Valores muy grandes
        resultado = conversionService.kilometrosAMillas(1000.0);
        System.out.printf("✅ Kilómetros a millas (1000km): %.2f mi%n", resultado);
        
        System.out.println();
    }
}


