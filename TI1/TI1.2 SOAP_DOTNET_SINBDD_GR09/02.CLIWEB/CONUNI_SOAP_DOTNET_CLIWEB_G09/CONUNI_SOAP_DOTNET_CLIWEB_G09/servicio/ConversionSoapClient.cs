using System.ServiceModel;
using CONUNI_SOAP_DOTNET_CLIWEB_G09.Models;

namespace CONUNI_SOAP_DOTNET_CLIWEB_G09.servicio
{
    /// <summary>
    /// Interfaz del servicio SOAP de conversiones
    /// </summary>
    [ServiceContract(Name = "WSConversion", Namespace = "http://tempuri.org/")]
    public interface IWSConversionClient
    {
        // Conversiones de Temperatura
        [OperationContract(Name = "celsiusAFahrenheit")]
        double CelsiusAFahrenheit(double celsius);

        [OperationContract(Name = "fahrenheitACelsius")]
        double FahrenheitACelsius(double fahrenheit);

        [OperationContract(Name = "celsiusAKelvin")]
        double CelsiusAKelvin(double celsius);

        [OperationContract(Name = "kelvinACelsius")]
        double KelvinACelsius(double kelvin);

        [OperationContract(Name = "fahrenheitAKelvin")]
        double FahrenheitAKelvin(double fahrenheit);

        [OperationContract(Name = "kelvinAFahrenheit")]
        double KelvinAFahrenheit(double kelvin);

        // Conversiones de Longitud
        [OperationContract(Name = "metrosAPies")]
        double MetrosAPies(double metros);

        [OperationContract(Name = "piesAMetros")]
        double PiesAMetros(double pies);

        [OperationContract(Name = "metrosAPulgadas")]
        double MetrosAPulgadas(double metros);

        [OperationContract(Name = "pulgadasAMetros")]
        double PulgadasAMetros(double pulgadas);

        [OperationContract(Name = "kilometrosAMillas")]
        double KilometrosAMillas(double kilometros);

        [OperationContract(Name = "millasAKilometros")]
        double MillasAKilometros(double millas);

        // Conversiones de Peso/Masa
        [OperationContract(Name = "kilogramosALibras")]
        double KilogramosALibras(double kilogramos);

        [OperationContract(Name = "librasAKilogramos")]
        double LibrasAKilogramos(double libras);

        [OperationContract(Name = "gramosAOnzas")]
        double GramosAOnzas(double gramos);

        [OperationContract(Name = "onzasAGramos")]
        double OnzasAGramos(double onzas);





        // Conversiones de Área



    }

