using System;
using GestionHotelera.Menus;
using GestionHotelera.Estructuras;
using GestionHotelera.Modelos;

namespace GestionHotelera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inicializar todas las estructuras de datos
            ListaReservas reservas = new ListaReservas();
            ListaClientesDoble clientes = new ListaClientesDoble();
            ListaTurnosCircular turnos = new ListaTurnosCircular();
            PilaAcciones pila = new PilaAcciones();
            ColaSolicitudes solicitudes = new ColaSolicitudes();
            ArbolHotel arbol = new ArbolHotel();
            ArbolHabitacionesABB abb = new ArbolHabitacionesABB();
            GrafoHotel grafo = new GrafoHotel();

            // Cargar datos iniciales
            CargarDatosIniciales(arbol, abb, grafo, pila);

            // Pasar estructuras al menu principal
            MenuPrincipal.Inicializar(reservas, clientes, turnos, pila, solicitudes, arbol, abb, grafo);

            // Login inicial
            string usuario, rol;
            MenuLogin.Mostrar(out usuario, out rol);
            MenuPrincipal.UsuarioActual = usuario;
            MenuPrincipal.RolActual = rol;

            // Mostrar menu principal en bucle
            MenuPrincipal.Mostrar();
        }

        static void CargarDatosIniciales(
            ArbolHotel arbol, ArbolHabitacionesABB abb,
            GrafoHotel grafo, PilaAcciones pila)
        {
            // Organigrama del hotel (Arbol binario)
            arbol.CargarDatos();

            // Habitaciones en el ABB
            AgregarHabitacion(abb, 205, "Doble", 180);
            AgregarHabitacion(abb, 101, "Simple", 120);
            AgregarHabitacion(abb, 302, "Matrimonial", 240);
            AgregarHabitacion(abb, 110, "Simple", 150);
            AgregarHabitacion(abb, 301, "Matrimonial", 220);
            AgregarHabitacion(abb, 203, "Doble", 190);

            // Grafo de rutas internas
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

        static void AgregarHabitacion(ArbolHabitacionesABB abb, int numero, string tipo, double precio)
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
