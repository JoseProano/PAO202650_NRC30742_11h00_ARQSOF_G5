package ec.edu.monster.vista;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

/**
 * Ventana de login profesional para Monsters Inc. Converter
 * Diseño empresarial con colores oficiales de Monsters Inc.
 * @author ACER NITRO V15
 */
public class VentanaLogin extends JFrame {
    
    // Colores oficiales de Monsters Inc.
    private static final Color AZUL_PRINCIPAL = new Color(33, 169, 218);      // #21a9da
    private static final Color ROJO_MONSTER = new Color(246, 105, 113);        // #f66971
    private static final Color AMARILLO_MONSTER = new Color(245, 216, 128);   // #f5d880
    private static final Color PURPURA_MONSTER = new Color(140, 107, 205);     // #8c6bcd
    private static final Color AZUL_CLARO = new Color(159, 220, 250);         // #9fdcfa
    private static final Color BLANCO = new Color(255, 255, 255);
    private static final Color GRIS_OSCURO = new Color(60, 60, 60);
    private static final Color GRIS_CLARO = new Color(240, 240, 240);
    
    // Credenciales correctas
    private static final String USUARIO_CORRECTO = "MONSTER";
    private static final String CONTRASENA_CORRECTA = "MONSTER9";
    private static final int MAX_INTENTOS = 3;
    
    // Componentes principales
    private JPanel panelPrincipal;
    private JPanel panelIzquierdo;
    private JPanel panelDerecho;
    
    // Panel izquierdo (logo y branding)
    private JLabel lblLogo;
    private JLabel lblTituloEmpresa;
    private JLabel lblSubtitulo;
    private JLabel lblVersion;
    
    // Panel derecho (formulario)
    private JPanel panelFormulario;
    private JLabel lblTituloLogin;
    private JLabel lblUsuario;
    private JLabel lblContrasena;
    private JTextField txtUsuario;
    private JPasswordField txtContrasena;
    private JButton btnIngresar;
    private JButton btnSalir;
    private JLabel lblMensaje;
    private JLabel lblIntento;
    
    // Variables de control
    private int intentos = 0;
    private boolean loginExitoso = false;
    
    public VentanaLogin() {
        inicializarComponentes();
        configurarVentana();
        configurarLayout();
        configurarEventos();
    }
    
