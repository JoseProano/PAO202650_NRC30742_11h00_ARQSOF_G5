/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.prueba;

import ec.edu.monster.servicios.ConversionService;

/**
 * Clase de prueba para el servicio de conversiones
 * @author ACER NITRO V15
 */
public class PruebaConversion {
    public static void main(String[] args) {
        //Datos de prueba
        double temperaturaCelsius = 25.0;
        double metros = 10.0;
        double kilogramos = 5.0;
        
        //Proceso
        ConversionService service = new ConversionService();
        
        // Pruebas de temperatura
        double fahrenheit = service.celsiusAFahrenheit(temperaturaCelsius);
        double kelvin = service.celsiusAKelvin(temperaturaCelsius);
        
        // Pruebas de longitud
        double pies = service.metrosAPies(metros);
        double pulgadas = service.metrosAPulgadas(metros);
        
        // Pruebas de peso
        double libras = service.kilogramosALibras(kilogramos);
        double gramos = kilogramos * 1000;
        double onzas = service.gramosAOnzas(gramos);
        
        //Reporte
        System.out.println("=== PRUEBAS DE CONVERSIÓN ===");
        System.out.println();
        
        System.out.println("--- TEMPERATURA ---");
        System.out.println("Celsius: " + temperaturaCelsius + "°C");
        System.out.println("Fahrenheit: " + String.format("%.2f", fahrenheit) + "°F");
        System.out.println("Kelvin: " + String.format("%.2f", kelvin) + "K");
        System.out.println();
        
        System.out.println("--- LONGITUD ---");
        System.out.println("Metros: " + metros + " m");
        System.out.println("Pies: " + String.format("%.2f", pies) + " ft");
        System.out.println("Pulgadas: " + String.format("%.2f", pulgadas) + " in");
        System.out.println();
        
        System.out.println("--- PESO/MASA ---");
        System.out.println("Kilogramos: " + kilogramos + " kg");
        System.out.println("Libras: " + String.format("%.2f", libras) + " lb");
        System.out.println("Gramos: " + gramos + " g");
        System.out.println("Onzas: " + String.format("%.2f", onzas) + " oz");
        System.out.println();
        
        System.out.println("=== PRUEBAS COMPLETADAS ===");
    }
}
