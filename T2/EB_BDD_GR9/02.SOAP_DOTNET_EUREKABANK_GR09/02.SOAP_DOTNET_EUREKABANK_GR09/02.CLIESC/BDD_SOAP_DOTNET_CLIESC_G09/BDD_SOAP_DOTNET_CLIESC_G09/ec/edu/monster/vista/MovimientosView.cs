using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.controlador;
using BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.modelo;

namespace BDD_SOAP_DOTNET_CLIESC_G09.ec.edu.monster.vista
{
    /// <summary>
    /// Vista para consultar movimientos bancarios - Diseño completamente mejorado
    /// </summary>
    public partial class MovimientosView : Form
    {
        private static readonly Color COLOR_PRIMARY = Color.FromArgb(58, 180, 217);      // #3ab4d9
        private static readonly Color COLOR_BACKGROUND_LIGHT = Color.FromArgb(175, 224, 248); // #afe0f8
        private static readonly Color COLOR_TEXT_DARK = Color.FromArgb(15, 23, 26);      // #0f171a
        private static readonly Color COLOR_INGRESO = Color.FromArgb(16, 185, 129);      // #10b981
        private static readonly Color COLOR_SALIDA = Color.FromArgb(239, 68, 68);        // #ef4444

        private Desktop_Controlador controlador;
        private DataGridView dgvMovimientos;
        private TextBox txtCuenta;
        private Panel searchPanel;
        private Panel tablePanel;

        public MovimientosView()
        {
            InitializeComponent();
            controlador = new Desktop_Controlador();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "EurekaBank - Consultar Movimientos";
            this.Size = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = COLOR_BACKGROUND_LIGHT;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Panel header = CrearHeader();
            this.Controls.Add(header);

            Panel content = CrearContenido();
            this.Controls.Add(content);

            this.ResumeLayout(false);
        }

