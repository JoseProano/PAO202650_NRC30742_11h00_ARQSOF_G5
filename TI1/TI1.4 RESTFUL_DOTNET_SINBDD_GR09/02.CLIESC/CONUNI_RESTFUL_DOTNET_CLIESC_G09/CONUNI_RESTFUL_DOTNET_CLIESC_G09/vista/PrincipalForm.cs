using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CONUNI_RESTFUL_DOTNET_CLIESC_G09.servicio;

namespace CONUNI_RESTFUL_DOTNET_CLIESC_G09.vista
{
    /// <summary>
    /// Formulario principal para las conversiones
    /// </summary>
    public partial class PrincipalForm : Form
    {
        private readonly ConversionApiClient _apiClient;
        
        private Panel pnlPrincipal;
        private Label lblTitulo;
        private ComboBox cmbCategoria;
        private ComboBox cmbUnidadOrigen;
        private ComboBox cmbUnidadDestino;
        private TextBox txtValor;
        private Button btnConvertir;
        private Label lblResultado;
        private TextBox txtResultado;
        private Label lblCategoria;
        private Label lblUnidadOrigen;
        private Label lblUnidadDestino;
        private Label lblValor;

        public PrincipalForm(ConversionApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            CargarCategorias();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario
            this.Text = "Monsters Inc. Converter RESTful - Cliente Escritorio";
            this.Size = new System.Drawing.Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 50);
            this.MinimumSize = new System.Drawing.Size(700, 500);

            // Panel principal
            pnlPrincipal = new Panel();
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.BackColor = System.Drawing.Color.FromArgb(40, 40, 60);
            pnlPrincipal.Padding = new Padding(20);

            // Título
            lblTitulo = new Label();
            lblTitulo.Text = "🔥 MONSTERS INC. CONVERTER RESTFUL 🔥";
            lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.Cyan;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new System.Drawing.Point(50, 20);

            // Label Categoría
            lblCategoria = new Label();
            lblCategoria.Text = "Categoría:";
            lblCategoria.ForeColor = System.Drawing.Color.White;
            lblCategoria.Location = new System.Drawing.Point(50, 80);
            lblCategoria.Size = new System.Drawing.Size(150, 20);
            lblCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);

            // ComboBox Categoría
            cmbCategoria = new ComboBox();
            cmbCategoria.Location = new System.Drawing.Point(50, 105);
            cmbCategoria.Size = new System.Drawing.Size(300, 25);
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            cmbCategoria.SelectedIndexChanged += CmbCategoria_SelectedIndexChanged;

