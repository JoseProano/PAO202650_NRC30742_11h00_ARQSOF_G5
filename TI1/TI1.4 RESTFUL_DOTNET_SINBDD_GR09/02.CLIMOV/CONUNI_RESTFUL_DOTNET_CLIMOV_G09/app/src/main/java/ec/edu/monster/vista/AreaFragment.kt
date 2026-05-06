package ec.edu.monster.vista

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import ec.edu.monster.R
import ec.edu.monster.controlador.ControladorMovil
import ec.edu.monster.databinding.FragmentAreaBinding
import kotlinx.coroutines.launch

class AreaFragment : Fragment() {

    private var _binding: FragmentAreaBinding? = null
    private val binding get() = _binding!!
    private lateinit var controlador: ControladorMovil

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentAreaBinding.inflate(inflater, container, false)
        controlador = ControladorMovil()
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        setupUI()
        setupListeners()
    }
    
    private fun setupUI() {
        val operaciones = controlador.getOperacionesArea()
        val adapter = ArrayAdapter(
            requireContext(),
            android.R.layout.simple_spinner_item,
            operaciones.map { it.second }
        )
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.spinnerOperacion.adapter = adapter
    }
    
    private fun setupListeners() {
        binding.btnBack.setOnClickListener { findNavController().navigateUp() }
        binding.btnConvertir.setOnClickListener { convertirArea() }
        binding.btnLimpiar.setOnClickListener { limpiarCampos() }
    }
    
    private fun convertirArea() {
        val valorTexto = binding.editValor.text?.toString()?.trim() ?: ""
        if (valorTexto.isEmpty()) {
            binding.editValor.error = "Ingrese un valor"
            return
        }
        val valor = valorTexto.toDoubleOrNull() ?: run {
            binding.editValor.error = "Valor inválido"
            return
        }
        val posicion = binding.spinnerOperacion.selectedItemPosition
        val operaciones = controlador.getOperacionesArea()
        val operacion = operaciones[posicion].first
        
        binding.btnConvertir.isEnabled = false
        binding.btnConvertir.text = "Convirtiendo..."
        
        lifecycleScope.launch {
            try {
                val resultado = controlador.convertirArea(operacion, valor)
                
                if (!isAdded || view == null) return@launch
                
                binding.btnConvertir.isEnabled = true
                binding.btnConvertir.text = "Convertir"
                
                if (resultado.exitosa) {
                    mostrarResultado(resultado)
                } else {
                    mostrarError(resultado.mensajeError)
                }
            } catch (e: Exception) {
                if (!isAdded || view == null) return@launch
                
                binding.btnConvertir.isEnabled = true
                binding.btnConvertir.text = "Convertir"
                mostrarError("Error de conexión: ${e.message}")
            }
        }
    }
    
    private fun mostrarResultado(conversion: ec.edu.monster.modelo.Conversion) {
        binding.textResultado.text = conversion.getResultadoConSimbolos()
        binding.textResultado.setTextColor(resources.getColor(R.color.monster_blue, null))
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