        private Panel CrearHeader()
        {
            Panel header = new Panel();
            header.Size = new Size(1400, 60);
            header.Location = new Point(0, 0);
            header.BackColor = Color.FromArgb(128, 255, 255, 255);
            header.Paint += (s, e) => {
                Graphics g = e.Graphics;
                using (Pen pen = new Pen(Color.FromArgb(77, 255, 255, 255), 1))
                {
                    g.DrawLine(pen, 0, 59, 1400, 59);
                }
                using (Font font = new Font("Segoe UI", 24, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(COLOR_TEXT_DARK))
                {
                    g.DrawString("EurekaBank", font, brush, 40, 15);
                }
            };

            ButtonRedondeado btnVolver = new ButtonRedondeado();
            btnVolver.Text = "Volver";
            btnVolver.Font = new Font("Segoe UI", 14);
            btnVolver.Size = new Size(100, 40);
            btnVolver.Location = new Point(1280, 10);
            btnVolver.BackColor = COLOR_PRIMARY;
            btnVolver.ForeColor = Color.White;
            btnVolver.BorderRadius = 8;
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.Click += (s, e) => {
                MenuView menuView = new MenuView();
                menuView.Show();
                this.Close();
            };
            btnVolver.MouseEnter += (s, e) => btnVolver.BackColor = Color.FromArgb(42, 159, 196);
            btnVolver.MouseLeave += (s, e) => btnVolver.BackColor = COLOR_PRIMARY;
            header.Controls.Add(btnVolver);

            return header;
        }

        private Panel CrearContenido()
        {
            Panel content = new Panel();
            content.Size = new Size(1400, 840);
            content.Location = new Point(0, 60);
            content.BackColor = COLOR_BACKGROUND_LIGHT;

            // Título - CENTRADO
            Label title = new Label();
            title.Text = "Consultar Movimientos";
            title.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            title.ForeColor = COLOR_TEXT_DARK;
            title.AutoSize = true;
            title.Location = new Point(0, 30);
            title.Width = 1400;
            title.TextAlign = ContentAlignment.MiddleCenter;

            // Panel de búsqueda con bordes redondeados
            searchPanel = new Panel();
            searchPanel.Size = new Size(900, 120);
            searchPanel.Location = new Point(250, 100);
            searchPanel.BackColor = Color.White;
            searchPanel.Paint += (s, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Bordes redondeados
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                    path.AddArc(searchPanel.Width - radius * 2 - 1, 0, radius * 2, radius * 2, 270, 90);
                    path.AddArc(searchPanel.Width - radius * 2 - 1, searchPanel.Height - radius * 2 - 1, radius * 2, radius * 2, 0, 90);
                    path.AddArc(0, searchPanel.Height - radius * 2 - 1, radius * 2, radius * 2, 90, 90);
                    path.CloseAllFigures();
                    
                    // Sombra
                    for (int i = 0; i < 5; i++)
                    {
                        using (Pen shadowPen = new Pen(Color.FromArgb(15 - i * 3, 0, 0, 0), 1))
                        {
                            Rectangle shadowRect = new Rectangle(i, i, searchPanel.Width - i * 2 - 1, searchPanel.Height - i * 2 - 1);
                            g.DrawPath(shadowPen, path);
                        }
                    }
                }
            };

            // Label ARRIBA del campo de texto
            Label label = new Label();
            label.Text = "Número de cuenta:";
            label.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            label.ForeColor = COLOR_TEXT_DARK;
            label.AutoSize = true;
            label.Location = new Point(30, 20);

            // TextBox con borde redondeado
            Panel txtContainer = new Panel();
            txtContainer.Size = new Size(500, 50);
            txtContainer.Location = new Point(30, 55);
            txtContainer.BackColor = Color.FromArgb(246, 247, 248);
            txtContainer.Paint += (s, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 8;
                    path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                    path.AddArc(txtContainer.Width - radius * 2 - 1, 0, radius * 2, radius * 2, 270, 90);
                    path.AddArc(txtContainer.Width - radius * 2 - 1, txtContainer.Height - radius * 2 - 1, radius * 2, radius * 2, 0, 90);
                    path.AddArc(0, txtContainer.Height - radius * 2 - 1, radius * 2, radius * 2, 90, 90);
                    path.CloseAllFigures();
                    using (Pen pen = new Pen(Color.FromArgb(209, 213, 219), 1.5f))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            };

            txtCuenta = new TextBox();
            txtCuenta.Font = new Font("Segoe UI", 16);
            txtCuenta.Size = new Size(480, 42);
            txtCuenta.Location = new Point(10, 4);
            txtCuenta.BackColor = Color.FromArgb(246, 247, 248);
            txtCuenta.BorderStyle = BorderStyle.None;
            txtContainer.Controls.Add(txtCuenta);

            // Botón Buscar con bordes redondeados
            ButtonRedondeado btnBuscar = new ButtonRedondeado();
            btnBuscar.Text = "Buscar";
            btnBuscar.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            btnBuscar.Size = new Size(160, 50);
            btnBuscar.Location = new Point(550, 55);
            btnBuscar.BackColor = COLOR_PRIMARY;
            btnBuscar.ForeColor = Color.White;
            btnBuscar.BorderRadius = 8;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.Click += BtnBuscar_Click;
            btnBuscar.MouseEnter += (s, e) => btnBuscar.BackColor = Color.FromArgb(42, 159, 196);
            btnBuscar.MouseLeave += (s, e) => btnBuscar.BackColor = COLOR_PRIMARY;

            searchPanel.Controls.Add(label);
            searchPanel.Controls.Add(txtContainer);
            searchPanel.Controls.Add(btnBuscar);

            // Panel para la tabla con bordes redondeados
            tablePanel = new Panel();
            tablePanel.Size = new Size(1300, 550);
            tablePanel.Location = new Point(50, 250);
            tablePanel.BackColor = Color.White;
            tablePanel.Paint += (s, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Bordes redondeados
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 12;
                    path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                    path.AddArc(tablePanel.Width - radius * 2 - 1, 0, radius * 2, radius * 2, 270, 90);
                    path.AddArc(tablePanel.Width - radius * 2 - 1, tablePanel.Height - radius * 2 - 1, radius * 2, radius * 2, 0, 90);
                    path.AddArc(0, tablePanel.Height - radius * 2 - 1, radius * 2, radius * 2, 90, 90);
                    path.CloseAllFigures();
                    
                    // Sombra
                    for (int i = 0; i < 5; i++)
                    {
                        using (Pen shadowPen = new Pen(Color.FromArgb(15 - i * 3, 0, 0, 0), 1))
                        {
                            Rectangle shadowRect = new Rectangle(i, i, tablePanel.Width - i * 2 - 1, tablePanel.Height - i * 2 - 1);
                            g.DrawPath(shadowPen, path);
                        }
                    }
                }
            };

            // DataGridView dentro del panel
            dgvMovimientos = new DataGridView();
            dgvMovimientos.Size = new Size(1280, 530);
            dgvMovimientos.Location = new Point(10, 10);
            dgvMovimientos.BackgroundColor = Color.White;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.MultiSelect = false;
            dgvMovimientos.RowHeadersVisible = false;
            dgvMovimientos.BorderStyle = BorderStyle.None;
            dgvMovimientos.Font = new Font("Segoe UI", 13); // Fuente más grande para mejor legibilidad
            dgvMovimientos.ColumnHeadersVisible = true; // Asegurar que los encabezados sean visibles
            dgvMovimientos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Encabezados más grandes
            dgvMovimientos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvMovimientos.ColumnHeadersDefaultCellStyle.ForeColor = COLOR_TEXT_DARK;
            dgvMovimientos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMovimientos.ColumnHeadersHeight = 50; // Altura mayor para los encabezados
            dgvMovimientos.RowTemplate.Height = 45; // Altura mayor para las filas
            dgvMovimientos.EnableHeadersVisualStyles = false;
            dgvMovimientos.GridColor = Color.FromArgb(229, 231, 235);
            dgvMovimientos.DefaultCellStyle.Padding = new Padding(8, 10, 8, 10); // Más padding para mejor espaciado
            dgvMovimientos.DefaultCellStyle.Font = new Font("Segoe UI", 13); // Asegurar fuente grande en todas las celdas

