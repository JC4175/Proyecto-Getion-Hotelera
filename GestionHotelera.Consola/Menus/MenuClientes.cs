using System;
using GestionHotelera.Consola.Helpers;
using GestionHotelera.Core.Estructuras;
using GestionHotelera.Core.Modelos;
using GestionHotelera.Core.Nodos;

namespace GestionHotelera.Consola.Menus
{
    public static class MenuClientes
    {
        public static void Mostrar(ListaClientesDoble clientes)
        {
            while (true)
            {
                Pantalla.Limpiar();
                Pantalla.Titulo("CLIENTES FRECUENTES");
                Console.WriteLine("  1. Registrar cliente frecuente");
                Console.WriteLine("  2. Mostrar clientes (primero al ultimo)");
                Console.WriteLine("  3. Mostrar clientes (ultimo al primero)");
                Console.WriteLine("  4. Buscar cliente por codigo");
                Console.WriteLine("  0. Volver");
                Console.WriteLine();

                int opcion = EntradaConsola.LeerOpcion("Opcion", 0, 4);

                switch (opcion)
                {
                    case 1: RegistrarCliente(clientes); break;
                    case 2: MostrarAdelante(clientes); break;
                    case 3: MostrarAtras(clientes); break;
                    case 4: BuscarCliente(clientes); break;
                    case 0: return;
                }
            }
        }

        private static void RegistrarCliente(ListaClientesDoble clientes)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("REGISTRAR CLIENTE FRECUENTE");

            Cliente cliente = new Cliente();

            while (true)
            {
                cliente.Codigo = EntradaConsola.LeerCodigo("Codigo de cliente");
                if (clientes.Buscar(cliente.Codigo) == null) break;
                Pantalla.Error("Ese codigo ya existe.");
            }

            cliente.Nombre = EntradaConsola.LeerLetras("Nombre completo");
            cliente.Puntos = EntradaConsola.LeerEntero("Puntos iniciales", 0);

            clientes.Insertar(cliente);
            Pantalla.Ok("Cliente registrado: " + cliente.Nombre + " (" + cliente.Puntos + " puntos)");
            Pantalla.Pausa();
        }

        private static void MostrarAdelante(ListaClientesDoble clientes)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("CLIENTES FRECUENTES");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + "Codigo".PadRight(12) + "| " + "Nombre".PadRight(25) + "| Puntos");
            Console.WriteLine("  " + new string('─', 50));
            Console.ResetColor();

            NodoCliente actual = clientes.Cabecera;
            if (actual == null) { Pantalla.Info("No hay clientes registrados."); Pantalla.Pausa(); return; }

            while (actual != null)
            {
                string puntos = actual.Dato.Puntos.ToString();
                ConsoleColor c = actual.Dato.Puntos >= 100 ? ConsoleColor.Green :
                                 actual.Dato.Puntos >= 50 ? ConsoleColor.Yellow : ConsoleColor.White;
                Console.Write("  " + actual.Dato.Codigo.PadRight(12) + "| " +
                    actual.Dato.Nombre.PadRight(25) + "| ");
                Pantalla.ColorEscribirLinea(puntos, c);
                actual = actual.Siguiente;
            }

            Pantalla.Pausa();
        }

        private static void MostrarAtras(ListaClientesDoble clientes)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("CLIENTES FRECUENTES (lista doble — fin a inicio)");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + "Codigo".PadRight(12) + "| " + "Nombre".PadRight(25) + "| Puntos");
            Console.WriteLine("  " + new string('─', 50));
            Console.ResetColor();

            NodoCliente actual = clientes.Ultimo;
            if (actual == null) { Pantalla.Info("No hay clientes registrados."); Pantalla.Pausa(); return; }

            while (actual != null)
            {
                Console.WriteLine("  " + actual.Dato.Codigo.PadRight(12) + "| " +
                    actual.Dato.Nombre.PadRight(25) + "| " + actual.Dato.Puntos);
                actual = actual.Anterior;
            }

            Pantalla.Pausa();
        }

        private static void BuscarCliente(ListaClientesDoble clientes)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("BUSCAR CLIENTE FRECUENTE");

            string codigo = EntradaConsola.LeerCodigo("Codigo de cliente");
            NodoCliente nodo = clientes.Buscar(codigo);

            if (nodo == null)
            {
                Pantalla.Error("Cliente no encontrado.");
            }
            else
            {
                Console.WriteLine();
                Pantalla.LineaDivisora();
                Console.WriteLine("  Codigo: " + nodo.Dato.Codigo);
                Console.WriteLine("  Nombre: " + nodo.Dato.Nombre);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  Puntos: " + nodo.Dato.Puntos);
                Console.ResetColor();

                string beneficio = nodo.Dato.Puntos >= 200 ? "Cliente VIP — 20% descuento" :
                                   nodo.Dato.Puntos >= 100 ? "Cliente Gold — 10% descuento" :
                                   nodo.Dato.Puntos >= 50 ? "Cliente Silver — 5% descuento" :
                                   "Sin beneficio aun (min. 50 puntos)";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  Nivel:  " + beneficio);
                Console.ResetColor();
                Pantalla.LineaDivisora();
            }

            Pantalla.Pausa();
        }
    }
}
