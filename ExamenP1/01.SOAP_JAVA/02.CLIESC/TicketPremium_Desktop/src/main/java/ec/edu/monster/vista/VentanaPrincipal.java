package ec.edu.monster.vista;

import ec.edu.monster.servicio.ClienteFederacion;
import ec.edu.monster.ws.generated.*;
import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.awt.*;
import java.util.List;

/**
 * Interfaz gráfica de escritorio (Swing) de TicketPremium.
 * Funcionalidades: ver partidos, ver localidades, comprar boletos, reporte.
 */
public class VentanaPrincipal extends JFrame {

    private final ClienteFederacion cliente = new ClienteFederacion();

    // Componentes - Panel Partidos
    private JTable tblPartidos;
    private DefaultTableModel modelPartidos;
    private JButton btnVerLocalidades, btnReporte, btnRefrescar;

    // Componentes - Panel Localidades
    private JTable tblLocalidades;
    private DefaultTableModel modelLocalidades;
    private JLabel lblPartidoSeleccionado;
    private JButton btnComprar;

    // Componentes - Panel Reporte
    private JTable tblReporte;
    private DefaultTableModel modelReporte;
    private JLabel lblInfoReporte;

    private int partidoSeleccionado = -1;

    public VentanaPrincipal() {
        configurarVentana();
        inicializarComponentes();
        cargarPartidos();
    }

    private void configurarVentana() {
        setTitle("TicketPremium - Venta de Boletos");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(1100, 700);
        setLocationRelativeTo(null);
    }

    private void inicializarComponentes() {
        JTabbedPane tabbedPane = new JTabbedPane();

        // ============ TAB 1: PARTIDOS ============
        JPanel panelPartidos = new JPanel(new BorderLayout(10, 10));
        panelPartidos.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        modelPartidos = new DefaultTableModel(
                new String[]{"Código", "Equipo Local", "Equipo Visita", "Fecha", "Lugar"}, 0) {
            public boolean isCellEditable(int r, int c) { return false; }
        };
        tblPartidos = new JTable(modelPartidos);
        tblPartidos.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
        tblPartidos.setRowHeight(25);

        JPanel panelBotonesPartidos = new JPanel(new FlowLayout(FlowLayout.LEFT));
        btnRefrescar = new JButton("Refrescar Partidos");
        btnVerLocalidades = new JButton("Ver Localidades del Partido Seleccionado");
        btnReporte = new JButton("Ver Reporte de Ventas");
        panelBotonesPartidos.add(btnRefrescar);
        panelBotonesPartidos.add(btnVerLocalidades);
        panelBotonesPartidos.add(btnReporte);

        panelPartidos.add(new JLabel("Partidos de Fútbol Disponibles", JLabel.CENTER), BorderLayout.NORTH);
        panelPartidos.add(new JScrollPane(tblPartidos), BorderLayout.CENTER);
        panelPartidos.add(panelBotonesPartidos, BorderLayout.SOUTH);

        // ============ TAB 2: LOCALIDADES ============
        JPanel panelLocalidades = new JPanel(new BorderLayout(10, 10));
        panelLocalidades.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        lblPartidoSeleccionado = new JLabel("Seleccione un partido en la pestaña anterior", JLabel.CENTER);
        lblPartidoSeleccionado.setFont(new Font("Arial", Font.BOLD, 14));

        modelLocalidades = new DefaultTableModel(
                new String[]{"ID", "Localidad", "Disponibles", "Precio ($)"}, 0) {
            public boolean isCellEditable(int r, int c) { return false; }
        };
        tblLocalidades = new JTable(modelLocalidades);
        tblLocalidades.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
        tblLocalidades.setRowHeight(25);

        btnComprar = new JButton("Comprar Boletos de la Localidad Seleccionada");
        btnComprar.setBackground(new Color(0, 150, 0));
        btnComprar.setForeground(Color.WHITE);
        btnComprar.setFont(new Font("Arial", Font.BOLD, 13));

        panelLocalidades.add(lblPartidoSeleccionado, BorderLayout.NORTH);
        panelLocalidades.add(new JScrollPane(tblLocalidades), BorderLayout.CENTER);
        panelLocalidades.add(btnComprar, BorderLayout.SOUTH);

        // ============ TAB 3: REPORTE ============
        JPanel panelReporte = new JPanel(new BorderLayout(10, 10));
        panelReporte.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        lblInfoReporte = new JLabel("Reporte de Ventas", JLabel.CENTER);
        lblInfoReporte.setFont(new Font("Arial", Font.BOLD, 14));

        modelReporte = new DefaultTableModel(
                new String[]{"Localidad", "Vendidos", "Total Recaudado ($)"}, 0) {
            public boolean isCellEditable(int r, int c) { return false; }
        };
        tblReporte = new JTable(modelReporte);
        tblReporte.setRowHeight(25);

        panelReporte.add(lblInfoReporte, BorderLayout.NORTH);
        panelReporte.add(new JScrollPane(tblReporte), BorderLayout.CENTER);

        // Agregar tabs
        tabbedPane.addTab("Partidos", panelPartidos);
        tabbedPane.addTab("Localidades", panelLocalidades);
        tabbedPane.addTab("Reporte de Ventas", panelReporte);
        add(tabbedPane);

        // ============ EVENTOS ============
        btnRefrescar.addActionListener(e -> cargarPartidos());

        btnVerLocalidades.addActionListener(e -> {
            int row = tblPartidos.getSelectedRow();
            if (row < 0) { JOptionPane.showMessageDialog(this, "Seleccione un partido."); return; }
            partidoSeleccionado = (int) modelPartidos.getValueAt(row, 0);
            String info = modelPartidos.getValueAt(row, 1) + " vs " + modelPartidos.getValueAt(row, 2);
            lblPartidoSeleccionado.setText("Localidades para: " + info);
            cargarLocalidades(partidoSeleccionado);
            tabbedPane.setSelectedIndex(1);
        });

        btnComprar.addActionListener(e -> realizarCompra());

        btnReporte.addActionListener(e -> {
            int row = tblPartidos.getSelectedRow();
            if (row < 0) { JOptionPane.showMessageDialog(this, "Seleccione un partido."); return; }
            int cod = (int) modelPartidos.getValueAt(row, 0);
            cargarReporte(cod, modelPartidos.getValueAt(row, 1) + " vs " + modelPartidos.getValueAt(row, 2),
                    (String) modelPartidos.getValueAt(row, 3));
            tabbedPane.setSelectedIndex(2);
        });
    }

