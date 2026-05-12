using System;
using CONUNI_SOAP_DOTNET_CLICON_G09.MiServicio;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicios
{
    /// <summary>
    /// Cliente SOAP para consumir el servicio de conversiones.
    /// </summary>
    public class ClienteConversionSOAP
    {
        private readonly WSConversionClient clienteSOAP;

        public ClienteConversionSOAP()
        {
            clienteSOAP = new WSConversionClient();
        }

        public bool Login(string usuario, string contrasena)
        {
            return clienteSOAP.login(usuario, contrasena);
        }

        public Conversion ConvertirTemperatura(string operacion, double valor)
        {
            return EjecutarConversion("Temperatura", operacion, valor);
        }

        public Conversion ConvertirLongitud(string operacion, double valor)
        {
            return EjecutarConversion("Longitud", operacion, valor);
        }

        public Conversion ConvertirPeso(string operacion, double valor)
        {
            return EjecutarConversion("Peso/Masa", operacion, valor);
        }

        private Conversion EjecutarConversion(string tipoConversion, string operacion, double valor)
        {
            var conversion = new Conversion(tipoConversion, operacion, valor, ObtenerUnidadOriginal(operacion), ObtenerUnidadDestino(operacion));

            try
            {
                conversion.ValorConvertido = RealizarConversion(operacion, valor);
                conversion.Exitosa = true;
            }
            catch (Exception ex)
            {
                conversion.Exitosa = false;
                conversion.MensajeError = ex.Message;
            }

            return conversion;
        }

        private double RealizarConversion(string operacion, double valor)
        {
            switch (operacion)
            {
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

                case "kilogramosALibras":
                    return clienteSOAP.kilogramosALibras(valor);
                case "librasAKilogramos":
                    return clienteSOAP.librasAKilogramos(valor);
                case "gramosAOnzas":
                    return clienteSOAP.gramosAOnzas(valor);
                case "onzasAGramos":
                    return clienteSOAP.onzasAGramos(valor);

                default:
                    throw new ArgumentException("Operación no soportada: " + operacion);
            }
        }

        private string ObtenerUnidadOriginal(string operacion)
        {
            switch (operacion)
            {
                case "celsiusAFahrenheit":
                case "celsiusAKelvin":
                    return "°C";
                case "fahrenheitACelsius":
                case "fahrenheitAKelvin":
                    return "°F";
                case "kelvinACelsius":
                case "kelvinAFahrenheit":
                    return "K";
                case "metrosAPies":
                case "metrosAPulgadas":
                    return "m";
                case "piesAMetros":
                    return "ft";
                case "pulgadasAMetros":
                    return "in";
                case "kilometrosAMillas":
                    return "km";
                case "millasAKilometros":
                    return "mi";
                case "kilogramosALibras":
                    return "kg";
                case "librasAKilogramos":
                    return "lb";
                case "gramosAOnzas":
                    return "g";
                case "onzasAGramos":
                    return "oz";
                default:
                    return "unidad";
            }
        }

        private string ObtenerUnidadDestino(string operacion)
        {
            switch (operacion)
            {
                case "celsiusAFahrenheit":
                    return "°F";
                case "fahrenheitACelsius":
                    return "°C";
                case "celsiusAKelvin":
                case "fahrenheitAKelvin":
                    return "K";
                case "kelvinACelsius":
                    return "°C";
                case "kelvinAFahrenheit":
                    return "°F";
                case "metrosAPies":
                    return "ft";
                case "piesAMetros":
                case "pulgadasAMetros":
                    return "m";
                case "metrosAPulgadas":
                    return "in";
                case "kilometrosAMillas":
                    return "mi";
                case "millasAKilometros":
                    return "km";
                case "kilogramosALibras":
                    return "lb";
                case "librasAKilogramos":
                    return "kg";
                case "gramosAOnzas":
                    return "oz";
                case "onzasAGramos":
                    return "g";
                default:
                    return "unidad";
            }
        }

        public void Cerrar()
        {
            clienteSOAP?.Close();
        }
    }
}