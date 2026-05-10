package ec.edu.monster.vista;

import ec.edu.monster.servicios.ClienteConversionSOAP;
import ec.edu.monster.modelo.Conversion;
import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

/**
 * Panel para conversiones de peso - Diseño compacto y funcional
 * @author ACER NITRO V15
 */
public class PanelPeso extends JPanel {
    
    // Colores oficiales de Monsters Inc.
    private static final Color AZUL_PRINCIPAL = new Color(33, 169, 218);      // #21a9da
    private static final Color ROJO_MONSTER = new Color(246, 105, 113);        // #f66971
    private static final Color AMARILLO_MONSTER = new Color(245, 216, 128);   // #f5d880
    private static final Color PURPURA_MONSTER = new Color(140, 107, 205);     // #8c6bcd
    private static final Color AZUL_CLARO = new Color(159, 220, 250);         // #9fdcfa
    private static final Color BLANCO = new Color(255, 255, 255);
    private static final Color GRIS_OSCURO = new Color(60, 60, 60);
    private static final Color GRIS_CLARO = new Color(240, 240, 240);
    
    // Componentes
    private JLabel lblTitulo;
    private JLabel lblValor;
    private JTextField txtValor;
    private JLabel lblConversion;
    private JComboBox<String> cmbConversion;
    private JButton btnConvertir;
    private JLabel lblResultado;
    
    // Cliente SOAP
    private ClienteConversionSOAP clienteSOAP;
    
    public PanelPeso() {
        clienteSOAP = new ClienteConversionSOAP();
        inicializarComponentes();
        configurarLayout();
        configurarEventos();
    }
    
