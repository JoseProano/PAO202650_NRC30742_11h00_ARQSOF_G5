using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CONUNI_RESTFUL_DOTNET_CLIESC_G09.servicio;

namespace CONUNI_RESTFUL_DOTNET_CLIESC_G09.vista
{
    /// <summary>
    /// Panel para conversiones de longitud - Diseño compacto y funcional
    /// </summary>
    public class PanelLongitud : Panel
    {
        // Colores oficiales de Monsters Inc.
        private static readonly Color AZUL_PRINCIPAL = Color.FromArgb(33, 169, 218);      // #21a9da
        private static readonly Color ROJO_MONSTER = Color.FromArgb(246, 105, 113);        // #f66971
        private static readonly Color AMARILLO_MONSTER = Color.FromArgb(245, 216, 128);   // #f5d880
        private static readonly Color PURPURA_MONSTER = Color.FromArgb(140, 107, 205);     // #8c6bcd
        private static readonly Color AZUL_CLARO = Color.FromArgb(159, 220, 250);         // #9fdcfa
        private static readonly Color BLANCO = Color.White;
        private static readonly Color GRIS_OSCURO = Color.FromArgb(60, 60, 60);
        private static readonly Color GRIS_CLARO = Color.FromArgb(240, 240, 240);

        // Componentes
        private Label lblTitulo;
        private Label lblValor;
        private TextBox txtValor;
        private Label lblConversion;
        private ComboBox cmbConversion;
        private Button btnConvertir;
        private Label lblResultado;

        // Cliente REST
        private ConversionApiClient clienteREST;

        public PanelLongitud(ConversionApiClient cliente)
        {
            clienteREST = cliente;
            InicializarComponentes();
            ConfigurarLayout();
            ConfigurarEventos();
        }

        private void InicializarComponentes()
        {
            // Título
            lblTitulo = new Label();
            lblTitulo.Text = "CONVERSIONES DE LONGITUD";
            lblTitulo.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = GRIS_OSCURO;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // Valor
            lblValor = new Label();
            lblValor.Text = "Valor a convertir:";
            lblValor.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblValor.ForeColor = GRIS_OSCURO;

            txtValor = new TextBox();
            txtValor.Font = new Font("Arial", 14F);
            txtValor.TextAlign = HorizontalAlignment.Center;
            txtValor.Size = new Size(200, 30);

            // Conversión
            lblConversion = new Label();
            lblConversion.Text = "Tipo de conversión:";
            lblConversion.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblConversion.ForeColor = GRIS_OSCURO;

            cmbConversion = new ComboBox();
            cmbConversion.Items.AddRange(new string[] {
                "Metros → Pies",
                "Pies → Metros",
                "Metros → Pulgadas",
                "Pulgadas → Metros",
                "Kilómetros → Millas",
                "Millas → Kilómetros"
            });
            cmbConversion.Font = new Font("Arial", 12F);
            cmbConversion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConversion.Size = new Size(300, 30);

            // Botón convertir
            btnConvertir = new Button();
            btnConvertir.Text = "CONVERTIR";
            btnConvertir.BackColor = AZUL_PRINCIPAL;
            btnConvertir.ForeColor = BLANCO;
            btnConvertir.Font = new Font("Arial", 16F, FontStyle.Bold);
            btnConvertir.Size = new Size(200, 45);
            btnConvertir.FlatStyle = FlatStyle.Flat;
            btnConvertir.Cursor = Cursors.Hand;

            // Resultado
            lblResultado = new Label();
            lblResultado.Text = "Resultado aparecerá aquí";
            lblResultado.Font = new Font("Arial", 14F);
            lblResultado.ForeColor = GRIS_OSCURO;
            lblResultado.TextAlign = ContentAlignment.MiddleCenter;
            lblResultado.BackColor = GRIS_CLARO;
            lblResultado.AutoSize = false;
            lblResultado.Size = new Size(500, 100);
            lblResultado.BorderStyle = BorderStyle.FixedSingle;
        }

        private Panel panelContenido;
        private Panel panelPrincipal;

