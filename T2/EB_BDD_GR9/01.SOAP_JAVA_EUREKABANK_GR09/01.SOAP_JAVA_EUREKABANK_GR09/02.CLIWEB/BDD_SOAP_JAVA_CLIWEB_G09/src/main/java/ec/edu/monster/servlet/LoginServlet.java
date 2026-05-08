/*
 * Servlet para manejar el login
 */
package ec.edu.monster.servlet;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.security.MessageDigest;

/**
 * Servlet que maneja el proceso de login
 */
public class LoginServlet extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        // Mostrar página de login
        request.getRequestDispatcher("/login.jsp").forward(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        String usuario = request.getParameter("usuario");
        String password = request.getParameter("password");
        
        System.out.println("Login intent - Usuario: " + usuario + ", Password: " + (password != null ? "***" : "null"));
        
        // Validación (usuario: MONSTER, password: MONSTER9)
        if (usuario != null && password != null) {
            // Normalizar a mayúsculas
            String usuarioUpper = usuario.trim().toUpperCase();
            String passwordUpper = password.trim().toUpperCase();
            
            System.out.println("Validando - Usuario: " + usuarioUpper + ", Password: " + passwordUpper);
            
            // Validar credenciales - usuario en mayúsculas y contraseña MONSTER9
            if (usuarioUpper.equals("MONSTER") && passwordUpper.equals("MONSTER9")) {
                System.out.println("Login exitoso - Creando sesión");
                
                // Crear sesión
                HttpSession session = request.getSession();
                session.setAttribute("usuario", usuarioUpper);
                session.setAttribute("autenticado", true);
                
                // Redirigir al menú principal
                String contextPath = request.getContextPath();
                System.out.println("Redirigiendo a: " + contextPath + "/menu");
                response.sendRedirect(contextPath + "/menu");
                return;
            } else {
                System.out.println("Credenciales incorrectas");
            }
        } else {
            System.out.println("Usuario o password nulos");
        }
        
        // Si las credenciales son incorrectas, mostrar error
        request.setAttribute("error", "Usuario o contraseña incorrectos");
        request.getRequestDispatcher("/login.jsp").forward(request, response);
    }
    
    /**
     * Genera el hash MD5 de una cadena
     */
    private String hashMD5(String input) {
        try {
            MessageDigest md = MessageDigest.getInstance("MD5");
            byte[] messageDigest = md.digest(input.getBytes());
            StringBuilder hexString = new StringBuilder();
            for (byte b : messageDigest) {
                String hex = Integer.toHexString(0xff & b);
                if (hex.length() == 1) {
                    hexString.append('0');
                }
                hexString.append(hex);
            }
            return hexString.toString().toUpperCase();
        } catch (Exception e) {
            return "";
        }
    }
}

