using System;
using GestionHotelera.Consola.Helpers;
using GestionHotelera.Core.Estructuras;
using GestionHotelera.Core.Modelos;
using GestionHotelera.Core.Nodos;

namespace GestionHotelera.Consola.Menus
{
    // Menu principal con catalogo visual al inicio y navegacion funcional
    public static class MenuPrincipal
    {
        private static ListaReservas reservas;
        private static ListaClientesDoble clientes;
        private static ListaTurnosCircular turnos;
        private static PilaAcciones pila;
        private static ColaSolicitudes solicitudes;
        private static ArbolHotel arbol;
        private static ArbolHabitacionesABB abb;
        private static GrafoHotel grafo;

        public static string UsuarioActual;
        public static string RolActual;

        public static void Inicializar(
            ListaReservas r, ListaClientesDoble c, ListaTurnosCircular t,
            PilaAcciones p, ColaSolicitudes s, ArbolHotel a,
            ArbolHabitacionesABB ab, GrafoHotel g)
        {
            reservas = r; clientes = c; turnos = t;
            pila = p; solicitudes = s; arbol = a; abb = ab; grafo = g;
        }

        public static void Mostrar()
        {
            while (true)
            {
                Pantalla.Limpiar();
                MostrarEncabezado();
                MostrarMiniCatalogo();
                MostrarOpciones();

                string entrada = Console.ReadLine();

                if (entrada == null) continue;
                entrada = entrada.Trim().ToUpper();

                if (entrada == "0") return;
                if (entrada == "T") { MenuTecnico.Mostrar(arbol, abb, grafo); continue; }

                int opcion;
                if (!int.TryParse(entrada, out opcion)) { Pantalla.Error("Opcion no valida."); Pantalla.Pausa(); continue; }

                switch (opcion)
                {
                    case 1: MenuCatalogo.Mostrar(abb, reservas, pila, UsuarioActual); break;
                    case 2: MenuReservas.Mostrar(reservas, abb, pila, UsuarioActual); break;
                    case 3: MenuClientes.Mostrar(clientes); break;
                    case 4: MenuRutas.Mostrar(grafo); break;
                    case 5: MenuReportes.Mostrar(reservas, abb, solicitudes); break;
                    case 6: MenuSolicitudes.Mostrar(solicitudes, pila, turnos); break;
                    case 7: CambiarUsuario(); break;
                    default: Pantalla.Error("Opcion no valida."); Pantalla.Pausa(); break;
                }
            }
        }

        private static void MostrarEncabezado()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  ╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("  ║           SISTEMA DE GESTIÓN HOTELERA                ║");
            Console.WriteLine("  ╚═══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Usuario: " + UsuarioActual + "  |  Rol: " + RolActual +
                "  |  " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void MostrarMiniCatalogo()
        {
            Pantalla.Seccion("Estado de habitaciones");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  ");
            Console.ResetColor();

            int disp = abb.ContarDisponibles();
            int ocup = abb.ContarOcupadas();
            int res = abb.ContarReservadas();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" ✓ Disponibles: " + disp + "  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" ✗ Ocupadas: " + ocup + "  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" ◆ Reservadas: " + res);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void MostrarOpciones()
        {
            Pantalla.Seccion("Menu principal");
            Console.WriteLine("  1. Catalogo de habitaciones      (ABB - vista interactiva)");
            Console.WriteLine("  2. Reservas y check-in/out        (Lista simple)");
            Console.WriteLine("  3. Clientes frecuentes            (Lista doble)");
            Console.WriteLine("  4. Rutas internas del hotel       (Grafo - guia de huesped)");
            Console.WriteLine("  5. Reportes y estadisticas");
            Console.WriteLine("  6. Solicitudes y notas internas   (Cola + Pila)");
            Console.WriteLine("  7. Cambiar usuario");
            Console.WriteLine("  0. Salir");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  T. Vista tecnica (estructuras de datos - sustentacion)");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("  Opcion: ");
        }

        private static void CambiarUsuario()
        {
            MenuLogin.Mostrar(out UsuarioActual, out RolActual);
        }
    }
}
