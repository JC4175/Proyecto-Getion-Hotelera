using System;
using GestionHotelera.Consola.Helpers;
using GestionHotelera.Core.Estructuras;
using GestionHotelera.Core.Modelos;
using GestionHotelera.Core.Nodos;

namespace GestionHotelera.Consola.Menus
{
    // Catalogo interactivo de habitaciones: seleccionas una y ves opciones segun estado
    public static class MenuCatalogo
    {
        private static ListaReservas reservas;
        private static ArbolHabitacionesABB abb;
        private static PilaAcciones pila;
        private static string usuario;

        public static void Mostrar(ArbolHabitacionesABB a, ListaReservas r, PilaAcciones p, string u)
        {
            abb = a; reservas = r; pila = p; usuario = u;

            while (true)
            {
                Pantalla.Limpiar();
                Pantalla.Titulo("CATALOGO DE HABITACIONES");

                Pantalla.EncabezadoCatalogo();
                abb.MostrarCatalogoVisual();
                Console.WriteLine();
                Pantalla.LineaDivisora();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  Verde: Disponible  |  Amarillo: Reservada  |  Rojo: Ocupada");
                Console.ResetColor();
                Console.WriteLine();

                Console.Write("  Numero de habitacion (0 para volver): ");
                string entrada = Console.ReadLine();

                if (entrada == null || entrada.Trim() == "0") return;

                int numero;
                if (!int.TryParse(entrada.Trim(), out numero)) { Pantalla.Error("Ingrese un numero valido."); Pantalla.Pausa(); continue; }

                NodoABB nodo = abb.Buscar(numero);
                if (nodo == null) { Pantalla.Error("Esa habitacion no existe en el catalogo."); Pantalla.Pausa(); continue; }

                string estado = abb.ObtenerEstado(nodo.Dato);

                if (estado == "Disponible")
                {
                    MenuHabitacionDisponible(nodo);
                }
                else if (estado == "Ocupada")
                {
                    MenuHabitacionOcupada(nodo);
                }
                else
                {
                    MenuHabitacionReservada(nodo);
                }
            }
        }

        private static void MenuHabitacionDisponible(NodoABB nodo)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("HAB. " + nodo.Dato.Numero + " — " + nodo.Dato.Tipo.ToUpper());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Estado: DISPONIBLE ✓");
            Console.ResetColor();
            Console.WriteLine("  Precio: S/ " + nodo.Dato.Precio.ToString("0.00") + " / noche");
            Console.WriteLine();
            Console.WriteLine("  1. Registrar reserva (entrada futura)");
            Console.WriteLine("  2. Registrar entrada directa (huesped ya llego)");
            Console.WriteLine("  0. Volver");
            Console.WriteLine();

            int op = EntradaConsola.LeerOpcion("Opcion", 0, 2);
            if (op == 0) return;

            if (op == 1)
            {
                RegistrarReservaDesdeHabitacion(nodo);
            }
            else
            {
                RegistrarEntradaDirecta(nodo);
            }
        }

        private static void MenuHabitacionOcupada(NodoABB nodo)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("HAB. " + nodo.Dato.Numero + " — " + nodo.Dato.Tipo.ToUpper());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Estado: OCUPADA ✗");
            Console.ResetColor();
            Console.WriteLine();

            NodoReserva r = reservas.BuscarActivaPorHabitacion(nodo.Dato.Numero);
            if (r != null) MostrarFichaHuesped(r.Dato);

            Console.WriteLine();
            Console.WriteLine("  1. Realizar check-out");
            Console.WriteLine("  0. Volver");

