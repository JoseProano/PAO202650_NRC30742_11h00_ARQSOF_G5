using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.vista
{
    /// <summary>
    /// Vista de login con diseño moderno mejorado - Completamente rediseñado
    /// </summary>
    public partial class LoginView : Form
    {
        // Paleta de colores
        private static readonly Color COLOR_PRIMARY = Color.FromArgb(58, 180, 217);      // #3ab4d9
        private static readonly Color COLOR_CORAL = Color.FromArgb(246, 126, 128);        // #f67e80
        private static readonly Color COLOR_YELLOW = Color.FromArgb(246, 222, 136);       // #f6de88
        private static readonly Color COLOR_PURPLE = Color.FromArgb(158, 124, 197);        // #9e7cc5
        private static readonly Color COLOR_LIGHT_BLUE = Color.FromArgb(175, 224, 248);   // #afe0f8
        private static readonly Color COLOR_BACKGROUND_LIGHT = Color.FromArgb(246, 247, 248); // #f6f7f8
        private static readonly Color COLOR_TEXT_DARK = Color.FromArgb(15, 23, 26);      // #0f171a

        // Credenciales
        private const string USUARIO = "MONSTER";
        private const string PASS = "6C3F6757E773775FD059E2F025BD14BA";

        private TextBox txtUsuario;
        private TextBox txtPassword;
        private Image logoImage;

        public LoginView()
        {
            InitializeComponent();
            // Cargar imagen del logo
            string logoPath = Path.Combine(Application.StartupPath, "images", "logo.png");
            if (File.Exists(logoPath))
            {
                try
                {
                    logoImage = Image.FromFile(logoPath);
                }
                catch
                {
                    logoImage = null;
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "EurekaBank - Iniciar Sesión";
            this.Size = new Size(1400, 900); // Ventana más grande
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new Size(1400, 900);

            // Contenedor principal
            Panel root = new Panel();
            root.Size = new Size(1400, 900);
            root.Location = new Point(0, 0);
            root.BackColor = COLOR_BACKGROUND_LIGHT;

            // Panel izquierdo - más ancho y mejor espaciado
            Panel leftPanel = CrearPanelIzquierdo();
            leftPanel.Size = new Size(700, 900);
            leftPanel.Location = new Point(0, 0);
            leftPanel.BackColor = COLOR_LIGHT_BLUE;

            // Panel derecho - formulario mejorado
            Panel rightPanel = CrearPanelDerecho();
            rightPanel.Size = new Size(700, 900);
            rightPanel.Location = new Point(700, 0);
            rightPanel.BackColor = COLOR_BACKGROUND_LIGHT;

            root.Controls.Add(leftPanel);
            root.Controls.Add(rightPanel);
            this.Controls.Add(root);

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Crea el panel izquierdo con diseño mejorado - imagen centrada y textos debajo
        /// </summary>
        private Panel CrearPanelIzquierdo()
        {
            Panel panel = new Panel();
            panel.BackColor = COLOR_LIGHT_BLUE;
            panel.Paint += (s, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                int centerX = 350; // Centro del panel (700/2)
                int startY = 150; // Inicio vertical

                // Círculos decorativos (más suaves y mejor posicionados)
                using (SolidBrush brush1 = new SolidBrush(Color.FromArgb(100, COLOR_CORAL)))
                {
                    g.FillEllipse(brush1, centerX - 250 - 144, startY - 100 - 144, 288, 288);
                }

                using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(80, COLOR_PURPLE)))
                {
                    g.FillEllipse(brush2, centerX + 150 - 192, startY + 300 - 192, 384, 384);
                }

                using (SolidBrush brush3 = new SolidBrush(Color.FromArgb(100, COLOR_YELLOW)))
                {
                    g.FillEllipse(brush3, centerX - 350 - 120, startY + 150 - 120, 240, 240);
                }

                // Dibujar imagen del logo - TAMAÑO NORMAL (proporcional, no expandida horizontalmente)
                int logoY = startY;
                if (logoImage != null)
                {
                    try
                    {
                        // Calcular tamaño proporcional manteniendo aspect ratio - tamaño más conservador
                        int maxWidth = 280; // Más pequeño para que no se expanda
                        int maxHeight = 280;
                        float ratio = Math.Min((float)maxWidth / logoImage.Width, (float)maxHeight / logoImage.Height);
                        int logoWidth = (int)(logoImage.Width * ratio);
                        int logoHeight = (int)(logoImage.Height * ratio);
                        
                        // Centrar la imagen horizontalmente
                        int logoX = centerX - logoWidth / 2;
                        
                        // Dibujar con opacity
                        ColorMatrix matrix = new ColorMatrix();
                        matrix.Matrix33 = 0.95f; // Opacity 0.95
                        ImageAttributes attributes = new ImageAttributes();
                        attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        
                        Rectangle logoRect = new Rectangle(logoX, logoY, logoWidth, logoHeight);
                        g.DrawImage(logoImage, logoRect, 0, 0, logoImage.Width, logoImage.Height, GraphicsUnit.Pixel, attributes);
                        
                        // Actualizar logoY para los textos debajo de la imagen
                        logoY = logoY + logoHeight + 30; // Espacio después de la imagen
                    }
                    catch
                    {
                        // Si falla, continuar sin imagen
                        logoY = startY + 50;
                    }
                }
                else
                {
                    // Si no hay imagen, ajustar logoY
                    logoY = startY + 50;
                }

                // Logo "EurekaBank" - DEBAJO de la imagen, grande y visible
                using (Font fontLogo = new Font("Segoe UI", 50, FontStyle.Bold))
                using (SolidBrush brushLogo = new SolidBrush(COLOR_TEXT_DARK))
                {
                    string logoText = "EurekaBank";
                    SizeF logoSize = g.MeasureString(logoText, fontLogo);
                    float logoX = centerX - logoSize.Width / 2;
                    float textY = logoY;
                    g.DrawString(logoText, fontLogo, brushLogo, logoX, textY);
                    
                    // Actualizar logoY para el siguiente texto
                    logoY = (int)(textY + logoSize.Height + 25);
                }

                // Título "Tu banca, más cerca que nunca." - DEBAJO del logo
                using (Font fontTitle = new Font("Segoe UI", 32, FontStyle.Bold))
                using (SolidBrush brushTitle = new SolidBrush(COLOR_TEXT_DARK))
                {
                    string title = "Tu banca, más cerca que nunca.";
                    RectangleF titleRect = new RectangleF(centerX - 300, logoY, 600, 90);
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    g.DrawString(title, fontTitle, brushTitle, titleRect, format);
                    
                    // Actualizar logoY
                    logoY = (int)(logoY + 90 + 20);
                }

                // Subtítulo - DEBAJO del título
                using (Font fontSubtitle = new Font("Segoe UI", 17))
                using (SolidBrush brushSubtitle = new SolidBrush(Color.FromArgb(74, 85, 104))) // #4a5568
                {
                    string subtitle = "Gestiona tus finanzas de forma segura, rápida y sencilla desde cualquier lugar.";
                    RectangleF subtitleRect = new RectangleF(centerX - 300, logoY, 600, 100);
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    g.DrawString(subtitle, fontSubtitle, brushSubtitle, subtitleRect, format);
                }
            };

            return panel;
        }

        /// <summary>
        /// Crea el panel derecho con formulario mejorado - bordes redondeados
        /// </summary>
        private Panel CrearPanelDerecho()
        {
            Panel panel = new Panel();
            panel.BackColor = COLOR_BACKGROUND_LIGHT;
            panel.Padding = new Padding(80, 100, 80, 100);

            // Contenedor del formulario centrado
            Panel formContainer = new Panel();
            formContainer.Size = new Size(540, 700);
            formContainer.Location = new Point(80, 100);
            formContainer.BackColor = Color.Transparent;

            // Título y subtítulo
            Label title = new Label();
            title.Text = "Bienvenido de vuelta";
            title.Font = new Font("Segoe UI", 36, FontStyle.Bold);
            title.ForeColor = COLOR_TEXT_DARK;
            title.AutoSize = true;
            title.Location = new Point(0, 0);

            Label subtitle = new Label();
            subtitle.Text = "Ingresa a tu cuenta para gestionar tus finanzas.";
            subtitle.Font = new Font("Segoe UI", 17);
            subtitle.ForeColor = Color.FromArgb(107, 114, 128); // #6b7280
            subtitle.AutoSize = true;
            subtitle.Location = new Point(0, 65);

            // Campo Usuario con borde redondeado
            Label usuarioLabel = new Label();
            usuarioLabel.Text = "Usuario";
            usuarioLabel.Font = new Font("Segoe UI", 17, FontStyle.Regular);
            usuarioLabel.ForeColor = COLOR_TEXT_DARK;
            usuarioLabel.AutoSize = true;
            usuarioLabel.Location = new Point(0, 140);

            // Panel contenedor con borde redondeado
            Panel txtUsuarioContainer = new Panel();
            txtUsuarioContainer.Size = new Size(540, 58);
            txtUsuarioContainer.Location = new Point(0, 175);
            txtUsuarioContainer.BackColor = Color.White;
            txtUsuarioContainer.Paint += (s, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                    path.AddArc(txtUsuarioContainer.Width - radius * 2 - 1, 0, radius * 2, radius * 2, 270, 90);
                    path.AddArc(txtUsuarioContainer.Width - radius * 2 - 1, txtUsuarioContainer.Height - radius * 2 - 1, radius * 2, radius * 2, 0, 90);
                    path.AddArc(0, txtUsuarioContainer.Height - radius * 2 - 1, radius * 2, radius * 2, 90, 90);
                    path.CloseAllFigures();
                    using (Pen pen = new Pen(Color.FromArgb(209, 213, 219), 1.5f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            };

            txtUsuario = new TextBox();
            txtUsuario.Font = new Font("Segoe UI", 16);
            txtUsuario.Size = new Size(520, 50);
            txtUsuario.Location = new Point(10, 4);
            txtUsuario.BackColor = Color.White;
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuarioContainer.Controls.Add(txtUsuario);

            // Campo Contraseña con borde redondeado
            Label passwordLabel = new Label();
            passwordLabel.Text = "Contraseña";
            passwordLabel.Font = new Font("Segoe UI", 17, FontStyle.Regular);
            passwordLabel.ForeColor = COLOR_TEXT_DARK;
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(0, 260);

            // Panel contenedor con borde redondeado
            Panel txtPasswordContainer = new Panel();
            txtPasswordContainer.Size = new Size(540, 58);
            txtPasswordContainer.Location = new Point(0, 295);
            txtPasswordContainer.BackColor = Color.White;
            txtPasswordContainer.Paint += (s, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                    path.AddArc(txtPasswordContainer.Width - radius * 2 - 1, 0, radius * 2, radius * 2, 270, 90);
                    path.AddArc(txtPasswordContainer.Width - radius * 2 - 1, txtPasswordContainer.Height - radius * 2 - 1, radius * 2, radius * 2, 0, 90);
                    path.AddArc(0, txtPasswordContainer.Height - radius * 2 - 1, radius * 2, radius * 2, 90, 90);
                    path.CloseAllFigures();
                    using (Pen pen = new Pen(Color.FromArgb(209, 213, 219), 1.5f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            };

            txtPassword = new TextBox();
            txtPassword.Font = new Font("Segoe UI", 16);
            txtPassword.Size = new Size(520, 50);
            txtPassword.Location = new Point(10, 4);
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.UseSystemPasswordChar = true;
            txtPasswordContainer.Controls.Add(txtPassword);

            // Botón de login con borde redondeado
            ButtonRedondeado btnLogin = new ButtonRedondeado();
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            btnLogin.Size = new Size(540, 58);
            btnLogin.Location = new Point(0, 380);
            btnLogin.BackColor = COLOR_PRIMARY;
            btnLogin.ForeColor = Color.White;
            btnLogin.BorderRadius = 12;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += BtnLogin_Click;
            btnLogin.MouseEnter += (s, e) => btnLogin.BackColor = Color.FromArgb(42, 159, 196); // #2a9fc4
            btnLogin.MouseLeave += (s, e) => btnLogin.BackColor = COLOR_PRIMARY;

            // Footer
            Label copyright = new Label();
            copyright.Text = "© 2024 EurekaBank. Todos los derechos reservados.";
            copyright.Font = new Font("Segoe UI", 14);
            copyright.ForeColor = Color.FromArgb(107, 114, 128); // #6b7280
            copyright.AutoSize = true;
            copyright.Location = new Point(70, 480);
            copyright.TextAlign = ContentAlignment.MiddleCenter;

            // Links
            Panel linksPanel = new Panel();
            linksPanel.Size = new Size(540, 30);
            linksPanel.Location = new Point(0, 520);
            linksPanel.BackColor = Color.Transparent;

            LinkLabel link1 = new LinkLabel();
            link1.Text = "Ayuda";
            link1.Font = new Font("Segoe UI", 14);
            link1.LinkColor = Color.FromArgb(107, 114, 128);
            link1.AutoSize = true;
            link1.Location = new Point(160, 5);

            Label separator1 = new Label();
            separator1.Text = "•";
            separator1.Font = new Font("Segoe UI", 14);
            separator1.ForeColor = Color.FromArgb(107, 114, 128);
            separator1.AutoSize = true;
            separator1.Location = new Point(220, 5);

            LinkLabel link2 = new LinkLabel();
            link2.Text = "Seguridad";
            link2.Font = new Font("Segoe UI", 14);
            link2.LinkColor = Color.FromArgb(107, 114, 128);
            link2.AutoSize = true;
            link2.Location = new Point(240, 5);

            Label separator2 = new Label();
            separator2.Text = "•";
            separator2.Font = new Font("Segoe UI", 14);
            separator2.ForeColor = Color.FromArgb(107, 114, 128);
            separator2.AutoSize = true;
            separator2.Location = new Point(330, 5);

            LinkLabel link3 = new LinkLabel();
            link3.Text = "Términos";
            link3.Font = new Font("Segoe UI", 14);
            link3.LinkColor = Color.FromArgb(107, 114, 128);
            link3.AutoSize = true;
            link3.Location = new Point(350, 5);

            linksPanel.Controls.Add(link1);
            linksPanel.Controls.Add(separator1);
            linksPanel.Controls.Add(link2);
            linksPanel.Controls.Add(separator2);
            linksPanel.Controls.Add(link3);

            formContainer.Controls.Add(title);
            formContainer.Controls.Add(subtitle);
            formContainer.Controls.Add(usuarioLabel);
            formContainer.Controls.Add(txtUsuarioContainer);
            formContainer.Controls.Add(passwordLabel);
            formContainer.Controls.Add(txtPasswordContainer);
            formContainer.Controls.Add(btnLogin);
            formContainer.Controls.Add(copyright);
            formContainer.Controls.Add(linksPanel);

            panel.Controls.Add(formContainer);

            return panel;
        }

        /// <summary>
        /// Acción del botón de login
        /// </summary>
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar con hash MD5
            string hashPassword = CalcularMD5(password);
            if (USUARIO.Equals(usuario, StringComparison.OrdinalIgnoreCase) && 
                PASS.Equals(hashPassword, StringComparison.OrdinalIgnoreCase))
            {
                // Abrir menú principal
                MenuView menuView = new MenuView();
                menuView.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Calcula el hash MD5 de una cadena
        /// </summary>
        private string CalcularMD5(string input)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] messageDigest = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder hexString = new StringBuilder();
                    for (int i = 0; i < messageDigest.Length; i++)
                    {
                        string hex = messageDigest[i].ToString("X2");
                        hexString.Append(hex);
                    }
                    return hexString.ToString();
                }
            }
            catch
            {
                return input;
            }
        }
    }


    /// <summary>
    /// Button personalizado con bordes redondeados
    /// </summary>
    public class ButtonRedondeado : Button
    {
        public int BorderRadius { get; set; } = 8;

        public ButtonRedondeado()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Fondo con bordes redondeados
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = BorderRadius;
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                path.AddArc(this.Width - radius * 2 - 1, 0, radius * 2, radius * 2, 270, 90);
                path.AddArc(this.Width - radius * 2 - 1, this.Height - radius * 2 - 1, radius * 2, radius * 2, 0, 90);
                path.AddArc(0, this.Height - radius * 2 - 1, radius * 2, radius * 2, 90, 90);
                path.CloseAllFigures();

                // Relleno
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    g.FillPath(brush, path);
                }

                // Sombra sutil
                using (GraphicsPath shadowPath = new GraphicsPath())
                {
                    shadowPath.AddArc(2, 2, radius * 2, radius * 2, 180, 90);
                    shadowPath.AddArc(this.Width - radius * 2 - 1, 2, radius * 2, radius * 2, 270, 90);
                    shadowPath.AddArc(this.Width - radius * 2 - 1, this.Height - radius * 2 - 1, radius * 2, radius * 2, 0, 90);
                    shadowPath.AddArc(2, this.Height - radius * 2 - 1, radius * 2, radius * 2, 90, 90);
                    shadowPath.CloseAllFigures();
                    
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                    {
                        g.FillPath(shadowBrush, shadowPath);
                    }
                }
            }

            // Texto centrado
            TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, 
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
