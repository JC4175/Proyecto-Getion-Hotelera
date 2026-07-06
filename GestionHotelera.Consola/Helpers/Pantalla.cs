using System;

namespace GestionHotelera.Consola.Helpers
{
    // Helpers visuales: titulos con bordes, colores, tablas
    public static class Pantalla
    {
        public static void Limpiar()
        {
            try { Console.Clear(); } catch { }
        }

        public static void Pausa()
        {
            Console.WriteLine();
            ColorEscribir("  Presione ENTER para continuar...", ConsoleColor.DarkGray);
            Console.ReadLine();
        }

        public static void Titulo(string texto)
        {
            string linea = new string('═', texto.Length + 4);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ╔" + linea + "╗");
            Console.WriteLine("  ║  " + texto + "  ║");
            Console.WriteLine("  ╚" + linea + "╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void Seccion(string texto)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  ─── " + texto + " ───");
            Console.ResetColor();
        }

        public static void Ok(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ✓ " + texto);
            Console.ResetColor();
        }

        public static void Error(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ✗ " + texto);
            Console.ResetColor();
        }

        public static void Info(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ► " + texto);
            Console.ResetColor();
        }

        public static void ColorEscribir(string texto, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(texto);
            Console.ResetColor();
        }

        public static void ColorEscribirLinea(string texto, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(texto);
            Console.ResetColor();
        }

        public static void LineaDivisora()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + new string('─', 55));
            Console.ResetColor();
        }

        public static void EncabezadoCatalogo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + "Nro".PadRight(8) + "| " + "Tipo".PadRight(14) +
                "| " + "Precio/noche".PadRight(14) + "| Estado");
            Console.WriteLine("  " + new string('─', 55));
            Console.ResetColor();
        }
    }
}
