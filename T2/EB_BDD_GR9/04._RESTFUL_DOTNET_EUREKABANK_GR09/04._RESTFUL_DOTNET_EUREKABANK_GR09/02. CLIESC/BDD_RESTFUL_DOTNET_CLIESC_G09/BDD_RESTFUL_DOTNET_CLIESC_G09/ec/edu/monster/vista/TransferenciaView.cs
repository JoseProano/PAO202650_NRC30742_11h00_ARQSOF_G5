using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BDD_RESTFUL_DOTNET_CLIESC_G09.ec.edu.monster.controlador;

namespace BDD_RESTFUL_DOTNET_CLIESC_G09.ec.edu.monster.vista
{
    /// <summary>
    /// Vista para realizar transferencias - Diseño mejorado (igual al Java)
    /// </summary>
    public partial class TransferenciaView : Form
    {
        private static readonly Color COLOR_PRIMARY = Color.FromArgb(58, 180, 217);      // #3ab4d9
        private static readonly Color COLOR_YELLOW = Color.FromArgb(246, 222, 136);       // #f6de88
        private static readonly Color COLOR_BACKGROUND_LIGHT = Color.FromArgb(175, 224, 248); // #afe0f8
        private static readonly Color COLOR_TEXT_DARK = Color.FromArgb(15, 23, 26);      // #0f171a

        private Desktop_Controlador controlador;
        private TextBox txtCuentaOrigen;
        private TextBox txtCuentaDestino;
        private TextBox txtImporte;

        public TransferenciaView()
        {
            InitializeComponent();
            controlador = new Desktop_Controlador();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "EurekaBank - Realizar Transferencia";
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = COLOR_BACKGROUND_LIGHT;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Panel header = CrearHeader();
            this.Controls.Add(header);

            Panel content = new Panel();
            content.Size = new Size(1000, 740);
            content.Location = new Point(0, 60);
            content.BackColor = COLOR_BACKGROUND_LIGHT;
            content.Padding = new Padding(40, 20, 40, 20);

            Panel card = new Panel();
            card.Size = new Size(700, 600);
            card.Location = new Point(150, 50);
            card.BackColor = Color.White;
            card.Padding = new Padding(40, 30, 40, 30);
            card.Paint += Card_Paint;

            Label title = new Label();
            title.Text = "Realizar Transferencia";
            title.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            title.ForeColor = COLOR_TEXT_DARK;
            title.AutoSize = true;
            title.Location = new Point(200, 20);

            Label subtitle = new Label();
            subtitle.Text = "Envía dinero a otras cuentas de forma segura";
            subtitle.Font = new Font("Segoe UI", 14);
            subtitle.ForeColor = Color.FromArgb(107, 114, 128);
            subtitle.AutoSize = true;
            subtitle.Location = new Point(150, 60);

            // Campo Cuenta Origen
            Label cuentaOrigenLabel = new Label();
            cuentaOrigenLabel.Text = "Cuenta Origen";
            cuentaOrigenLabel.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cuentaOrigenLabel.ForeColor = COLOR_TEXT_DARK;
            cuentaOrigenLabel.AutoSize = true;
            cuentaOrigenLabel.Location = new Point(40, 140);

            txtCuentaOrigen = new TextBox();
            txtCuentaOrigen.Font = new Font("Segoe UI", 15);
            txtCuentaOrigen.Size = new Size(600, 46);
            txtCuentaOrigen.Location = new Point(40, 170);
            txtCuentaOrigen.BackColor = Color.FromArgb(246, 247, 248);
            txtCuentaOrigen.BorderStyle = BorderStyle.FixedSingle;

            // Campo Cuenta Destino
            Label cuentaDestinoLabel = new Label();
            cuentaDestinoLabel.Text = "Cuenta Destino";
            cuentaDestinoLabel.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cuentaDestinoLabel.ForeColor = COLOR_TEXT_DARK;
            cuentaDestinoLabel.AutoSize = true;
            cuentaDestinoLabel.Location = new Point(40, 240);

            txtCuentaDestino = new TextBox();
            txtCuentaDestino.Font = new Font("Segoe UI", 15);
            txtCuentaDestino.Size = new Size(600, 46);
            txtCuentaDestino.Location = new Point(40, 270);
            txtCuentaDestino.BackColor = Color.FromArgb(246, 247, 248);
            txtCuentaDestino.BorderStyle = BorderStyle.FixedSingle;

            // Campo Importe
            Label importeLabel = new Label();
            importeLabel.Text = "Importe";
            importeLabel.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            importeLabel.ForeColor = COLOR_TEXT_DARK;
            importeLabel.AutoSize = true;
            importeLabel.Location = new Point(40, 340);

            txtImporte = new TextBox();
            txtImporte.Font = new Font("Segoe UI", 15);
            txtImporte.Size = new Size(600, 46);
            txtImporte.Location = new Point(40, 370);
            txtImporte.BackColor = Color.FromArgb(246, 247, 248);
            txtImporte.BorderStyle = BorderStyle.FixedSingle;

            // Botón de transferencia (600px ancho, yellow, texto dark)
            Button btnTransferir = new Button();
            btnTransferir.Text = "Realizar Transferencia";
            btnTransferir.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            btnTransferir.Size = new Size(600, 48);
            btnTransferir.Location = new Point(40, 450);
            btnTransferir.BackColor = COLOR_YELLOW;
            btnTransferir.ForeColor = COLOR_TEXT_DARK;
            btnTransferir.FlatStyle = FlatStyle.Flat;
            btnTransferir.FlatAppearance.BorderSize = 0;
            btnTransferir.Cursor = Cursors.Hand;
            btnTransferir.Click += BtnTransferir_Click;
            btnTransferir.MouseEnter += (s, e) => btnTransferir.BackColor = Color.FromArgb(229, 200, 104); // #e5c868
            btnTransferir.MouseLeave += (s, e) => btnTransferir.BackColor = COLOR_YELLOW;

            card.Controls.Add(title);
            card.Controls.Add(subtitle);
            card.Controls.Add(cuentaOrigenLabel);
            card.Controls.Add(txtCuentaOrigen);
            card.Controls.Add(cuentaDestinoLabel);
            card.Controls.Add(txtCuentaDestino);
            card.Controls.Add(importeLabel);
            card.Controls.Add(txtImporte);
            card.Controls.Add(btnTransferir);

            content.Controls.Add(card);
            this.Controls.Add(content);

            this.ResumeLayout(false);
        }