        private void ConfigurarLayout()
        {
            this.BackColor = BLANCO;
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(0);

            // Panel principal para centrar todo
            panelPrincipal = new Panel();
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.BackColor = BLANCO;

            // Contenedor centrado para el contenido
            panelContenido = new Panel();
            panelContenido.Size = new Size(600, 500);
            panelContenido.BackColor = BLANCO;
            panelContenido.Anchor = AnchorStyles.None;

            // Título centrado
            lblTitulo.Location = new Point(0, 20);
            lblTitulo.Size = new Size(600, 50);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            panelContenido.Controls.Add(lblTitulo);

            // Valor a convertir
            lblValor.Location = new Point(50, 100);
            lblValor.Size = new Size(200, 30);
            lblValor.TextAlign = ContentAlignment.MiddleLeft;
            panelContenido.Controls.Add(lblValor);

            txtValor.Location = new Point(250, 100);
            txtValor.Size = new Size(300, 35);
            txtValor.Font = new Font("Arial", 12F);
            panelContenido.Controls.Add(txtValor);

            // Tipo de conversión
            lblConversion.Location = new Point(50, 160);
            lblConversion.Size = new Size(200, 30);
            lblConversion.TextAlign = ContentAlignment.MiddleLeft;
            panelContenido.Controls.Add(lblConversion);

            cmbConversion.Location = new Point(250, 160);
            cmbConversion.Size = new Size(300, 35);
            cmbConversion.SelectedIndex = 0;
            panelContenido.Controls.Add(cmbConversion);

            // Botón convertir (centrado)
            btnConvertir.Location = new Point(200, 230);
            btnConvertir.Size = new Size(200, 45);
            btnConvertir.BackColor = AZUL_CLARO;
            btnConvertir.ForeColor = BLANCO;
            btnConvertir.Font = new Font("Arial", 14F, FontStyle.Bold);
            panelContenido.Controls.Add(btnConvertir);

            // Área de resultado
            lblResultado.Location = new Point(50, 310);
            lblResultado.Size = new Size(500, 100);
            lblResultado.BackColor = GRIS_CLARO;
            lblResultado.ForeColor = GRIS_OSCURO;
            lblResultado.BorderStyle = BorderStyle.FixedSingle;
            lblResultado.Text = "Resultado aparecerá aquí";
            panelContenido.Controls.Add(lblResultado);

            // Ajustar posición cuando cambie el tamaño
            panelPrincipal.Resize += PanelPrincipal_Resize;
            this.Resize += PanelPrincipal_Resize;

            panelPrincipal.Controls.Add(panelContenido);
            this.Controls.Add(panelPrincipal);

            // Centrar inicialmente
            CentrarContenido();
        }

        private void PanelPrincipal_Resize(object sender, EventArgs e)
        {
            CentrarContenido();
        }

        private void CentrarContenido()
        {
            if (panelContenido != null && panelPrincipal != null)
            {
                panelContenido.Location = new Point(
                    Math.Max(0, (panelPrincipal.Width - panelContenido.Width) / 2),
                    Math.Max(0, (panelPrincipal.Height - panelContenido.Height) / 2)
                );
            }
        }

        private void ConfigurarEventos()
        {
            btnConvertir.Click += BtnConvertir_Click;
            btnConvertir.MouseEnter += (s, e) => btnConvertir.BackColor = PURPURA_MONSTER;
            btnConvertir.MouseLeave += (s, e) => btnConvertir.BackColor = AZUL_CLARO;
        }

        private async void BtnConvertir_Click(object sender, EventArgs e)
        {
            try
            {
                double valor = double.Parse(txtValor.Text.Trim());
                int indice = cmbConversion.SelectedIndex;

                ConversionResponse resultado = null;

                switch (indice)
                {
                    case 0:
                        resultado = await clienteREST.MetrosAPiesAsync(valor);
                        break;
                    case 1:
                        resultado = await clienteREST.PiesAMetrosAsync(valor);
                        break;
                    case 2:
                        resultado = await clienteREST.MetrosAPulgadasAsync(valor);
                        break;
                    case 3:
                        resultado = await clienteREST.PulgadasAMetrosAsync(valor);
                        break;
                    case 4:
                        resultado = await clienteREST.KilometrosAMillasAsync(valor);
                        break;
                    case 5:
                        resultado = await clienteREST.MillasAKilometrosAsync(valor);
                        break;
                }

                if (resultado != null && resultado.Exito)
                {
                    string resultadoTexto = string.Format("{0:F2} {1} = {2:F2} {3}",
                        resultado.ValorOriginal, resultado.UnidadOrigen,
                        resultado.ValorConvertido, resultado.UnidadDestino);
                    MostrarResultado(resultadoTexto);
                }
                else
                {
                    MostrarError(resultado?.Mensaje ?? "Error desconocido");
                }
            }
            catch (FormatException)
            {
                MostrarError("Ingrese un valor numérico válido");
            }
            catch (Exception ex)
            {
                MostrarError("Error al enviar la solicitud: " + ex.Message);
            }
        }

        public void MostrarResultado(string resultado)
        {
            lblResultado.Text = resultado;
            lblResultado.ForeColor = AZUL_PRINCIPAL;
            lblResultado.BackColor = AZUL_CLARO;
            lblResultado.Font = new Font("Arial", 14F, FontStyle.Bold);
        }

        public void MostrarError(string error)
        {
            lblResultado.Text = "ERROR: " + error;
            lblResultado.ForeColor = ROJO_MONSTER;
            lblResultado.BackColor = Color.FromArgb(255, 240, 240);
            lblResultado.Font = new Font("Arial", 12F, FontStyle.Bold);
        }
    }
}
