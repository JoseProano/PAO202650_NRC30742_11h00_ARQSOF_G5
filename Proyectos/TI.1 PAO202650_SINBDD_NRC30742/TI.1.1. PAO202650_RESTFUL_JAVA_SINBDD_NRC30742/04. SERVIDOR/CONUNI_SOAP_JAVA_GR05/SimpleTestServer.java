import jakarta.jws.WebMethod;
import jakarta.jws.WebService;
import jakarta.jws.WebParam;
import jakarta.jws.soap.SOAPBinding;

@WebService(serviceName = "SimpleTestService")
@SOAPBinding(style = SOAPBinding.Style.RPC, use = SOAPBinding.Use.LITERAL)
public class SimpleTestServer {
    
    @WebMethod(operationName = "testMethod")
    public String testMethod(@WebParam(name = "input") String input) {
        System.out.println("DEBUG: testMethod recibió input=" + input);
        return "Respuesta: " + input;
    }
    
    @WebMethod(operationName = "celsiusAFahrenheit")
    public double celsiusAFahrenheit(@WebParam(name = "celsius") double celsius) {
        System.out.println("DEBUG: celsiusAFahrenheit recibió celsius=" + celsius);
        return (celsius * 9.0 / 5.0) + 32.0;
    }
}




