using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.controlador;

namespace BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.vista
{
    /// <summary>
    /// Vista del Menú Principal - EurekaBank - Diseño Dashboard Mejorado
    /// </summary>
    public partial class MenuView : Form
    {
        private static readonly Color COLOR_PRIMARY = Color.FromArgb(58, 180, 217);      // #3ab4d9
        private static readonly Color COLOR_CORAL = Color.FromArgb(246, 126, 128);        // #f67e80
        private static readonly Color COLOR_YELLOW = Color.FromArgb(246, 222, 136);       // #f6de88
        private static readonly Color COLOR_PURPLE = Color.FromArgb(158, 124, 197);        // #9e7cc5
        private static readonly Color COLOR_LIGHT_BLUE = Color.FromArgb(175, 224, 248);   // #afe0f8
        private static readonly Color COLOR_TEXT_DARK = Color.FromArgb(15, 23, 26);      // #0f171a

        private Desktop_Controlador controlador;

        public MenuView()
        {
            InitializeComponent();
            controlador = new Desktop_Controlador();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "EurekaBank - Dashboard";
            this.Size = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = COLOR_LIGHT_BLUE;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Header
            Panel header = CrearHeader();
            this.Controls.Add(header);

            // Contenido principal tipo dashboard
            Panel content = CrearContenidoDashboard();
            this.Controls.Add(content);

            this.ResumeLayout(false);
        }

        private Panel CrearHeader()
        {
            Panel header = new Panel();
            header.Size = new Size(1400, 60);
            header.Location = new Point(0, 0);
            header.BackColor = Color.FromArgb(128, 255, 255, 255); // rgba(255,255,255,0.5)
            header.Paint += (s, e) => {
                Graphics g = e.Graphics;
                // Borde inferior
                using (Pen pen = new Pen(Color.FromArgb(77, 255, 255, 255), 1)) // rgba(255,255,255,0.3)
                {
                    g.DrawLine(pen, 0, 59, 1400, 59);
                }
                // Logo
                using (Font font = new Font("Segoe UI", 24, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(COLOR_TEXT_DARK))
                {
                    g.DrawString("EurekaBank", font, brush, 40, 15);
                }
            };

            Button btnSalir = new Button();
            btnSalir.Text = "Salir";
            btnSalir.Font = new Font("Segoe UI", 14);
            btnSalir.Size = new Size(80, 36);
            btnSalir.Location = new Point(1300, 12);
            btnSalir.BackColor = COLOR_CORAL;
            btnSalir.ForeColor = Color.White;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.Click += (s, e) => {
                LoginView loginView = new LoginView();
                loginView.Show();
                this.Close();
            };
            header.Controls.Add(btnSalir);

            return header;
        }

        private Panel CrearContenidoDashboard()
        {
            Panel content = new Panel();
            content.Size = new Size(1400, 840);
            content.Location = new Point(0, 60);
            content.BackColor = COLOR_LIGHT_BLUE;

            // Título de bienvenida - CENTRADO
            Label title = new Label();
            title.Text = "Bienvenido a EurekaBank";
            title.Font = new Font("Segoe UI", 36, FontStyle.Bold);
            title.ForeColor = COLOR_TEXT_DARK;
            title.AutoSize = true;
            title.Location = new Point(0, 30);
            title.Width = 1400;
            title.TextAlign = ContentAlignment.MiddleCenter;

            Label subtitle = new Label();
            subtitle.Text = "Selecciona una operación para continuar";
            subtitle.Font = new Font("Segoe UI", 18);
            subtitle.ForeColor = Color.FromArgb(74, 85, 104); // #4a5568
            subtitle.AutoSize = true;
            subtitle.Location = new Point(0, 85);
            subtitle.Width = 1400;
            subtitle.TextAlign = ContentAlignment.MiddleCenter;

            // Grid de 2x2 para las 4 tarjetas - CENTRADO
            int cardWidth = 320;
            int cardHeight = 320;
            int gap = 30;
            int totalWidth = (cardWidth * 2) + gap;
            int totalHeight = (cardHeight * 2) + gap;
            int startX = (1400 - totalWidth) / 2; // Centrado horizontal
            int startY = 150; // Espacio después del subtítulo

            // Fila 1
            Panel cardMovimientos = CrearCardAccion(
                "Consultar Historial",
                COLOR_PRIMARY,
                "Ver el historial de transacciones de tus cuentas",
                "📊"
            );
            cardMovimientos.Location = new Point(startX, startY);
            Button btnMovimientos = (Button)cardMovimientos.Controls[cardMovimientos.Controls.Count - 1];
            btnMovimientos.Click += (s, e) => {
                MovimientosView movimientosView = new MovimientosView();
                movimientosView.Show();
                this.Hide();
            };

            Panel cardDeposito = CrearCardAccion(
                "Realizar Depósito",
                COLOR_PURPLE,
                "Agregar fondos a tu cuenta bancaria",
                "💰"
            );
            cardDeposito.Location = new Point(startX + cardWidth + gap, startY);
            Button btnDeposito = (Button)cardDeposito.Controls[cardDeposito.Controls.Count - 1];
            btnDeposito.Click += (s, e) => {
                DepositoView depositoView = new DepositoView();
                depositoView.Show();
                this.Hide();
            };

            // Fila 2
            Panel cardRetiro = CrearCardAccion(
                "Realizar Retiro",
                COLOR_CORAL,
                "Retirar dinero de tu cuenta bancaria",
                "💸"
            );
            cardRetiro.Location = new Point(startX, startY + cardHeight + gap);
            Button btnRetiro = (Button)cardRetiro.Controls[cardRetiro.Controls.Count - 1];
            btnRetiro.Click += (s, e) => {
                RetiroView retiroView = new RetiroView();
                retiroView.Show();
                this.Hide();
            };

            Panel cardTransferencia = CrearCardAccion(
                "Realizar Transferencia",
                COLOR_YELLOW,
                "Enviar dinero a otras cuentas",
                "🔄"
            );
            cardTransferencia.Location = new Point(startX + cardWidth + gap, startY + cardHeight + gap);
            Button btnTransferencia = (Button)cardTransferencia.Controls[cardTransferencia.Controls.Count - 1];
            btnTransferencia.Click += (s, e) => {
                TransferenciaView transferenciaView = new TransferenciaView();
                transferenciaView.Show();
                this.Hide();
            };

            content.Controls.Add(title);
            content.Controls.Add(subtitle);
            content.Controls.Add(cardMovimientos);
            content.Controls.Add(cardDeposito);
            content.Controls.Add(cardRetiro);
            content.Controls.Add(cardTransferencia);

            return content;
        }

        private Panel CrearCardAccion(string titulo, Color color, string descripcion, string icono)
        {
            Panel card = new Panel();
            card.Size = new Size(320, 320); // Tarjeta más grande
            card.BackColor = Color.White;
            card.Padding = new Padding(20);
            // SIN BORDES - solo sombra sutil
            card.Paint += (s, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Sombra sutil alrededor de la tarjeta (dropshadow mejorado)
                for (int i = 0; i < 8; i++)
                {
                    using (Pen shadowPen = new Pen(Color.FromArgb(15 - i * 2, 0, 0, 0), 1))
                    {
                        Rectangle shadowRect = new Rectangle(i, i, card.Width - i * 2 - 1, card.Height - i * 2 - 1);
                        g.DrawRectangle(shadowPen, shadowRect);
                    }
                }
            };

            // Contenedor para el icono con fondo circular - PERFECTAMENTE CENTRADO
            Panel iconContainer = new Panel();
            iconContainer.Size = new Size(100, 100);
            iconContainer.Location = new Point((card.Width - 100) / 2, 20); // Centrado horizontalmente
            iconContainer.BackColor = Color.Transparent;
            iconContainer.Paint += (s, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                // Círculo de fondo
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(38, color.R, color.G, color.B))) // 0.15 opacity
                using (Pen pen = new Pen(color, 2))
                {
                    g.FillEllipse(brush, 0, 0, 100, 100);
                    g.DrawEllipse(pen, 0, 0, 100, 100);
                }
            };