    private void cargarPartidos() {
        modelPartidos.setRowCount(0);
        List<PartidoFutbol> partidos = cliente.obtenerPartidosDisponibles();
        if (partidos != null) {
            for (PartidoFutbol p : partidos) {
                modelPartidos.addRow(new Object[]{
                        p.getCodigo(), p.getEquipoLocal(), p.getEquipoVisita(),
                        p.getFecha(), p.getLugar()
                });
            }
        }
    }

    private void cargarLocalidades(int codigoPartido) {
        modelLocalidades.setRowCount(0);
        List<LocalidadPartido> locs = cliente.obtenerLocalidades(codigoPartido);
        if (locs != null) {
            for (LocalidadPartido l : locs) {
                modelLocalidades.addRow(new Object[]{
                        l.getId(), l.getCodigoLocalidad(), l.getDisponibilidad(),
                        String.format("%.2f", l.getPrecio())
                });
            }
        }
    }

    private void realizarCompra() {
        int row = tblLocalidades.getSelectedRow();
        if (row < 0 || partidoSeleccionado < 0) {
            JOptionPane.showMessageDialog(this, "Seleccione una localidad.");
            return;
        }

        int idLocalidad = (int) modelLocalidades.getValueAt(row, 0);
        String codigoLocalidad = (String) modelLocalidades.getValueAt(row, 1);
        int disponibles = (int) modelLocalidades.getValueAt(row, 2);
        double precio = Double.parseDouble((String) modelLocalidades.getValueAt(row, 3));

        String cantidadStr = JOptionPane.showInputDialog(this, "Cantidad de boletos (máx: " + disponibles + "):");
        if (cantidadStr == null) return;
        int cantidad;
        try { cantidad = Integer.parseInt(cantidadStr); } catch (NumberFormatException e) { return; }
        if (cantidad <= 0 || cantidad > disponibles) {
            JOptionPane.showMessageDialog(this, "Cantidad no válida."); return;
        }

        String nombre = JOptionPane.showInputDialog(this, "Nombre del cliente:");
        if (nombre == null || nombre.trim().isEmpty()) return;

        Factura factura = cliente.comprarBoleto(partidoSeleccionado, nombre,
                codigoLocalidad, idLocalidad, cantidad, precio);

        if (factura == null) {
            JOptionPane.showMessageDialog(this, "Error al realizar la compra.", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        String msg = String.format(
                "========= FACTURA =========\n" +
                "N° Factura: %d\n" +
                "Cliente: %s\n" +
                "Localidad: %s\n" +
                "Cantidad: %d\n" +
                "P. Unitario: $%.2f\n" +
                "----------------------------\n" +
                "Subtotal: $%.2f\n" +
                "IVA (15%%): $%.2f\n" +
                "TOTAL: $%.2f\n" +
                "===========================",
                factura.getId(), nombre, codigoLocalidad, cantidad, precio,
                factura.getSubtotal(), factura.getIva(), factura.getTotal());

        JOptionPane.showMessageDialog(this, msg, "Compra Exitosa", JOptionPane.INFORMATION_MESSAGE);
        cargarLocalidades(partidoSeleccionado); // refrescar
    }

    private void cargarReporte(int codigoPartido, String vsText, String fecha) {
        modelReporte.setRowCount(0);
        lblInfoReporte.setText("Partido: " + vsText + "  |  Fecha: " + fecha);
        List<ResumenVenta> resumen = cliente.obtenerResumenVentas(codigoPartido);
        if (resumen != null) {
            for (ResumenVenta r : resumen) {
                modelReporte.addRow(new Object[]{
                        r.getCodigoLocalidad(), r.getVendidos(),
                        String.format("$%.2f", r.getTotalRecaudado())
                });
            }
        }
    }
}