            // Label Unidad Origen
            lblUnidadOrigen = new Label();
            lblUnidadOrigen.Text = "Unidad Origen:";
            lblUnidadOrigen.ForeColor = System.Drawing.Color.White;
            lblUnidadOrigen.Location = new System.Drawing.Point(50, 150);
            lblUnidadOrigen.Size = new System.Drawing.Size(150, 20);
            lblUnidadOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);

            // ComboBox Unidad Origen
            cmbUnidadOrigen = new ComboBox();
            cmbUnidadOrigen.Location = new System.Drawing.Point(50, 175);
            cmbUnidadOrigen.Size = new System.Drawing.Size(300, 25);
            cmbUnidadOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnidadOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);

            // Label Unidad Destino
            lblUnidadDestino = new Label();
            lblUnidadDestino.Text = "Unidad Destino:";
            lblUnidadDestino.ForeColor = System.Drawing.Color.White;
            lblUnidadDestino.Location = new System.Drawing.Point(50, 220);
            lblUnidadDestino.Size = new System.Drawing.Size(150, 20);
            lblUnidadDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);

            // ComboBox Unidad Destino
            cmbUnidadDestino = new ComboBox();
            cmbUnidadDestino.Location = new System.Drawing.Point(50, 245);
            cmbUnidadDestino.Size = new System.Drawing.Size(300, 25);
            cmbUnidadDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnidadDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);

            // Label Valor
            lblValor = new Label();
            lblValor.Text = "Valor a Convertir:";
            lblValor.ForeColor = System.Drawing.Color.White;
            lblValor.Location = new System.Drawing.Point(50, 290);
            lblValor.Size = new System.Drawing.Size(150, 20);
            lblValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);

            // TextBox Valor
            txtValor = new TextBox();
            txtValor.Location = new System.Drawing.Point(50, 315);
            txtValor.Size = new System.Drawing.Size(300, 25);
            txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            txtValor.KeyPress += TxtValor_KeyPress;

            // Botón Convertir
            btnConvertir = new Button();
            btnConvertir.Text = "🔄 CONVERTIR";
            btnConvertir.Location = new System.Drawing.Point(380, 280);
            btnConvertir.Size = new System.Drawing.Size(250, 60);
            btnConvertir.BackColor = System.Drawing.Color.FromArgb(0, 150, 0);
            btnConvertir.ForeColor = System.Drawing.Color.White;
            btnConvertir.FlatStyle = FlatStyle.Flat;
            btnConvertir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            btnConvertir.Click += BtnConvertir_Click;

            // Label Resultado
            lblResultado = new Label();
            lblResultado.Text = "Resultado:";
            lblResultado.ForeColor = System.Drawing.Color.White;
            lblResultado.Location = new System.Drawing.Point(50, 370);
            lblResultado.Size = new System.Drawing.Size(150, 20);
            lblResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);

            // TextBox Resultado
            txtResultado = new TextBox();
            txtResultado.Location = new System.Drawing.Point(50, 395);
            txtResultado.Size = new System.Drawing.Size(580, 25);
            txtResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            txtResultado.ReadOnly = true;
            txtResultado.BackColor = System.Drawing.Color.FromArgb(20, 20, 40);
            txtResultado.ForeColor = System.Drawing.Color.LimeGreen;

            // Agregar controles al panel
            pnlPrincipal.Controls.Add(lblTitulo);
            pnlPrincipal.Controls.Add(lblCategoria);
            pnlPrincipal.Controls.Add(cmbCategoria);
            pnlPrincipal.Controls.Add(lblUnidadOrigen);
            pnlPrincipal.Controls.Add(cmbUnidadOrigen);
            pnlPrincipal.Controls.Add(lblUnidadDestino);
            pnlPrincipal.Controls.Add(cmbUnidadDestino);
            pnlPrincipal.Controls.Add(lblValor);
            pnlPrincipal.Controls.Add(txtValor);
            pnlPrincipal.Controls.Add(btnConvertir);
            pnlPrincipal.Controls.Add(lblResultado);
            pnlPrincipal.Controls.Add(txtResultado);

            this.Controls.Add(pnlPrincipal);

            this.ResumeLayout(false);
        }

        private void CargarCategorias()
        {
            cmbCategoria.Items.Add("Temperatura");
            cmbCategoria.Items.Add("Longitud");
            cmbCategoria.Items.Add("Peso");
            cmbCategoria.Items.Add("Volumen");
            cmbCategoria.Items.Add("Area");
        }

        private void CmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUnidadOrigen.Items.Clear();
            cmbUnidadDestino.Items.Clear();

            string categoria = cmbCategoria.SelectedItem?.ToString()?.ToLower() ?? "";

            switch (categoria)
            {
                case "temperatura":
                    cmbUnidadOrigen.Items.AddRange(new[] { "celsius", "fahrenheit", "kelvin" });
                    cmbUnidadDestino.Items.AddRange(new[] { "celsius", "fahrenheit", "kelvin" });
                    break;
                case "longitud":
                    cmbUnidadOrigen.Items.AddRange(new[] { "metros", "pies", "pulgadas", "kilometros", "millas" });
                    cmbUnidadDestino.Items.AddRange(new[] { "metros", "pies", "pulgadas", "kilometros", "millas" });
                    break;
                case "peso":
                    cmbUnidadOrigen.Items.AddRange(new[] { "kilogramos", "libras", "gramos", "onzas" });
                    cmbUnidadDestino.Items.AddRange(new[] { "kilogramos", "libras", "gramos", "onzas" });
                    break;
                case "volumen":
                    cmbUnidadOrigen.Items.AddRange(new[] { "litros", "galones", "mililitros", "onzasFluidas" });
                    cmbUnidadDestino.Items.AddRange(new[] { "litros", "galones", "mililitros", "onzasFluidas" });
                    break;
                case "area":
                    cmbUnidadOrigen.Items.AddRange(new[] { "metrosCuadrados", "piesCuadrados", "hectareas", "acres" });
                    cmbUnidadDestino.Items.AddRange(new[] { "metrosCuadrados", "piesCuadrados", "hectareas", "acres" });
                    break;
            }
        }

        private void TxtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal y backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || txtValor.Text.Contains(".")) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private async void BtnConvertir_Click(object sender, EventArgs e)
        {
            if (!ValidarEntrada())
            {
                return;
            }

            btnConvertir.Enabled = false;
            btnConvertir.Text = "Procesando...";
            txtResultado.Text = "Esperando respuesta del servidor...";

            try
            {
                if (!double.TryParse(txtValor.Text.Replace(",", "."), out double valor))
                {
                    MessageBox.Show("Por favor ingrese un valor numérico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string categoria = cmbCategoria.SelectedItem.ToString().ToLower();
                string unidadOrigen = cmbUnidadOrigen.SelectedItem.ToString();
                string unidadDestino = cmbUnidadDestino.SelectedItem.ToString();

                var request = new ConversionRequest(valor, unidadOrigen, unidadDestino, categoria);
                var resultado = await _apiClient.ConvertirAsync(request);

                if (resultado.Exito)
                {
                    txtResultado.Text = $"{resultado.ValorOriginal} {resultado.UnidadOrigen} = {resultado.ValorConvertido:F4} {resultado.UnidadDestino}";
                    txtResultado.ForeColor = Color.LimeGreen;
                    MessageBox.Show($"✅ Conversión exitosa!\n\n{resultado.ValorOriginal} {resultado.UnidadOrigen} = {resultado.ValorConvertido:F4} {resultado.UnidadDestino}", 
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txtResultado.Text = $"❌ Error: {resultado.Mensaje}";
                    txtResultado.ForeColor = Color.Red;
                    MessageBox.Show($"❌ Error: {resultado.Mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                txtResultado.Text = $"❌ Error: {ex.Message}";
                txtResultado.ForeColor = Color.Red;
                MessageBox.Show($"Error al conectar con el servidor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConvertir.Enabled = true;
                btnConvertir.Text = "🔄 CONVERTIR";
            }
        }

        private bool ValidarEntrada()
        {
            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoria.Focus();
                return false;
            }

            if (cmbUnidadOrigen.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una unidad de origen.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUnidadOrigen.Focus();
                return false;
            }

            if (cmbUnidadDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una unidad de destino.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUnidadDestino.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Por favor ingrese un valor a convertir.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValor.Focus();
                return false;
            }

            if (cmbUnidadOrigen.SelectedItem.ToString() == cmbUnidadDestino.SelectedItem.ToString())
            {
                MessageBox.Show("La unidad de origen y destino no pueden ser iguales.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _apiClient?.Dispose();
        }
    }
}

