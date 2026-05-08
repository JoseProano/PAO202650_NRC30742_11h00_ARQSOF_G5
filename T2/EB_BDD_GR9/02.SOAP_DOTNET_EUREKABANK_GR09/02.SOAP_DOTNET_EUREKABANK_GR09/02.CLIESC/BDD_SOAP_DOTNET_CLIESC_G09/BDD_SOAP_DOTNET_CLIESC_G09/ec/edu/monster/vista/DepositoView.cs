using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.controlador;

namespace BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.vista
{
    /// <summary>
    /// Vista para realizar depósitos - Diseño mejorado (igual al Java)
    /// </summary>
    public partial class DepositoView : Form
    {
        private static readonly Color COLOR_PRIMARY = Color.FromArgb(58, 180, 217);      // #3ab4d9
        private static readonly Color COLOR_PURPLE = Color.FromArgb(158, 124, 197);        // #9e7cc5
        private static readonly Color COLOR_BACKGROUND_LIGHT = Color.FromArgb(175, 224, 248); // #afe0f8
        private static readonly Color COLOR_TEXT_DARK = Color.FromArgb(15, 23, 26);      // #0f171a

        private Desktop_Controlador controlador;
        private TextBox txtCuenta;
        private TextBox txtImporte;

        public DepositoView()
        {
            InitializeComponent();
            controlador = new Desktop_Controlador();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "EurekaBank - Realizar Depósito";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = COLOR_BACKGROUND_LIGHT;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Header
            Panel header = CrearHeader();
            this.Controls.Add(header);

            // Contenido
            Panel content = new Panel();
            content.Size = new Size(900, 640);
            content.Location = new Point(0, 60);
            content.BackColor = COLOR_BACKGROUND_LIGHT;
            content.Padding = new Padding(40, 20, 40, 20);

            // Tarjeta principal (VBox con gap 20, padding 30,40,30,40)
            Panel card = new Panel();
            card.Size = new Size(600, 500);
            card.Location = new Point(150, 50);
            card.BackColor = Color.White;
            card.Padding = new Padding(40, 30, 40, 30);
            card.Paint += Card_Paint;

            // Icono decorativo (Circle 35, fill purple 0.2, stroke purple, strokeWidth 3)
            // Se dibuja en Card_Paint

            // Título (28px, font-weight 900)
            Label title = new Label();
            title.Text = "Realizar Depósito";
            title.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            title.ForeColor = COLOR_TEXT_DARK;
            title.AutoSize = true;
            title.Location = new Point(150, 20);

            // Subtítulo (14px, color #6b7280)
            Label subtitle = new Label();
            subtitle.Text = "Agrega fondos a tu cuenta bancaria";
            subtitle.Font = new Font("Segoe UI", 14);
            subtitle.ForeColor = Color.FromArgb(107, 114, 128); // #6b7280
            subtitle.AutoSize = true;
            subtitle.Location = new Point(150, 60);

            // Formulario (VBox con gap 16, prefWidth 500)
            // Campo Cuenta (VBox con gap 8)
            Label cuentaLabel = new Label();
            cuentaLabel.Text = "Número de cuenta";
            cuentaLabel.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cuentaLabel.ForeColor = COLOR_TEXT_DARK;
            cuentaLabel.AutoSize = true;
            cuentaLabel.Location = new Point(40, 140);

            txtCuenta = new TextBox();
            txtCuenta.Font = new Font("Segoe UI", 15);
            txtCuenta.Size = new Size(500, 46);
            txtCuenta.Location = new Point(40, 170);
            txtCuenta.BackColor = Color.FromArgb(246, 247, 248); // #f6f7f8
            txtCuenta.BorderStyle = BorderStyle.FixedSingle;

            // Campo Importe (VBox con gap 8)
            Label importeLabel = new Label();
            importeLabel.Text = "Importe";
            importeLabel.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            importeLabel.ForeColor = COLOR_TEXT_DARK;
            importeLabel.AutoSize = true;
            importeLabel.Location = new Point(40, 240);

            txtImporte = new TextBox();
            txtImporte.Font = new Font("Segoe UI", 15);
            txtImporte.Size = new Size(500, 46);
            txtImporte.Location = new Point(40, 270);
            txtImporte.BackColor = Color.FromArgb(246, 247, 248); // #f6f7f8
            txtImporte.BorderStyle = BorderStyle.FixedSingle;

            // Botón de depósito (16px bold, 500px ancho, 48px alto, purple)
            Button btnDepositar = new Button();
            btnDepositar.Text = "Realizar Depósito";
            btnDepositar.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            btnDepositar.Size = new Size(500, 48);
            btnDepositar.Location = new Point(40, 350);
            btnDepositar.BackColor = COLOR_PURPLE;
            btnDepositar.ForeColor = Color.White;
            btnDepositar.FlatStyle = FlatStyle.Flat;
            btnDepositar.FlatAppearance.BorderSize = 0;
            btnDepositar.Cursor = Cursors.Hand;
            btnDepositar.Click += BtnDepositar_Click;
            btnDepositar.MouseEnter += (s, e) => btnDepositar.BackColor = Color.FromArgb(125, 95, 160); // #7d5fa0
            btnDepositar.MouseLeave += (s, e) => btnDepositar.BackColor = COLOR_PURPLE;

            card.Controls.Add(title);
            card.Controls.Add(subtitle);
            card.Controls.Add(cuentaLabel);
            card.Controls.Add(txtCuenta);
            card.Controls.Add(importeLabel);
            card.Controls.Add(txtImporte);
            card.Controls.Add(btnDepositar);

            content.Controls.Add(card);
            this.Controls.Add(content);

            this.ResumeLayout(false);
        }

