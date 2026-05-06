package ec.edu.monster.vista

import android.os.Bundle
import android.text.InputType
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import ec.edu.monster.R
import ec.edu.monster.databinding.FragmentLoginBinding

/**
 * Fragmento de login para autenticación
 * Credenciales: MONSTER / MONSTER9
 */
class LoginFragment : Fragment() {

    private var _binding: FragmentLoginBinding? = null
    private val binding get() = _binding!!

    // Credenciales fijas
    private val USUARIO_VALIDO = "MONSTER"
    private val PASSWORD_VALIDO = "MONSTER9"
    
    // Estado de visibilidad de contraseña
    private var isPasswordVisible = false

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentLoginBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        
        setupUI()
        setupListeners()
    }

    private fun setupUI() {
        // Ocultar mensaje de error inicialmente
        binding.textError.visibility = View.GONE
        binding.progressBar.visibility = View.GONE
    }

    private fun setupListeners() {
        // Botones del formulario
        binding.btnLogin.setOnClickListener {
            realizarLogin()
        }
        
        binding.btnSalir.setOnClickListener {
            salirAplicacion()
        }
        
        // Botón para mostrar/ocultar contraseña
        binding.btnTogglePassword.setOnClickListener {
            togglePasswordVisibility()
        }
    }

    private fun realizarLogin() {
        val usuario = binding.editUsuario.text?.toString()?.trim() ?: ""
        val password = binding.editPassword.text?.toString()?.trim() ?: ""

        // Validar campos vacíos
        if (usuario.isEmpty()) {
            binding.editUsuario.error = "Ingrese el usuario"
            binding.editUsuario.requestFocus()
            return
        }

        if (password.isEmpty()) {
            binding.editPassword.error = "Ingrese la contraseña"
            binding.editPassword.requestFocus()
            return
        }

        // Mostrar loading
        mostrarLoading(true)

        // Simular validación (en una app real sería asíncrona)
        validarCredenciales(usuario, password)
    }

    private fun validarCredenciales(usuario: String, password: String) {
        // Simular delay de red
        binding.root.postDelayed({
            if (usuario.equals(USUARIO_VALIDO, ignoreCase = true) && 
                password == PASSWORD_VALIDO) {
                // Login exitoso
                mostrarLoading(false)
                navegarAMain()
            } else {
                // Login fallido
                mostrarLoading(false)
                mostrarError("Usuario o contraseña incorrectos")
            }
        }, 1000) // 1 segundo de delay
    }

    private fun navegarAMain() {
        try {
            findNavController().navigate(R.id.InicioFragment)
        } catch (e: Exception) {
            // Si no hay navegación configurada, mostrar mensaje
            Toast.makeText(requireContext(), "Login exitoso", Toast.LENGTH_SHORT).show()
        }
    }

    private fun salirAplicacion() {
        requireActivity().finish()
    }

    private fun mostrarLoading(mostrar: Boolean) {
        binding.progressBar.visibility = if (mostrar) View.VISIBLE else View.GONE
        binding.btnLogin.isEnabled = !mostrar
        binding.btnSalir.isEnabled = !mostrar
        binding.editUsuario.isEnabled = !mostrar
        binding.editPassword.isEnabled = !mostrar
    }

    private fun mostrarError(mensaje: String) {
        binding.textError.text = mensaje
        binding.textError.visibility = View.VISIBLE
        
        // Ocultar error después de 3 segundos
        binding.root.postDelayed({
            binding.textError.visibility = View.GONE
        }, 3000)
    }
    
    private fun togglePasswordVisibility() {
        isPasswordVisible = !isPasswordVisible
        
        if (isPasswordVisible) {
            // Mostrar contraseña
            binding.editPassword.inputType = InputType.TYPE_TEXT_VARIATION_VISIBLE_PASSWORD
            binding.btnTogglePassword.setImageResource(android.R.drawable.ic_menu_close_clear_cancel)
        } else {
            // Ocultar contraseña
            binding.editPassword.inputType = InputType.TYPE_CLASS_TEXT or InputType.TYPE_TEXT_VARIATION_PASSWORD
            binding.btnTogglePassword.setImageResource(android.R.drawable.ic_menu_view)
        }
        
        // Mover cursor al final
        binding.editPassword.setSelection(binding.editPassword.text?.length ?: 0)
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}


