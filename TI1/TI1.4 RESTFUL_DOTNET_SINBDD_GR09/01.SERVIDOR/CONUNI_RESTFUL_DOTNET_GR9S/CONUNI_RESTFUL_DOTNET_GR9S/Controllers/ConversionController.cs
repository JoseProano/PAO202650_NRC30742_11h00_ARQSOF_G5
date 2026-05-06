using System;
using System.Linq;
using System.Web.Http;
using System.Diagnostics;
using CONUNI_RESTFUL_DOTNET_GR9S.Models;

namespace CONUNI_RESTFUL_DOTNET_GR9S.Controllers
{
    /// <summary>
    /// Controlador REST para las conversiones de unidades
    /// Maneja todas las peticiones HTTP y expone los endpoints RESTful
    /// TODOS LOS CÁLCULOS SE HACEN DIRECTAMENTE EN EL SERVIDOR
    /// </summary>
    [RoutePrefix("api/conversion")]
    public class ConversionController : ApiController
    {
        private readonly ConversionService _conversionService = new ConversionService();
        
        /// <summary>
        /// Método auxiliar para logging
        /// </summary>
        private void Log(string mensaje)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensaje}";
            Debug.WriteLine(logMessage);
            Trace.WriteLine(logMessage);
            System.Console.WriteLine(logMessage);
        }
        
        /// <summary>
        /// Obtiene la IP del cliente desde la petición HTTP
        /// </summary>
        private string GetClientIpAddress()
        {
            try
            {
                if (Request.Properties.ContainsKey("MS_HttpContext"))
                {
                    var httpContext = Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper;
                    if (httpContext != null)
                    {
                        return httpContext.Request.UserHostAddress;
                    }
                }
                
                // Fallback: obtener del header X-Forwarded-For
                if (Request.Headers.Contains("X-Forwarded-For"))
                {
                    var forwardedFor = Request.Headers.GetValues("X-Forwarded-For").FirstOrDefault();
                    if (!string.IsNullOrEmpty(forwardedFor))
                    {
                        return forwardedFor.Split(',')[0].Trim();
                    }
                }
                
                // Obtener del header X-Real-IP
                if (Request.Headers.Contains("X-Real-IP"))
                {
                    var realIp = Request.Headers.GetValues("X-Real-IP").FirstOrDefault();
                    if (!string.IsNullOrEmpty(realIp))
                    {
                        return realIp;
                    }
                }
                
                return "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }
        
        // ========== ENDPOINTS GENERALES ==========
        
        /// <summary>
        /// Endpoint base para mostrar información del servicio
        /// GET /api/conversion/
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult ObtenerInfoBase()
        {
            string info = "Monsters Inc. Converter RESTful API v1.0 - Powered by Sullivan's Energy\n\n" +
                         "Endpoints disponibles:\n" +
                         "- GET /api/conversion/info - Información del servicio\n" +
                         "- GET /api/conversion/temperatura/* - Conversiones de temperatura\n" +
                         "- GET /api/conversion/longitud/* - Conversiones de longitud\n" +
                         "- GET /api/conversion/peso/* - Conversiones de peso/masa\n" +
                         "- GET /api/conversion/volumen/* - Conversiones de volumen\n" +
                         "- GET /api/conversion/area/* - Conversiones de área\n" +
                         "- POST /api/conversion/convertir - Conversión genérica\n\n" +
                         "Ejemplo: GET /api/conversion/temperatura/celsius-to-fahrenheit?celsius=100";
            return Ok(info);
        }
        
        /// <summary>
        /// Endpoint principal para conversiones genéricas
        /// POST /api/conversion/convertir
        /// </summary>
        [HttpPost]
        [Route("convertir")]
        public IHttpActionResult Convertir(ConversionRequest request)
        {
            try
            {
                // Log de la petición entrante
                string ipCliente = GetClientIpAddress();
                Log($"POST /api/conversion/convertir - IP Cliente: {ipCliente}");
                Log($"Request: Valor={request?.Valor}, Origen={request?.UnidadOrigen}, Destino={request?.UnidadDestino}, Categoria={request?.Categoria}");
                
                // Validar entrada
                if (request.Valor < 0 && !request.Categoria.Equals(Conversion.CATEGORIA_TEMPERATURA))
                {
                    var errorResponse = ConversionResponse.CrearError("El valor no puede ser negativo para esta categoría");
                    return BadRequest(errorResponse.Mensaje);
                }
                
                if (!Conversion.EsConversionValida(request.UnidadOrigen, request.UnidadDestino, request.Categoria))
                {
                    var errorResponse = ConversionResponse.CrearError($"Conversión no válida entre {request.UnidadOrigen} y {request.UnidadDestino}");
                    return BadRequest(errorResponse.Mensaje);
                }
                
                // Realizar conversión directamente en el servidor
                double resultado = RealizarConversionDirecta(request.Valor, request.UnidadOrigen, request.UnidadDestino, request.Categoria);
                
                var response = ConversionResponse.CrearExito(
                    request.Valor, resultado, request.UnidadOrigen, request.UnidadDestino, request.Categoria
                );
                
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error interno: {e.Message}");
                return InternalServerError(new Exception(errorResponse.Mensaje));
            }
        }
        
        /// <summary>
        /// Endpoint para obtener información del servicio
        /// GET /api/conversion/info
        /// </summary>
        [HttpGet]
        [Route("info")]
        public IHttpActionResult ObtenerInfo()
        {
            string info = "Monsters Inc. Converter RESTful API v1.0 - Powered by Sullivan's Energy";
            return Ok(info);
        }
        
        /// <summary>
        /// Endpoint para obtener lista de endpoints disponibles
        /// GET /api/conversion/endpoints
        /// </summary>
        [HttpGet]
        [Route("endpoints")]
        public IHttpActionResult ObtenerEndpoints()
        {
            try
            {
                var endpoints = new
                {
                    servicio = "Monsters Inc. Converter RESTful API",
                    version = "1.0",
                    baseUrl = "http://localhost:PORT/CONUNI_RESTFUL_DOTNET_GR9S/api/conversion",
                    endpoints = new
                    {
                        temperatura = new[]
                        {
                            "GET /temperatura/celsius-to-fahrenheit?celsius=100",
                            "GET /temperatura/fahrenheit-to-celsius?fahrenheit=212",
                            "GET /temperatura/celsius-to-kelvin?celsius=0",
                            "GET /temperatura/kelvin-to-celsius?kelvin=273.15",
                            "GET /temperatura/fahrenheit-to-kelvin?fahrenheit=32",
                            "GET /temperatura/kelvin-to-fahrenheit?kelvin=273.15"
                        },
                        longitud = new[]
                        {
                            "GET /longitud/metros-to-pies?metros=1",
                            "GET /longitud/pies-to-metros?pies=3.28084",
                            "GET /longitud/metros-to-pulgadas?metros=1",
                            "GET /longitud/pulgadas-to-metros?pulgadas=39.3701",
                            "GET /longitud/kilometros-to-millas?kilometros=1",
                            "GET /longitud/millas-to-kilometros?millas=0.621371"
                        },
                        peso = new[]
                        {
                            "GET /peso/kilogramos-to-libras?kilogramos=1",
                            "GET /peso/libras-to-kilogramos?libras=2.20462",
                            "GET /peso/gramos-to-onzas?gramos=28.3495",
                            "GET /peso/onzas-to-gramos?onzas=1"
                        },
                        volumen = new[]
                        {
                            "GET /volumen/litros-to-galones?litros=1",
                            "GET /volumen/galones-to-litros?galones=1",
                            "GET /volumen/mililitros-to-onzas-fluidas?mililitros=29.5735",
                            "GET /volumen/onzas-fluidas-to-mililitros?onzasFluidas=1"
                        },
                        area = new[]
                        {
                            "GET /area/metros-cuadrados-to-pies-cuadrados?metrosCuadrados=1",
                            "GET /area/pies-cuadrados-to-metros-cuadrados?piesCuadrados=10.7639",
                            "GET /area/hectareas-to-acres?hectareas=1",
                            "GET /area/acres-to-hectareas?acres=2.47105"
                        },
                        generico = new[]
                        {
                            "POST /convertir"
                        }
                    }
                };
                return Ok(endpoints);
            }
            catch (Exception e)
            {
                return InternalServerError(new Exception($"Error al obtener endpoints: {e.Message}"));
            }
        }
        
        // ========== ENDPOINTS DE TEMPERATURA ==========
        
        [HttpGet]
        [Route("temperatura/celsius-to-fahrenheit")]
        public IHttpActionResult CelsiusAFahrenheit([FromUri] double celsius)
        {
            try
            {
                double resultado = (celsius * 9.0 / 5.0) + 32.0;
                var response = ConversionResponse.CrearExito(celsius, resultado, Conversion.CELSIUS, Conversion.FAHRENHEIT, Conversion.CATEGORIA_TEMPERATURA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("temperatura/fahrenheit-to-celsius")]
        public IHttpActionResult FahrenheitACelsius([FromUri] double fahrenheit)
        {
            try
            {
                double resultado = (fahrenheit - 32.0) * 5.0 / 9.0;
                var response = ConversionResponse.CrearExito(fahrenheit, resultado, Conversion.FAHRENHEIT, Conversion.CELSIUS, Conversion.CATEGORIA_TEMPERATURA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("temperatura/celsius-to-kelvin")]
        public IHttpActionResult CelsiusAKelvin([FromUri] double celsius)
        {
            try
            {
                double resultado = celsius + 273.15;
                var response = ConversionResponse.CrearExito(celsius, resultado, Conversion.CELSIUS, Conversion.KELVIN, Conversion.CATEGORIA_TEMPERATURA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("temperatura/kelvin-to-celsius")]
        public IHttpActionResult KelvinACelsius([FromUri] double kelvin)
        {
            try
            {
                double resultado = kelvin - 273.15;
                var response = ConversionResponse.CrearExito(kelvin, resultado, Conversion.KELVIN, Conversion.CELSIUS, Conversion.CATEGORIA_TEMPERATURA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("temperatura/fahrenheit-to-kelvin")]
        public IHttpActionResult FahrenheitAKelvin([FromUri] double fahrenheit)
        {
            try
            {
                double resultado = (fahrenheit - 32.0) * 5.0 / 9.0 + 273.15;
                var response = ConversionResponse.CrearExito(fahrenheit, resultado, Conversion.FAHRENHEIT, Conversion.KELVIN, Conversion.CATEGORIA_TEMPERATURA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("temperatura/kelvin-to-fahrenheit")]
        public IHttpActionResult KelvinAFahrenheit([FromUri] double kelvin)
        {
            try
            {
                double resultado = (kelvin - 273.15) * 9.0 / 5.0 + 32.0;
                var response = ConversionResponse.CrearExito(kelvin, resultado, Conversion.KELVIN, Conversion.FAHRENHEIT, Conversion.CATEGORIA_TEMPERATURA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        // ========== ENDPOINTS DE LONGITUD ==========
        
        [HttpGet]
        [Route("longitud/metros-to-pies")]
        public IHttpActionResult MetrosAPies([FromUri] double metros)
        {
            try
            {
                double resultado = metros * 3.28084;
                var response = ConversionResponse.CrearExito(metros, resultado, Conversion.METROS, Conversion.PIES, Conversion.CATEGORIA_LONGITUD);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("longitud/pies-to-metros")]
        public IHttpActionResult PiesAMetros([FromUri] double pies)
        {
            try
            {
                double resultado = pies / 3.28084;
                var response = ConversionResponse.CrearExito(pies, resultado, Conversion.PIES, Conversion.METROS, Conversion.CATEGORIA_LONGITUD);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("longitud/metros-to-pulgadas")]
        public IHttpActionResult MetrosAPulgadas([FromUri] double metros)
        {
            try
            {
                double resultado = metros * 39.3701;
                var response = ConversionResponse.CrearExito(metros, resultado, Conversion.METROS, Conversion.PULGADAS, Conversion.CATEGORIA_LONGITUD);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("longitud/pulgadas-to-metros")]
        public IHttpActionResult PulgadasAMetros([FromUri] double pulgadas)
        {
            try
            {
                double resultado = pulgadas / 39.3701;
                var response = ConversionResponse.CrearExito(pulgadas, resultado, Conversion.PULGADAS, Conversion.METROS, Conversion.CATEGORIA_LONGITUD);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("longitud/kilometros-to-millas")]
        public IHttpActionResult KilometrosAMillas([FromUri] double kilometros)
        {
            try
            {
                double resultado = kilometros * 0.621371;
                var response = ConversionResponse.CrearExito(kilometros, resultado, Conversion.KILOMETROS, Conversion.MILLAS, Conversion.CATEGORIA_LONGITUD);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("longitud/millas-to-kilometros")]
        public IHttpActionResult MillasAKilometros([FromUri] double millas)
        {
            try
            {
                double resultado = millas / 0.621371;
                var response = ConversionResponse.CrearExito(millas, resultado, Conversion.MILLAS, Conversion.KILOMETROS, Conversion.CATEGORIA_LONGITUD);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        // ========== ENDPOINTS DE PESO ==========
        
        [HttpGet]
        [Route("peso/kilogramos-to-libras")]
        public IHttpActionResult KilogramosALibras([FromUri] double kilogramos)
        {
            try
            {
                double resultado = kilogramos * 2.20462;
                var response = ConversionResponse.CrearExito(kilogramos, resultado, Conversion.KILOGRAMOS, Conversion.LIBRAS, Conversion.CATEGORIA_PESO);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("peso/libras-to-kilogramos")]
        public IHttpActionResult LibrasAKilogramos([FromUri] double libras)
        {
            try
            {
                double resultado = libras / 2.20462;
                var response = ConversionResponse.CrearExito(libras, resultado, Conversion.LIBRAS, Conversion.KILOGRAMOS, Conversion.CATEGORIA_PESO);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("peso/gramos-to-onzas")]
        public IHttpActionResult GramosAOnzas([FromUri] double gramos)
        {
            try
            {
                double resultado = gramos * 0.035274;
                var response = ConversionResponse.CrearExito(gramos, resultado, Conversion.GRAMOS, Conversion.ONZAS, Conversion.CATEGORIA_PESO);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("peso/onzas-to-gramos")]
        public IHttpActionResult OnzasAGramos([FromUri] double onzas)
        {
            try
            {
                double resultado = onzas / 0.035274;
                var response = ConversionResponse.CrearExito(onzas, resultado, Conversion.ONZAS, Conversion.GRAMOS, Conversion.CATEGORIA_PESO);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        // ========== ENDPOINTS DE VOLUMEN ==========
        
        [HttpGet]
        [Route("volumen/litros-to-galones")]
        public IHttpActionResult LitrosAGalones([FromUri] double litros)
        {
            try
            {
                double resultado = litros * 0.264172;
                var response = ConversionResponse.CrearExito(litros, resultado, Conversion.LITROS, Conversion.GALONES, Conversion.CATEGORIA_VOLUMEN);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("volumen/galones-to-litros")]
        public IHttpActionResult GalonesALitros([FromUri] double galones)
        {
            try
            {
                double resultado = galones / 0.264172;
                var response = ConversionResponse.CrearExito(galones, resultado, Conversion.GALONES, Conversion.LITROS, Conversion.CATEGORIA_VOLUMEN);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("volumen/mililitros-to-onzas-fluidas")]
        public IHttpActionResult MililitrosAOnzasFluidas([FromUri] double mililitros)
        {
            try
            {
                double resultado = mililitros * 0.033814;
                var response = ConversionResponse.CrearExito(mililitros, resultado, Conversion.MILILITROS, Conversion.ONZAS_FLUIDAS, Conversion.CATEGORIA_VOLUMEN);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("volumen/onzas-fluidas-to-mililitros")]
        public IHttpActionResult OnzasFluidasAMililitros([FromUri] double onzasFluidas)
        {
            try
            {
                double resultado = onzasFluidas / 0.033814;
                var response = ConversionResponse.CrearExito(onzasFluidas, resultado, Conversion.ONZAS_FLUIDAS, Conversion.MILILITROS, Conversion.CATEGORIA_VOLUMEN);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        // ========== ENDPOINTS DE ÁREA ==========
        
        [HttpGet]
        [Route("area/metros-cuadrados-to-pies-cuadrados")]
        public IHttpActionResult MetrosCuadradosAPiesCuadrados([FromUri] double metrosCuadrados)
        {
            try
            {
                double resultado = metrosCuadrados * 10.7639;
                var response = ConversionResponse.CrearExito(metrosCuadrados, resultado, Conversion.METROS_CUADRADOS, Conversion.PIES_CUADRADOS, Conversion.CATEGORIA_AREA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("area/pies-cuadrados-to-metros-cuadrados")]
        public IHttpActionResult PiesCuadradosAMetrosCuadrados([FromUri] double piesCuadrados)
        {
            try
            {
                double resultado = piesCuadrados / 10.7639;
                var response = ConversionResponse.CrearExito(piesCuadrados, resultado, Conversion.PIES_CUADRADOS, Conversion.METROS_CUADRADOS, Conversion.CATEGORIA_AREA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("area/hectareas-to-acres")]
        public IHttpActionResult HectareasAAcres([FromUri] double hectareas)
        {
            try
            {
                double resultado = hectareas * 2.47105;
                var response = ConversionResponse.CrearExito(hectareas, resultado, Conversion.HECTAREAS, Conversion.ACRES, Conversion.CATEGORIA_AREA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        [HttpGet]
        [Route("area/acres-to-hectareas")]
        public IHttpActionResult AcresAHectareas([FromUri] double acres)
        {
            try
            {
                double resultado = acres / 2.47105;
                var response = ConversionResponse.CrearExito(acres, resultado, Conversion.ACRES, Conversion.HECTAREAS, Conversion.CATEGORIA_AREA);
                return Ok(response);
            }
            catch (Exception e)
            {
                var errorResponse = ConversionResponse.CrearError($"Error en conversión: {e.Message}");
                return BadRequest(errorResponse.Mensaje);
            }
        }
        
        // ========== MÉTODO AUXILIAR PARA CONVERSIONES DIRECTAS ==========
        
        /// <summary>
        /// Realiza la conversión directamente en el servidor según los parámetros
        /// TODOS LOS CÁLCULOS SE HACEN AQUÍ EN EL SERVIDOR
        /// </summary>
        private double RealizarConversionDirecta(double valor, string unidadOrigen, string unidadDestino, string categoria)
        {
            string operacion = unidadOrigen + "A" + unidadDestino;
            
            switch (categoria.ToLower())
            {
                case Conversion.CATEGORIA_TEMPERATURA:
                    return ConvertirTemperaturaDirecta(valor, operacion);
                case Conversion.CATEGORIA_LONGITUD:
                    return ConvertirLongitudDirecta(valor, operacion);
                case Conversion.CATEGORIA_PESO:
                    return ConvertirPesoDirecta(valor, operacion);
                case Conversion.CATEGORIA_VOLUMEN:
                    return ConvertirVolumenDirecta(valor, operacion);
                case Conversion.CATEGORIA_AREA:
                    return ConvertirAreaDirecta(valor, operacion);
                default:
                    throw new ArgumentException($"Categoría no soportada: {categoria}");
            }
        }
        
        private double ConvertirTemperaturaDirecta(double valor, string operacion)
        {
            switch (operacion)
            {
                case "celsiusAfahrenheit": return (valor * 9.0 / 5.0) + 32.0;
                case "fahrenheitAcelsius": return (valor - 32.0) * 5.0 / 9.0;
                case "celsiusAkelvin": return valor + 273.15;
                case "kelvinAcelsius": return valor - 273.15;
                case "fahrenheitAkelvin": return (valor - 32.0) * 5.0 / 9.0 + 273.15;
                case "kelvinAfahrenheit": return (valor - 273.15) * 9.0 / 5.0 + 32.0;
                default: throw new ArgumentException($"Operación de temperatura no soportada: {operacion}");
            }
        }
        
        private double ConvertirLongitudDirecta(double valor, string operacion)
        {
            switch (operacion)
            {
                case "metrosApies": return valor * 3.28084;
                case "piesAmetros": return valor / 3.28084;
                case "metrosApulgadas": return valor * 39.3701;
                case "pulgadasAmetros": return valor / 39.3701;
                case "kilometrosAmillas": return valor * 0.621371;
                case "millasAkilometros": return valor / 0.621371;
                default: throw new ArgumentException($"Operación de longitud no soportada: {operacion}");
            }
        }
        
        private double ConvertirPesoDirecta(double valor, string operacion)
        {
            switch (operacion)
            {
                case "kilogramosAlibras": return valor * 2.20462;
                case "librasAkilogramos": return valor / 2.20462;
                case "gramosAonzas": return valor * 0.035274;
                case "onzasAgramos": return valor / 0.035274;
                default: throw new ArgumentException($"Operación de peso no soportada: {operacion}");
            }
        }
        
        private double ConvertirVolumenDirecta(double valor, string operacion)
        {
            switch (operacion)
            {
                case "litrosAgalones": return valor * 0.264172;
                case "galonesAlitros": return valor / 0.264172;
                case "mililitrosAonzasFluidas": return valor * 0.033814;
                case "onzasFluidasAmililitros": return valor / 0.033814;
                default: throw new ArgumentException($"Operación de volumen no soportada: {operacion}");
            }
        }
        
        private double ConvertirAreaDirecta(double valor, string operacion)
        {
            switch (operacion)
            {
                case "metrosCuadradosApiesCuadrados": return valor * 10.7639;
                case "piesCuadradosAmetrosCuadrados": return valor / 10.7639;
                case "hectareasAacres": return valor * 2.47105;
                case "acresAhectareas": return valor / 2.47105;
                default: throw new ArgumentException($"Operación de área no soportada: {operacion}");
            }
        }
    }
}