            ConfigurarColumnas();

            tablePanel.Controls.Add(dgvMovimientos);

            content.Controls.Add(title);
            content.Controls.Add(searchPanel);
            content.Controls.Add(tablePanel);

            return content;
        }

        private void ConfigurarColumnas()
        {
            dgvMovimientos.Columns.Clear();

            dgvMovimientos.Columns.Add("Cuenta", "Cuenta");
            dgvMovimientos.Columns.Add("NroMov", "Nro Mov");
            dgvMovimientos.Columns.Add("Fecha", "Fecha");
            dgvMovimientos.Columns.Add("Tipo", "Tipo");
            dgvMovimientos.Columns.Add("Accion", "Acción");
            dgvMovimientos.Columns.Add("Importe", "Importe");

            dgvMovimientos.Columns["Cuenta"].Width = 150;
            dgvMovimientos.Columns["NroMov"].Width = 120;
            dgvMovimientos.Columns["Fecha"].Width = 150;
            dgvMovimientos.Columns["Tipo"].Width = 120;
            dgvMovimientos.Columns["Accion"].Width = 150;
            dgvMovimientos.Columns["Importe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Centrar todas las columnas
            foreach (DataGridViewColumn col in dgvMovimientos.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvMovimientos.CellFormatting += DgvMovimientos_CellFormatting;
        }

        private void DgvMovimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridView dgv = (DataGridView)sender;
            string columna = dgv.Columns[e.ColumnIndex].Name;

            if (columna == "Accion")
            {
                string accion = e.Value?.ToString() ?? "";
                if (accion == "INGRESO")
                {
                    e.CellStyle.ForeColor = COLOR_INGRESO;
                    e.CellStyle.BackColor = Color.FromArgb(25, COLOR_INGRESO.R, COLOR_INGRESO.G, COLOR_INGRESO.B);
                    e.CellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold); // Fuente más grande
                }
                else if (accion == "SALIDA")
                {
                    e.CellStyle.ForeColor = COLOR_SALIDA;
                    e.CellStyle.BackColor = Color.FromArgb(25, COLOR_SALIDA.R, COLOR_SALIDA.G, COLOR_SALIDA.B);
                    e.CellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold); // Fuente más grande
                }
                else
                {
                    e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128);
                }
            }
            else if (columna == "Importe")
            {
                string importeStr = e.Value?.ToString() ?? "";
                if (importeStr.StartsWith("+"))
                {
                    e.CellStyle.ForeColor = COLOR_INGRESO;
                    e.CellStyle.BackColor = Color.FromArgb(38, COLOR_INGRESO.R, COLOR_INGRESO.G, COLOR_INGRESO.B);
                    e.CellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold); // Fuente más grande
                }
                else if (importeStr.StartsWith("-"))
                {
                    e.CellStyle.ForeColor = COLOR_SALIDA;
                    e.CellStyle.BackColor = Color.FromArgb(38, COLOR_SALIDA.R, COLOR_SALIDA.G, COLOR_SALIDA.B);
                    e.CellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold); // Fuente más grande
                }
                else
                {
                    e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128);
                }
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string cuenta = txtCuenta.Text.Trim();
            if (string.IsNullOrEmpty(cuenta))
            {
                MessageBox.Show("Por favor, ingrese un número de cuenta.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                List<Movimiento> movimientos = controlador.TraerMovimientos(cuenta);
                dgvMovimientos.Rows.Clear();

                if (movimientos != null && movimientos.Count > 0)
                {
                    foreach (var mov in movimientos)
                    {
                        string fecha = mov.getMovifecha().ToString("dd/MM/yyyy");
                        string accion = mov.getAccion() ?? "N/A";
                        string signo = "INGRESO".Equals(accion) ? "+" : "-";
                        string importe = $"{signo} $ {Math.Abs(mov.getMoviimporte()):F2}";

                        dgvMovimientos.Rows.Add(
                            mov.getCuencodigo() ?? "",
                            mov.getMovinumero(),
                            fecha,
                            mov.getTipocodigo() ?? "",
                            accion,
                            importe
                        );
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontraron movimientos para la cuenta: {cuenta}", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                if (ex.InnerException != null)
                {
                    mensaje += "\n\nDetalles: " + ex.InnerException.Message;
                }
                MessageBox.Show($"Error al consultar movimientos:\n\n{mensaje}\n\n" +
                    "Por favor, verifique que:\n" +
                    "1. El servicio SOAP esté corriendo\n" +
                    "2. La URL del servicio sea correcta\n" +
                    "3. No haya problemas de red o firewall", 
                    "Error de Conexión", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
