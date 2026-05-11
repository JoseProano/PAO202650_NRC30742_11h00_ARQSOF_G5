using System;

namespace CONUNI_RESTFUL_DOTNET_GR9S.Models
{
    /// <summary>
    /// Servicio para conversiones de temperatura y unidades
    /// Capa de servicios que contiene toda la lógica de negocio
    /// </summary>
    public class ConversionService
    {
        // ========== CONVERSIONES DE TEMPERATURA ==========
        
        public double CelsiusAFahrenheit(double celsius)
        {
            return (celsius * Conversion.CELSIUS_TO_FAHRENHEIT_MULTIPLIER) + Conversion.CELSIUS_TO_FAHRENHEIT_OFFSET;
        }
        
        public double FahrenheitACelsius(double fahrenheit)
        {
            return (fahrenheit - Conversion.CELSIUS_TO_FAHRENHEIT_OFFSET) / Conversion.CELSIUS_TO_FAHRENHEIT_MULTIPLIER;
        }
        
        public double CelsiusAKelvin(double celsius)
        {
            return celsius + Conversion.KELVIN_OFFSET;
        }
        
        public double KelvinACelsius(double kelvin)
        {
            return kelvin - Conversion.KELVIN_OFFSET;
        }
        
        public double FahrenheitAKelvin(double fahrenheit)
        {
            return FahrenheitACelsius(fahrenheit) + Conversion.KELVIN_OFFSET;
        }
        
        public double KelvinAFahrenheit(double kelvin)
        {
            return CelsiusAFahrenheit(KelvinACelsius(kelvin));
        }
        
        // ========== CONVERSIONES DE LONGITUD ==========
        
        public double MetrosAPies(double metros)
        {
            return metros * Conversion.METERS_TO_FEET;
        }
        
        public double PiesAMetros(double pies)
        {
            return pies / Conversion.METERS_TO_FEET;
        }
        
        public double MetrosAPulgadas(double metros)
        {
            return metros * Conversion.METERS_TO_INCHES;
        }
        
        public double PulgadasAMetros(double pulgadas)
        {
            return pulgadas / Conversion.METERS_TO_INCHES;
        }
        
        public double KilometrosAMillas(double kilometros)
        {
            return kilometros * Conversion.KILOMETERS_TO_MILES;
        }
        
        public double MillasAKilometros(double millas)
        {
            return millas / Conversion.KILOMETERS_TO_MILES;
        }
        
        // ========== CONVERSIONES DE PESO/MASA ==========
        
        public double KilogramosALibras(double kilogramos)
        {
            return kilogramos * Conversion.KILOGRAMS_TO_POUNDS;
        }
        
        public double LibrasAKilogramos(double libras)
        {
            return libras / Conversion.KILOGRAMS_TO_POUNDS;
        }
        
        public double GramosAOnzas(double gramos)
        {
            return gramos * Conversion.GRAMS_TO_OUNCES;
        }
        
        public double OnzasAGramos(double onzas)
        {
            return onzas / Conversion.GRAMS_TO_OUNCES;
        }
        
        
        {
        }
        
        {
        }
        
        {
        }
        
        {
        }
        
        // ========== CONVERSIONES DE ÁREA ==========
        
        {
        }
        
        {
        }
        
        {
        }
        
        {
        }
        
        // ========== MÉTODO PRINCIPAL DE CONVERSIÓN ==========
        
        public ConversionResponse Convertir(double valor, string unidadOrigen, string unidadDestino, string categoria)
        {
            try
            {
                if (valor < 0 && !categoria.Equals(Conversion.CATEGORIA_TEMPERATURA))
                {
                    return ConversionResponse.CrearError("El valor no puede ser negativo para esta categoría");
                }
                
                if (!Conversion.EsConversionValida(unidadOrigen, unidadDestino, categoria))
                {
                    return ConversionResponse.CrearError($"Conversión no válida entre {unidadOrigen} y {unidadDestino}");
                }
                
                double resultado = RealizarConversion(valor, unidadOrigen, unidadDestino, categoria);
                
                return ConversionResponse.CrearExito(valor, resultado, unidadOrigen, unidadDestino, categoria);
                
            }
            catch (Exception e)
            {
                return ConversionResponse.CrearError($"Error en la conversión: {e.Message}");
            }
        }
        
        private double RealizarConversion(double valor, string unidadOrigen, string unidadDestino, string categoria)
        {
            string operacion = unidadOrigen + "A" + unidadDestino;
            
            switch (categoria.ToLower())
            {
                case Conversion.CATEGORIA_TEMPERATURA:
                    return ConvertirTemperatura(valor, operacion);
                case Conversion.CATEGORIA_LONGITUD:
                    return ConvertirLongitud(valor, operacion);
                case Conversion.CATEGORIA_PESO:
                    return ConvertirPeso(valor, operacion);
                default:
                    throw new ArgumentException($"Categoría no soportada: {categoria}");
            }
        }
        
        private double ConvertirTemperatura(double valor, string operacion)
        {
            switch (operacion)
            {
                case "celsiusAfahrenheit": return CelsiusAFahrenheit(valor);
                case "fahrenheitAcelsius": return FahrenheitACelsius(valor);
                case "celsiusAkelvin": return CelsiusAKelvin(valor);
                case "kelvinAcelsius": return KelvinACelsius(valor);
                case "fahrenheitAkelvin": return FahrenheitAKelvin(valor);
                case "kelvinAfahrenheit": return KelvinAFahrenheit(valor);
                default: throw new ArgumentException($"Operación de temperatura no soportada: {operacion}");
            }
        }
        
        private double ConvertirLongitud(double valor, string operacion)
        {
            switch (operacion)
            {
                case "metrosApies": return MetrosAPies(valor);
                case "piesAmetros": return PiesAMetros(valor);
                case "metrosApulgadas": return MetrosAPulgadas(valor);
                case "pulgadasAmetros": return PulgadasAMetros(valor);
                case "kilometrosAmillas": return KilometrosAMillas(valor);
                case "millasAkilometros": return MillasAKilometros(valor);
                default: throw new ArgumentException($"Operación de longitud no soportada: {operacion}");
            }
        }
        
        private double ConvertirPeso(double valor, string operacion)
        {
            switch (operacion)
            {
                case "kilogramosAlibras": return KilogramosALibras(valor);
                case "librasAkilogramos": return LibrasAKilogramos(valor);
                case "gramosAonzas": return GramosAOnzas(valor);
                case "onzasAgramos": return OnzasAGramos(valor);
                default: throw new ArgumentException($"Operación de peso no soportada: {operacion}");
            }
        }
        
        {
            switch (operacion)
            {
            }
        }
        
        {
            switch (operacion)
            {
                default: throw new ArgumentException($"Operación de área no soportada: {operacion}");
            }
        }
    }
}

