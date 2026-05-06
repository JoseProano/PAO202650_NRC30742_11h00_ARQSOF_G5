using System;
using System.Windows.Forms;

namespace CONUNI_RESTFUL_DOTNET_CLIESC_G09.vista
{
    /// <summary>
    /// Formulario de login
    /// </summary>
    public partial class LoginForm : Form
    {
        private const string USUARIO_VALIDO = "MONSTER";
        private const string CONTRASENA_VALIDA = "MONSTER9";
        private int intentos = 0;
        private const int MAX_INTENTOS = 3;

        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnLogin;
        private Button btnSalir;
        private Label lblUsuario;
        private Label lblContrasena;
        private Label lblTitulo;
        private Label lblIntentos;

        public bool LoginExitoso { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            LoginExitoso = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Configuración del formulario
            this.Text = "Monsters Inc. Converter - Login";
            this.Size = new System.Drawing.Size(450, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 50);

            // Título
            lblTitulo = new Label();
            lblTitulo.Text = "🔥 MONSTERS INC. CONVERTER 🔥";
            lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.Cyan;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new System.Drawing.Point(50, 20);
            lblTitulo.Size = new System.Drawing.Size(350, 24);

            // Label Usuario
            lblUsuario = new Label();
            lblUsuario.Text = "Usuario:";
            lblUsuario.ForeColor = System.Drawing.Color.White;
            lblUsuario.Location = new System.Drawing.Point(50, 70);
            lblUsuario.Size = new System.Drawing.Size(100, 20);

            // TextBox Usuario
            txtUsuario = new TextBox();
            txtUsuario.Location = new System.Drawing.Point(50, 95);
            txtUsuario.Size = new System.Drawing.Size(350, 25);
            txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);

            // Label Contraseña
            lblContrasena = new Label();
            lblContrasena.Text = "Contraseña:";
            lblContrasena.ForeColor = System.Drawing.Color.White;
            lblContrasena.Location = new System.Drawing.Point(50, 130);
            lblContrasena.Size = new System.Drawing.Size(100, 20);

            // TextBox Contraseña
            txtContrasena = new TextBox();
            txtContrasena.Location = new System.Drawing.Point(50, 155);
            txtContrasena.Size = new System.Drawing.Size(350, 25);
            txtContrasena.PasswordChar = '*';
            txtContrasena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            txtContrasena.KeyDown += TxtContrasena_KeyDown;

            // Label Intentos
            lblIntentos = new Label();
            lblIntentos.Text = $"Intentos restantes: {MAX_INTENTOS}";
            lblIntentos.ForeColor = System.Drawing.Color.Yellow;
            lblIntentos.Location = new System.Drawing.Point(50, 185);
            lblIntentos.Size = new System.Drawing.Size(200, 20);

            // Botón Login
            btnLogin = new Button();
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.Location = new System.Drawing.Point(50, 220);
            btnLogin.Size = new System.Drawing.Size(150, 35);
            btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 150, 0);
            btnLogin.ForeColor = System.Drawing.Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            btnLogin.Click += BtnLogin_Click;

            // Botón Salir
            btnSalir = new Button();
            btnSalir.Text = "Salir";
            btnSalir.Location = new System.Drawing.Point(250, 220);
            btnSalir.Size = new System.Drawing.Size(150, 35);
            btnSalir.BackColor = System.Drawing.Color.FromArgb(150, 0, 0);
            btnSalir.ForeColor = System.Drawing.Color.White;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            btnSalir.Click += BtnSalir_Click;

            // Agregar controles al formulario
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblUsuario);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(lblContrasena);
            this.Controls.Add(txtContrasena);
            this.Controls.Add(lblIntentos);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnSalir);

            this.ResumeLayout(false);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            ValidarLogin();
        }

        private void TxtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ValidarLogin();
            }
        }

        private void ValidarLogin()
        {
            string usuario = txtUsuario.Text.Trim().ToUpper();
            string contrasena = txtContrasena.Text;

            if (usuario == USUARIO_VALIDO && contrasena == CONTRASENA_VALIDA)
            {
                LoginExitoso = true;
                MessageBox.Show("¡Login exitoso!\nBienvenido al sistema Monsters Inc. Converter RESTful", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                intentos++;
                lblIntentos.Text = $"Intentos restantes: {MAX_INTENTOS - intentos}";
                
                if (intentos >= MAX_INTENTOS)
                {
                    MessageBox.Show("ACCESO DENEGADO\nDemasiados intentos fallidos.\nContacte al administrador del sistema.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas.\nIntente nuevamente.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContrasena.Clear();
                    txtContrasena.Focus();
                }
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


