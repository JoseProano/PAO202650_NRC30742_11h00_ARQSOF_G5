package ec.edu.monster.vista

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.button.MaterialButton
import com.google.android.material.textfield.TextInputEditText
import ec.edu.eb_restful_java.R
import java.security.MessageDigest

class LoginActivity : AppCompatActivity() {
    private lateinit var etUsername: TextInputEditText
    private lateinit var etPassword: TextInputEditText
    private lateinit var btnLogin: MaterialButton

    private val USUARIO = "MONSTER"
    private val PASS_HASH = "6C3F6757E773775FD059E2F025BD14BA" // MD5 de "MONSTER9"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        etUsername = findViewById(R.id.etUsername)
        etPassword = findViewById(R.id.etPassword)
        btnLogin = findViewById(R.id.btnLogin)

        btnLogin.setOnClickListener {
            val usuario = etUsername.text?.toString() ?: ""
            val password = etPassword.text?.toString() ?: ""

            if (usuario.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Por favor, complete todos los campos", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val hashPassword = calcularMD5(password)
            if (USUARIO.equals(usuario, ignoreCase = true) && PASS_HASH.equals(hashPassword, ignoreCase = true)) {
                val intent = Intent(this, MenuActivity::class.java)
                intent.putExtra("usuario", usuario)
                startActivity(intent)
                finish()
            } else {
                Toast.makeText(this, "Usuario o contraseña incorrectos", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun calcularMD5(input: String): String {
        return try {
            val md = MessageDigest.getInstance("MD5")
            val messageDigest = md.digest(input.toByteArray())
            val hexString = StringBuilder()
            for (b in messageDigest) {
                val hex = String.format("%02X", b)
                hexString.append(hex)
            }
            hexString.toString()
        } catch (e: Exception) {
            input
        }
    }
}



