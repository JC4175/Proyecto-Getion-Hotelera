using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    internal partial class Program
    {
        static ListaReservas reservas = new ListaReservas();
        static ListaClientesDoble clientes = new ListaClientesDoble();
        static ListaTurnosCircular turnos = new ListaTurnosCircular();
        static PilaAcciones pila = new PilaAcciones();
        static ColaSolicitudes solicitudes = new ColaSolicitudes();
        static ArbolHotel arbol = new ArbolHotel();
        static ArbolHabitacionesABB abb = new ArbolHabitacionesABB();
        static GrafoHotel grafo = new GrafoHotel();
        static string usuarioActual = "";
        static string rolActual = "";

        static void Main(string[] args)
        {
            CargarDatosIniciales();
            Login();

            while (true)
            {
                Limpiar();
                Titulo("HOTEL - PROYECTO FINAL");
                Console.WriteLine("Usuario: " + usuarioActual + " | Rol: " + rolActual);
                Console.WriteLine();
                Console.WriteLine("1. Reservas de habitaciones       (Lista simple)");
                Console.WriteLine("2. Clientes frecuentes con puntos (Lista doble)");
                Console.WriteLine("3. Turnos del personal            (Lista circular)");
                Console.WriteLine("4. Historial de movimientos       (Pila)");
                Console.WriteLine("5. Solicitudes de huespedes       (Cola)");
                Console.WriteLine("6. Organigrama del hotel          (Arbol)");
                Console.WriteLine("7. Catalogo de habitaciones       (ABB)");
                Console.WriteLine("8. Rutas internas del hotel       (Grafo)");
                Console.WriteLine("9. Reporte general");
                Console.WriteLine("10. Cerrar sesion / cambiar usuario");
                Console.WriteLine("0. Salir");

                int opcion = LeerOpcion("Opcion", 0, 10);

                switch (opcion)
                {
                    case 1: MenuReservas(); break;
                    case 2: MenuClientes(); break;
                    case 3: MenuTurnos(); break;
                    case 4: MenuPila(); break;
                    case 5: MenuCola(); break;
                    case 6: MenuArbol(); break;
                    case 7: MenuABB(); break;
                    case 8: MenuGrafo(); break;
                    case 9: Reporte(); break;
                    case 10: CambiarUsuario(); break;
                    case 0: return;
                }
            }
        }

        static void CargarDatosIniciales()
        {
            arbol.CargarDatos();

            AgregarHabitacion(205, "Doble", 180);
            AgregarHabitacion(101, "Simple", 120);
            AgregarHabitacion(302, "Matrimonial", 240);
            AgregarHabitacion(110, "Simple", 150);
            AgregarHabitacion(301, "Matrimonial", 220);

            grafo.AgregarArea("Recepcion");
            grafo.AgregarArea("Habitaciones");
            grafo.AgregarArea("Restaurante");
            grafo.AgregarArea("Piscina");
            grafo.AgregarArea("Limpieza");

            grafo.AgregarRuta("Recepcion", "Habitaciones", 40);
            grafo.AgregarRuta("Recepcion", "Restaurante", 25);
            grafo.AgregarRuta("Restaurante", "Piscina", 20);
            grafo.AgregarRuta("Habitaciones", "Limpieza", 15);
            grafo.AgregarRuta("Habitaciones", "Piscina", 35);
            grafo.AgregarRuta("Restaurante", "Limpieza", 45);

            pila.Apilar("Sistema iniciado con datos base");
        }

        static void AgregarHabitacion(int numero, string tipo, double precio)
        {
            Habitacion habitacion = new Habitacion();
            habitacion.Numero = numero;
            habitacion.Tipo = tipo;
            habitacion.Precio = precio;
            habitacion.Disponible = true;
            habitacion.Estado = "Disponible";
            abb.Insertar(habitacion);
        }

    }
}
