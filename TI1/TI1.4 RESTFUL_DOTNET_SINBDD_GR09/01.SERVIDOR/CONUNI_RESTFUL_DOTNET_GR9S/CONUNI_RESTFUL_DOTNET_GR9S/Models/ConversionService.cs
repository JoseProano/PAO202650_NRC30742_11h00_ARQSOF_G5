using System;

namespace CONUNI_RESTFUL_DOTNET_GR9S.Models
{
    /// <summary>
    /// Servicio de lógica de negocio para conversiones.
    /// </summary>
    public class ConversionService
    {
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
            return CelsiusAKelvin(FahrenheitACelsius(fahrenheit));
        }

        public double KelvinAFahrenheit(double kelvin)
        {
            return CelsiusAFahrenheit(KelvinACelsius(kelvin));
        }

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

        public ConversionResponse Convertir(double valor, string unidadOrigen, string unidadDestino, string categoria)
        {
            try
            {
                if (valor < 0 && !Conversion.CATEGORIA_TEMPERATURA.Equals(categoria, StringComparison.OrdinalIgnoreCase))
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
            catch (Exception ex)
            {
                return ConversionResponse.CrearError($"Error en la conversión: {ex.Message}");
            }
        }

        private double RealizarConversion(double valor, string unidadOrigen, string unidadDestino, string categoria)
        {
            string origen = unidadOrigen.ToLowerInvariant();
            string destino = unidadDestino.ToLowerInvariant();

            switch (categoria.ToLowerInvariant())
            {
                case Conversion.CATEGORIA_TEMPERATURA:
                    return ConvertirTemperatura(valor, origen, destino);
                case Conversion.CATEGORIA_LONGITUD:
                    return ConvertirLongitud(valor, origen, destino);
                case Conversion.CATEGORIA_PESO:
                    return ConvertirPeso(valor, origen, destino);
                default:
                    throw new ArgumentException($"Categoría no soportada: {categoria}");
            }
        }

        private double ConvertirTemperatura(double valor, string origen, string destino)
        {
            switch ($"{origen}A{destino}")
            {
                case "celsiusAfahrenheit":
                    return CelsiusAFahrenheit(valor);
                case "fahrenheitAcelsius":
                    return FahrenheitACelsius(valor);
                case "celsiusAkelvin":
                    return CelsiusAKelvin(valor);
                case "kelvinAcelsius":
                    return KelvinACelsius(valor);
                case "fahrenheitAkelvin":
                    return FahrenheitAKelvin(valor);
                case "kelvinAfahrenheit":
                    return KelvinAFahrenheit(valor);
                default:
                    throw new ArgumentException($"Operación de temperatura no soportada: {origen} -> {destino}");
            }
        }

        private double ConvertirLongitud(double valor, string origen, string destino)
        {
            switch ($"{origen}A{destino}")
            {
                case "metrosApies":
                    return MetrosAPies(valor);
                case "piesAmetros":
                    return PiesAMetros(valor);
                case "metrosApulgadas":
                    return MetrosAPulgadas(valor);
                case "pulgadasAmetros":
                    return PulgadasAMetros(valor);
                case "kilometrosAmillas":
                    return KilometrosAMillas(valor);
                case "millasAkilometros":
                    return MillasAKilometros(valor);
                default:
                    throw new ArgumentException($"Operación de longitud no soportada: {origen} -> {destino}");
            }
        }

        private double ConvertirPeso(double valor, string origen, string destino)
        {
            switch ($"{origen}A{destino}")
            {
                case "kilogramosAlibras":
                    return KilogramosALibras(valor);
                case "librasAkilogramos":
                    return LibrasAKilogramos(valor);
                case "gramosAonzas":
                    return GramosAOnzas(valor);
                case "onzasAgramos":
                    return OnzasAGramos(valor);
                default:
                    throw new ArgumentException($"Operación de peso no soportada: {origen} -> {destino}");
            }
        }
    }
}
