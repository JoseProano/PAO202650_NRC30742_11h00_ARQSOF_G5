import jakarta.jws.WebMethod;
import jakarta.jws.WebService;
import jakarta.jws.WebParam;
import jakarta.jws.soap.SOAPBinding;

@WebService(serviceName = "TestServer")
@SOAPBinding(style = SOAPBinding.Style.RPC, use = SOAPBinding.Use.LITERAL)
public class TestServer {
    
    @WebMethod(operationName = "celsiusAFahrenheit")
    public double celsiusAFahrenheit(@WebParam(name = "celsius") double celsius) {
        System.out.println("DEBUG: TestServer.celsiusAFahrenheit recibió celsius=" + celsius);
        try {
            double result = (celsius * 9.0 / 5.0) + 32.0;
            System.out.println("DEBUG: TestServer resultado=" + result);
            return result;
        } catch (Exception e) {
            System.err.println("ERROR en TestServer: " + e.getMessage());
            e.printStackTrace();
            throw e;
        }
    }
}




