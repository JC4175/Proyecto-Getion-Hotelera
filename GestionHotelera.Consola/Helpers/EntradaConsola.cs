using System;
using System.Globalization;

namespace GestionHotelera.Consola.Helpers
{
    // Helpers de entrada de consola — logica intacta, mismos metodos
    public static class EntradaConsola
    {
        public static string LeerTexto(string mensaje)
        {
            while (true)
            {
                Console.Write("  " + mensaje + ": ");
                string texto = Console.ReadLine();

                if (texto != null)
                {
                    texto = NormalizarEspacios(texto);
                }

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    return texto;
                }

                Pantalla.Error("No puede estar vacio.");
            }
        }

        public static string NormalizarEspacios(string texto)
        {
            if (texto == null)
            {
                return "";
            }

            texto = texto.Trim();
            string resultado = "";
            bool espacioAnterior = false;

            for (int i = 0; i < texto.Length; i++)
            {
                if (char.IsWhiteSpace(texto[i]))
                {
                    if (!espacioAnterior)
                    {
                        resultado = resultado + " ";
                        espacioAnterior = true;
                    }
                }
                else
                {
                    resultado = resultado + texto[i];
                    espacioAnterior = false;
                }
            }

            return resultado;
        }

        public static string LeerCodigo(string mensaje)
        {
            while (true)
            {
                string codigo = LeerTexto(mensaje).ToUpper();

                if (codigo.Length >= 2 && codigo.Length <= 12 && SoloLetrasNumeros(codigo, false))
                {
                    return codigo;
                }

                Pantalla.Error("Use solo letras y numeros, sin espacios. Longitud: 2 a 12 caracteres.");
            }
        }

        public static string LeerClave(string mensaje)
        {
            while (true)
            {
                Console.Write("  " + mensaje + ": ");
                string clave = "";

                if (Console.IsInputRedirected)
                {
                    clave = Console.ReadLine();
                }
                else
                {
                    ConsoleKeyInfo tecla;

                    while (true)
                    {
                        tecla = Console.ReadKey(true);

                        if (tecla.Key == ConsoleKey.Enter)
                        {
                            Console.WriteLine();
                            break;
                        }

                        if (tecla.Key == ConsoleKey.Backspace)
                        {
                            if (clave.Length > 0)
                            {
                                clave = clave.Substring(0, clave.Length - 1);
                                Console.Write("\b \b");
                            }
                        }
                        else if (!char.IsControl(tecla.KeyChar))
                        {
                            clave = clave + tecla.KeyChar;
                            Console.Write("*");
                        }
                    }
                }

                if (clave != null)
                {
                    clave = clave.Trim();
                }

                if (!string.IsNullOrWhiteSpace(clave) &&
                    clave.Length >= 2 && clave.Length <= 12 && SoloLetrasNumeros(clave, false))
                {
                    return clave;
                }

                Pantalla.Error("La clave debe tener de 2 a 12 letras o numeros, sin espacios.");
            }
        }

        public static string LeerLetras(string mensaje)
        {
            while (true)
            {
                string texto = LeerTexto(mensaje);

                if (texto.Length <= 50 && SoloLetras(texto))
                {
                    return texto;
                }

                Pantalla.Error("Use solo letras y espacios simples. Minimo 3 letras y maximo 50 caracteres.");
            }
        }

        public static bool SoloLetras(string texto)
        {
            int contador = 0;

            for (int i = 0; i < texto.Length; i++)
            {
                if (char.IsLetter(texto[i]))
                {
                    contador++;
                }
                else if (texto[i] != ' ')
                {
                    return false;
                }
            }

            return contador >= 3;
        }

        public static string LeerNumeros(string mensaje, int minimoDigitos, int maximoDigitos)
        {
            while (true)
            {
                string texto = LeerTexto(mensaje);

                if (texto.Length >= minimoDigitos && texto.Length <= maximoDigitos && SoloNumeros(texto))
                {
                    return texto;
                }

                Pantalla.Error("Ingrese solo numeros. Longitud permitida: " + minimoDigitos + " a " +
                    maximoDigitos + " digitos.");
            }
        }