    private void inicializarComponentes() {
        // Panel principal
        panelPrincipal = new JPanel();
        panelPrincipal.setBackground(BLANCO);
        
        // Panel izquierdo (branding)
        panelIzquierdo = new JPanel();
        panelIzquierdo.setBackground(AZUL_PRINCIPAL);
        panelIzquierdo.setPreferredSize(new Dimension(560, 700));
        
        // Panel derecho (formulario)
        panelDerecho = new JPanel();
        panelDerecho.setBackground(GRIS_CLARO);
        panelDerecho.setPreferredSize(new Dimension(500, 700));
        
        // Componentes del panel izquierdo
        lblLogo = new JLabel();
        lblLogo.setHorizontalAlignment(SwingConstants.CENTER);
        lblLogo.setVerticalAlignment(SwingConstants.CENTER);
        
        lblTituloEmpresa = new JLabel("MONSTERS INC.");
        lblTituloEmpresa.setFont(new Font("Arial", Font.BOLD, 42));
        lblTituloEmpresa.setForeground(BLANCO);
        lblTituloEmpresa.setHorizontalAlignment(SwingConstants.CENTER);
        
        lblSubtitulo = new JLabel("Sistema de Conversiones");
        lblSubtitulo.setFont(new Font("Arial", Font.BOLD, 20));
        lblSubtitulo.setForeground(AZUL_CLARO);
        lblSubtitulo.setHorizontalAlignment(SwingConstants.CENTER);
        
        lblVersion = new JLabel("Edición Monstruosa v1.0");
        lblVersion.setFont(new Font("Arial", Font.ITALIC, 14));
        lblVersion.setForeground(AZUL_CLARO);
        lblVersion.setHorizontalAlignment(SwingConstants.CENTER);
        
        // Panel formulario
        panelFormulario = new JPanel();
        panelFormulario.setBackground(BLANCO);
        panelFormulario.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(GRIS_CLARO, 1),
            BorderFactory.createEmptyBorder(30, 30, 30, 30)
        ));
        panelFormulario.setPreferredSize(new Dimension(520, 420));
        
        // Componentes del formulario
        lblTituloLogin = new JLabel("LOGUÉATE");
        lblTituloLogin.setFont(new Font("Arial", Font.BOLD, 28));
        lblTituloLogin.setForeground(AZUL_PRINCIPAL);
        lblTituloLogin.setHorizontalAlignment(SwingConstants.CENTER);
        
        lblUsuario = new JLabel("Usuario:");
        lblUsuario.setFont(new Font("Arial", Font.BOLD, 14));
        lblUsuario.setForeground(GRIS_OSCURO);
        
        txtUsuario = new JTextField(20);
        txtUsuario.setFont(new Font("Arial", Font.PLAIN, 14));
        txtUsuario.setHorizontalAlignment(SwingConstants.CENTER);
        txtUsuario.setPreferredSize(new Dimension(380, 50));
        txtUsuario.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(new Color(210,210,210), 1),
            BorderFactory.createEmptyBorder(14, 16, 14, 16)
        ));
        
        lblContrasena = new JLabel("Contraseña:");
        lblContrasena.setFont(new Font("Arial", Font.BOLD, 14));
        lblContrasena.setForeground(GRIS_OSCURO);
        
        txtContrasena = new JPasswordField(20);
        txtContrasena.setFont(new Font("Arial", Font.PLAIN, 14));
        txtContrasena.setHorizontalAlignment(SwingConstants.CENTER);
        txtContrasena.setPreferredSize(new Dimension(380, 50));
        txtContrasena.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(new Color(210,210,210), 1),
            BorderFactory.createEmptyBorder(14, 16, 14, 16)
        ));

        // (sin enlace de recuperación por pedido del usuario)
        
        btnIngresar = new JButton("ACCEDER");
        btnIngresar.setBackground(AZUL_PRINCIPAL);
        btnIngresar.setForeground(BLANCO);
        btnIngresar.setFont(new Font("Arial", Font.BOLD, 16));
        btnIngresar.setPreferredSize(new Dimension(180, 48));
        btnIngresar.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(AZUL_PRINCIPAL, 1),
            BorderFactory.createEmptyBorder(12, 25, 12, 25)
        ));
        btnIngresar.setFocusPainted(false);
        
        btnSalir = new JButton("SALIR");
        btnSalir.setBackground(ROJO_MONSTER);
        btnSalir.setForeground(BLANCO);
        btnSalir.setFont(new Font("Arial", Font.BOLD, 14));
        btnSalir.setPreferredSize(new Dimension(180, 48));
        btnSalir.setBorder(BorderFactory.createCompoundBorder(
            BorderFactory.createLineBorder(ROJO_MONSTER, 1),
            BorderFactory.createEmptyBorder(10, 20, 10, 20)
        ));
        btnSalir.setFocusPainted(false);
        
        lblMensaje = new JLabel("Ingrese sus credenciales para acceder");
        lblMensaje.setFont(new Font("Arial", Font.PLAIN, 13));
        lblMensaje.setForeground(GRIS_OSCURO);
        lblMensaje.setHorizontalAlignment(SwingConstants.CENTER);
        
        lblIntento = new JLabel("");
        lblIntento.setFont(new Font("Arial", Font.BOLD, 12));
        lblIntento.setHorizontalAlignment(SwingConstants.CENTER);
    }
    
    private void configurarVentana() {
        setTitle("Monsters Inc. Conversiones pepas");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(1200, 800);
        setLocationRelativeTo(null);
        setResizable(true);
        getContentPane().setBackground(BLANCO);
        
        // Configurar icono de la ventana (comentado hasta agregar imagen)
        // try {
        //     setIconImage(Toolkit.getDefaultToolkit().createImage(getClass().getResource("/images/icon.png")));
        // } catch (Exception e) {
        //     // Si no hay icono, continuar sin él
        // }
    }
    
    private void configurarLayout() {
        setLayout(new BorderLayout());
        
        // Panel principal con layout horizontal
        panelPrincipal.setLayout(new BorderLayout());
        
        // Panel izquierdo (branding)
        configurarPanelIzquierdo();
        panelPrincipal.add(panelIzquierdo, BorderLayout.WEST);
        
        // Panel derecho (formulario)
        configurarPanelDerecho();
        panelPrincipal.add(panelDerecho, BorderLayout.CENTER);
        
        add(panelPrincipal, BorderLayout.CENTER);
    }
    
    private void configurarPanelIzquierdo() {
        panelIzquierdo.setLayout(new GridBagLayout());
        GridBagConstraints gbc = new GridBagConstraints();
        
        // Logo (espacio reservado para imagen)
        gbc.gridx = 0; gbc.gridy = 0;
        gbc.insets = new Insets(50, 0, 30, 0);
        gbc.anchor = GridBagConstraints.CENTER;
        
        // Panel para logo (usando JLabel con imagen escalada para mayor compatibilidad)
        JPanel panelLogo = new JPanel(new BorderLayout());
        // Fondo igual al del panel izquierdo para que la imagen no se vea con borde amarillo
        panelLogo.setBackground(AZUL_PRINCIPAL);
        panelLogo.setPreferredSize(new Dimension(320, 320));
        // Sin borde para evitar el efecto de "marco"
        panelLogo.setBorder(BorderFactory.createEmptyBorder(0, 0, 0, 0));
        
        // Intentar cargar imagen del logo, si no existe usar texto
        ImageIcon logoIcon = cargarImagen("/images/logo.png");
        if (logoIcon != null) {
            Image img = logoIcon.getImage().getScaledInstance(300, 300, Image.SCALE_SMOOTH);
            JLabel lblLogoEscalado = new JLabel(new ImageIcon(img));
            lblLogoEscalado.setHorizontalAlignment(SwingConstants.CENTER);
            lblLogoEscalado.setVerticalAlignment(SwingConstants.CENTER);
            panelLogo.add(lblLogoEscalado, BorderLayout.CENTER);
        } else {
            JLabel lblIcono = new JLabel("MI");
            lblIcono.setFont(new Font("Arial", Font.BOLD, 64));
            lblIcono.setForeground(BLANCO);
            lblIcono.setHorizontalAlignment(SwingConstants.CENTER);
            panelLogo.add(lblIcono, BorderLayout.CENTER);
        }
        
        panelIzquierdo.add(panelLogo, gbc);
        
        // Título empresa
        gbc.gridx = 0; gbc.gridy = 1;
        gbc.insets = new Insets(20, 0, 10, 0);
        panelIzquierdo.add(lblTituloEmpresa, gbc);
        
        // Subtítulo
        gbc.gridx = 0; gbc.gridy = 2;
        gbc.insets = new Insets(0, 0, 30, 0);
        panelIzquierdo.add(lblSubtitulo, gbc);
        
        // Versión
        gbc.gridx = 0; gbc.gridy = 3;
        gbc.insets = new Insets(0, 0, 0, 0);
        panelIzquierdo.add(lblVersion, gbc);
    }
    
    private void configurarPanelDerecho() {
        panelDerecho.setLayout(new BorderLayout());
        
        // Panel formulario centrado
        JPanel panelCentro = new JPanel();
        panelCentro.setBackground(GRIS_CLARO);
        panelCentro.setLayout(new BorderLayout());
        
        // Centrar el formulario en el medio vertical y horizontal
        JPanel panelWrapper = new JPanel(new GridBagLayout());
        panelWrapper.setBackground(GRIS_CLARO);
        GridBagConstraints gbcWrap = new GridBagConstraints();
        gbcWrap.gridx = 0;
        gbcWrap.gridy = 0;
        gbcWrap.anchor = GridBagConstraints.CENTER;
        panelWrapper.add(panelFormulario, gbcWrap);
        
        panelCentro.add(panelWrapper, BorderLayout.CENTER);
        panelDerecho.add(panelCentro, BorderLayout.CENTER);
        
        // Configurar layout del formulario centrado
        panelFormulario.setLayout(new BorderLayout());
        
        // Panel central con todo el contenido
        JPanel panelCentral = new JPanel();
        panelCentral.setLayout(new BoxLayout(panelCentral, BoxLayout.Y_AXIS));
        panelCentral.setBackground(BLANCO);
        
        // Título login
        JPanel panelTitulo = new JPanel();
        panelTitulo.setBackground(BLANCO);
        panelTitulo.add(lblTituloLogin);
        panelCentral.add(panelTitulo);
        panelCentral.add(Box.createVerticalStrut(30));
        
        // Panel de campos
        JPanel panelCampos = new JPanel();
        panelCampos.setLayout(new GridBagLayout());
        panelCampos.setBackground(BLANCO);
        GridBagConstraints gbc = new GridBagConstraints();
        
        // Usuario
        gbc.gridx = 0; gbc.gridy = 0;
        gbc.anchor = GridBagConstraints.WEST;
        gbc.insets = new Insets(10, 0, 10, 15);
        panelCampos.add(lblUsuario, gbc);
        
        gbc.gridx = 1; gbc.gridy = 0;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.weightx = 1.0;
        gbc.insets = new Insets(10, 0, 10, 0);
        panelCampos.add(txtUsuario, gbc);
        
        // Contraseña
        gbc.gridx = 0; gbc.gridy = 1;
        gbc.fill = GridBagConstraints.NONE;
        gbc.weightx = 0.0;
        gbc.anchor = GridBagConstraints.WEST;
        gbc.insets = new Insets(10, 0, 10, 15);
        panelCampos.add(lblContrasena, gbc);
        
        gbc.gridx = 1; gbc.gridy = 1;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.weightx = 1.0;
        gbc.insets = new Insets(10, 0, 10, 0);
        panelCampos.add(txtContrasena, gbc);
        
        panelCentral.add(panelCampos);
        panelCentral.add(Box.createVerticalStrut(30));
        
        // Botones centrados
        JPanel panelBotones = new JPanel(new FlowLayout(FlowLayout.CENTER, 20, 0));
        panelBotones.setBackground(BLANCO);
        panelBotones.add(btnIngresar);
        panelBotones.add(btnSalir);
        panelCentral.add(panelBotones);
        panelCentral.add(Box.createVerticalStrut(20));
        
        // Mensaje
        JPanel panelMensaje = new JPanel();
        panelMensaje.setBackground(BLANCO);
        panelMensaje.add(lblMensaje);
        panelCentral.add(panelMensaje);
        panelCentral.add(Box.createVerticalStrut(10));
        
        // Intento
        JPanel panelIntento = new JPanel();
        panelIntento.setBackground(BLANCO);
        panelIntento.add(lblIntento);
        panelCentral.add(panelIntento);
        
        // Centrar todo el contenido
        panelFormulario.add(panelCentral, BorderLayout.CENTER);
    }
    
    private void configurarEventos() {
        // Botón ingresar
        btnIngresar.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                verificarCredenciales();
            }
        });
        
        // Botón salir
        btnSalir.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                System.exit(0);
            }
        });
        
        // Enter en campos de texto
        txtUsuario.addKeyListener(new KeyListener() {
            @Override
            public void keyPressed(KeyEvent e) {
                if (e.getKeyCode() == KeyEvent.VK_ENTER) {
                    txtContrasena.requestFocus();
                }
            }
            @Override
            public void keyReleased(KeyEvent e) {}
            @Override
            public void keyTyped(KeyEvent e) {}
        });
        
        txtContrasena.addKeyListener(new KeyListener() {
            @Override
            public void keyPressed(KeyEvent e) {
                if (e.getKeyCode() == KeyEvent.VK_ENTER) {
                    verificarCredenciales();
                }
            }
            @Override
            public void keyReleased(KeyEvent e) {}
            @Override
            public void keyTyped(KeyEvent e) {}
        });
        
        // Efectos hover en botones
        btnIngresar.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseEntered(java.awt.event.MouseEvent evt) {
                btnIngresar.setBackground(PURPURA_MONSTER);
            }
            public void mouseExited(java.awt.event.MouseEvent evt) {
                btnIngresar.setBackground(AZUL_PRINCIPAL);
            }
        });
        
        btnSalir.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseEntered(java.awt.event.MouseEvent evt) {
                btnSalir.setBackground(new Color(200, 50, 60));
            }
            public void mouseExited(java.awt.event.MouseEvent evt) {
                btnSalir.setBackground(ROJO_MONSTER);
            }
        });
    }
    
    private void verificarCredenciales() {
        String usuario = txtUsuario.getText().trim();
        String contrasena = new String(txtContrasena.getPassword()).trim();
        
        intentos++;
        
        if (USUARIO_CORRECTO.equals(usuario) && CONTRASENA_CORRECTA.equals(contrasena)) {
            // Login exitoso
            lblMensaje.setText("¡Autenticación exitosa! Bienvenido al sistema.");
            lblMensaje.setForeground(AZUL_PRINCIPAL);
            lblIntento.setText("");
            loginExitoso = true;
            
            // Cambiar color del botón
            btnIngresar.setBackground(AZUL_PRINCIPAL);
            btnIngresar.setText("ACCEDIENDO...");
            btnIngresar.setEnabled(false);
            
            // Cerrar ventana de login después de un breve delay
            Timer timer = new Timer(2000, e -> {
                dispose();
                abrirVentanaPrincipal();
            });
            timer.setRepeats(false);
            timer.start();
            
        } else {
            // Credenciales incorrectas
            if (intentos >= MAX_INTENTOS) {
                lblMensaje.setText("ACCESO DENEGADO");
                lblMensaje.setForeground(ROJO_MONSTER);
                lblIntento.setText("Demasiados intentos fallidos. Sistema bloqueado.");
                lblIntento.setForeground(ROJO_MONSTER);
                btnIngresar.setEnabled(false);
                btnIngresar.setBackground(GRIS_CLARO);
                btnIngresar.setText("BLOQUEADO");
                
                Timer timer = new Timer(3000, e -> System.exit(0));
                timer.setRepeats(false);
                timer.start();
                
            } else {
                lblMensaje.setText("Credenciales incorrectas");
                lblMensaje.setForeground(ROJO_MONSTER);
                lblIntento.setText("Intento " + intentos + " de " + MAX_INTENTOS);
                lblIntento.setForeground(ROJO_MONSTER);
                txtUsuario.setText("");
                txtContrasena.setText("");
                txtUsuario.requestFocus();
            }
        }
    }
    
    private void abrirVentanaPrincipal() {
        VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();
        ventanaPrincipal.setVisible(true);
    }
    
    /**
     * Carga una imagen de forma segura desde resources
     */
    private ImageIcon cargarImagen(String ruta) {
        try {
            java.net.URL url = getClass().getResource(ruta);
            if (url != null) {
                return new ImageIcon(url);
            }
        } catch (Exception e) {
            System.out.println("No se pudo cargar la imagen: " + ruta);
        }
        return null;
    }
    
    /**
     * Crea un icono de texto como fallback
     */
    private JLabel crearIconoTexto(String texto, Color colorFondo, Color colorTexto) {
        JLabel icono = new JLabel(texto);
        icono.setFont(new Font("Arial", Font.BOLD, 24));
        icono.setForeground(colorTexto);
        icono.setBackground(colorFondo);
        icono.setOpaque(true);
        icono.setHorizontalAlignment(SwingConstants.CENTER);
        icono.setVerticalAlignment(SwingConstants.CENTER);
        icono.setPreferredSize(new Dimension(80, 80));
        return icono;
    }
    
    public boolean isLoginExitoso() {
        return loginExitoso;
    }
}