    private void inicializarComponentes() {
        // Título
        lblTitulo = new JLabel("CONVERSIONES DE PESO");
        lblTitulo.setFont(new Font("Arial", Font.BOLD, 18));
        lblTitulo.setForeground(GRIS_OSCURO);
        lblTitulo.setHorizontalAlignment(SwingConstants.CENTER);
        
        // Valor
        lblValor = new JLabel("Valor a convertir:");
        lblValor.setFont(new Font("Arial", Font.BOLD, 14));
        lblValor.setForeground(GRIS_OSCURO);
        
        txtValor = new JTextField(15);
        txtValor.setFont(new Font("Arial", Font.PLAIN, 14));
        txtValor.setHorizontalAlignment(SwingConstants.CENTER);
        txtValor.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(GRIS_CLARO, 1),
            BorderFactory.createEmptyBorder(8, 12, 8, 12)
        ));
        
        // Conversión
        lblConversion = new JLabel("Tipo de conversión:");
        lblConversion.setFont(new Font("Arial", Font.BOLD, 14));
        lblConversion.setForeground(GRIS_OSCURO);
        
        String[] conversiones = {
            "Kilogramos → Libras",
            "Libras → Kilogramos", 
            "Gramos → Onzas",
            "Onzas → Gramos"
        };
        cmbConversion = new JComboBox<>(conversiones);
        cmbConversion.setFont(new Font("Arial", Font.PLAIN, 12));
        cmbConversion.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(GRIS_CLARO, 1),
            BorderFactory.createEmptyBorder(5, 8, 5, 8)
        ));
        
        // Botón convertir
        btnConvertir = new JButton("CONVERTIR");
        btnConvertir.setBackground(ROJO_MONSTER);
        btnConvertir.setForeground(BLANCO);
        btnConvertir.setFont(new Font("Arial", Font.BOLD, 16));
        btnConvertir.setPreferredSize(new Dimension(200, 45));
        btnConvertir.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(ROJO_MONSTER, 1),
            BorderFactory.createEmptyBorder(12, 25, 12, 25)
        ));
        btnConvertir.setFocusPainted(false);
        btnConvertir.setCursor(new Cursor(Cursor.HAND_CURSOR));
        
        // Resultado
        lblResultado = new JLabel("Resultado aparecerá aquí");
        lblResultado.setFont(new Font("Arial", Font.BOLD, 16));
        lblResultado.setForeground(GRIS_OSCURO);
        lblResultado.setHorizontalAlignment(SwingConstants.CENTER);
        lblResultado.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(GRIS_CLARO, 1),
            BorderFactory.createEmptyBorder(15, 20, 15, 20)
        ));
        lblResultado.setBackground(GRIS_CLARO);
        lblResultado.setOpaque(true);
    }
    
    private void configurarLayout() {
        setBackground(BLANCO);
        setLayout(new GridBagLayout());
        GridBagConstraints gbc = new GridBagConstraints();
        
        // Título
        gbc.gridx = 0; gbc.gridy = 0; gbc.gridwidth = 2;
        gbc.insets = new Insets(20, 0, 30, 0);
        add(lblTitulo, gbc);
        
        // Valor
        gbc.gridx = 0; gbc.gridy = 1; gbc.gridwidth = 1;
        gbc.anchor = GridBagConstraints.EAST;
        gbc.insets = new Insets(10, 20, 10, 10);
        add(lblValor, gbc);
        
        gbc.gridx = 1; gbc.gridy = 1;
        gbc.anchor = GridBagConstraints.WEST;
        gbc.insets = new Insets(10, 10, 10, 20);
        add(txtValor, gbc);
        
        // Conversión
        gbc.gridx = 0; gbc.gridy = 2;
        gbc.anchor = GridBagConstraints.EAST;
        gbc.insets = new Insets(10, 20, 10, 10);
        add(lblConversion, gbc);
        
        gbc.gridx = 1; gbc.gridy = 2;
        gbc.anchor = GridBagConstraints.WEST;
        gbc.insets = new Insets(10, 10, 10, 20);
        add(cmbConversion, gbc);
        
        // Botón
        gbc.gridx = 0; gbc.gridy = 3; gbc.gridwidth = 2;
        gbc.anchor = GridBagConstraints.CENTER;
        gbc.insets = new Insets(20, 0, 20, 0);
        add(btnConvertir, gbc);
        
        // Resultado
        gbc.gridx = 0; gbc.gridy = 4; gbc.gridwidth = 2;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(20, 20, 20, 20);
        add(lblResultado, gbc);
    }
    
    private void configurarEventos() {
        btnConvertir.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                convertirPeso();
            }
        });
        
        // Efecto hover en botón
        btnConvertir.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseEntered(java.awt.event.MouseEvent evt) {
                btnConvertir.setBackground(AZUL_PRINCIPAL);
            }
            public void mouseExited(java.awt.event.MouseEvent evt) {
                btnConvertir.setBackground(ROJO_MONSTER);
            }
        });
    }
    
    private void convertirPeso() {
        try {
            double valor = Double.parseDouble(txtValor.getText().trim());
            int indice = cmbConversion.getSelectedIndex();
            
            String operacion = "";
            
            switch (indice) {
                case 0: 
                    operacion = "kilogramosALibras"; 
                    break;
                case 1: 
                    operacion = "librasAKilogramos"; 
                    break;
                case 2: 
                    operacion = "gramosAOnzas"; 
                    break;
                case 3: 
                    operacion = "onzasAGramos"; 
                    break;
            }
            
            // Realizar conversión usando el servicio SOAP
            Conversion conversion = clienteSOAP.convertirPeso(operacion, valor);
            
            if (conversion.isExitosa()) {
                String resultadoTexto = String.format("%.2f %s = %.2f %s", 
                    conversion.getValor(), conversion.getUnidadOrigen(), 
                    conversion.getResultado(), conversion.getUnidadDestino());
                mostrarResultado(resultadoTexto);
            } else {
                mostrarError(conversion.getMensajeError());
            }
            
        } catch (NumberFormatException ex) {
            mostrarError("ERROR: Ingrese un valor numérico válido");
        }
    }
    
    
    public void mostrarResultado(String resultado) {
        // Mostrar resultado localmente (abajo del botón)
        lblResultado.setText(resultado);
        lblResultado.setForeground(ROJO_MONSTER);
        lblResultado.setBackground(AZUL_CLARO);
        lblResultado.setOpaque(true);
        lblResultado.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(ROJO_MONSTER, 1),
            BorderFactory.createEmptyBorder(10, 15, 10, 15)
        ));
        lblResultado.setFont(new Font("Arial", Font.BOLD, 14));
    }
    
    public void mostrarError(String error) {
        // Mostrar error localmente (abajo del botón)
        lblResultado.setText("ERROR: " + error);
        lblResultado.setForeground(ROJO_MONSTER);
        lblResultado.setBackground(new Color(255, 240, 240));
        lblResultado.setOpaque(true);
        lblResultado.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(Color.RED, 1),
            BorderFactory.createEmptyBorder(10, 15, 10, 15)
        ));
        lblResultado.setFont(new Font("Arial", Font.BOLD, 14));
    }
}