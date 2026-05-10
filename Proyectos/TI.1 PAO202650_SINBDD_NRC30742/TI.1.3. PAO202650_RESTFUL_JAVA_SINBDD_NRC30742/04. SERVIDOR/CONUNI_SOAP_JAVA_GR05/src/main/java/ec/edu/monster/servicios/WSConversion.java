package ec.edu.monster.servicios;

import jakarta.jws.WebMethod;
import jakarta.jws.WebService;
import jakarta.jws.WebParam;
import jakarta.jws.soap.SOAPBinding;

@WebService(serviceName = "WSConversion")
@SOAPBinding(style = SOAPBinding.Style.RPC, use = SOAPBinding.Use.LITERAL)
public class WSConversion {

    private ConversionService   conversionService   = new ConversionService();
    private AutenticacionService autenticacionService = new AutenticacionService();

    // ========== MÉTODO DE AUTENTICACIÓN ==========

    /**
     * Valida las credenciales del usuario.
     * La verificación ocurre ÚNICAMENTE en el servidor.
     *
     * @param usuario   nombre de usuario
     * @param contrasena contraseña en texto plano
     * @return {@code true} si las credenciales son válidas
     */
    @WebMethod(operationName = "login")
    public boolean login(
            @WebParam(name = "usuario")    String usuario,
            @WebParam(name = "contrasena") String contrasena) {
        System.out.println("DEBUG: login – intento de autenticación para usuario='" + usuario + "'");
        return autenticacionService.validarCredenciales(usuario, contrasena);
    }

    // ========== MÉTODOS DE TEMPERATURA ==========
    
    @WebMethod(operationName = "celsiusAFahrenheit")
    public double celsiusAFahrenheit(@WebParam(name = "celsius") double celsius) {
        System.out.println("DEBUG: celsiusAFahrenheit recibió celsius=" + celsius);
        return conversionService.celsiusAFahrenheit(celsius);
    }

    @WebMethod(operationName = "fahrenheitACelsius")
    public double fahrenheitACelsius(@WebParam(name = "fahrenheit") double fahrenheit) {
        System.out.println("DEBUG: fahrenheitACelsius recibió fahrenheit=" + fahrenheit);
        return conversionService.fahrenheitACelsius(fahrenheit);
    }

    @WebMethod(operationName = "celsiusAKelvin")
    public double celsiusAKelvin(@WebParam(name = "celsius") double celsius) {
        System.out.println("DEBUG: celsiusAKelvin recibió celsius=" + celsius);
        return conversionService.celsiusAKelvin(celsius);
    }

    @WebMethod(operationName = "kelvinACelsius")
    public double kelvinACelsius(@WebParam(name = "kelvin") double kelvin) {
        System.out.println("DEBUG: kelvinACelsius recibió kelvin=" + kelvin);
        return conversionService.kelvinACelsius(kelvin);
    }

    @WebMethod(operationName = "fahrenheitAKelvin")
    public double fahrenheitAKelvin(@WebParam(name = "fahrenheit") double fahrenheit) {
        System.out.println("DEBUG: fahrenheitAKelvin recibió fahrenheit=" + fahrenheit);
        return conversionService.fahrenheitAKelvin(fahrenheit);
    }

    @WebMethod(operationName = "kelvinAFahrenheit")
    public double kelvinAFahrenheit(@WebParam(name = "kelvin") double kelvin) {
        System.out.println("DEBUG: kelvinAFahrenheit recibió kelvin=" + kelvin);
        return conversionService.kelvinAFahrenheit(kelvin);
    }

    // ========== MÉTODOS DE LONGITUD ==========
    
    @WebMethod(operationName = "metrosAPies")
    public double metrosAPies(@WebParam(name = "metros") double metros) {
        System.out.println("DEBUG: metrosAPies recibió metros=" + metros);
        return conversionService.metrosAPies(metros);
    }

    @WebMethod(operationName = "piesAMetros")
    public double piesAMetros(@WebParam(name = "pies") double pies) {
        System.out.println("DEBUG: piesAMetros recibió pies=" + pies);
        return conversionService.piesAMetros(pies);
    }

    @WebMethod(operationName = "metrosAPulgadas")
    public double metrosAPulgadas(@WebParam(name = "metros") double metros) {
        System.out.println("DEBUG: metrosAPulgadas recibió metros=" + metros);
        return conversionService.metrosAPulgadas(metros);
    }

    @WebMethod(operationName = "pulgadasAMetros")
    public double pulgadasAMetros(@WebParam(name = "pulgadas") double pulgadas) {
        System.out.println("DEBUG: pulgadasAMetros recibió pulgadas=" + pulgadas);
        return conversionService.pulgadasAMetros(pulgadas);
    }