        private void Card_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Icono decorativo: Circle 35, fill purple 0.2, stroke purple, strokeWidth 3
            using (Pen pen = new Pen(COLOR_PURPLE, 3))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(51, COLOR_PURPLE))) // 0.2 opacity
            {
                g.FillEllipse(brush, 265, 20, 70, 70);
                g.DrawEllipse(pen, 265, 20, 70, 70);
            }
        }

        private Panel CrearHeader()
        {
            Panel header = new Panel();
            header.Size = new Size(900, 60);
            header.Location = new Point(0, 0);
            header.BackColor = Color.FromArgb(128, 255, 255, 255); // rgba(255,255,255,0.5)
            header.Paint += (s, e) => {
                Graphics g = e.Graphics;
                // Borde inferior
                using (Pen pen = new Pen(Color.FromArgb(77, 255, 255, 255), 1)) // rgba(255,255,255,0.3)
                {
                    g.DrawLine(pen, 0, 59, 900, 59);
                }
                // Logo
                using (Font font = new Font("Segoe UI", 24, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(COLOR_TEXT_DARK))
                {
                    g.DrawString("EurekaBank", font, brush, 40, 15);
                }
            };

            Button btnVolver = new Button();
            btnVolver.Text = "Volver";
            btnVolver.Font = new Font("Segoe UI", 14);
            btnVolver.Size = new Size(80, 36);
            btnVolver.Location = new Point(800, 12);
            btnVolver.BackColor = COLOR_PRIMARY;
            btnVolver.ForeColor = Color.White;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.Click += (s, e) => {
                MenuView menuView = new MenuView();
                menuView.Show();
                this.Close();
            };
            header.Controls.Add(btnVolver);

            return header;
        }

        private void BtnDepositar_Click(object sender, EventArgs e)
        {
            string cuenta = txtCuenta.Text.Trim();
            string importeStr = txtImporte.Text.Trim();

            if (string.IsNullOrEmpty(cuenta) || string.IsNullOrEmpty(importeStr))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                double importe = double.Parse(importeStr.Replace(",", "."));
                if (importe <= 0)
                {
                    MessageBox.Show("El importe debe ser mayor a cero.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int resultado = controlador.RegDeposito(cuenta, importe);
                if (resultado == 1)
                {
                    MessageBox.Show("Depósito realizado exitosamente.", "Éxito", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCuenta.Clear();
                    txtImporte.Clear();
                }
                else
                {
                    MessageBox.Show("Error al procesar el depósito. Verifique los datos.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato inválido. Ingrese un número válido.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
