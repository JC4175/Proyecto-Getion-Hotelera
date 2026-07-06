using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    internal partial class Program
    {
        static void Login()
        {
            string[] usuarios = { "admin", "recepcion" };
            string[] claves = { "1234", "1234" };
            string[] roles = { "ADMIN", "RECEPCIONISTA" };

            while (true)
            {
                Limpiar();
                Titulo("INICIO DE SESION");
                Console.WriteLine("Usuarios disponibles:");
                Console.WriteLine("admin");
                Console.WriteLine("recepcion");
                Console.WriteLine();

                string usuario = LeerCodigo("Usuario").ToLower();
                string clave = LeerClave("Clave");

                for (int i = 0; i < usuarios.Length; i++)
                {
                    if (usuario == usuarios[i] && clave == claves[i])
                    {
                        usuarioActual = usuarios[i];
                        rolActual = roles[i];
                        pila.Apilar("Inicio de sesion: " + usuarioActual + " (" + rolActual + ")");
                        return;
                    }
                }

                Error("Usuario o clave incorrectos.");
                Pausa();
            }
        }

        static void CambiarUsuario()
        {
            pila.Apilar("Cierre de sesion: " + usuarioActual + " (" + rolActual + ")");
            usuarioActual = "";
            rolActual = "";
            Login();
        }

        static bool ValidarPermisoAdmin()
        {
            if (rolActual == "ADMIN")
            {
                return true;
            }

            Error("Solo el usuario ADMIN puede realizar esta accion.");
            Pausa();
            return false;
        }
    }
}
