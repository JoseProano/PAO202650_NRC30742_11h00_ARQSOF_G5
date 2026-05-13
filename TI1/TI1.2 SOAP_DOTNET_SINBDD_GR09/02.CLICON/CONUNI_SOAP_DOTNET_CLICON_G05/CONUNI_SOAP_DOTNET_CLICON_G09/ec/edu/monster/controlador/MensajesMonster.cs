using System;
using System.Collections.Generic;

namespace ec.edu.monster.controlador
{
    /// <summary>
    /// Clase para manejar mensajes de error personalizados con temática Monster Inc
    /// </summary>
    public static class MensajesMonster
    {
        private static readonly Random random = new Random();
        
        // Mensajes de error para problemas de conexión SOAP
        private static readonly List<string> mensajesServidorDesconectado = new List<string>
        {
            "¡Algo monstruoso pasó con el servidor! Los monstruos están trabajando para solucionarlo pronto.",
            "¡Ups! Parece que Sully y Mike están tomando un descanso. El servidor volverá pronto.",
            "¡Los monstruos están en mantenimiento! Pronto estarán de vuelta trabajando.",
            "¡El departamento de conversiones está temporalmente cerrado! Los monstruos regresarán pronto.",
            "¡Boo se escapó y desconectó el servidor! Los monstruos la están buscando.",
            "¡Los monstruos están en una reunión importante! Inténtalo más tarde.",
            "¡El sistema de conversiones necesita un poco de energía de risas! Vuelve pronto.",
            "¡Los monstruos están actualizando sus herramientas! Pronto estarán listos."
        };
        
        // Mensajes de error para problemas de red
        private static readonly List<string> mensajesErrorRed = new List<string>
        {
            "¡Los monstruos no pueden encontrar la puerta correcta! Verifica tu conexión.",
            "¡Parece que hay un problema en el sistema de puertas! Los monstruos están investigando.",
            "¡La energía de risas está baja! Los monstruos necesitan más energía para funcionar.",
            "¡Los monstruos están confundidos! No pueden procesar tu solicitud ahora mismo.",
            "¡El departamento de conversiones está sobrecargado! Inténtalo en un momento.",
            "¡Los monstruos están aprendiendo nuevas conversiones! Pronto estarán listos.",
            "¡Hay demasiado tráfico en el sistema de puertas! Espera un momento.",
            "¡Los monstruos están reorganizando sus archivos! Vuelve pronto."
        };
        
        // Mensajes de error para problemas de autenticación
        private static readonly List<string> mensajesErrorAuth = new List<string>
        {
            "¡Credenciales incorrectas! Los monstruos no te reconocen.",
            "¡Acceso denegado! Solo los monstruos autorizados pueden usar este sistema.",
            "¡Las credenciales no coinciden! Los monstruos son muy estrictos con la seguridad.",
            "¡Usuario no autorizado! Los monstruos protegen sus secretos de conversión.",
            "¡Contraseña incorrecta! Los monstruos necesitan las credenciales correctas.",
            "¡Acceso restringido! Solo los empleados de Monster Inc pueden usar este sistema."
        };
        
        // Mensajes de error para problemas de datos
        private static readonly List<string> mensajesErrorDatos = new List<string>
        {
            "¡Los monstruos no pueden procesar ese valor! Intenta con un número válido.",
            "¡Ese valor es demasiado monstruoso! Los monstruos necesitan números más pequeños.",
            "¡Los monstruos están confundidos con ese número! Intenta con algo más simple.",
            "¡Ese valor no existe en el mundo de los monstruos! Usa números reales.",
            "¡Los monstruos no pueden convertir valores negativos! Usa números positivos.",
            "¡Ese número es demasiado grande para los monstruos! Intenta con algo más pequeño."
        };
        
        /// <summary>
        /// Obtiene un mensaje de error personalizado basado en el tipo de excepción
        /// </summary>
        public static string ObtenerMensajeError(Exception ex)
        {
            string mensajeExcepcion = ex.Message.ToLower();
            
            // Detectar tipo de error basado en el mensaje
            if (mensajeExcepcion.Contains("no había ningún extremo") || 
                mensajeExcepcion.Contains("endpoint") ||
                mensajeExcepcion.Contains("connection") ||
                mensajeExcepcion.Contains("servicio no disponible"))
            {
                return ObtenerMensajeAleatorio(mensajesServidorDesconectado);
            }
            else if (mensajeExcepcion.Contains("timeout") ||
                     mensajeExcepcion.Contains("network") ||
                     mensajeExcepcion.Contains("socket"))
            {
                return ObtenerMensajeAleatorio(mensajesErrorRed);
            }
            else if (mensajeExcepcion.Contains("unauthorized") ||
                     mensajeExcepcion.Contains("forbidden") ||
                     mensajeExcepcion.Contains("access denied"))
            {
                return ObtenerMensajeAleatorio(mensajesErrorAuth);
            }
            else if (mensajeExcepcion.Contains("invalid") ||
                     mensajeExcepcion.Contains("format") ||
                     mensajeExcepcion.Contains("number"))
            {
                return ObtenerMensajeAleatorio(mensajesErrorDatos);
            }
            else
            {
                // Mensaje genérico para errores no categorizados
                return "¡Algo inesperado pasó en el mundo de los monstruos! Inténtalo de nuevo.";
            }
        }
        
        /// <summary>
        /// Obtiene un mensaje aleatorio de la lista especificada
        /// </summary>
        private static string ObtenerMensajeAleatorio(List<string> mensajes)
        {
            int indice = random.Next(mensajes.Count);
            return mensajes[indice];
        }
        
        /// <summary>
        /// Obtiene un mensaje de éxito personalizado
        /// </summary>
        public static string ObtenerMensajeExito()
        {
            var mensajesExito = new List<string>
            {
                "¡Conversión exitosa! Los monstruos han trabajado muy bien.",
                "¡Perfecto! Los monstruos han completado la conversión.",
                "¡Excelente! El sistema de monstruos funcionó perfectamente.",
                "¡Genial! Los monstruos han procesado tu solicitud correctamente.",
                "¡Fantástico! La conversión fue un éxito total.",
                "¡Increíble! Los monstruos han hecho su trabajo perfectamente."
            };
            
            return ObtenerMensajeAleatorio(mensajesExito);
        }
        
        /// <summary>
        /// Obtiene un mensaje de bienvenida personalizado
        /// </summary>
        public static string ObtenerMensajeBienvenida()
        {
            var mensajesBienvenida = new List<string>
            {
                "¡Bienvenido al sistema de conversiones Monster Inc!",
                "¡Los monstruos están listos para ayudarte con tus conversiones!",
                "¡Sully y Mike te dan la bienvenida al sistema de conversiones!",
                "¡Prepárate para convertir con el poder de los monstruos!",
                "¡El departamento de conversiones Monster Inc está a tu servicio!"
            };
            
            return ObtenerMensajeAleatorio(mensajesBienvenida);
        }
    }
}

