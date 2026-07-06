using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    internal partial class Program
    {
        static void MenuReservas()
        {
            while (true)
            {
                Limpiar();
                Titulo("RESERVAS Y HOSPEDAJE - LISTA SIMPLE");
                Console.WriteLine("1. Registrar reserva usando una habitacion del ABB");
                Console.WriteLine("2. Mostrar reservas");
                Console.WriteLine("3. Buscar reserva");
                Console.WriteLine("4. Realizar check-in");
                Console.WriteLine("5. Realizar check-out");
                Console.WriteLine("6. Cancelar o eliminar reserva");
                Console.WriteLine("0. Volver");

                int opcion = LeerOpcion("Opcion", 0, 6);

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

        static void RegistrarReserva()
        {
            Limpiar();
            Titulo("REGISTRAR RESERVA");

            Reserva reserva = new Reserva();
            reserva.Codigo = LeerCodigoNuevoReserva();
            reserva.Cliente = LeerLetras("Nombre del cliente");
            reserva.Documento = LeerDni("DNI del cliente (8 digitos)");
            reserva.FechaHoraEntradaProgramada = LeerFechaHora("Fecha y hora de entrada (dd/mm/aaaa hh:mm)");
            reserva.FechaEntrada = reserva.FechaHoraEntradaProgramada.Date;
            reserva.Noches = LeerEntero("Cantidad de noches", 1, 30);
            reserva.FechaHoraSalidaProgramada = reserva.FechaHoraEntradaProgramada.AddDays(reserva.Noches);
            reserva.Estado = "Reservada";
            reserva.UsuarioRegistro = usuarioActual;

            Console.WriteLine();
            Console.WriteLine("Catalogo de habitaciones:");
            abb.MostrarCatalogoVisual();
            Console.WriteLine("Verde: disponible | Amarillo: reservada | Rojo: ocupada");
            Console.WriteLine();

            NodoABB nodoHabitacion = LeerHabitacionDisponible();
            reserva.Habitacion = nodoHabitacion.Dato.Numero;
            reserva.TipoHabitacion = nodoHabitacion.Dato.Tipo;
            reserva.PrecioNoche = nodoHabitacion.Dato.Precio;
            reserva.TotalPagar = reserva.PrecioNoche * reserva.Noches;
            nodoHabitacion.Dato.Disponible = false;
            nodoHabitacion.Dato.Estado = "Reservada";

            reservas.Insertar(reserva);
            pila.Apilar("Reserva registrada: " + reserva.Codigo);
            Mensaje("Reserva registrada. Total a pagar: S/ " + reserva.TotalPagar.ToString("0.00"));
            Pausa();
        }

        static NodoABB LeerHabitacionDisponible()
        {
            while (true)
            {
                int numero = LeerEntero("Numero de habitacion disponible", 1, 999);
                NodoABB nodo = abb.Buscar(numero);

                if (nodo != null && nodo.Dato.Estado == "Disponible")
                {
                    return nodo;
                }

                if (nodo == null)
                {
                    Error("Esa habitacion no existe en el catalogo ABB.");
                }
                else
                {
                    Error("Esa habitacion no esta disponible. Elija una habitacion en verde.");
                }
            }
        }

        static string LeerCodigoNuevoReserva()
        {
            while (true)
            {
                string codigo = LeerCodigo("Codigo");
                if (reservas.Buscar(codigo) == null)
                {
                    return codigo;
                }
                Error("Ese codigo ya existe.");
            }
        }

        static void MostrarReservas()
        {
            Limpiar();
            Titulo("LISTA DE RESERVAS");
            reservas.Mostrar();
            Pausa();
        }

        static void BuscarReserva()
        {
            Limpiar();
            Titulo("BUSCAR RESERVA");

            string codigo = LeerCodigo("Codigo");
            NodoReserva nodo = reservas.Buscar(codigo);

            if (nodo == null)
            {
                Error("Reserva no encontrada.");
            }
            else
            {
                MostrarFichaReserva(nodo.Dato);
            }
            Pausa();
        }

        static void RealizarCheckIn()
        {
            Limpiar();
            Titulo("CHECK-IN DE HUESPED");

            string codigo = LeerCodigo("Codigo de reserva");
            NodoReserva nodo = reservas.Buscar(codigo);

            if (nodo == null)
            {
                Error("Reserva no encontrada.");
                Pausa();
                return;
            }

            if (nodo.Dato.Estado != "Reservada")
            {
                Error("Solo se puede hacer check-in a una reserva en estado Reservada.");
                MostrarFichaReserva(nodo.Dato);
                Pausa();
                return;
            }

            nodo.Dato.FechaHoraCheckIn = LeerFechaHora("Fecha y hora real de entrada (dd/mm/aaaa hh:mm)");
            nodo.Dato.Estado = "Ocupada";
            nodo.Dato.UsuarioCheckIn = usuarioActual;
            ActualizarEstadoHabitacion(nodo.Dato.Habitacion);

            pila.Apilar("Check-in realizado: " + codigo);
            Mensaje("Check-in realizado. Habitacion marcada como ocupada.");
            Pausa();
        }

        static void RealizarCheckOut()
        {
            Limpiar();
            Titulo("CHECK-OUT DE HUESPED");

            string codigo = LeerCodigo("Codigo de reserva");
            NodoReserva nodo = reservas.Buscar(codigo);

            if (nodo == null)
            {
                Error("Reserva no encontrada.");
                Pausa();
                return;
            }

            if (nodo.Dato.Estado != "Ocupada")
            {
                Error("Solo se puede hacer check-out a una reserva en estado Ocupada.");
                MostrarFichaReserva(nodo.Dato);
                Pausa();
                return;
            }

            nodo.Dato.FechaHoraCheckOut = LeerFechaHora("Fecha y hora real de salida (dd/mm/aaaa hh:mm)");
            nodo.Dato.Estado = "Finalizada";
            nodo.Dato.UsuarioCheckOut = usuarioActual;
            ActualizarEstadoHabitacion(nodo.Dato.Habitacion);

            pila.Apilar("Check-out realizado: " + codigo);
            Mensaje("Check-out realizado. Habitacion liberada.");
            Pausa();
        }

        static void EliminarReserva()
        {
            Limpiar();
            Titulo("CANCELAR O ELIMINAR RESERVA");

            string codigo = LeerCodigo("Codigo");
            NodoReserva reserva = reservas.Buscar(codigo);

            if (reserva != null && reserva.Dato.Estado == "Ocupada")
            {
                Error("No se puede eliminar una reserva ocupada. Primero realice el check-out.");
                Pausa();
                return;
            }

            if (reservas.Eliminar(codigo))
            {
                ActualizarEstadoHabitacion(reserva.Dato.Habitacion);
                pila.Apilar("Reserva cancelada/eliminada: " + codigo);
                Mensaje("Reserva eliminada.");
            }
            else
            {
                Error("Reserva no encontrada.");
            }

            Pausa();
        }

        static void MostrarFichaReserva(Reserva reserva)
        {
            Console.WriteLine("Codigo: " + reserva.Codigo);
            Console.WriteLine("Cliente: " + reserva.Cliente);
            Console.WriteLine("Documento: " + reserva.Documento);
            Console.WriteLine("Habitacion: " + reserva.Habitacion + " (" + reserva.TipoHabitacion + ")");
            Console.WriteLine("Precio por noche: S/ " + reserva.PrecioNoche.ToString("0.00"));
            Console.WriteLine("Entrada programada: " + reserva.FechaHoraEntradaProgramada.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Salida programada: " + reserva.FechaHoraSalidaProgramada.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Noches: " + reserva.Noches);
            Console.WriteLine("Estado: " + reserva.Estado);
            Console.WriteLine("Total: S/ " + reserva.TotalPagar.ToString("0.00"));
            Console.WriteLine("Registrado por: " + reserva.UsuarioRegistro);

            if (reserva.FechaHoraCheckIn.Year > 1)
            {
                Console.WriteLine("Check-in real: " + reserva.FechaHoraCheckIn.ToString("dd/MM/yyyy HH:mm") +
                    " por " + reserva.UsuarioCheckIn);
            }

            if (reserva.FechaHoraCheckOut.Year > 1)
            {
                Console.WriteLine("Check-out real: " + reserva.FechaHoraCheckOut.ToString("dd/MM/yyyy HH:mm") +
                    " por " + reserva.UsuarioCheckOut);
            }
        }

        static void ActualizarEstadoHabitacion(int numero)
        {
            NodoABB habitacion = abb.Buscar(numero);
            if (habitacion == null)
            {
                return;
            }

            NodoReserva reservaActiva = reservas.BuscarActivaPorHabitacion(numero);
            if (reservaActiva == null)
            {
                habitacion.Dato.Estado = "Disponible";
                habitacion.Dato.Disponible = true;
            }
            else
            {
                habitacion.Dato.Estado = reservaActiva.Dato.Estado;
                habitacion.Dato.Disponible = false;
            }
        }

    }
}