    /// <summary>
    /// Cliente SOAP para consumir el servicio de conversiones
    /// </summary>
    public class ConversionSoapClient : ClientBase<IWSConversionClient>, IWSConversionClient
    {
        public ConversionSoapClient(string endpoint) : base(GetBinding(), 
            new EndpointAddress(endpoint))
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentException("El endpoint no puede ser nulo o vacío", nameof(endpoint));
            }
        }

        private static BasicHttpBinding GetBinding()
        {
            var binding = new BasicHttpBinding();
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxDepth = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
            binding.OpenTimeout = TimeSpan.FromSeconds(30);
            binding.CloseTimeout = TimeSpan.FromSeconds(30);
            binding.SendTimeout = TimeSpan.FromSeconds(30);
            binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
            return binding;
        }

        // Implementación de métodos de temperatura
        public double CelsiusAFahrenheit(double celsius) => Channel.CelsiusAFahrenheit(celsius);
        public double FahrenheitACelsius(double fahrenheit) => Channel.FahrenheitACelsius(fahrenheit);
        public double CelsiusAKelvin(double celsius) => Channel.CelsiusAKelvin(celsius);
        public double KelvinACelsius(double kelvin) => Channel.KelvinACelsius(kelvin);
        public double FahrenheitAKelvin(double fahrenheit) => Channel.FahrenheitAKelvin(fahrenheit);
        public double KelvinAFahrenheit(double kelvin) => Channel.KelvinAFahrenheit(kelvin);

        // Implementación de métodos de longitud
        public double MetrosAPies(double metros) => Channel.MetrosAPies(metros);
        public double PiesAMetros(double pies) => Channel.PiesAMetros(pies);
        public double MetrosAPulgadas(double metros) => Channel.MetrosAPulgadas(metros);
        public double PulgadasAMetros(double pulgadas) => Channel.PulgadasAMetros(pulgadas);
        public double KilometrosAMillas(double kilometros) => Channel.KilometrosAMillas(kilometros);
        public double MillasAKilometros(double millas) => Channel.MillasAKilometros(millas);

        // Implementación de métodos de peso
        public double KilogramosALibras(double kilogramos) => Channel.KilogramosALibras(kilogramos);
        public double LibrasAKilogramos(double libras) => Channel.LibrasAKilogramos(libras);
        public double GramosAOnzas(double gramos) => Channel.GramosAOnzas(gramos);
        public double OnzasAGramos(double onzas) => Channel.OnzasAGramos(onzas);


        // Implementación de métodos de área
    }

    /// <summary>
    /// Servicio wrapper para facilitar el uso del cliente SOAP
    /// </summary>
    public class ConversionSoapService
    {
        private readonly string _endpoint;

        public ConversionSoapService(string endpoint)
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentException("El endpoint no puede ser nulo o vacío", nameof(endpoint));
            }
            _endpoint = endpoint;
        }

        public async Task<ConversionResponse> ConvertirAsync(ConversionRequest request)
        {
            try
            {
                double resultado = await Task.Run(() =>
                {
                    try
                    {
                        using var client = new ConversionSoapClient(_endpoint);
                        
                        // Mapear según la categoría y unidades
                        return request.Categoria.ToLower() switch
                        {
                            "temperatura" => ConvertirTemperatura(client, request),
                            "longitud" => ConvertirLongitud(client, request),
                            "peso" => ConvertirPeso(client, request),
                            _ => throw new NotSupportedException("Categoría no válida")
                        };
                    }
                    catch (Exception ex)
                    {
                        // Re-lanzar la excepción para que se capture en el catch externo
                        throw;
                    }
                });

                return new ConversionResponse
                {
                    Exito = true,
                    ValorOriginal = request.Valor,
                    ValorConvertido = resultado,
                    UnidadOrigen = request.UnidadOrigen,
                    UnidadDestino = request.UnidadDestino,
                    Categoria = request.Categoria,
                    Mensaje = "Conversión exitosa",
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };
            }
            catch (NotSupportedException ex)
            {
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = ex.Message
                };
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = "Servidor desconectado. El servicio SOAP no está disponible. Por favor, inicia el servidor SOAP en el puerto 62533."
                };
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                // Verificar si es un error de conexión
                var innerEx = ex.InnerException;
                while (innerEx != null)
                {
                    if (innerEx is System.Net.Sockets.SocketException || 
                        innerEx is System.Net.Http.HttpRequestException)
                    {
                        return new ConversionResponse
                        {
                            Exito = false,
                            Mensaje = "Servidor desconectado. El servicio SOAP no está disponible. Por favor, inicia el servidor SOAP."
                        };
                    }
                    innerEx = innerEx.InnerException;
                }
                
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = "Servidor desconectado. No se puede establecer conexión con el servicio SOAP. Verifica que el servidor esté ejecutándose."
                };
            }
            catch (AggregateException aggEx)
            {
                // Capturar excepciones agregadas de Task.Run
                var innerEx = aggEx.InnerException;
                while (innerEx != null)
                {
                    if (innerEx is System.ServiceModel.CommunicationException ||
                        innerEx is System.ServiceModel.EndpointNotFoundException)
                    {
                        return new ConversionResponse
                        {
                            Exito = false,
                            Mensaje = "Servidor desconectado. El servicio SOAP no está disponible. Por favor, inicia el servidor SOAP."
                        };
                    }
                    if (innerEx is System.Net.Sockets.SocketException ||
                        innerEx is System.Net.Http.HttpRequestException)
                    {
                        return new ConversionResponse
                        {
                            Exito = false,
                            Mensaje = "Servidor desconectado. El servicio SOAP no está disponible. Por favor, inicia el servidor SOAP."
                        };
                    }
                    innerEx = innerEx.InnerException;
                }
                
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = $"Error: {aggEx.Message}"
                };
            }
            catch (Exception ex)
            {
                // Verificar todas las excepciones anidadas
                var innerEx = ex.InnerException;
                while (innerEx != null)
                {
                    if (innerEx is System.ServiceModel.CommunicationException ||
                        innerEx is System.ServiceModel.EndpointNotFoundException ||
                        innerEx is System.Net.Sockets.SocketException ||
                        innerEx is System.Net.Http.HttpRequestException)
                    {
                        return new ConversionResponse
                        {
                            Exito = false,
                            Mensaje = "Servidor desconectado. El servicio SOAP no está disponible. Por favor, inicia el servidor SOAP."
                        };
                    }
                    innerEx = innerEx.InnerException;
                }
                
                return new ConversionResponse
                {
                    Exito = false,
                    Mensaje = $"Error: {ex.Message}"
                };
            }
        }

        private double ConvertirTemperatura(ConversionSoapClient client, ConversionRequest request)
        {
            var from = request.UnidadOrigen.ToLower();
            var to = request.UnidadDestino.ToLower();

            return (from, to) switch
            {
                ("celsius", "fahrenheit") => client.CelsiusAFahrenheit(request.Valor),
                ("fahrenheit", "celsius") => client.FahrenheitACelsius(request.Valor),
                ("celsius", "kelvin") => client.CelsiusAKelvin(request.Valor),
                ("kelvin", "celsius") => client.KelvinACelsius(request.Valor),
                ("fahrenheit", "kelvin") => client.FahrenheitAKelvin(request.Valor),
                ("kelvin", "fahrenheit") => client.KelvinAFahrenheit(request.Valor),
                _ => throw new NotSupportedException($"Conversión de {from} a {to} no soportada")
            };
        }

        private double ConvertirLongitud(ConversionSoapClient client, ConversionRequest request)
        {
            var from = request.UnidadOrigen.ToLower();
            var to = request.UnidadDestino.ToLower();

            return (from, to) switch
            {
                ("metros", "pies") => client.MetrosAPies(request.Valor),
                ("pies", "metros") => client.PiesAMetros(request.Valor),
                ("metros", "pulgadas") => client.MetrosAPulgadas(request.Valor),
                ("pulgadas", "metros") => client.PulgadasAMetros(request.Valor),
                ("kilometros", "millas") => client.KilometrosAMillas(request.Valor),
                ("millas", "kilometros") => client.MillasAKilometros(request.Valor),
                _ => throw new NotSupportedException($"Conversión de {from} a {to} no soportada")
            };
        }

        private double ConvertirPeso(ConversionSoapClient client, ConversionRequest request)
        {
            var from = request.UnidadOrigen.ToLower();
            var to = request.UnidadDestino.ToLower();

            return (from, to) switch
            {
                ("kilogramos", "libras") => client.KilogramosALibras(request.Valor),
                ("libras", "kilogramos") => client.LibrasAKilogramos(request.Valor),
                ("gramos", "onzas") => client.GramosAOnzas(request.Valor),
                ("onzas", "gramos") => client.OnzasAGramos(request.Valor),
                _ => throw new NotSupportedException($"Conversión de {from} a {to} no soportada")
            };
        }

        {
            var from = request.UnidadOrigen.ToLower();
            var to = request.UnidadDestino.ToLower();

            return (from, to) switch
            {
                _ => throw new NotSupportedException($"Conversión de {from} a {to} no soportada")
            };
        }

        {
            var from = request.UnidadOrigen.ToLower();
            var to = request.UnidadDestino.ToLower();

            return (from, to) switch
            {
                _ => throw new NotSupportedException($"Conversión de {from} a {to} no soportada")
            };
        }
    }
}
