using System;
using System.Globalization;
using System.IO;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    internal partial class Program
    {
        static string LeerTexto(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje + ": ");
                string texto = Console.ReadLine();

                if (texto != null)
                {
                    texto = NormalizarEspacios(texto);
                }

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    return texto;
                }

                Error("No puede estar vacio.");
            }
        }

        static string NormalizarEspacios(string texto)
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

        static string LeerCodigo(string mensaje)
        {
            while (true)
            {
                string codigo = LeerTexto(mensaje).ToUpper();

                if (codigo.Length >= 2 && codigo.Length <= 12 && SoloLetrasNumeros(codigo, false))
                {
                    return codigo;
                }

                Error("Use solo letras y numeros, sin espacios. Longitud permitida: 2 a 12 caracteres.");
            }
        }

        static string LeerClave(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje + ": ");
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

                Error("La clave debe tener de 2 a 12 letras o numeros, sin espacios.");
            }
        }

        static string LeerLetras(string mensaje)
        {
            while (true)
            {
                string texto = LeerTexto(mensaje);

                if (texto.Length <= 50 && SoloLetras(texto))
                {
                    return texto;
                }

                Error("Use solo letras y espacios simples. Minimo 3 letras y maximo 50 caracteres.");
            }
        }

        static bool SoloLetras(string texto)
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

        static string LeerNumeros(string mensaje, int minimoDigitos, int maximoDigitos)
        {
            while (true)
            {
                string texto = LeerTexto(mensaje);

                if (texto.Length >= minimoDigitos && texto.Length <= maximoDigitos && SoloNumeros(texto))
                {
                    return texto;
                }

                Error("Ingrese solo numeros. Longitud permitida: " + minimoDigitos + " a " +
                    maximoDigitos + " digitos.");
            }
        }

        static string LeerDni(string mensaje)
        {
            while (true)
            {
                string dni = LeerNumeros(mensaje, 8, 8);

                if (dni.Length == 8)
                {
                    return dni;
                }

                Error("El DNI debe tener exactamente 8 digitos.");
            }
        }

        static string LeerLetrasNumeros(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                string texto = LeerTexto(mensaje);

                if (texto.Length >= minimo && texto.Length <= maximo && SoloLetrasNumeros(texto, true))
                {
                    return texto;
                }

                Error("Use solo letras, numeros y espacios simples. Longitud permitida: " +
                    minimo + " a " + maximo + " caracteres.");
            }
        }

        static bool SoloNumeros(string texto)
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

        static bool SoloLetrasNumeros(string texto, bool permiteEspacios)
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

        static int LeerOpcion(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                Console.Write(mensaje + ": ");
                string texto = Console.ReadLine();
                int numero;

                if (int.TryParse(texto, out numero) && numero >= minimo && numero <= maximo)
                {
                    return numero;
                }

                Error("Ingrese un numero entre " + minimo + " y " + maximo + ".");
            }
        }

        static int LeerEntero(string mensaje, int minimo)
        {
            while (true)
            {
                Console.Write(mensaje + ": ");
                string texto = Console.ReadLine();
                int numero;

                if (int.TryParse(texto, out numero) && numero >= minimo)
                {
                    return numero;
                }

                Error("Ingrese un entero mayor o igual a " + minimo + ".");
            }
        }

        static int LeerEntero(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                Console.Write(mensaje + ": ");
                string texto = Console.ReadLine();
                int numero;

                if (int.TryParse(texto, out numero) && numero >= minimo && numero <= maximo)
                {
                    return numero;
                }

                Error("Ingrese un entero entre " + minimo + " y " + maximo + ".");
            }
        }

        static double LeerDecimal(string mensaje, double minimo)
        {
            while (true)
            {
                Console.Write(mensaje + ": ");
                string texto = Console.ReadLine();
                double numero;

                if (double.TryParse(texto, out numero) && numero >= minimo)
                {
                    return numero;
                }

                Error("Ingrese un numero mayor o igual a " + minimo + ".");
            }
        }

        static DateTime LeerFecha(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje + ": ");
                string texto = Console.ReadLine();
                DateTime fecha;

                if (DateTime.TryParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out fecha))
                {
                    return fecha.Date;
                }

                Error("Ingrese una fecha valida con formato dd/mm/aaaa.");
            }
        }

        static DateTime LeerFechaHora(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje + ": ");
                string texto = Console.ReadLine();
                DateTime fecha;
                string[] formatos = { "dd/MM/yyyy HH:mm", "dd/MM/yyyy H:mm" };

                if (DateTime.TryParseExact(texto, formatos, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out fecha))
                {
                    return fecha;
                }

                Error("Ingrese una fecha y hora valida con formato dd/mm/aaaa hh:mm.");
            }
        }

        static void Titulo(string texto)
        {
            Console.WriteLine("====================================");
            Console.WriteLine(texto);
            Console.WriteLine("====================================");
        }

        static void Mensaje(string texto)
        {
            Console.WriteLine("OK: " + texto);
        }

        static void Error(string texto)
        {
            Console.WriteLine("ERROR: " + texto);
        }

        static void Pausa()
        {
            Console.WriteLine();
            Console.Write("Presione ENTER...");
            Console.ReadLine();
        }

        static void Limpiar()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
            }
        }
    }
}
