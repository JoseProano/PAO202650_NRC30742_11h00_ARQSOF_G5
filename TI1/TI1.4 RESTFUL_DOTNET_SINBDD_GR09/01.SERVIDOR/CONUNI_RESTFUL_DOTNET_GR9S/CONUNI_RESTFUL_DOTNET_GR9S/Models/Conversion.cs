namespace CONUNI_RESTFUL_DOTNET_GR9S.Models
{
    /// <summary>
    /// Modelo para las conversiones de unidades
    /// Contiene las constantes y métodos auxiliares para las conversiones
    /// </summary>
    public static class Conversion
    {
        // ========== CONSTANTES DE CONVERSIÓN ==========
        
        // Temperatura
        public const double CELSIUS_TO_FAHRENHEIT_MULTIPLIER = 9.0 / 5.0;
        public const double CELSIUS_TO_FAHRENHEIT_OFFSET = 32.0;
        public const double KELVIN_OFFSET = 273.15;
        
        // Longitud
        public const double METERS_TO_FEET = 3.28084;
        public const double METERS_TO_INCHES = 39.3701;
        public const double KILOMETERS_TO_MILES = 0.621371;
        
        // Peso/Masa
        public const double KILOGRAMS_TO_POUNDS = 2.20462;
        public const double GRAMS_TO_OUNCES = 0.035274;
        
        // Volumen
        public const double LITERS_TO_GALLONS = 0.264172;
        public const double MILLILITERS_TO_FLUID_OUNCES = 0.033814;
        
        // Área
        public const double SQUARE_METERS_TO_SQUARE_FEET = 10.7639;
        public const double HECTARES_TO_ACRES = 2.47105;
        
        // ========== CATEGORÍAS DE CONVERSIÓN ==========
        
        public const string CATEGORIA_TEMPERATURA = "temperatura";
        public const string CATEGORIA_LONGITUD = "longitud";
        public const string CATEGORIA_PESO = "peso";
        public const string CATEGORIA_VOLUMEN = "volumen";
        public const string CATEGORIA_AREA = "area";
        
        // ========== UNIDADES POR CATEGORÍA ==========
        
        // Temperatura
        public const string CELSIUS = "celsius";
        public const string FAHRENHEIT = "fahrenheit";
        public const string KELVIN = "kelvin";
        
        // Longitud
        public const string METROS = "metros";
        public const string PIES = "pies";
        public const string PULGADAS = "pulgadas";
        public const string KILOMETROS = "kilometros";
        public const string MILLAS = "millas";
        
        // Peso/Masa
        public const string KILOGRAMOS = "kilogramos";
        public const string LIBRAS = "libras";
        public const string GRAMOS = "gramos";
        public const string ONZAS = "onzas";
        
        // Volumen
        public const string LITROS = "litros";
        public const string GALONES = "galones";
        public const string MILILITROS = "mililitros";
        public const string ONZAS_FLUIDAS = "onzasFluidas";
        
        // Área
        public const string METROS_CUADRADOS = "metrosCuadrados";
        public const string PIES_CUADRADOS = "piesCuadrados";
        public const string HECTAREAS = "hectareas";
        public const string ACRES = "acres";
        
        // ========== MÉTODOS AUXILIARES ==========
        
        /// <summary>
        /// Valida si una conversión es válida
        /// </summary>
        public static bool EsConversionValida(string unidadOrigen, string unidadDestino, string categoria)
        {
            if (string.IsNullOrEmpty(unidadOrigen) || string.IsNullOrEmpty(unidadDestino) || string.IsNullOrEmpty(categoria))
            {
                return false;
            }
            
            switch (categoria.ToLower())
            {
                case CATEGORIA_TEMPERATURA:
                    return EsUnidadTemperatura(unidadOrigen) && EsUnidadTemperatura(unidadDestino);
                case CATEGORIA_LONGITUD:
                    return EsUnidadLongitud(unidadOrigen) && EsUnidadLongitud(unidadDestino);
                case CATEGORIA_PESO:
                    return EsUnidadPeso(unidadOrigen) && EsUnidadPeso(unidadDestino);
                case CATEGORIA_VOLUMEN:
                    return EsUnidadVolumen(unidadOrigen) && EsUnidadVolumen(unidadDestino);
                case CATEGORIA_AREA:
                    return EsUnidadArea(unidadOrigen) && EsUnidadArea(unidadDestino);
                default:
                    return false;
            }
        }
        
        private static bool EsUnidadTemperatura(string unidad)
        {
            return CELSIUS.Equals(unidad) || FAHRENHEIT.Equals(unidad) || KELVIN.Equals(unidad);
        }
        
        private static bool EsUnidadLongitud(string unidad)
        {
            return METROS.Equals(unidad) || PIES.Equals(unidad) || PULGADAS.Equals(unidad) ||
                   KILOMETROS.Equals(unidad) || MILLAS.Equals(unidad);
        }
        
        private static bool EsUnidadPeso(string unidad)
        {
            return KILOGRAMOS.Equals(unidad) || LIBRAS.Equals(unidad) || 
                   GRAMOS.Equals(unidad) || ONZAS.Equals(unidad);
        }
        
        private static bool EsUnidadVolumen(string unidad)
        {
            return LITROS.Equals(unidad) || GALONES.Equals(unidad) || 
                   MILILITROS.Equals(unidad) || ONZAS_FLUIDAS.Equals(unidad);
        }
        
        private static bool EsUnidadArea(string unidad)
        {
            return METROS_CUADRADOS.Equals(unidad) || PIES_CUADRADOS.Equals(unidad) ||
                   HECTAREAS.Equals(unidad) || ACRES.Equals(unidad);
        }
        
        /// <summary>
        /// Obtiene el símbolo de una unidad
        /// </summary>
        public static string ObtenerSimbolo(string unidad)
        {
            if (string.IsNullOrEmpty(unidad)) return unidad;
            
            switch (unidad.ToLower())
            {
                // Temperatura
                case CELSIUS: return "°C";
                case FAHRENHEIT: return "°F";
                case KELVIN: return "K";
                
                // Longitud
                case METROS: return "m";
                case PIES: return "ft";
                case PULGADAS: return "in";
                case KILOMETROS: return "km";
                case MILLAS: return "mi";
                
                // Peso/Masa
                case KILOGRAMOS: return "kg";
                case LIBRAS: return "lb";
                case GRAMOS: return "g";
                case ONZAS: return "oz";
                
                // Volumen
                case LITROS: return "L";
                case GALONES: return "gal";
                case MILILITROS: return "mL";
                case ONZAS_FLUIDAS: return "fl oz";
                
                // Área
                case METROS_CUADRADOS: return "m²";
                case PIES_CUADRADOS: return "ft²";
                case HECTAREAS: return "ha";
                case ACRES: return "ac";
                
                default: return unidad;
            }
        }
    }
}


