import jakarta.jws.WebMethod;
import jakarta.jws.WebService;
import jakarta.jws.WebParam;
import jakarta.jws.soap.SOAPBinding;

@WebService(serviceName = "NewTestServer")
@SOAPBinding(style = SOAPBinding.Style.RPC, use = SOAPBinding.Use.LITERAL)
public class NewTestServer {
    
    @WebMethod(operationName = "celsiusAFahrenheit")
    public double celsiusAFahrenheit(@WebParam(name = "celsius") double celsius) {
        System.out.println("DEBUG: NewTestServer.celsiusAFahrenheit recibió celsius=" + celsius);
        double result = (celsius * 9.0 / 5.0) + 32.0;
        System.out.println("DEBUG: NewTestServer resultado=" + result);
        return result;
    }
    
    @WebMethod(operationName = "hello")
    public String hello(@WebParam(name = "name") String name) {
        System.out.println("DEBUG: NewTestServer.hello recibió name=" + name);
        return "Hola " + name + "!";
    }
}




