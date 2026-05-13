using System;
using System.Threading.Tasks;
using CONUNI_RESTFUL_DOTNET_CLICON_G09.servicio;
using CONUNI_RESTFUL_DOTNET_CLICON_G09.vista;

namespace CONUNI_RESTFUL_DOTNET_CLICON_G09.prueba
{
    /// <summary>
    /// Clase para probar el servicio RESTful de conversión
    /// </summary>
    public class PruebaConversion
    {
        private readonly ConversionApiClient _client;
        private readonly VistaConsola _vista;

        public PruebaConversion(ConversionApiClient client)
        {
            _client = client;
            _vista = new VistaConsola();
        }

        /// <summary>
        /// Ejecuta el menú principal
        /// </summary>
        public async Task EjecutarMenuAsync()
        {
            _vista.MostrarBienvenida();

            while (true)
            {
                int opcion = _vista.MostrarMenu();

                switch (opcion)
                {
                    case 1:
                        await MenuTemperaturaAsync();
                        break;
                    case 2:
                        await MenuLongitudAsync();
                        break;
                    case 3:
                        await MenuPesoAsync();
                        break;
                    case 4:
                        await MostrarInfoServicioAsync();
                        break;
                    case 0:
                        _vista.MostrarDespedida();
                        return;
                    default:
                        _vista.MostrarOpcionNoValida();
                        _vista.MostrarPausa();
                        break;
                }
            }
        }

        private async Task MenuTemperaturaAsync()
        {
            Console.Clear();
            _vista.MostrarSubmenuTemperatura();

            try
            {
                string opcionStr = Console.ReadLine()?.Trim() ?? "";
                int opcion = int.Parse(opcionStr);

                if (opcion == 0) return;

                double valor = _vista.SolicitarValor("");

                ConversionResponse resultado = null;

                switch (opcion)
                {
                    case 1:
                        resultado = await _client.CelsiusAFahrenheitAsync(valor);
                        break;
                    case 2:
                        resultado = await _client.FahrenheitACelsiusAsync(valor);
                        break;
                    case 3:
                        resultado = await _client.CelsiusAKelvinAsync(valor);
                        break;
                    case 4:
                        resultado = await _client.KelvinACelsiusAsync(valor);
                        break;
                    case 5:
                        resultado = await _client.FahrenheitAKelvinAsync(valor);
                        break;
                    case 6:
                        resultado = await _client.KelvinAFahrenheitAsync(valor);
                        break;
                    default:
                        _vista.MostrarOpcionNoValida();
                        _vista.MostrarPausa();
                        return;
                }

                if (resultado != null)
                {
                    _vista.MostrarResultado(resultado);
                }
            }
            catch
            {
                _vista.MostrarErrorEntrada("Opción inválida");
            }

            _vista.MostrarPausa();
        }

        private async Task MenuLongitudAsync()
        {
            Console.Clear();
            _vista.MostrarSubmenuLongitud();

            try
            {
                string opcionStr = Console.ReadLine()?.Trim() ?? "";
                int opcion = int.Parse(opcionStr);

                if (opcion == 0) return;

                double valor = _vista.SolicitarValor("");

                ConversionResponse resultado = null;

                switch (opcion)
                {
                    case 1:
                        resultado = await _client.MetrosAPiesAsync(valor);
                        break;
                    case 2:
                        resultado = await _client.PiesAMetrosAsync(valor);
                        break;
                    case 3:
                        resultado = await _client.MetrosAPulgadasAsync(valor);
                        break;
                    case 4:
                        resultado = await _client.PulgadasAMetrosAsync(valor);
                        break;
                    case 5:
                        resultado = await _client.KilometrosAMillasAsync(valor);
                        break;
                    case 6:
                        resultado = await _client.MillasAKilometrosAsync(valor);
                        break;
                    default:
                        _vista.MostrarOpcionNoValida();
                        _vista.MostrarPausa();
                        return;
                }

                if (resultado != null)
                {
                    _vista.MostrarResultado(resultado);
                }
            }
            catch
            {
                _vista.MostrarErrorEntrada("Opción inválida");
            }

            _vista.MostrarPausa();
        }

        private async Task MenuPesoAsync()
        {
            Console.Clear();
            _vista.MostrarSubmenuPeso();

            try
            {
                string opcionStr = Console.ReadLine()?.Trim() ?? "";
                int opcion = int.Parse(opcionStr);

                if (opcion == 0) return;

                double valor = _vista.SolicitarValor("");

                ConversionResponse resultado = null;

                switch (opcion)
                {
                    case 1:
                        resultado = await _client.KilogramosALibrasAsync(valor);
                        break;
                    case 2:
                        resultado = await _client.LibrasAKilogramosAsync(valor);
                        break;
                    case 3:
                        resultado = await _client.GramosAOnzasAsync(valor);
                        break;
                    case 4:
                        resultado = await _client.OnzasAGramosAsync(valor);
                        break;
                    default:
                        _vista.MostrarOpcionNoValida();
                        _vista.MostrarPausa();
                        return;
                }

                if (resultado != null)
                {
                    _vista.MostrarResultado(resultado);
                }
            }
            catch
            {
                _vista.MostrarErrorEntrada("Opción inválida");
            }

            _vista.MostrarPausa();
        }


        private async Task MostrarInfoServicioAsync()
        {
            Console.Clear();
            var info = await _client.ObtenerInfoAsync();
            _vista.MostrarInfoServicio(info);
            _vista.MostrarPausa();
        }
    }
}