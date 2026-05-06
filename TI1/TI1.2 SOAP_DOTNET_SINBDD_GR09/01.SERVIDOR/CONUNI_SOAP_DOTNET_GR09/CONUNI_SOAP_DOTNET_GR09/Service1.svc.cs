using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CONUNI_SOAP_DOTNET_GR09
{
    /// <summary>
    /// Servicio de conversiones de unidades - Replicado desde Java
    /// Implementa todas las operaciones de conversión de temperatura, longitud, peso, volumen y área
    /// </summary>
    public class WSConversion : IWSConversion
    {
        private readonly AutenticacionService _autenticacionService = new AutenticacionService();

        // ========== MÉTODO DE AUTENTICACIÓN ==========

        /// <summary>
        /// Valida las credenciales del usuario. La verificación ocurre ÚNICAMENTE en el servidor.
        /// </summary>
        public bool Login(string usuario, string contrasena)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: Login – intento de autenticación para usuario='{usuario}'");
            return _autenticacionService.ValidarCredenciales(usuario, contrasena);
        }

        // ========== MÉTODOS DE TEMPERATURA ==========
        
        public double CelsiusAFahrenheit(double celsius)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: CelsiusAFahrenheit recibió celsius={celsius}");
            return (celsius * 9.0 / 5.0) + 32.0;
        }

        public double FahrenheitACelsius(double fahrenheit)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: FahrenheitACelsius recibió fahrenheit={fahrenheit}");
            return (fahrenheit - 32.0) * 5.0 / 9.0;
        }

        public double CelsiusAKelvin(double celsius)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: CelsiusAKelvin recibió celsius={celsius}");
            return celsius + 273.15;
        }

        public double KelvinACelsius(double kelvin)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: KelvinACelsius recibió kelvin={kelvin}");
            return kelvin - 273.15;
        }

        public double FahrenheitAKelvin(double fahrenheit)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: FahrenheitAKelvin recibió fahrenheit={fahrenheit}");
            return (fahrenheit - 32.0) * 5.0 / 9.0 + 273.15;
        }

        public double KelvinAFahrenheit(double kelvin)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: KelvinAFahrenheit recibió kelvin={kelvin}");
            return (kelvin - 273.15) * 9.0 / 5.0 + 32.0;
        }

        // ========== MÉTODOS DE LONGITUD ==========
        
        public double MetrosAPies(double metros)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: MetrosAPies recibió metros={metros}");
            return metros * 3.28084;
        }

        public double PiesAMetros(double pies)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: PiesAMetros recibió pies={pies}");
            return pies / 3.28084;
        }

        public double MetrosAPulgadas(double metros)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: MetrosAPulgadas recibió metros={metros}");
            return metros * 39.3701;
        }

        public double PulgadasAMetros(double pulgadas)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: PulgadasAMetros recibió pulgadas={pulgadas}");
            return pulgadas / 39.3701;
        }

        public double KilometrosAMillas(double kilometros)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: KilometrosAMillas recibió kilometros={kilometros}");
            return kilometros * 0.621371;
        }

        public double MillasAKilometros(double millas)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: MillasAKilometros recibió millas={millas}");
            return millas / 0.621371;
        }

        // ========== MÉTODOS DE PESO/MASA ==========
        
        public double KilogramosALibras(double kilogramos)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: KilogramosALibras recibió kilogramos={kilogramos}");
            return kilogramos * 2.20462;
        }

        public double LibrasAKilogramos(double libras)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: LibrasAKilogramos recibió libras={libras}");
            return libras / 2.20462;
        }

        public double GramosAOnzas(double gramos)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: GramosAOnzas recibió gramos={gramos}");
            return gramos * 0.035274;
        }

        public double OnzasAGramos(double onzas)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: OnzasAGramos recibió onzas={onzas}");
            return onzas / 0.035274;
        }

        // ========== MÉTODOS DE VOLUMEN ==========
        
        public double LitrosAGalones(double litros)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: LitrosAGalones recibió litros={litros}");
            return litros * 0.264172;
        }

        public double GalonesALitros(double galones)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: GalonesALitros recibió galones={galones}");
            return galones / 0.264172;
        }

        public double MililitrosAOnzasFluidas(double mililitros)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: MililitrosAOnzasFluidas recibió mililitros={mililitros}");
            return mililitros * 0.033814;
        }

        public double OnzasFluidasAMililitros(double onzasFluidas)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: OnzasFluidasAMililitros recibió onzasFluidas={onzasFluidas}");
            return onzasFluidas / 0.033814;
        }

        // ========== MÉTODOS DE ÁREA ==========
        
        public double MetrosCuadradosAPiesCuadrados(double metrosCuadrados)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: MetrosCuadradosAPiesCuadrados recibió metrosCuadrados={metrosCuadrados}");
            return metrosCuadrados * 10.7639;
        }

        public double PiesCuadradosAMetrosCuadrados(double piesCuadrados)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: PiesCuadradosAMetrosCuadrados recibió piesCuadrados={piesCuadrados}");
            return piesCuadrados / 10.7639;
        }

        public double HectareasAAcres(double hectareas)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: HectareasAAcres recibió hectareas={hectareas}");
            return hectareas * 2.47105;
        }

        public double AcresAHectareas(double acres)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: AcresAHectareas recibió acres={acres}");
            return acres / 2.47105;
        }
    }
}
