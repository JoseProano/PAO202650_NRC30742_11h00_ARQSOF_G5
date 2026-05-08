package ec.edu.monster.servicio;

import ec.edu.monster.modelo.*;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

/**
 * Capa de servicio/DAO para acceso a la base de datos MySQL.
 * Maneja todas las operaciones CRUD de la Federación de Fútbol.
 */
public class FederacionService {

    private static final String URL = "jdbc:mysql://localhost:3306/ticketpremium_db?useSSL=false&serverTimezone=America/Guayaquil&allowPublicKeyRetrieval=true";
    private static final String USER = "root";
    private static final String PASS = "root";

    static {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
        } catch (ClassNotFoundException e) {
            System.err.println("ERROR: Driver MySQL no encontrado: " + e.getMessage());
        }
    }

    private Connection getConnection() throws SQLException {
        return DriverManager.getConnection(URL, USER, PASS);
    }

    // ==========================================
    // PARTIDOS DISPONIBLES (fecha >= ahora)
    // ==========================================
    public List<PartidoFutbol> obtenerPartidosDisponibles() {
        List<PartidoFutbol> partidos = new ArrayList<>();
        String sql = "SELECT CODIGO, EQUIPO_LOCAL, EQUIPO_VISITA, "
                   + "DATE_FORMAT(FECHA, '%Y-%m-%d %H:%i') AS FECHA, LUGAR "
                   + "FROM PARTIDO_FUTBOL WHERE FECHA >= NOW() ORDER BY FECHA";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql);
             ResultSet rs = ps.executeQuery()) {
            while (rs.next()) {
                PartidoFutbol p = new PartidoFutbol();
                p.setCodigo(rs.getInt("CODIGO"));
                p.setEquipoLocal(rs.getString("EQUIPO_LOCAL"));
                p.setEquipoVisita(rs.getString("EQUIPO_VISITA"));
                p.setFecha(rs.getString("FECHA"));
                p.setLugar(rs.getString("LUGAR"));
                partidos.add(p);
            }
        } catch (SQLException e) {
            System.err.println("Error obtenerPartidosDisponibles: " + e.getMessage());
        }
        return partidos;
    }

    // ==========================================
    // LOCALIDADES DISPONIBLES PARA UN PARTIDO
    // ==========================================
    public List<LocalidadPartido> obtenerLocalidades(int codigoPartido) {
        List<LocalidadPartido> localidades = new ArrayList<>();
        String sql = "SELECT ID, CODIGO_PARTIDO, CODIGO_LOCALIDAD, DISPONIBILIDAD, PRECIO "
                   + "FROM LOCALIDAD_PARTIDO "
                   + "WHERE CODIGO_PARTIDO = ? AND DISPONIBILIDAD > 0";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, codigoPartido);
            try (ResultSet rs = ps.executeQuery()) {
                while (rs.next()) {
                    LocalidadPartido l = new LocalidadPartido();
                    l.setId(rs.getInt("ID"));
                    l.setCodigoPartido(rs.getInt("CODIGO_PARTIDO"));
                    l.setCodigoLocalidad(rs.getString("CODIGO_LOCALIDAD"));
                    l.setDisponibilidad(rs.getInt("DISPONIBILIDAD"));
                    l.setPrecio(rs.getDouble("PRECIO"));
                    localidades.add(l);
                }
            }
        } catch (SQLException e) {
            System.err.println("Error obtenerLocalidades: " + e.getMessage());
        }
        return localidades;
    }

    // ==========================================
    // DECREMENTAR DISPONIBILIDAD
    // ==========================================
    public boolean decrementarDisponibilidad(int idLocalidad, int cantidad) {
        String sql = "UPDATE LOCALIDAD_PARTIDO "
                   + "SET DISPONIBILIDAD = DISPONIBILIDAD - ? "
                   + "WHERE ID = ? AND DISPONIBILIDAD >= ?";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, cantidad);
            ps.setInt(2, idLocalidad);
            ps.setInt(3, cantidad);
            return ps.executeUpdate() > 0;
        } catch (SQLException e) {
            System.err.println("Error decrementarDisponibilidad: " + e.getMessage());
            return false;
        }
    }

    // ==========================================
    // REGISTRAR COMPRA (Factura + Detalles)
    // ==========================================
    public Factura registrarCompra(int codigoPartido, String nombreCliente,
                                   String codigoLocalidad, int cantidad, double precioUnitario) {
        double subtotal = cantidad * precioUnitario;
        double iva = subtotal * 0.15;          // IVA Ecuador 15%
        double total = subtotal + iva;

        Factura factura = new Factura();
        factura.setCodigoPartido(codigoPartido);
        factura.setNombreCliente(nombreCliente);
        factura.setSubtotal(subtotal);
        factura.setIva(iva);
        factura.setTotal(total);

        String sqlFactura = "INSERT INTO FACTURA (CODIGO_PARTIDO, NOMBRE_CLIENTE, FECHA, SUBTOTAL, IVA, TOTAL) "
                          + "VALUES (?, ?, NOW(), ?, ?, ?)";
        String sqlDetalle = "INSERT INTO DETALLE_FACTURA (ID_FACTURA, CODIGO_PARTIDO, CODIGO_LOCALIDAD, CANTIDAD, PRECIO_UNITARIO, SUBTOTAL) "
                          + "VALUES (?, ?, ?, ?, ?, ?)";

        try (Connection conn = getConnection()) {
            conn.setAutoCommit(false);
            try {
                // Insertar factura
                try (PreparedStatement ps = conn.prepareStatement(sqlFactura, Statement.RETURN_GENERATED_KEYS)) {
                    ps.setInt(1, codigoPartido);
                    ps.setString(2, nombreCliente);
                    ps.setDouble(3, subtotal);
                    ps.setDouble(4, iva);
                    ps.setDouble(5, total);
                    ps.executeUpdate();

                    try (ResultSet rs = ps.getGeneratedKeys()) {
                        if (rs.next()) {
                            factura.setId(rs.getInt(1));
                        }
                    }
                }

                // Insertar detalle
                try (PreparedStatement ps = conn.prepareStatement(sqlDetalle)) {
                    ps.setInt(1, factura.getId());
                    ps.setInt(2, codigoPartido);
                    ps.setString(3, codigoLocalidad);
                    ps.setInt(4, cantidad);
                    ps.setDouble(5, precioUnitario);
                    ps.setDouble(6, subtotal);
                    ps.executeUpdate();
                }

                conn.commit();
            } catch (SQLException e) {
                conn.rollback();
                System.err.println("Error registrarCompra (rollback): " + e.getMessage());
                return null;
            }
        } catch (SQLException e) {
            System.err.println("Error registrarCompra: " + e.getMessage());
            return null;
        }
        return factura;
    }

    // ==========================================
    // OBTENER PARTIDO POR CÓDIGO
    // ==========================================
    public PartidoFutbol obtenerPartido(int codigoPartido) {
        String sql = "SELECT CODIGO, EQUIPO_LOCAL, EQUIPO_VISITA, "
                   + "DATE_FORMAT(FECHA, '%Y-%m-%d %H:%i') AS FECHA, LUGAR "
                   + "FROM PARTIDO_FUTBOL WHERE CODIGO = ?";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, codigoPartido);
            try (ResultSet rs = ps.executeQuery()) {
                if (rs.next()) {
                    PartidoFutbol p = new PartidoFutbol();
                    p.setCodigo(rs.getInt("CODIGO"));
                    p.setEquipoLocal(rs.getString("EQUIPO_LOCAL"));
                    p.setEquipoVisita(rs.getString("EQUIPO_VISITA"));
                    p.setFecha(rs.getString("FECHA"));
                    p.setLugar(rs.getString("LUGAR"));
                    return p;
                }
            }
        } catch (SQLException e) {
            System.err.println("Error obtenerPartido: " + e.getMessage());
        }
        return null;
    }

    // ==========================================
    // RESUMEN DE VENTAS DE UN PARTIDO
    // ==========================================
    public List<ResumenVenta> obtenerResumenVentas(int codigoPartido) {
        List<ResumenVenta> resumen = new ArrayList<>();
        String sql = "SELECT CODIGO_LOCALIDAD, "
                   + "SUM(CANTIDAD) AS VENDIDOS, "
                   + "SUM(SUBTOTAL) AS TOTAL_RECAUDADO "
                   + "FROM DETALLE_FACTURA "
                   + "WHERE CODIGO_PARTIDO = ? "
                   + "GROUP BY CODIGO_LOCALIDAD";
        try (Connection conn = getConnection();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, codigoPartido);
            try (ResultSet rs = ps.executeQuery()) {
                while (rs.next()) {
                    ResumenVenta r = new ResumenVenta();
                    r.setCodigoLocalidad(rs.getString("CODIGO_LOCALIDAD"));
                    r.setVendidos(rs.getInt("VENDIDOS"));
                    r.setTotalRecaudado(rs.getDouble("TOTAL_RECAUDADO"));
                    resumen.add(r);
                }
            }
        } catch (SQLException e) {
            System.err.println("Error obtenerResumenVentas: " + e.getMessage());
        }
        return resumen;
    }
}