            int op = EntradaConsola.LeerOpcion("Opcion", 0, 1);
            if (op == 1 && r != null) RealizarCheckOut(r);
        }

        private static void MenuHabitacionReservada(NodoABB nodo)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("HAB. " + nodo.Dato.Numero + " — " + nodo.Dato.Tipo.ToUpper());
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Estado: RESERVADA ◆");
            Console.ResetColor();
            Console.WriteLine();

            NodoReserva r = reservas.BuscarActivaPorHabitacion(nodo.Dato.Numero);
            if (r != null) MostrarFichaHuesped(r.Dato);

            Console.WriteLine();
            Console.WriteLine("  1. Realizar check-in (huesped llego)");
            Console.WriteLine("  2. Cancelar reserva");
            Console.WriteLine("  0. Volver");

            int op = EntradaConsola.LeerOpcion("Opcion", 0, 2);
            if (op == 1 && r != null) RealizarCheckIn(r);
            if (op == 2 && r != null) CancelarReserva(r, nodo);
        }

        private static void RegistrarReservaDesdeHabitacion(NodoABB nodo)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("REGISTRAR RESERVA — HAB. " + nodo.Dato.Numero);

            Reserva reserva = new Reserva();
            reserva.Codigo = LeerCodigoNuevoReserva();
            reserva.Cliente = EntradaConsola.LeerLetras("Nombre del huesped");
            reserva.Documento = EntradaConsola.LeerDni("DNI del huesped (8 digitos)");
            reserva.FechaHoraEntradaProgramada = EntradaConsola.LeerFechaHora("Fecha y hora de entrada (dd/mm/aaaa hh:mm)");
            reserva.FechaEntrada = reserva.FechaHoraEntradaProgramada.Date;
            reserva.Noches = EntradaConsola.LeerEntero("Cantidad de noches", 1, 30);
            reserva.FechaHoraSalidaProgramada = reserva.FechaHoraEntradaProgramada.AddDays(reserva.Noches);
            reserva.Habitacion = nodo.Dato.Numero;
            reserva.TipoHabitacion = nodo.Dato.Tipo;
            reserva.PrecioNoche = nodo.Dato.Precio;
            reserva.TotalPagar = reserva.PrecioNoche * reserva.Noches;
            reserva.Estado = "Reservada";
            reserva.UsuarioRegistro = usuario;

            nodo.Dato.Disponible = false;
            nodo.Dato.Estado = "Reservada";

            reservas.Insertar(reserva);
            pila.Apilar("Reserva registrada: " + reserva.Codigo);

            Console.WriteLine();
            MostrarComprobante(reserva, "RESERVA");
            Pantalla.Pausa();
        }

        private static void RegistrarEntradaDirecta(NodoABB nodo)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("REGISTRAR ENTRADA — HAB. " + nodo.Dato.Numero);

            Reserva reserva = new Reserva();
            reserva.Codigo = LeerCodigoNuevoReserva();
            reserva.Cliente = EntradaConsola.LeerLetras("Nombre del huesped");
            reserva.Documento = EntradaConsola.LeerDni("DNI del huesped (8 digitos)");
            reserva.FechaHoraEntradaProgramada = DateTime.Now;
            reserva.FechaHoraCheckIn = DateTime.Now;
            reserva.FechaEntrada = DateTime.Now.Date;
            reserva.Noches = EntradaConsola.LeerEntero("Cantidad de noches", 1, 30);
            reserva.FechaHoraSalidaProgramada = DateTime.Now.AddDays(reserva.Noches);
            reserva.Habitacion = nodo.Dato.Numero;
            reserva.TipoHabitacion = nodo.Dato.Tipo;
            reserva.PrecioNoche = nodo.Dato.Precio;
            reserva.TotalPagar = reserva.PrecioNoche * reserva.Noches;
            reserva.Estado = "Ocupada";
            reserva.UsuarioRegistro = usuario;
            reserva.UsuarioCheckIn = usuario;

            nodo.Dato.Disponible = false;
            nodo.Dato.Estado = "Ocupada";

            reservas.Insertar(reserva);
            pila.Apilar("Entrada directa registrada: " + reserva.Codigo);

            Console.WriteLine();
            MostrarComprobante(reserva, "ENTRADA");
            Console.WriteLine();
            Console.WriteLine("  Desea generar: 1. Boleta   2. Factura   0. Omitir");
            int doc = EntradaConsola.LeerOpcion("Opcion", 0, 2);
            if (doc > 0) MostrarDocumento(reserva, doc == 1 ? "BOLETA" : "FACTURA");
            Pantalla.Pausa();
        }

        private static void RealizarCheckIn(NodoReserva r)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("CHECK-IN — " + r.Dato.Codigo);

            r.Dato.FechaHoraCheckIn = EntradaConsola.LeerFechaHora("Fecha y hora real de entrada (dd/mm/aaaa hh:mm)");
            r.Dato.Estado = "Ocupada";
            r.Dato.UsuarioCheckIn = usuario;

            NodoABB hab = abb.Buscar(r.Dato.Habitacion);
            if (hab != null) { hab.Dato.Estado = "Ocupada"; hab.Dato.Disponible = false; }

            pila.Apilar("Check-in: " + r.Dato.Codigo);
            Pantalla.Ok("Check-in realizado. Habitacion marcada como ocupada.");
            Pantalla.Pausa();
        }

        private static void RealizarCheckOut(NodoReserva r)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("CHECK-OUT — " + r.Dato.Codigo);

            r.Dato.FechaHoraCheckOut = EntradaConsola.LeerFechaHora("Fecha y hora real de salida (dd/mm/aaaa hh:mm)");
            r.Dato.Estado = "Finalizada";
            r.Dato.UsuarioCheckOut = usuario;

            NodoABB hab = abb.Buscar(r.Dato.Habitacion);
            if (hab != null) { hab.Dato.Estado = "Disponible"; hab.Dato.Disponible = true; }

            pila.Apilar("Check-out: " + r.Dato.Codigo);
            Pantalla.Ok("Check-out realizado. Habitacion liberada.");

            Console.WriteLine();
            Console.WriteLine("  Desea generar: 1. Boleta   2. Factura   0. Omitir");
            int doc = EntradaConsola.LeerOpcion("Opcion", 0, 2);
            if (doc > 0) MostrarDocumento(r.Dato, doc == 1 ? "BOLETA" : "FACTURA");
            Pantalla.Pausa();
        }

        private static void CancelarReserva(NodoReserva r, NodoABB nodo)
        {
            Pantalla.Info("Cancelando reserva " + r.Dato.Codigo + "...");
            reservas.Eliminar(r.Dato.Codigo);
            nodo.Dato.Estado = "Disponible";
            nodo.Dato.Disponible = true;
            pila.Apilar("Reserva cancelada: " + r.Dato.Codigo);
            Pantalla.Ok("Reserva cancelada. Habitacion disponible.");
            Pantalla.Pausa();
        }

        private static void MostrarFichaHuesped(Reserva r)
        {
            Pantalla.LineaDivisora();
            Console.WriteLine("  Reserva:  " + r.Codigo);
            Console.WriteLine("  Huesped:  " + r.Cliente);
            Console.WriteLine("  DNI:      " + r.Documento);
            Console.WriteLine("  Entrada:  " + r.FechaHoraEntradaProgramada.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("  Salida:   " + r.FechaHoraSalidaProgramada.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("  Noches:   " + r.Noches);
            Console.WriteLine("  Total:    S/ " + r.TotalPagar.ToString("0.00"));
            Pantalla.LineaDivisora();
        }

        private static void MostrarComprobante(Reserva r, string tipo)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ╔══════════════════════════════════════╗");
            Console.WriteLine("  ║         " + tipo.PadRight(29) + "║");
            Console.WriteLine("  ╠══════════════════════════════════════╣");
            Console.ResetColor();
            Console.WriteLine("  ║  Codigo:    " + r.Codigo.PadRight(25) + "║");
            Console.WriteLine("  ║  Huesped:   " + r.Cliente.PadRight(25) + "║");
            Console.WriteLine("  ║  DNI:       " + r.Documento.PadRight(25) + "║");
            Console.WriteLine("  ║  Habitacion: " + (r.Habitacion + " (" + r.TipoHabitacion + ")").PadRight(24) + "║");
            Console.WriteLine("  ║  Noches:    " + r.Noches.ToString().PadRight(25) + "║");
            Console.WriteLine("  ║  Precio/n:  S/ " + r.PrecioNoche.ToString("0.00").PadRight(22) + "║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ║  TOTAL:     S/ " + r.TotalPagar.ToString("0.00").PadRight(22) + "║");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ╚══════════════════════════════════════╝");
            Console.ResetColor();
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

        private static string LeerCodigoNuevoReserva()
        {
            while (true)
            {
                string codigo = EntradaConsola.LeerCodigo("Codigo de reserva");
                if (reservas.Buscar(codigo) == null)
                {
                    return codigo;
                }
                Pantalla.Error("Ese codigo ya existe.");
            }
        }
    }
}
