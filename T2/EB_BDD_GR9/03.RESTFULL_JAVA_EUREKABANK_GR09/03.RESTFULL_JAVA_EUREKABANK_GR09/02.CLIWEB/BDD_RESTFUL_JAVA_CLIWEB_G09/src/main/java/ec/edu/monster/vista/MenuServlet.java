/*
 * Servlet para mostrar el menú principal
 */
package ec.edu.monster.vista;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;

/**
 * Servlet que muestra el menú principal
 */
public class MenuServlet extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        // Verificar autenticación
        HttpSession session = request.getSession();
        Boolean autenticado = (Boolean) session.getAttribute("autenticado");
        
        if (autenticado == null || !autenticado) {
            response.sendRedirect(request.getContextPath() + "/login");
            return;
        }
        
        // Mostrar menú
        request.getRequestDispatcher("/menu.jsp").forward(request, response);
    }
}

