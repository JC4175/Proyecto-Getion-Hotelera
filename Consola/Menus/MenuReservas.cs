using System;
using GestionHotelera.Helpers;
using GestionHotelera.Estructuras;
using GestionHotelera.Modelos;
using GestionHotelera.Nodos;

namespace GestionHotelera.Menus
{
    public static class MenuReservas
    {
        private static ListaReservas reservas;
        private static ArbolHabitacionesABB abb;
        private static PilaAcciones pila;
        private static string usuario;

        public static void Mostrar(ListaReservas r, ArbolHabitacionesABB a, PilaAcciones p, string u)
        {
            reservas = r; abb = a; pila = p; usuario = u;

            while (true)
            {
                Pantalla.Limpiar();
                Pantalla.Titulo("RESERVAS Y HOSPEDAJE");
                Console.WriteLine("  1. Registrar reserva");
                Console.WriteLine("  2. Mostrar todas las reservas");
                Console.WriteLine("  3. Buscar reserva por codigo");
                Console.WriteLine("  4. Realizar check-in");
                Console.WriteLine("  5. Realizar check-out");
                Console.WriteLine("  6. Cancelar o eliminar reserva");
                Console.WriteLine("  0. Volver");
                Console.WriteLine();

                int opcion = EntradaConsola.LeerOpcion("Opcion", 0, 6);

                switch (opcion)
                {
                    case 1: RegistrarReserva(); break;
                    case 2: MostrarReservas(); break;
                    case 3: BuscarReserva(); break;
                    case 4: RealizarCheckIn(); break;
                    case 5: RealizarCheckOut(); break;
                    case 6: EliminarReserva(); break;
                    case 0: return;
                }
            }
        }

        private static void RegistrarReserva()
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("REGISTRAR RESERVA");

            Reserva reserva = new Reserva();
            reserva.Codigo = LeerCodigoNuevo();
            reserva.Cliente = EntradaConsola.LeerLetras("Nombre del huesped");
            reserva.Documento = EntradaConsola.LeerDni("DNI del huesped (8 digitos)");
            reserva.FechaHoraEntradaProgramada = EntradaConsola.LeerFechaHora("Fecha y hora de entrada (dd/mm/aaaa hh:mm)");
            reserva.FechaEntrada = reserva.FechaHoraEntradaProgramada.Date;
            reserva.Noches = EntradaConsola.LeerEntero("Cantidad de noches", 1, 30);
            reserva.FechaHoraSalidaProgramada = reserva.FechaHoraEntradaProgramada.AddDays(reserva.Noches);
            reserva.Estado = "Reservada";
            reserva.UsuarioRegistro = usuario;

            Console.WriteLine();
            Pantalla.Seccion("Catalogo de habitaciones disponibles");
            Pantalla.EncabezadoCatalogo();
            abb.MostrarCatalogoVisual();
            Console.WriteLine();

            NodoABB nodoHab = LeerHabitacionDisponible();
            reserva.Habitacion = nodoHab.Dato.Numero;
            reserva.TipoHabitacion = nodoHab.Dato.Tipo;
            reserva.PrecioNoche = nodoHab.Dato.Precio;
            reserva.TotalPagar = reserva.PrecioNoche * reserva.Noches;
            nodoHab.Dato.Disponible = false;
            nodoHab.Dato.Estado = "Reservada";

            reservas.Insertar(reserva);
            pila.Apilar("Reserva registrada: " + reserva.Codigo);
            Pantalla.Ok("Reserva registrada. Total: S/ " + reserva.TotalPagar.ToString("0.00"));
            Pantalla.Pausa();
        }