        private void Card_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Icono decorativo: Circle 35, fill yellow 0.2, stroke yellow, strokeWidth 3
            using (Pen pen = new Pen(COLOR_YELLOW, 3))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(51, COLOR_YELLOW))) // 0.2 opacity
            {
                g.FillEllipse(brush, 315, 20, 70, 70);
                g.DrawEllipse(pen, 315, 20, 70, 70);
            }
        }

        private Panel CrearHeader()
        {
            Panel header = new Panel();
            header.Size = new Size(1000, 60);
            header.Location = new Point(0, 0);
            header.BackColor = Color.FromArgb(128, 255, 255, 255);
            header.Paint += (s, e) => {
                Graphics g = e.Graphics;
                using (Pen pen = new Pen(Color.FromArgb(77, 255, 255, 255), 1))
                {
                    g.DrawLine(pen, 0, 59, 1000, 59);
                }
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
            btnVolver.Location = new Point(900, 12);
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

        private void BtnTransferir_Click(object sender, EventArgs e)
        {
            string cuentaOrigen = txtCuentaOrigen.Text.Trim();
            string cuentaDestino = txtCuentaDestino.Text.Trim();
            string importeStr = txtImporte.Text.Trim();

            if (string.IsNullOrEmpty(cuentaOrigen) || string.IsNullOrEmpty(cuentaDestino) || string.IsNullOrEmpty(importeStr))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cuentaOrigen.Equals(cuentaDestino))
            {
                MessageBox.Show("No se puede transferir a la misma cuenta.", "Error", 
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

                int resultado = controlador.RegTransferencia(cuentaOrigen, cuentaDestino, importe);
                if (resultado == 1)
                {
                    MessageBox.Show("Transferencia realizada exitosamente.", "Éxito", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCuentaOrigen.Clear();
                    txtCuentaDestino.Clear();
                    txtImporte.Clear();
                }
                else
                {
                    MessageBox.Show("Error al procesar la transferencia. Verifique los datos.", "Error", 
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



