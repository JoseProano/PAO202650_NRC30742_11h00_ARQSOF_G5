/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.servicios;

/**
 * Servicio para conversiones de temperatura y unidades
 * @author ACER NITRO V15
 */
public class ConversionService {
    
    // ========== CONVERSIONES DE TEMPERATURA ==========
    
    /**
     * Convierte Celsius a Fahrenheit
     */
    public double celsiusAFahrenheit(double celsius) {
        return (celsius * 9.0 / 5.0) + 32.0;
    }
    
    /**
     * Convierte Fahrenheit a Celsius
     */
    public double fahrenheitACelsius(double fahrenheit) {
        return (fahrenheit - 32.0) * 5.0 / 9.0;
    }
    
    /**
     * Convierte Celsius a Kelvin
     */
    public double celsiusAKelvin(double celsius) {
        return celsius + 273.15;
    }
    
    /**
     * Convierte Kelvin a Celsius
     */
    public double kelvinACelsius(double kelvin) {
        return kelvin - 273.15;
    }
    
    /**
     * Convierte Fahrenheit a Kelvin
     */
    public double fahrenheitAKelvin(double fahrenheit) {
        return (fahrenheit - 32.0) * 5.0 / 9.0 + 273.15;
    }
    
    /**
     * Convierte Kelvin a Fahrenheit
     */
    public double kelvinAFahrenheit(double kelvin) {
        return (kelvin - 273.15) * 9.0 / 5.0 + 32.0;
    }
    
    // ========== CONVERSIONES DE LONGITUD ==========
    
    /**
     * Convierte metros a pies
     */
    public double metrosAPies(double metros) {
        return metros * 3.28084;
    }
    
    /**
     * Convierte pies a metros
     */
    public double piesAMetros(double pies) {
        return pies / 3.28084;
    }
    
    /**
     * Convierte metros a pulgadas
     */
    public double metrosAPulgadas(double metros) {
        return metros * 39.3701;
    }
    
    /**
     * Convierte pulgadas a metros
     */
    public double pulgadasAMetros(double pulgadas) {
        return pulgadas / 39.3701;
    }
    
    /**
     * Convierte kilómetros a millas
     */
    public double kilometrosAMillas(double kilometros) {
        return kilometros * 0.621371;
    }
    
    /**
     * Convierte millas a kilómetros
     */
    public double millasAKilometros(double millas) {
        return millas / 0.621371;
    }
    
    // ========== CONVERSIONES DE PESO/MASA ==========
    
    /**
     * Convierte kilogramos a libras
     */
    public double kilogramosALibras(double kilogramos) {
        return kilogramos * 2.20462;
    }
    
    /**
     * Convierte libras a kilogramos
     */
    public double librasAKilogramos(double libras) {
        return libras / 2.20462;
    }
    
    /**
     * Convierte gramos a onzas
     */
    public double gramosAOnzas(double gramos) {
        return gramos * 0.035274;
    }
    
    /**
     * Convierte onzas a gramos
     */
    public double onzasAGramos(double onzas) {
        return onzas / 0.035274;
    }
    
}

