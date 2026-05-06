using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ec.edu.monster.modelo;
using ec.edu.monster.controlador;
using CONUNI_SOAP_DOTNET_CLICON_G09.MiServicio;

namespace ec.edu.monster.servicios
{
    /// <summary>
    /// Cliente SOAP para consumir el servicio de conversiones
    /// Replicado desde Java - Adaptado para .NET
    /// </summary>
    public class ClienteConversionSOAP
    {
        private WSConversionClient clienteSOAP;

        public ClienteConversionSOAP()
        {
            clienteSOAP = new WSConversionClient();
        }

        // ========== MÉTODO DE AUTENTICACIÓN ==========

        /// <summary>
        /// Llama a la operación Login del servidor SOAP para validar credenciales.
        /// El cliente NUNCA almacena ni conoce las credenciales correctas.
        /// </summary>
        /// <param name="usuario">Nombre de usuario ingresado</param>
        /// <param name="contrasena">Contraseña ingresada</param>
        /// <returns><c>true</c> si el servidor confirma las credenciales</returns>
        public bool Login(string usuario, string contrasena)
        {
            return clienteSOAP.login(usuario, contrasena);
        }


        public Conversion ConvertirTemperatura(string operacion, double valor)
        {
            var conversion = new Conversion("Temperatura", operacion, valor, 
                GetUnidadOriginal(operacion), GetUnidadDestino(operacion));
            
            try
            {
                double resultado = RealizarConversion(operacion, valor);
                conversion.ValorConvertido = resultado;
                conversion.Exitosa = true;
            }
            catch (Exception e)
            {
                conversion.Exitosa = false;
                conversion.MensajeError = MensajesMonster.ObtenerMensajeError(e);
            }
            
            return conversion;
        }
        
        /// <summary>
        /// Realiza una conversión de longitud
        /// </summary>
        public Conversion ConvertirLongitud(string operacion, double valor)
        {
            var conversion = new Conversion("Longitud", operacion, valor, 
                GetUnidadOriginal(operacion), GetUnidadDestino(operacion));
            
            try
            {
                double resultado = RealizarConversion(operacion, valor);
                conversion.ValorConvertido = resultado;
                conversion.Exitosa = true;
            }
            catch (Exception e)
            {
                conversion.Exitosa = false;
                conversion.MensajeError = MensajesMonster.ObtenerMensajeError(e);
            }
            
            return conversion;
        }
        
        /// <summary>
        /// Realiza una conversión de peso/masa
        /// </summary>
        public Conversion ConvertirPeso(string operacion, double valor)
        {
            var conversion = new Conversion("Peso/Masa", operacion, valor, 
                GetUnidadOriginal(operacion), GetUnidadDestino(operacion));
            
            try
            {
                double resultado = RealizarConversion(operacion, valor);
                conversion.ValorConvertido = resultado;
                conversion.Exitosa = true;
            }
            catch (Exception e)
            {
                conversion.Exitosa = false;
                conversion.MensajeError = MensajesMonster.ObtenerMensajeError(e);
            }
            
            return conversion;
        }
        
        /// <summary>
        /// Realiza una conversión de volumen
        /// </summary>
        public Conversion ConvertirVolumen(string operacion, double valor)
        {
            var conversion = new Conversion("Volumen", operacion, valor, 
                GetUnidadOriginal(operacion), GetUnidadDestino(operacion));
            
            try
            {
                double resultado = RealizarConversion(operacion, valor);
                conversion.ValorConvertido = resultado;
                conversion.Exitosa = true;
            }
            catch (Exception e)
            {
                conversion.Exitosa = false;
                conversion.MensajeError = MensajesMonster.ObtenerMensajeError(e);
            }
            
            return conversion;
        }
        
        /// <summary>
        /// Realiza una conversión de área
        /// </summary>
        public Conversion ConvertirArea(string operacion, double valor)
        {
            var conversion = new Conversion("Área", operacion, valor, 
                GetUnidadOriginal(operacion), GetUnidadDestino(operacion));
            
            try
            {
                double resultado = RealizarConversion(operacion, valor);
                conversion.ValorConvertido = resultado;
                conversion.Exitosa = true;
            }
            catch (Exception e)
            {
                conversion.Exitosa = false;
                conversion.MensajeError = MensajesMonster.ObtenerMensajeError(e);
            }
            
            return conversion;
        }
        