    @WebMethod(operationName = "kilometrosAMillas")
    public double kilometrosAMillas(@WebParam(name = "kilometros") double kilometros) {
        System.out.println("DEBUG: kilometrosAMillas recibió kilometros=" + kilometros);
        return conversionService.kilometrosAMillas(kilometros);
    }

    @WebMethod(operationName = "millasAKilometros")
    public double millasAKilometros(@WebParam(name = "millas") double millas) {
        System.out.println("DEBUG: millasAKilometros recibió millas=" + millas);
        return conversionService.millasAKilometros(millas);
    }

    // ========== MÉTODOS DE PESO/MASA ==========
    
    @WebMethod(operationName = "kilogramosALibras")
    public double kilogramosALibras(@WebParam(name = "kilogramos") double kilogramos) {
        System.out.println("DEBUG: kilogramosALibras recibió kilogramos=" + kilogramos);
        return conversionService.kilogramosALibras(kilogramos);
    }

    @WebMethod(operationName = "librasAKilogramos")
    public double librasAKilogramos(@WebParam(name = "libras") double libras) {
        System.out.println("DEBUG: librasAKilogramos recibió libras=" + libras);
        return conversionService.librasAKilogramos(libras);
    }

    @WebMethod(operationName = "gramosAOnzas")
    public double gramosAOnzas(@WebParam(name = "gramos") double gramos) {
        System.out.println("DEBUG: gramosAOnzas recibió gramos=" + gramos);
        return conversionService.gramosAOnzas(gramos);
    }

    @WebMethod(operationName = "onzasAGramos")
    public double onzasAGramos(@WebParam(name = "onzas") double onzas) {
        System.out.println("DEBUG: onzasAGramos recibió onzas=" + onzas);
        return conversionService.onzasAGramos(onzas);
    }

    // ========== MÉTODOS DE VOLUMEN ==========
    
    @WebMethod(operationName = "litrosAGalones")
    public double litrosAGalones(@WebParam(name = "litros") double litros) {
        System.out.println("DEBUG: litrosAGalones recibió litros=" + litros);
        return conversionService.litrosAGalones(litros);
    }

    @WebMethod(operationName = "galonesALitros")
    public double galonesALitros(@WebParam(name = "galones") double galones) {
        System.out.println("DEBUG: galonesALitros recibió galones=" + galones);
        return conversionService.galonesALitros(galones);
    }

    @WebMethod(operationName = "mililitrosAOnzasFluidas")
    public double mililitrosAOnzasFluidas(@WebParam(name = "mililitros") double mililitros) {
        System.out.println("DEBUG: mililitrosAOnzasFluidas recibió mililitros=" + mililitros);
        return conversionService.mililitrosAOnzasFluidas(mililitros);
    }

    @WebMethod(operationName = "onzasFluidasAMililitros")
    public double onzasFluidasAMililitros(@WebParam(name = "onzasFluidas") double onzasFluidas) {
        System.out.println("DEBUG: onzasFluidasAMililitros recibió onzasFluidas=" + onzasFluidas);
        return conversionService.onzasFluidasAMililitros(onzasFluidas);
    }

    // ========== MÉTODOS DE ÁREA ==========
    
    @WebMethod(operationName = "metrosCuadradosAPiesCuadrados")
    public double metrosCuadradosAPiesCuadrados(@WebParam(name = "metrosCuadrados") double metrosCuadrados) {
        System.out.println("DEBUG: metrosCuadradosAPiesCuadrados recibió metrosCuadrados=" + metrosCuadrados);
        return conversionService.metrosCuadradosAPiesCuadrados(metrosCuadrados);
    }

    @WebMethod(operationName = "piesCuadradosAMetrosCuadrados")
    public double piesCuadradosAMetrosCuadrados(@WebParam(name = "piesCuadrados") double piesCuadrados) {
        System.out.println("DEBUG: piesCuadradosAMetrosCuadrados recibió piesCuadrados=" + piesCuadrados);
        return conversionService.piesCuadradosAMetrosCuadrados(piesCuadrados);
    }

    @WebMethod(operationName = "hectareasAAcres")
    public double hectareasAAcres(@WebParam(name = "hectareas") double hectareas) {
        System.out.println("DEBUG: hectareasAAcres recibió hectareas=" + hectareas);
        return conversionService.hectareasAAcres(hectareas);
    }

    @WebMethod(operationName = "acresAHectareas")
    public double acresAHectareas(@WebParam(name = "acres") double acres) {
        System.out.println("DEBUG: acresAHectareas recibió acres=" + acres);
        return conversionService.acresAHectareas(acres);
    }
}

