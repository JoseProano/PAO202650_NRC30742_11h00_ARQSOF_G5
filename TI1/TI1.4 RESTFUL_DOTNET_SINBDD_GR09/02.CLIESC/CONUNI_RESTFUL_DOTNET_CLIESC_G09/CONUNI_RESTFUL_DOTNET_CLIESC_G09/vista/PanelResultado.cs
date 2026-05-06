using System;
using System.Drawing;
using System.Windows.Forms;

namespace CONUNI_RESTFUL_DOTNET_CLIESC_G09.vista
{
    /// <summary>
    /// Panel para mostrar resultados de conversiones - Diseño compacto y funcional
    /// </summary>
    public class PanelResultado : Panel
    {
        private static readonly Color AZUL_PRINCIPAL = Color.FromArgb(33, 169, 218);
        private static readonly Color ROJO_MONSTER = Color.FromArgb(246, 105, 113);
        private static readonly Color AMARILLO_MONSTER = Color.FromArgb(245, 216, 128);
        private static readonly Color PURPURA_MONSTER = Color.FromArgb(140, 107, 205);
        private static readonly Color AZUL_CLARO = Color.FromArgb(159, 220, 250);
        private static readonly Color BLANCO = Color.White;
        private static readonly Color GRIS_OSCURO = Color.FromArgb(60, 60, 60);
        private static readonly Color GRIS_CLARO = Color.FromArgb(240, 240, 240);

        private Label lblTitulo;
        private TextBox areaResultado;

        public PanelResultado()
        {
            InicializarComponentes();
            ConfigurarLayout();
        }

        private void InicializarComponentes()
        {
            lblTitulo = new Label();
            lblTitulo.Text = "RESULTADO DE LA CONVERSIÓN";
            lblTitulo.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = AZUL_PRINCIPAL;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.Dock = DockStyle.Top;

            areaResultado = new TextBox();
            areaResultado.Font = new Font("Arial", 14F);
            areaResultado.ForeColor = GRIS_OSCURO;
            areaResultado.BackColor = Color.FromArgb(248, 249, 250);
            areaResultado.Multiline = true;
            areaResultado.ReadOnly = true;
            areaResultado.ScrollBars = ScrollBars.Vertical;
            areaResultado.WordWrap = true;
            areaResultado.Text = "El resultado de la conversión aparecerá aquí...";
            areaResultado.Dock = DockStyle.Fill;
            areaResultado.BorderStyle = BorderStyle.None;
            areaResultado.Padding = new Padding(20);
        }

        private void ConfigurarLayout()
        {
            this.BackColor = BLANCO;
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(10);

            this.Controls.Add(areaResultado);
            this.Controls.Add(lblTitulo);
        }

        public void MostrarResultado(string resultado)
        {
            areaResultado.Text = resultado;
            areaResultado.ForeColor = AZUL_PRINCIPAL;
            areaResultado.BackColor = AZUL_CLARO;
            areaResultado.Font = new Font("Arial", 16F, FontStyle.Bold);
        }

        public void MostrarError(string error)
        {
            areaResultado.Text = "ERROR: " + error;
            areaResultado.ForeColor = ROJO_MONSTER;
            areaResultado.BackColor = Color.FromArgb(255, 240, 240);
            areaResultado.Font = new Font("Arial", 14F, FontStyle.Bold);
        }

        public void Limpiar()
        {
            areaResultado.Text = "El resultado de la conversión aparecerá aquí...";
            areaResultado.ForeColor = GRIS_OSCURO;
            areaResultado.BackColor = Color.FromArgb(248, 249, 250);
            areaResultado.Font = new Font("Arial", 14F);
        }
    }
}