        public static string LeerDni(string mensaje)
        {
            while (true)
            {
                string dni = LeerNumeros(mensaje, 8, 8);

                if (dni.Length == 8)
                {
                    return dni;
                }

                Pantalla.Error("El DNI debe tener exactamente 8 digitos.");
            }
        }

        public static string LeerLetrasNumeros(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                string texto = LeerTexto(mensaje);

                if (texto.Length >= minimo && texto.Length <= maximo && SoloLetrasNumeros(texto, true))
                {
                    return texto;
                }

                Pantalla.Error("Use solo letras, numeros y espacios. Longitud: " + minimo + " a " + maximo + " caracteres.");
            }
        }

        public static bool SoloNumeros(string texto)
        {
            for (int i = 0; i < texto.Length; i++)
            {
                if (!char.IsDigit(texto[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool SoloLetrasNumeros(string texto, bool permiteEspacios)
        {
            int contador = 0;

            for (int i = 0; i < texto.Length; i++)
            {
                if (char.IsLetterOrDigit(texto[i]))
                {
                    contador++;
                }
                else if (permiteEspacios && texto[i] == ' ')
                {
                }
                else
                {
                    return false;
                }
            }

            return contador >= 2;
        }

        public static int LeerOpcion(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                Console.Write("  " + mensaje + ": ");
                string texto = Console.ReadLine();
                int numero;

                if (int.TryParse(texto, out numero) && numero >= minimo && numero <= maximo)
                {
                    return numero;
                }

                Pantalla.Error("Ingrese un numero entre " + minimo + " y " + maximo + ".");
            }
        }

        public static int LeerEntero(string mensaje, int minimo)
        {
            while (true)
            {
                Console.Write("  " + mensaje + ": ");
                string texto = Console.ReadLine();
                int numero;

                if (int.TryParse(texto, out numero) && numero >= minimo)
                {
                    return numero;
                }

                Pantalla.Error("Ingrese un entero mayor o igual a " + minimo + ".");
            }
        }

        public static int LeerEntero(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                Console.Write("  " + mensaje + ": ");
                string texto = Console.ReadLine();
                int numero;

                if (int.TryParse(texto, out numero) && numero >= minimo && numero <= maximo)
                {
                    return numero;
                }

                Pantalla.Error("Ingrese un entero entre " + minimo + " y " + maximo + ".");
            }
        }

        public static double LeerDecimal(string mensaje, double minimo)
        {
            while (true)
            {
                Console.Write("  " + mensaje + ": ");
                string texto = Console.ReadLine();
                double numero;

                if (double.TryParse(texto, out numero) && numero >= minimo)
                {
                    return numero;
                }

                Pantalla.Error("Ingrese un numero mayor o igual a " + minimo + ".");
            }
        }

        public static DateTime LeerFecha(string mensaje)
        {
            while (true)
            {
                Console.Write("  " + mensaje + ": ");
                string texto = Console.ReadLine();
                DateTime fecha;

                if (DateTime.TryParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out fecha))
                {
                    return fecha.Date;
                }

                Pantalla.Error("Ingrese una fecha valida con formato dd/mm/aaaa.");
            }
        }

        public static DateTime LeerFechaHora(string mensaje)
        {
            while (true)
            {
                Console.Write("  " + mensaje + ": ");
                string texto = Console.ReadLine();
                DateTime fecha;
                string[] formatos = { "dd/MM/yyyy HH:mm", "dd/MM/yyyy H:mm" };

                if (DateTime.TryParseExact(texto, formatos, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out fecha))
                {
                    return fecha;
                }

                Pantalla.Error("Ingrese una fecha y hora valida con formato dd/mm/aaaa hh:mm.");
            }
        }
    }
}