        /// <summary>
        /// Realiza la llamada SOAP al servicio web
        /// </summary>
        private double RealizarConversion(string operacion, double valor)
        {
            switch (operacion)
            {
                // Temperatura
                case "celsiusAFahrenheit":
                    return clienteSOAP.celsiusAFahrenheit(valor);
                case "fahrenheitACelsius":
                    return clienteSOAP.fahrenheitACelsius(valor);
                case "celsiusAKelvin":
                    return clienteSOAP.celsiusAKelvin(valor);
                case "kelvinACelsius":
                    return clienteSOAP.kelvinACelsius(valor);
                case "fahrenheitAKelvin":
                    return clienteSOAP.fahrenheitAKelvin(valor);
                case "kelvinAFahrenheit":
                    return clienteSOAP.kelvinAFahrenheit(valor);

                // Longitud
                case "metrosAPies":
                    return clienteSOAP.metrosAPies(valor);
                case "piesAMetros":
                    return clienteSOAP.piesAMetros(valor);
                case "metrosAPulgadas":
                    return clienteSOAP.metrosAPulgadas(valor);
                case "pulgadasAMetros":
                    return clienteSOAP.pulgadasAMetros(valor);
                case "kilometrosAMillas":
                    return clienteSOAP.kilometrosAMillas(valor);
                case "millasAKilometros":
                    return clienteSOAP.millasAKilometros(valor);

                // Peso/Masa
                case "kilogramosALibras":
                    return clienteSOAP.kilogramosALibras(valor);
                case "librasAKilogramos":
                    return clienteSOAP.librasAKilogramos(valor);
                case "gramosAOnzas":
                    return clienteSOAP.gramosAOnzas(valor);
                case "onzasAGramos":
                    return clienteSOAP.onzasAGramos(valor);

                // Volumen
                case "litrosAGalones":
                    return clienteSOAP.litrosAGalones(valor);
                case "galonesALitros":
                    return clienteSOAP.galonesALitros(valor);
                case "mililitrosAOnzasFluidas":
                    return clienteSOAP.mililitrosAOnzasFluidas(valor);
                case "onzasFluidasAMililitros":
                    return clienteSOAP.onzasFluidasAMililitros(valor);

                // Área
                case "metrosCuadradosAPiesCuadrados":
                    return clienteSOAP.metrosCuadradosAPiesCuadrados(valor);
                case "piesCuadradosAMetrosCuadrados":
                    return clienteSOAP.piesCuadradosAMetrosCuadrados(valor);
                case "hectareasAAcres":
                    return clienteSOAP.hectareasAAcres(valor);
                case "acresAHectareas":
                    return clienteSOAP.acresAHectareas(valor);

                default:
                    throw new ArgumentException($"Operación no soportada: {operacion}");
            }
        }
        
        /// <summary>
        /// Obtiene la unidad original para la operación
        /// </summary>
        private string GetUnidadOriginal(string operacion)
        {
            if (operacion.Contains("celsiusA")) return "°C";
            if (operacion.Contains("fahrenheitA")) return "°F";
            if (operacion.Contains("kelvinA")) return "K";
            if (operacion.Contains("metrosA")) return "m";
            if (operacion.Contains("piesA")) return "ft";
            if (operacion.Contains("pulgadasA")) return "in";
            if (operacion.Contains("kilometrosA")) return "km";
            if (operacion.Contains("millasA")) return "mi";
            if (operacion.Contains("kilogramosA")) return "kg";
            if (operacion.Contains("librasA")) return "lb";
            if (operacion.Contains("gramosA")) return "g";
            if (operacion.Contains("onzasA")) return "oz";
            if (operacion.Contains("litrosA")) return "L";
            if (operacion.Contains("galonesA")) return "gal";
            if (operacion.Contains("mililitrosA")) return "mL";
            if (operacion.Contains("onzasFluidasA")) return "fl oz";
            if (operacion.Contains("metrosCuadradosA")) return "m²";
            if (operacion.Contains("piesCuadradosA")) return "ft²";
            if (operacion.Contains("hectareasA")) return "ha";
            if (operacion.Contains("acresA")) return "ac";
            return "unidad";
        }
        
        /// <summary>
        /// Obtiene la unidad destino para la operación
        /// </summary>
        private string GetUnidadDestino(string operacion)
        {
            if (operacion.Contains("AFahrenheit")) return "°F";
            if (operacion.Contains("ACelsius")) return "°C";
            if (operacion.Contains("AKelvin")) return "K";
            if (operacion.Contains("APies")) return "ft";
            if (operacion.Contains("AMetros")) return "m";
            if (operacion.Contains("APulgadas")) return "in";
            if (operacion.Contains("AMillas")) return "mi";
            if (operacion.Contains("AKilometros")) return "km";
            if (operacion.Contains("ALibras")) return "lb";
            if (operacion.Contains("AKilogramos")) return "kg";
            if (operacion.Contains("AOnzas")) return "oz";
            if (operacion.Contains("AGramos")) return "g";
            if (operacion.Contains("AGalones")) return "gal";
            if (operacion.Contains("ALitros")) return "L";
            if (operacion.Contains("AOnzasFluidas")) return "fl oz";
            if (operacion.Contains("AMililitros")) return "mL";
            if (operacion.Contains("APiesCuadrados")) return "ft²";
            if (operacion.Contains("AMetrosCuadrados")) return "m²";
            if (operacion.Contains("AAcres")) return "ac";
            if (operacion.Contains("AHectareas")) return "ha";
            return "unidad";
        }

        /// <summary>
        /// Cierra el cliente SOAP
        /// </summary>
        public void Cerrar()
        {
            clienteSOAP?.Close();
        }
    }
}
