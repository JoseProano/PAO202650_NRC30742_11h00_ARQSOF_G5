package ec.edu.monster

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.EditText
import android.widget.ImageView
import android.widget.ProgressBar
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class LoginActivity : AppCompatActivity() {
    private lateinit var editUsuario: EditText
    private lateinit var editPassword: EditText
    private lateinit var btnTogglePassword: ImageView
    private lateinit var btnLogin: com.google.android.material.button.MaterialButton
    private lateinit var btnSalir: com.google.android.material.button.MaterialButton
    private lateinit var textError: TextView
    private lateinit var progressBar: ProgressBar
    
    private var passwordVisible = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        editUsuario = findViewById(R.id.editUsuario)
        editPassword = findViewById(R.id.editPassword)
        btnTogglePassword = findViewById(R.id.btnTogglePassword)
        btnLogin = findViewById(R.id.btnLogin)
        btnSalir = findViewById(R.id.btnSalir)
        textError = findViewById(R.id.textError)
        progressBar = findViewById(R.id.progressBar)

        btnTogglePassword.setOnClickListener {
            togglePasswordVisibility()
        }

        btnLogin.setOnClickListener {
            attemptLogin()
        }

        btnSalir.setOnClickListener {
            finish()
        }
    }

    private fun togglePasswordVisibility() {
        passwordVisible = !passwordVisible
        val inputType = if (passwordVisible) {
            android.text.InputType.TYPE_TEXT_VARIATION_VISIBLE_PASSWORD
        } else {
            android.text.InputType.TYPE_CLASS_TEXT or android.text.InputType.TYPE_TEXT_VARIATION_PASSWORD
        }
        editPassword.inputType = inputType
        editPassword.setSelection(editPassword.text.length)
    }

    private fun attemptLogin() {
        val usuario = editUsuario.text.toString().trim()
        val password = editPassword.text.toString().trim()

        if (usuario.isEmpty() || password.isEmpty()) {
            showError("Por favor, complete todos los campos")
            return
        }

        // Validación simple: MONSTER / MONSTER9
        if (usuario.equals("MONSTER", ignoreCase = true) && password == "MONSTER9") {
            progressBar.visibility = View.VISIBLE
            btnLogin.isEnabled = false
            
            // Simular validación (puedes agregar llamada REST aquí si tienes endpoint de login)
            editUsuario.postDelayed({
                progressBar.visibility = View.GONE
                btnLogin.isEnabled = true
                hideError()
                
                // Navegar a la pantalla principal
                val intent = Intent(this, MainActivity::class.java)
                startActivity(intent)
                finish()
            }, 500)
        } else {
            showError("Credenciales inválidas. Usuario: MONSTER, Contraseña: MONSTER9")
        }
    }

    private fun showError(message: String) {
        textError.text = message
        textError.visibility = View.VISIBLE
    }

    private fun hideError() {
        textError.visibility = View.GONE
    }
}

