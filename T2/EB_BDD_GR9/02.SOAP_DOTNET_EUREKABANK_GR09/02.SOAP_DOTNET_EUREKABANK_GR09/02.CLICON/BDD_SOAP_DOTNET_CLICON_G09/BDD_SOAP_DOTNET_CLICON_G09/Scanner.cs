using System;

namespace BDD_SOAP_DOTNET_CLICON_G09
{
    public class Scanner
    {
        public int ReadInt()
        {
            int valor;
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Por favor ingrese un número entero válido:");
            }
            return valor;
        }

        public double ReadDouble()
        {
            double valor;
            while (!double.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Por favor ingrese un número decimal válido:");
            }
            return valor;
        }
    }
}



