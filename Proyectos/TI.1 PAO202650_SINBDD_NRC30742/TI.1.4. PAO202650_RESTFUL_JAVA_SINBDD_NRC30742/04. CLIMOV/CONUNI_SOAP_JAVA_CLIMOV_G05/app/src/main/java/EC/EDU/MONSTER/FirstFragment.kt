package EC.EDU.MONSTER

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.navigation.fragment.findNavController
import EC.EDU.MONSTER.R
import EC.EDU.MONSTER.databinding.FragmentFirstBinding

/**
 * Fragmento de bienvenida que redirige al InicioFragment
 */
class FirstFragment : Fragment() {

    private var _binding: FragmentFirstBinding? = null
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentFirstBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        // Redirigir automáticamente al InicioFragment
        findNavController().navigate(R.id.InicioFragment)
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}