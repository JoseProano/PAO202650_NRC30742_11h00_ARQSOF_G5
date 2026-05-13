namespace CONUNI_RESTFUL_DOTNET_GR9S.Models
{
    /// <summary>
    /// Constantes y utilidades para conversiones de unidades.
    /// </summary>
    public static class Conversion
    {
        public const double CELSIUS_TO_FAHRENHEIT_MULTIPLIER = 9.0 / 5.0;
        public const double CELSIUS_TO_FAHRENHEIT_OFFSET = 32.0;
        public const double KELVIN_OFFSET = 273.15;

        public const double METERS_TO_FEET = 3.28084;
        public const double METERS_TO_INCHES = 39.3701;
        public const double KILOMETERS_TO_MILES = 0.621371;

        public const double KILOGRAMS_TO_POUNDS = 2.20462;
        public const double GRAMS_TO_OUNCES = 0.035274;

        public const string CATEGORIA_TEMPERATURA = "temperatura";
        public const string CATEGORIA_LONGITUD = "longitud";
        public const string CATEGORIA_PESO = "peso";

        public const string CELSIUS = "celsius";
        public const string FAHRENHEIT = "fahrenheit";
        public const string KELVIN = "kelvin";

        public const string METROS = "metros";
        public const string PIES = "pies";
        public const string PULGADAS = "pulgadas";
        public const string KILOMETROS = "kilometros";
        public const string MILLAS = "millas";

        public const string KILOGRAMOS = "kilogramos";
        public const string LIBRAS = "libras";
        public const string GRAMOS = "gramos";
        public const string ONZAS = "onzas";

        public static bool EsConversionValida(string unidadOrigen, string unidadDestino, string categoria)
        {
            if (string.IsNullOrWhiteSpace(unidadOrigen) || string.IsNullOrWhiteSpace(unidadDestino) || string.IsNullOrWhiteSpace(categoria))
            {
                return false;
            }

            switch (categoria.ToLowerInvariant())
            {
                case CATEGORIA_TEMPERATURA:
                    return EsUnidadTemperatura(unidadOrigen) && EsUnidadTemperatura(unidadDestino);
                case CATEGORIA_LONGITUD:
                    return EsUnidadLongitud(unidadOrigen) && EsUnidadLongitud(unidadDestino);
                case CATEGORIA_PESO:
                    return EsUnidadPeso(unidadOrigen) && EsUnidadPeso(unidadDestino);
                default:
                    return false;
            }
        }

        public static string ObtenerSimbolo(string unidad)
        {
            if (string.IsNullOrWhiteSpace(unidad))
            {
                return unidad;
            }

            switch (unidad.ToLowerInvariant())
            {
                case CELSIUS:
                    return "°C";
                case FAHRENHEIT:
                    return "°F";
                case KELVIN:
                    return "K";
                case METROS:
                    return "m";
                case PIES:
                    return "ft";
                case PULGADAS:
                    return "in";
                case KILOMETROS:
                    return "km";
                case MILLAS:
                    return "mi";
                case KILOGRAMOS:
                    return "kg";
                case LIBRAS:
                    return "lb";
                case GRAMOS:
                    return "g";
                case ONZAS:
                    return "oz";
                default:
                    return unidad;
            }
        }

        private static bool EsUnidadTemperatura(string unidad)
        {
            string normalizada = unidad.ToLowerInvariant();
            return normalizada == CELSIUS || normalizada == FAHRENHEIT || normalizada == KELVIN;
        }

        private static bool EsUnidadLongitud(string unidad)
        {
            string normalizada = unidad.ToLowerInvariant();
            return normalizada == METROS || normalizada == PIES || normalizada == PULGADAS ||
                   normalizada == KILOMETROS || normalizada == MILLAS;
        }

        private static bool EsUnidadPeso(string unidad)
        {
            string normalizada = unidad.ToLowerInvariant();
            return normalizada == KILOGRAMOS || normalizada == LIBRAS ||
                   normalizada == GRAMOS || normalizada == ONZAS;
        }
    }
}