            // Icono con emoji - PERFECTAMENTE CENTRADO en el círculo
            Label iconLabel = new Label();
            iconLabel.Text = icono;
            iconLabel.Font = new Font("Segoe UI", 64);
            iconLabel.AutoSize = true;
            // Centrar el icono en el círculo de 100x100
            SizeF iconSize = TextRenderer.MeasureText(icono, iconLabel.Font);
            iconLabel.Location = new Point(
                (100 - (int)iconSize.Width) / 2,
                (100 - (int)iconSize.Height) / 2
            );
            iconLabel.BackColor = Color.Transparent;
            iconContainer.Controls.Add(iconLabel);

            // Título - CENTRADO, tamaño ajustado para que se vea completo
            Label titleLabel = new Label();
            titleLabel.Text = titulo;
            titleLabel.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            titleLabel.ForeColor = COLOR_TEXT_DARK;
            titleLabel.AutoSize = false;
            titleLabel.Size = new Size(280, 50); // Más espacio vertical
            titleLabel.Location = new Point(20, 130);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Descripción - CENTRADA, tamaño ajustado para que se vea completa
            Label descLabel = new Label();
            descLabel.Text = descripcion;
            descLabel.Font = new Font("Segoe UI", 13);
            descLabel.ForeColor = Color.FromArgb(107, 114, 128); // #6b7280
            descLabel.AutoSize = false;
            descLabel.Size = new Size(280, 50); // Más espacio vertical
            descLabel.Location = new Point(20, 180);
            descLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Botón de acción - CENTRADO
            Button btnAccion = new Button();
            btnAccion.Text = "Continuar";
            btnAccion.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnAccion.Size = new Size(280, 45);
            btnAccion.Location = new Point(20, 240);
            btnAccion.BackColor = color;
            btnAccion.ForeColor = Color.White;
            btnAccion.FlatStyle = FlatStyle.Flat;
            btnAccion.FlatAppearance.BorderSize = 0;
            btnAccion.Cursor = Cursors.Hand;
            btnAccion.MouseEnter += (s, e) => {
                Color darker = OscurecerColor(color);
                btnAccion.BackColor = darker;
            };
            btnAccion.MouseLeave += (s, e) => btnAccion.BackColor = color;

            card.Controls.Add(iconContainer);
            card.Controls.Add(titleLabel);
            card.Controls.Add(descLabel);
            card.Controls.Add(btnAccion);

            return card;
        }

        private Color OscurecerColor(Color color)
        {
            if (color == COLOR_PRIMARY) return Color.FromArgb(42, 159, 196); // #2a9fc4
            if (color == COLOR_PURPLE) return Color.FromArgb(125, 95, 160); // #7d5fa0
            if (color == COLOR_CORAL) return Color.FromArgb(224, 93, 95); // #e05d5f
            if (color == COLOR_YELLOW) return Color.FromArgb(229, 200, 104); // #e5c868
            return color;
        }
    }
}
