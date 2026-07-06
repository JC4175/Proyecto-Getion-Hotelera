using System;
using GestionHotelera.Consola.Helpers;

namespace GestionHotelera.Consola.Menus
{
    public static class MenuLogin
    {
        private static string[] usuarios = { "admin", "recepcion" };
        private static string[] claves = { "admin123", "hotel2025" };
        private static string[] roles = { "Administrador", "Recepcionista" };

        public static void Mostrar(out string usuarioOut, out string rolOut)
        {
            usuarioOut = "";
            rolOut = "";

            while (true)
            {
                Pantalla.Limpiar();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("  ║           SISTEMA DE GESTIÓN HOTELERA                ║");
                Console.WriteLine("  ║                  Iniciar sesion                      ║");
                Console.WriteLine("  ╚═══════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();

                string usuario = EntradaConsola.LeerCodigo("Usuario");
                string clave = EntradaConsola.LeerClave("Clave");

                int indice = BuscarUsuario(usuario, clave);

                if (indice != -1)
                {
                    usuarioOut = usuarios[indice];
                    rolOut = roles[indice];
                    Pantalla.Ok("Bienvenido, " + usuarioOut + " (" + rolOut + ")");
                    System.Threading.Thread.Sleep(800);
                    return;
                }

                Pantalla.Error("Usuario o clave incorrectos. Intente nuevamente.");
                Pantalla.Pausa();
            }
        }

        private static int BuscarUsuario(string usuario, string clave)
        {
            for (int i = 0; i < usuarios.Length; i++)
            {
                if (usuarios[i].ToUpper() == usuario.ToUpper() && claves[i] == clave)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
