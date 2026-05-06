package EC.EDU.MONSTER.vista

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import EC.EDU.MONSTER.R
import EC.EDU.MONSTER.controlador.ControladorMovil
import EC.EDU.MONSTER.databinding.FragmentLongitudBinding
import kotlinx.coroutines.launch

/**
 * Fragmento para conversiones de longitud
 * Permite convertir entre metros, pies, pulgadas, kilómetros y millas
 */
class LongitudFragment : Fragment() {

    private var _binding: FragmentLongitudBinding? = null
    private val binding get() = _binding!!
    
    private lateinit var controlador: ControladorMovil

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentLongitudBinding.inflate(inflater, container, false)
        controlador = ControladorMovil()
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        
        setupUI()
        setupListeners()
    }
    
    private fun setupUI() {
        // Configurar el spinner con las operaciones de longitud
        val operaciones = controlador.getOperacionesLongitud()
        val adapter = ArrayAdapter(
            requireContext(),
            android.R.layout.simple_spinner_item,
            operaciones.map { it.second }
        )
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.spinnerOperacion.adapter = adapter
    }
    
    private fun setupListeners() {
        // Botón de regreso (flecha en el header)
        binding.btnBack.setOnClickListener {
            findNavController().navigateUp()
        }
        
        binding.btnConvertir.setOnClickListener {
            convertirLongitud()
        }
        
        binding.btnLimpiar.setOnClickListener {
            limpiarCampos()
        }
    }
    
    private fun convertirLongitud() {
        val valorTexto = binding.editValor.text?.toString()?.trim() ?: ""
        
        if (valorTexto.isEmpty()) {
            binding.editValor.error = "Ingrese un valor"
            return
        }
        
        val valor = valorTexto.toDoubleOrNull()
        if (valor == null) {
            binding.editValor.error = "Valor inválido"
            return
        }
        
        val posicion = binding.spinnerOperacion.selectedItemPosition
        val operaciones = controlador.getOperacionesLongitud()
        val operacion = operaciones[posicion].first
        
        // Mostrar loading
        binding.btnConvertir.isEnabled = false
        binding.btnConvertir.text = "Convirtiendo..."
        
        // Realizar conversión en corrutina
        lifecycleScope.launch {
            try {
                val resultado = controlador.convertirLongitud(operacion, valor)
                
                // Actualizar UI en el hilo principal
                requireActivity().runOnUiThread {
                    binding.btnConvertir.isEnabled = true
                    binding.btnConvertir.text = "Convertir"
                    
                    if (resultado.exitosa) {
                        mostrarResultado(resultado)
                    } else {
                        mostrarError(resultado.mensajeError)
                    }
                }
            } catch (e: Exception) {
                requireActivity().runOnUiThread {
                    binding.btnConvertir.isEnabled = true
                    binding.btnConvertir.text = "Convertir"
                    mostrarError("Error de conexión: ${e.message}")
                }
            }
        }
    }
    
    private fun mostrarResultado(conversion: EC.EDU.MONSTER.modelo.Conversion) {
        binding.textResultado.text = conversion.getResultadoConSimbolos()
        binding.textResultado.setTextColor(resources.getColor(R.color.monster_pink, null))
        binding.layoutResultado.visibility = View.VISIBLE
        
        Toast.makeText(requireContext(), "Conversión exitosa", Toast.LENGTH_SHORT).show()
    }
    
    private fun mostrarError(mensaje: String) {
        binding.textResultado.text = mensaje
        binding.textResultado.setTextColor(resources.getColor(android.R.color.holo_red_dark, null))
        binding.layoutResultado.visibility = View.VISIBLE
        
        Toast.makeText(requireContext(), "Error en la conversión", Toast.LENGTH_SHORT).show()
    }
    
    private fun limpiarCampos() {
        binding.editValor.text?.clear()
        binding.spinnerOperacion.setSelection(0)
        binding.layoutResultado.visibility = View.GONE
        binding.editValor.error = null
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}