        private static void MostrarReservas()
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("LISTA DE RESERVAS");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + "Codigo".PadRight(10) + "| " + "Huesped".PadRight(18) +
                "| " + "Hab".PadRight(5) + "| " + "Entrada".PadRight(17) +
                "| " + "Salida".PadRight(17) + "| Estado");
            Console.WriteLine("  " + new string('─', 80));
            Console.ResetColor();

            NodoReserva actual = reservas.Cabecera;
            if (actual == null)
            {
                Pantalla.Info("No hay reservas registradas.");
                Pantalla.Pausa();
                return;
            }

            while (actual != null)
            {
                string estado = actual.Dato.Estado ?? "Reservada";
                ConsoleColor c = estado == "Ocupada" ? ConsoleColor.Red :
                                 estado == "Finalizada" ? ConsoleColor.DarkGray :
                                 estado == "Reservada" ? ConsoleColor.Yellow : ConsoleColor.Green;

                Console.Write("  " + actual.Dato.Codigo.PadRight(10) + "| " +
                    actual.Dato.Cliente.PadRight(18) + "| " +
                    actual.Dato.Habitacion.ToString().PadRight(5) + "| " +
                    actual.Dato.FechaHoraEntradaProgramada.ToString("dd/MM/yyyy HH:mm").PadRight(17) + "| " +
                    actual.Dato.FechaHoraSalidaProgramada.ToString("dd/MM/yyyy HH:mm").PadRight(17) + "| ");
                Pantalla.ColorEscribirLinea(estado, c);

                actual = actual.Siguiente;
            }

            Pantalla.Pausa();
        }

        private static void BuscarReserva()
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("BUSCAR RESERVA");

            string codigo = EntradaConsola.LeerCodigo("Codigo de reserva");
            NodoReserva nodo = reservas.Buscar(codigo);

            if (nodo == null)
            {
                Pantalla.Error("Reserva no encontrada.");
            }
            else
            {
                MostrarFichaReserva(nodo.Dato);
            }
            Pantalla.Pausa();
        }

        private static void RealizarCheckIn()
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("CHECK-IN DE HUESPED");

            string codigo = EntradaConsola.LeerCodigo("Codigo de reserva");
            NodoReserva nodo = reservas.Buscar(codigo);

            if (nodo == null) { Pantalla.Error("Reserva no encontrada."); Pantalla.Pausa(); return; }
            if (nodo.Dato.Estado != "Reservada") { Pantalla.Error("Solo reservas en estado 'Reservada' pueden hacer check-in."); MostrarFichaReserva(nodo.Dato); Pantalla.Pausa(); return; }

            nodo.Dato.FechaHoraCheckIn = EntradaConsola.LeerFechaHora("Fecha y hora real de entrada (dd/mm/aaaa hh:mm)");
            nodo.Dato.Estado = "Ocupada";
            nodo.Dato.UsuarioCheckIn = usuario;

            NodoABB hab = abb.Buscar(nodo.Dato.Habitacion);
            if (hab != null) { hab.Dato.Estado = "Ocupada"; hab.Dato.Disponible = false; }

            pila.Apilar("Check-in: " + codigo);
            Pantalla.Ok("Check-in realizado. Habitacion marcada como ocupada.");
            Pantalla.Pausa();
        }

        private static void RealizarCheckOut()
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("CHECK-OUT DE HUESPED");

            string codigo = EntradaConsola.LeerCodigo("Codigo de reserva");
            NodoReserva nodo = reservas.Buscar(codigo);

            if (nodo == null) { Pantalla.Error("Reserva no encontrada."); Pantalla.Pausa(); return; }
            if (nodo.Dato.Estado != "Ocupada") { Pantalla.Error("Solo reservas en estado 'Ocupada' pueden hacer check-out."); MostrarFichaReserva(nodo.Dato); Pantalla.Pausa(); return; }

            nodo.Dato.FechaHoraCheckOut = EntradaConsola.LeerFechaHora("Fecha y hora real de salida (dd/mm/aaaa hh:mm)");
            nodo.Dato.Estado = "Finalizada";
            nodo.Dato.UsuarioCheckOut = usuario;

            NodoABB hab = abb.Buscar(nodo.Dato.Habitacion);
            if (hab != null) { hab.Dato.Estado = "Disponible"; hab.Dato.Disponible = true; }

            pila.Apilar("Check-out: " + codigo);
            Pantalla.Ok("Check-out realizado. Habitacion liberada.");

            Console.WriteLine();
            Console.WriteLine("  Desea generar: 1. Boleta   2. Factura   0. Omitir");
            int doc = EntradaConsola.LeerOpcion("Opcion", 0, 2);
            if (doc > 0) MostrarDocumento(nodo.Dato, doc == 1 ? "BOLETA" : "FACTURA");
            Pantalla.Pausa();
        }

        private static void EliminarReserva()
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("CANCELAR RESERVA");

            string codigo = EntradaConsola.LeerCodigo("Codigo de reserva");
            NodoReserva reserva = reservas.Buscar(codigo);

            if (reserva != null && reserva.Dato.Estado == "Ocupada")
            {
                Pantalla.Error("No se puede cancelar una reserva ocupada. Primero realice el check-out.");
                Pantalla.Pausa();
                return;
            }

            if (reservas.Eliminar(codigo))
            {
                if (reserva != null)
                {
                    NodoABB hab = abb.Buscar(reserva.Dato.Habitacion);
                    if (hab != null) { hab.Dato.Estado = "Disponible"; hab.Dato.Disponible = true; }
                }
                pila.Apilar("Reserva cancelada: " + codigo);
                Pantalla.Ok("Reserva cancelada. Habitacion disponible nuevamente.");
            }
            else
            {
                Pantalla.Error("Reserva no encontrada.");
            }

            Pantalla.Pausa();
        }

        public static void MostrarFichaReserva(Reserva r)
        {
            Console.WriteLine();
            Pantalla.LineaDivisora();
            Console.WriteLine("  Codigo:    " + r.Codigo);
            Console.WriteLine("  Huesped:   " + r.Cliente);
            Console.WriteLine("  DNI:       " + r.Documento);
            Console.WriteLine("  Hab. " + r.Habitacion + " (" + r.TipoHabitacion + ")");
            Console.WriteLine("  Precio/n:  S/ " + r.PrecioNoche.ToString("0.00"));
            Console.WriteLine("  Entrada:   " + r.FechaHoraEntradaProgramada.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("  Salida:    " + r.FechaHoraSalidaProgramada.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("  Noches:    " + r.Noches);
            Console.Write("  Estado:    ");
            ConsoleColor c = r.Estado == "Ocupada" ? ConsoleColor.Red :
                             r.Estado == "Finalizada" ? ConsoleColor.DarkGray :
                             r.Estado == "Reservada" ? ConsoleColor.Yellow : ConsoleColor.Green;
            Pantalla.ColorEscribirLinea(r.Estado ?? "Reservada", c);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Total:     S/ " + r.TotalPagar.ToString("0.00"));
            Console.ResetColor();
            Pantalla.LineaDivisora();
        }

        private static void MostrarDocumento(Reserva r, string tipo)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  ┌──────────────────────────────────────┐");
            Console.WriteLine("  │        HOTEL GESTION HOTELERA        │");
            Console.WriteLine("  │              " + tipo.PadRight(24) + "│");
            Console.WriteLine("  ├──────────────────────────────────────┤");
            Console.ResetColor();
            Console.WriteLine("  │  Cliente:   " + r.Cliente.PadRight(25) + "│");
            Console.WriteLine("  │  DNI:       " + r.Documento.PadRight(25) + "│");
            Console.WriteLine("  │  Hab. " + r.Habitacion.ToString().PadRight(5) + " " + r.TipoHabitacion.PadRight(20) + "│");
            Console.WriteLine("  │  " + r.Noches + " noches x S/ " + r.PrecioNoche.ToString("0.00").PadRight(22) + "│");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  │  TOTAL:          S/ " + r.TotalPagar.ToString("0.00").PadRight(16) + "│");
            Console.ResetColor();
            Console.WriteLine("  │  Fecha:     " + DateTime.Now.ToString("dd/MM/yyyy HH:mm").PadRight(25) + "│");
            Console.WriteLine("  └──────────────────────────────────────┘");
        }

        private static NodoABB LeerHabitacionDisponible()
        {
            while (true)
            {
                int numero = EntradaConsola.LeerEntero("Numero de habitacion", 1, 999);
                NodoABB nodo = abb.Buscar(numero);

                if (nodo != null && nodo.Dato.Estado == "Disponible")
                {
                    return nodo;
                }

                if (nodo == null) Pantalla.Error("Esa habitacion no existe en el catalogo.");
                else Pantalla.Error("Esa habitacion no esta disponible. Elija una en verde.");
            }
        }

        private static string LeerCodigoNuevo()
        {
            while (true)
            {
                string codigo = EntradaConsola.LeerCodigo("Codigo de reserva");
                if (reservas.Buscar(codigo) == null) return codigo;
                Pantalla.Error("Ese codigo ya existe.");
            }
        }
    }
}
