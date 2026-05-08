/*
 * Cliente REST para el servicio EurekaBank
 * Consume los endpoints REST del servidor
 */
package ec.edu.monster.servicio;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import ec.edu.monster.modelo.Movimiento;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.TimeZone;

public class EurekaService {

    // Ajusta el contexto según tu despliegue
    private static final String BASE_URL = "http://localhost:8080/WSEurekaBank_Restfull_Java_G4/resources/corebancario";

    private final HttpClient http;
    private final Gson gson;

    public EurekaService() {
        this.http = HttpClient.newHttpClient();
        this.gson = new GsonBuilder().setLenient().create();
    }

    public List<Movimiento> traerMovimientos(String cuenta) {
        List<Movimiento> lista = new ArrayList<>();
        try {
            String url = BASE_URL + "/movimientos/" + encode(cuenta);
            HttpRequest req = HttpRequest.newBuilder()
                    .uri(URI.create(url))
                    .GET()
                    .header("Accept", "application/json")
                    .build();
            HttpResponse<String> resp = http.send(req, HttpResponse.BodyHandlers.ofString());
            if (resp.statusCode() == 200) {
                JsonArray arr = gson.fromJson(resp.body(), JsonArray.class);
                for (JsonElement el : arr) {
                    JsonObject o = el.getAsJsonObject();
                    Movimiento m = new Movimiento();
                    // Mapear campos del servicio REST al modelo del cliente
                    m.setCuencodigo(getAsString(o, "cuenta"));
                    m.setMovinumero(getAsInt(o, "nromov"));
                    m.setMoviimporte(getAsDouble(o, "importe"));
                    // El servidor ahora devuelve el código del tipo en el campo "tipo"
                    m.setTipocodigo(getAsString(o, "tipo"));
                    m.setCuenreferencia(getAsString(o, "referencia"));
                    Date fecha = parseFecha(getAsString(o, "fecha"));
                    if (fecha != null) m.setMovifecha(fecha);
                    lista.add(m);
                }
            } else {
                System.err.println("Error HTTP al traer movimientos: " + resp.statusCode());
                System.err.println("Respuesta: " + resp.body());
            }
        } catch (Exception e) {
            System.err.println("Error al consultar movimientos: " + e.getMessage());
        }
        return lista;
    }

    public int regDeposito(String cuenta, double importe) {
        try {
            String url = BASE_URL + "/deposito?cuenta=" + encode(cuenta) + "&importe=" + importe;
            HttpRequest req = HttpRequest.newBuilder()
                    .uri(URI.create(url))
                    .POST(HttpRequest.BodyPublishers.noBody())
                    .header("Accept", "application/json")
                    .build();
            HttpResponse<String> resp = http.send(req, HttpResponse.BodyHandlers.ofString());
            return parseEstado(resp);
        } catch (Exception e) {
            System.err.println("Error al registrar depósito: " + e.getMessage());
            return 0;
        }
    }

    public int regRetiro(String cuenta, double importe) {
        try {
            String url = BASE_URL + "/retiro?cuenta=" + encode(cuenta) + "&importe=" + importe;
            HttpRequest req = HttpRequest.newBuilder()
                    .uri(URI.create(url))
                    .POST(HttpRequest.BodyPublishers.noBody())
                    .header("Accept", "application/json")
                    .build();
            HttpResponse<String> resp = http.send(req, HttpResponse.BodyHandlers.ofString());
            return parseEstado(resp);
        } catch (Exception e) {
            System.err.println("Error al registrar retiro: " + e.getMessage());
            return 0;
        }
    }

    public int regTransferencia(String cuentaOrigen, String cuentaDestino, double importe) {
        try {
            String url = BASE_URL + "/transferencia?cuentaOrigen=" + encode(cuentaOrigen)
                    + "&cuentaDestino=" + encode(cuentaDestino)
                    + "&importe=" + importe;
            HttpRequest req = HttpRequest.newBuilder()
                    .uri(URI.create(url))
                    .POST(HttpRequest.BodyPublishers.noBody())
                    .header("Accept", "application/json")
                    .build();
            HttpResponse<String> resp = http.send(req, HttpResponse.BodyHandlers.ofString());
            return parseEstado(resp);
        } catch (Exception e) {
            System.err.println("Error al registrar transferencia: " + e.getMessage());
            return 0;
        }
    }

    private static String encode(String s) {
        return java.net.URLEncoder.encode(s, java.nio.charset.StandardCharsets.UTF_8);
    }

    private int parseEstado(HttpResponse<String> resp) {
        try {
            if (resp.statusCode() >= 200 && resp.statusCode() < 300) {
                JsonObject o = gson.fromJson(resp.body(), JsonObject.class);
                int estado = o.has("estado") ? o.get("estado").getAsInt() : 0;
                return estado == 1 ? 1 : 0;
            }
        } catch (Exception ignore) {
        }
        return 0;
    }

    private static String getAsString(JsonObject o, String key) {
        return o.has(key) && !o.get(key).isJsonNull() ? o.get(key).getAsString() : null;
    }

    private static int getAsInt(JsonObject o, String key) {
        try {
            return o.has(key) && !o.get(key).isJsonNull() ? o.get(key).getAsInt() : 0;
        } catch (Exception e) {
            return 0;
        }
    }

    private static double getAsDouble(JsonObject o, String key) {
        try {
            return o.has(key) && !o.get(key).isJsonNull() ? o.get(key).getAsDouble() : 0.0;
        } catch (Exception e) {
            return 0.0;
        }
    }

    private static Date parseFecha(String s) {
        if (s == null || s.isEmpty()) return null;
        // Intentar varios formatos comunes
        String[] formatos = new String[]{
                "yyyy-MM-dd'T'HH:mm:ss.SSSXXX",
                "yyyy-MM-dd'T'HH:mm:ssXXX",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd"
        };
        for (String f : formatos) {
            try {
                SimpleDateFormat sdf = new SimpleDateFormat(f);
                sdf.setTimeZone(TimeZone.getTimeZone("UTC"));
                return sdf.parse(s);
            } catch (ParseException ignore) {}
        }
        return null;
    }
}


